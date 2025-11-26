using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using System.Collections;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerScript : MonoBehaviour
{
    public static PlayerScript instance;

    private Rigidbody2D mover;
    private Vector2 movement;
    private float shoveDir;
    public float speed=1;
    public float height;
    private float fallingVelocity;

    private bool accelerating;
    private float ultraBoost;
    private float trickBoost;

    public float fuel=1;
    private float decay;
    [SerializeField] private Image fuelIndicator;
    [SerializeField] private ParticleSystem smoke;
    [SerializeField] private ParticleSystem dirt;
    [SerializeField] private ParticleSystem boostTrail;
    [SerializeField] private Animator playerVisual;
    [SerializeField] private Transform shadow;
    [SerializeField] private BikeType currentBike;
    public AudioSource engineSource;
    public AudioSource hitSoundEffect;

    //Engine pitch changing stuff
    [Header("Engine Sound")]
    private float idlePitch = 1.3f;
    private float accelPitch = 2.0f;
    private float noFuelPitch = 0.5f;
    private float pitchChangeSpeed = 5f;
    private float targetPitch;



    private Collider2D hopTo;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Application.targetFrameRate = 60;
        instance = this;
        mover = GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        fuel = Mathf.Clamp(fuel - Time.deltaTime/15, 0, 1);
        fuelIndicator.rectTransform.sizeDelta = new Vector2(120, fuel*120);

        playerVisual.transform.localPosition = new Vector3(0, height, 0);
        shadow.localPosition = new Vector3(0, -.5f, 0);
        shadow.localScale = Vector3.Lerp(new Vector3(1, .2f, 1), Vector3.zero,height/5);

        playerVisual.SetBool("Accelerating", accelerating);
        if(accelerating) 
        {
            ultraBoost += Time.deltaTime;
            if (ultraBoost > 5)
            {
                Time.timeScale = 1.5f + ultraBoost / 40;
                playerVisual.GetComponent<SpriteRenderer>().color = Color.red;
            }
            else
            {
                Time.timeScale = 1.3f;
            }
        }
        else
        {
            Time.timeScale = 1;
            ultraBoost = Mathf.Clamp(ultraBoost - Time.deltaTime, 0, 5);
        }
        Time.timeScale += trickBoost;
        trickBoost = Mathf.Clamp(trickBoost-Time.deltaTime,0,1);

        //slows the bike down if it touches grass
        if((transform.position.y >= 1.5f || transform.position.y <= -2.5f) && height == 0)
        {
            if (!dirt.isPlaying) { dirt.Play(); }
            fuel = Mathf.Clamp(fuel - Time.deltaTime / 13, 0, 1); //fuel goes down quicker
        }
        else
        {
            dirt.Stop();
        }
        if(height != 0)
        {
            boostTrail.Stop();
        }

            if(fuel<=0f)
            {
                targetPitch = noFuelPitch;
            }
            else
            {
                targetPitch = accelerating ? accelPitch : idlePitch;
            }
        engineSource.pitch = Mathf.Lerp(engineSource.pitch, targetPitch, Time.deltaTime * pitchChangeSpeed);
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        mover.linearVelocity = speed * new Vector2(movement.x*4,movement.y*3);
        playerVisual.SetFloat("SpeedY", mover.linearVelocityY);

        if(height!=0)fallingVelocity += -9.81f * Time.deltaTime;
        height = Mathf.Clamp(height+fallingVelocity*Time.deltaTime,0,15);
        playerVisual.SetFloat("Height", height);

        if (fuel == 0)
        {
            speed = .2f;
            decay += Time.deltaTime;
            mover.linearVelocity += Vector2.left * decay;
            if (!smoke.isPlaying) { smoke.Play(); }

            if (transform.position.x < -10) { Debug.Log("You LOSE!!!"); }
        }
        else 
        {
            transform.position = new Vector3(Mathf.Clamp(transform.position.x, -8, 8), Mathf.Clamp(transform.position.y,-5,5), transform.position.z);
            if (smoke.isPlaying) { smoke.Stop(); speed = 1; decay = 0; } 
        }
    }
    public void MoveInput(InputAction.CallbackContext context)
    {
        movement = context.ReadValue<Vector2>().normalized;
        if(movement.y != 0) { shoveDir = Mathf.Abs(movement.y) / movement.y; }
    }
    public void AccelerateInput(InputAction.CallbackContext context)
    {
        if (context.ReadValueAsButton())
        {
            accelerating = true;
            if(height == 0) { boostTrail.Play(); };
            
        }
        else
        {
            accelerating = false;
            boostTrail.Stop();
            playerVisual.GetComponent<SpriteRenderer>().color = Color.white;
            Time.timeScale = 1;
        }
    }
    public void AttackInput(InputAction.CallbackContext context)
    {
        if (context.performed) 
        {
            playerVisual.Play((shoveDir==1?"Player_Shove_Up":"Player_Shove_Down"));
            Collider2D hit = Physics2D.OverlapCircle(transform.position + Vector3.up * shoveDir, .5f, LayerMask.GetMask("Enemy"));
            if(hit != null)
            {
                hitSoundEffect.Play();
                hit.GetComponent<EnemyScript>().Shoved(mover.linearVelocityY);
            }
        }
    }
    public void TrickInput(InputAction.CallbackContext context)
    {
        if (context.ReadValueAsButton() && height == 0) 
        {
            hopTo = Physics2D.OverlapCircle(transform.position, 2, LayerMask.GetMask("Bike"));
            if(hopTo == null)
            {
                fallingVelocity = Mathf.Sqrt(1 * 9.81f * 2);
                StartCoroutine("WaitToLand");
            }
            else
            {
                StartCoroutine("HopToBike");
            }

        }
    }
    IEnumerator WaitToLand()
    {
        height += 0.01f;
        while (height != 0)
        {
            yield return null;
        }
        trickBoost += .2f;
    }
    IEnumerator HopToBike()
    {
        engineSource.Stop();
        BikeScript leftBike = Instantiate(currentBike.prefab, transform.position, new Quaternion()).GetComponent<BikeScript>();
        leftBike.fuel = fuel-3f/15f;

        playerVisual.SetTrigger("Hop");
        fallingVelocity = Mathf.Sqrt(1 * 9.81f * 2);
        float timer = 0;
        Vector3 og = transform.position;
        height += 0.01f;

        while (height > 0)
        {
            timer += Time.deltaTime;
            transform.position = Vector3.Lerp(og, hopTo.transform.position, timer);
            yield return null;
        }

        transform.position = hopTo.transform.position;
        fuel = hopTo.GetComponent<BikeScript>().fuel;
        currentBike = hopTo.GetComponent<BikeScript>().type;
        Destroy(hopTo.gameObject);
        trickBoost += .4f;
        engineSource.Play();
    }
}

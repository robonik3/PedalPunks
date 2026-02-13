using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using System.Collections;
using UnityEngine.SceneManagement;


[RequireComponent(typeof(Rigidbody2D))]
public class PlayerScript : MonoBehaviour
{
    public static PlayerScript instance;
    private Rigidbody2D mover;
    [SerializeField] private BoxCollider2D back;
    private Vector2 movement;
    private float shoveDir;
    public float cooldown;

    public float speed=1;
    private float speedForward=4; //This is left & Right movement
    private float speedTurning=3; //This is up & down movement

    public float height;
    private float fallingVelocity;
    private float gravity = -9.81f;

    private bool accelerating;
    public float ultraBoost;
    private float trickBoost;

    public float fuel=1;
    private float decay;
    [SerializeField] private Image fuelIndicator;
    [SerializeField] private ParticleSystem smoke;
    [SerializeField] private ParticleSystem dirt;
    [SerializeField] private ParticleSystem boostTrail;
    [SerializeField] private Animator playerVisual;
    [SerializeField] private Animator bikeVisual;
    [SerializeField] private Transform shadow;
    [SerializeField] private BikeType currentBike;
    [SerializeField] private CharacterSelect character;
    public AudioSource engineSource;
    public AudioSource hitSoundEffect;

    //Engine pitch changing stuff
    [Header("Engine Sound")]
    private float idlePitch = 1.3f;
    private float accelPitch = 2.0f;
    private float noFuelPitch = 0.5f;
    private float pitchChangeSpeed = 5f;
    private float targetPitch;

    //The limit or boundary y positions that player cant cross
    public float upperYLimit;
    public float lowerYLimit;

    //player sprite (sprite renderer so I can use it in this script)
    public GameObject visualRoot;
    public GameObject shadowSprite;
    public GameObject explosionPrefab;
    FadeScript screenFade;

    private Collider2D hopTo;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Application.targetFrameRate = 60;
        instance = this;
        mover = GetComponent<Rigidbody2D>();
        screenFade = FindFirstObjectByType<FadeScript>();

        PlayerData data = SaveSystem.LoadPlayer();

        if (data != null)
        {
            PlayerType c = character.characterList[data.selectedCharacter];
            currentBike = character.bikeList[data.selectedBike];

            if(c.visual!=null)playerVisual.runtimeAnimatorController = c.visual;
            speedTurning = c.turningSpeed;
            speedForward = c.forwardSpeed;
            gravity = c.gravity;

            bikeVisual.runtimeAnimatorController = character.bikeList[data.selectedBike].visual;
        }
    }
    private void Update()
    {
        cooldown = Mathf.Clamp(cooldown -= Time.deltaTime, 0, .5f);
        fuel = Mathf.Clamp(fuel - Time.deltaTime/15, 0, 1);
        fuelIndicator.rectTransform.sizeDelta = new Vector2(120, fuel*120);
    
        playerVisual.transform.localPosition = new Vector3(0, height, 0);
        bikeVisual.transform.localPosition = new Vector3(0, height, 0);
        shadow.localPosition = new Vector3(0, -.5f, 0);
        shadow.localScale = Vector3.Lerp(new Vector3(1, .2f, 1), Vector3.zero,height/5);

        playerVisual.SetBool("Accelerating", accelerating);
        bikeVisual.SetBool("Accelerating", accelerating);
        if(accelerating) 
        {
            ultraBoost += Time.deltaTime;
            if (ultraBoost > 5)
            {
                Time.timeScale = 1.5f + ultraBoost / 40;
                playerVisual.GetComponent<SpriteRenderer>().color = Color.red;
                bikeVisual.GetComponent<SpriteRenderer>().color = Color.red;
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
        mover.linearVelocity = speed * new Vector2(movement.x*speedForward,movement.y*speedTurning);
        playerVisual.SetFloat("SpeedY", mover.linearVelocityY);
        bikeVisual.SetFloat("SpeedY", mover.linearVelocityY);

        if(height!=0)fallingVelocity += gravity * Time.deltaTime;
        height = Mathf.Clamp(height+fallingVelocity*Time.deltaTime,0,15);
        playerVisual.SetFloat("Height", height);
        bikeVisual.SetFloat("Height", height);

        if (fuel == 0)
        {
            speed = .2f;
            decay += Time.deltaTime;
            mover.linearVelocity += Vector2.left * decay;
            if (!smoke.isPlaying) { smoke.Play(); }

            if (transform.position.x < -15) { 
                //Debug.Log("You LOSE!!!"); 
                Time.timeScale = 0f;
                SceneManager.LoadScene("GameOver");
                }
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
            bikeVisual.GetComponent<SpriteRenderer>().color = Color.white;
            Time.timeScale = 1;
        }
    }
    public void AttackInput(InputAction.CallbackContext context)
    {
        if (context.performed && cooldown==0) 
        {            

            Debug.Log("Attack");
            playerVisual.Play((shoveDir==1?"Player_Shove_Up":"Player_Shove_Down"));
            Collider2D hit = Physics2D.OverlapCircle(transform.position + Vector3.up * shoveDir, .5f, LayerMask.GetMask("Enemy"));
            if(hit != null)
            {   
                cooldown = .5f;
                hitSoundEffect.Play();
                hit.GetComponent<EnemyScript>().Shoved();
                if(TryGetComponent(out ScooterEnemyScript s))
                {
                    cooldown = .1f;
                }
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
    public void Die()
    {
        StartCoroutine(DeathSequence());
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
        bikeVisual.SetTrigger("Hop");
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
        bikeVisual.runtimeAnimatorController = currentBike.visual;
        Destroy(hopTo.gameObject);
        trickBoost += .4f;
        engineSource.Play();
    }
    IEnumerator DeathSequence()
    {

        visualRoot.SetActive(false);
        shadowSprite.SetActive(false);
        Instantiate(explosionPrefab, transform.position, Quaternion.identity);
        //wait for explosion
        yield return new WaitForSeconds(0.4f);
        //Fade to black
        if(screenFade != null) yield return StartCoroutine(screenFade.FadeIn());
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}

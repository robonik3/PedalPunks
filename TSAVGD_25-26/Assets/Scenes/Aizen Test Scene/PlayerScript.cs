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
    public float speed=1;
    private float height;
    private float fallingVelocity;

    private bool accelerating;
    private float ultraBoost;
    private float trickBoost;

    public float fuel=1;
    private float decay;
    [SerializeField] private Image fuelIndicator;
    [SerializeField] private ParticleSystem smoke;
    [SerializeField] private SpriteRenderer playerVisual;
    [SerializeField] private Transform shadow;


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

        if(accelerating) 
        {
            playerVisual.transform.localPosition += new Vector3(-.33f, .1f, 0);
            shadow.localPosition += new Vector3(-.25f,0, 0);

            ultraBoost += Time.deltaTime;
            if (ultraBoost > 5)
            {
                Time.timeScale = 1.5f + ultraBoost / 40;
                playerVisual.color = Color.red;
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
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        mover.linearVelocity = speed * new Vector2(movement.x*4,movement.y*3);

        if(height!=0)fallingVelocity += -9.81f * Time.deltaTime;
        height = Mathf.Clamp(height+fallingVelocity*Time.deltaTime,0,15);
        

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
    }
    public void AccelerateInput(InputAction.CallbackContext context)
    {
        if (context.ReadValueAsButton())
        {
            accelerating = true;
            playerVisual.transform.eulerAngles = new Vector3(0, 0, 45);
        }
        else
        {
            accelerating = false;
            playerVisual.color = Color.white;
            playerVisual.transform.eulerAngles = new Vector3(0, 0, 0);
            Time.timeScale = 1;
        }
    }
    public void AttackInput(InputAction.CallbackContext context)
    {
        if(context.performed)fuel = 1;
    }
    public void TrickInput(InputAction.CallbackContext context)
    {
        if (context.ReadValueAsButton() && height == 0) 
        {
            Collider2D hopTo = Physics2D.OverlapCircle(transform.position, 2, LayerMask.GetMask("Bike"));
            if(hopTo == null)
            {
                fallingVelocity = Mathf.Sqrt(1 * 9.81f * 2); 
                StartCoroutine("WaitToLand");
            }
            else
            {
                //
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
}

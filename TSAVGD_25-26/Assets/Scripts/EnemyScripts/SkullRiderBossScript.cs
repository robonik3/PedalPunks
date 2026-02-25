using System.Collections;
using UnityEngine.UI;
using UnityEngine;

public class SkullRiderBossScript : EnemyScript
{
    public float HP=10;
    [SerializeField] GameObject fire;
    private float timer;
    private bool playsound;
    private float fireTime;
    private bool upOrDown;
    private Animator animator;
    private ProgressBar progressBar;
    [SerializeField] private Sprite progressMarker;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        mover = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();


        progressBar = FindFirstObjectByType<ProgressBar>();
        progressBar.bar.transform.GetChild(0).GetComponent<Image>().color = Color.orangeRed;
        progressBar.bar.value = 1;


        state = 6;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
  
        switch (state)
        {
            
            case 0:
                Drive();
                AnimateTurning();
                break;

            case 1:
                BackTarget();
                AnimateTurning();
                break;

            case 2:
                FrontTarget();
                AnimateTurning();
                break;

            case 3:
                FireLane();
                break;

            case 4:
                Swoop();
                AnimateTurning();
                break;
            case 5:
                Avoidance();
                AnimateTurning();
                break;

            case 6:
                BackwardsEntrance();
                break;

        }

    }
    void Drive()
    {
        

        mover.linearVelocityX = 0;
        mover.linearVelocityY = 0;
        if (transform.position.y < -2)
        {
            mover.linearVelocityY = 2;
        } 
        if (transform.position.y > 1.5f)
        {
            mover.linearVelocityY = -2;
        }
        if (Physics2D.BoxCast(transform.position, new Vector2(1,1.3f), 0, Vector2.right, 6, LayerMask.GetMask("Pothole"))) 
        {
            mover.linearVelocityY = -3;
        }
        timer += Time.deltaTime;
        if (timer > 1)
        {
            animator.Play("Wheelie");

            playsound = true;

            timer = 0;
            if (transform.position.x > -3)
            {

                state = 1;
            }
            else
            {
                upOrDown = Random.Range(0, 2) == 1;
                state = 2;
            }
        }
    }
    void BackTarget()
    {
        if (transform.position.x > -6)
        {
            mover.linearVelocityX = -6;
        }
        else
        {
            GetComponent<SpriteRenderer>().color = Color.red;
            mover.linearVelocityX = 0;
            timer += Time.deltaTime;
            if (playsound) { playsound = false; AudioPlayer.instance.Play("EngineRev", Random.Range(.65f, .9f)); }

        }
        if (timer > 1)
        {
            mover.linearVelocityX = 6;
            state = 3;
            timer = 0;
            playsound = true;

        }
    }
    void FrontTarget()
    {
        if (transform.position.x < 8)
        {
            mover.linearVelocityX = 6;
        }
        else
        {
            GetComponent<SpriteRenderer>().color = Color.red;

            mover.linearVelocityX = 0;

        }
        if (upOrDown)
        {
            if (transform.position.y < 1)
            {
                mover.linearVelocityY = 2;
            }
            else
            {
            GetComponent<SpriteRenderer>().color = Color.red;
                mover.linearVelocityY = 0;
                timer += Time.deltaTime;
                if (playsound) { playsound = false; AudioPlayer.instance.Play("EngineRev",Random.Range(.65f,.9f)); }
            }
        }
        else
        {
            if (transform.position.y > -2)
            {
                mover.linearVelocityY = -2;
            }
            else
            {
            GetComponent<SpriteRenderer>().color = Color.red;
                mover.linearVelocityY = 0;
                timer += Time.deltaTime;
                if (playsound) { playsound = false; AudioPlayer.instance.Play("EngineRev", Random.Range(.65f, .9f)); }

            }
        }
        if (timer > 1)
        {
            if (upOrDown)
            {
                mover.linearVelocityY = -2;
            }
            else
            {
                mover.linearVelocityY = 2;
            }
            state = 3;
            playsound = true;

        }


    }

    void FireLane()
    {
        fireTime = Mathf.Clamp01(fireTime -= Time.deltaTime);
        if(fireTime == 0)
        {
            Instantiate(fire,transform.position, new Quaternion());

            fireTime += .1f;
        }
        if (transform.position.y < -3)
        {
            mover.linearVelocityY = 2;
        }
        if (transform.position.y > 2f)
        {
            mover.linearVelocityY = -2;
        }
        timer += Time.deltaTime;
        if (timer > 1.5f)
        {
            GetComponent<SpriteRenderer>().color = Color.white;
            animator.Play("Drive");

            playsound = true;
            timer = 0;
            state = 4;
            playsound = true;

            return;
        }
    }
    

    void Avoidance()
    {
        mover.linearVelocityX = 3f;

        if (Mathf.Abs(PlayerScript.instance.transform.position.y - transform.position.y) < 1)
        {
            mover.linearVelocityX = 2f;

            if (PlayerScript.instance.transform.position.y > transform.position.y)
            {
                mover.linearVelocityY = -3;
            }
            else
            {
                mover.linearVelocityY = 3;
            }
        }
        else
        {
            mover.linearVelocityY = 0;
        }


        timer += Time.deltaTime;

        if (timer > 1)
        {
            timer = 0;
            state = 0;
            return;
        }
    }

    void Swoop()
    {
        mover.linearVelocityX = timer - 1 * 4;
        mover.linearVelocityY = 0;

        timer += Time.deltaTime;

        if (timer > 1)
        {
            timer = 0;
            state = 5;
            playsound = true;
            return;
        }
    }
    public override void Explode()
    {
        AudioPlayer.instance.Play("CrunchPunch");

        Shoved();
    }
    public override void Shoved()
    {
        
        //imagine player gets flung backward 

        HP--;
        progressBar.bar.value = HP/10;

        GetComponent<ParticleSystem>().Play();

        if (!(state == 1 || state == 2 || state ==3)) {playsound=true; state = Random.Range(1, 3); }
        timer = 0;

        GetComponent<SpriteRenderer>().color = Color.white;

        if (HP < 1)
        {
            progressBar.bar.transform.GetChild(0).GetComponent<Image>().color = new Color(186/255f,1,190/255f,1);
            progressBar.bar.transform.GetChild(1).GetComponent<Image>().sprite = progressMarker;
            progressBar.bar.transform.GetChild(1).GetComponent<Image>().color = Color.black;
            progressBar.levelLengthTime = progressBar.progressTimer + 15;
            progressBar.CanComplete = true;

            AudioPlayer.instance.Play("CrunchPunch");
            FindFirstObjectByType<ProgressBar>().CanComplete = true;
            base.Shoved();
        }
    }
    void BackwardsEntrance()
    {

        mover.linearVelocityX = -4;
        mover.linearVelocityY = 0;
        if (Physics2D.BoxCast(transform.position, new Vector2(1, 1.3f), 0, Vector2.right, 6, LayerMask.GetMask("Pothole")))
        {
            mover.linearVelocityY = 3;
        }

        timer += Time.deltaTime;
        if (timer > 1)
        {
            animator.Play("Wheelie");

            timer = 0;
            state = 0;
        }
    }
    void AnimateTurning()
    {
        animator.SetFloat("SpeedY", mover.linearVelocityY);
    }
}

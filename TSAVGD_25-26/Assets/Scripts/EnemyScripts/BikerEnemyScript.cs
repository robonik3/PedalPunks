using System.Collections;
using UnityEngine;

public class BikerEnemyScript : EnemyScript
{
    private float timer;
    private float random;
    private float randomSpeed;
    private bool playsound;
    private bool goBackwards;
    private bool stunned = false;
    private Animator animator;
    public bool shield;
    private float shieldExplodePreventer;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        mover = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        random = Random.Range(1.75f, 4f);
        randomSpeed = Random.Range(1.75f, 3);
        if (EnemySpawner.instance == null) { randomSpeed = 1.75f; }
        slide = false;
        if (transform.position.x > 9)
        {
            state = 6;
        }
        if (transform.parent)
        {
            state = 7;
        }
        if (shield)
        {
            random -= .25f;
            randomSpeed += .25f;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //Debug.Log(stunned);
        if (slide)
        {

                animator.Play("Drive");
                GetComponent<SpriteRenderer>().color = Color.white;
                state = 0;
                stunned = true;
                //Debug.Log("Stunned, driving");

        } else
            {
                //Debug.Log("not stunned");
                stunned = false;
            }
        switch (state)
        {
            
            case 0:
                Drive();
                AnimateTurning();
                break;
            case 1:
                Target();
                break;
            case 2:
                Wheelie();
                break;
            case 3:
                Avoidance();
                AnimateTurning();
                break;
            case 6:
                BackwardsEntrance();
                break;
            case 8:
                JumpOffTruck();
                break;

        }
        if (shield) { shieldExplodePreventer = Mathf.Clamp01(shieldExplodePreventer - Time.deltaTime); }
    }
    void Drive()
    {
        if (PlayerScript.instance.transform.position.x < transform.position.x)
        {
            timer += Time.deltaTime;
            mover.linearVelocityX = 1;
        }
        else
        {
            mover.linearVelocityX = randomSpeed;
            if (transform.position.x > 9) { mover.linearVelocityX = -4; }
        }

        mover.linearVelocityY = 0;
        if (Physics2D.BoxCast(transform.position, new Vector2(1,1.3f), 0, Vector2.right, 6, LayerMask.GetMask("Pothole"))) 
        {
            mover.linearVelocityY = -3;
        }

        if(transform.position.x>-9)timer += Time.deltaTime;
        if ((timer > random) && !stunned)
        {
            animator.Play("Wheelie");
            playsound = true;
            timer = 0;
            state = 1;
        }
    }
    void Target()
    {

        if (stunned)
        {
            return;
        }
        mover.linearVelocityX = -.75f;
        if (PlayerScript.instance.transform.position.x+2 < transform.position.x)
        {
            mover.linearVelocityX = -3;
        }

        if (Mathf.Abs(PlayerScript.instance.transform.position.y - transform.position.y) < 1.3f)
        {
            mover.linearVelocityY = Mathf.Sign(PlayerScript.instance.transform.position.y - transform.position.y) * -2;
        }
        else
        {
            mover.linearVelocityY = 0;
        }
        timer += Time.deltaTime;
        if (playsound && timer > .4f) { AudioPlayer.instance.Play("BikerWheelie4", Random.Range(.8f, 1.2f), transform.position, .5f); playsound = false; }

        if (timer > .75f)
        {
            playsound = true;
            goBackwards = false;
            timer = 0;
            if (randomSpeed > 2.5f) { timer -= .2f; }
            state = 2;
            return;
        }
    }
    void Wheelie()
    {
        if (stunned)
        {
            return;
        }
        GetComponent<SpriteRenderer>().color = Color.red;
        timer += Time.deltaTime;
        mover.linearVelocityX = 4+(randomSpeed/3);
        
        //checks if enemy is in front of player to start bakcwards movement
        if(!goBackwards&&transform.position.x-1*randomSpeed > PlayerScript.instance.transform.position.x)
        {
            goBackwards = true;
            timer -= .1f;
        }
        if (goBackwards)
        {
            if (Mathf.Abs(PlayerScript.instance.transform.position.y - transform.position.y) < 1)
            {
                mover.linearVelocityY = Mathf.Sign(PlayerScript.instance.transform.position.y - transform.position.y) * 1f;

                mover.linearVelocityX = -.75f;

            }
            else 
            {
                mover.linearVelocityY = Mathf.Sign(PlayerScript.instance.transform.position.y - transform.position.y) * 2;

                mover.linearVelocityX = 1;
            }
        }
        else { mover.linearVelocityY = 0; }

        if (Physics2D.OverlapCircle(transform.position, .2f, LayerMask.GetMask("Player")) && PlayerScript.instance.height == 0)
        {
            PlayerScript.instance.Die();
            state = 0;
            return;
        }

        if (playsound) { playsound= false; 

        }

        if (timer > 1.5f)
        {
            GetComponent<SpriteRenderer>().color = Color.white;
            animator.Play("Drive");

            playsound = true;
            timer = 0;
            state = 3;
            return;
        }
    }

    void Avoidance()
    {
        if (stunned)
        {
            return;
        }
        mover.linearVelocityX = -3f;

        if (Mathf.Abs(PlayerScript.instance.transform.position.y - transform.position.y) < 1)
        {
            mover.linearVelocityX = -2f;

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
    void BackwardsEntrance()
    {

        mover.linearVelocityX = -randomSpeed-randomSpeed/3;
        mover.linearVelocityY = 0;
        if (Physics2D.BoxCast(transform.position, new Vector2(1, 1.3f), 0, Vector2.right, 6, LayerMask.GetMask("Pothole")))
        {
            mover.linearVelocityY = 3;
        }

        timer += Time.deltaTime;
        if ((timer > random) && !stunned)
        {
            animator.Play("Wheelie");

            timer = 0;
            state = 1;
        }
    }
    void JumpOffTruck()
    {
        fallingVelocity += -9.81f * Time.deltaTime;
        height += fallingVelocity * Time.deltaTime;
        transform.position += new Vector3(0,fallingVelocity * Time.deltaTime);
        if(height < 0)
        {
            height = 0;
            timer= 0;
            playsound = true;
            state = 1;
        }
    }
    void AnimateTurning()
    {
        animator.SetFloat("SpeedY", mover.linearVelocityY);
    }
    public override void Shoved()
    {
        if (shield)
        {
            PlayerScript.instance.slide = Vector2.up * (PlayerScript.instance.transform.position.y > transform.position.y ? 3 : -3);
            PlayerScript.instance.state = 1;
            shield = false;
            AudioPlayer.instance.Play("CrunchPunch", Random.Range(1,1.3f));
            transform.GetChild(2).gameObject.SetActive(false);
        }
        else
        {
            base.Shoved();

        }
    }
    public override void DepartTruck()
    {
        mover.simulated = true;

        animator.Play("Wheelie");

        mover.linearVelocityX = 2*(transform.position.x-(EnemySpawner.instance.truck.transform.position.x-.5f));
        mover.linearVelocityY = Mathf.Sign(-transform.position.y);

        fallingVelocity = Mathf.Sqrt(9.81f * 2);
        height = .1f;

        state = 8;
        transform.parent = null;
    }
    public override void Stun()
    {
        if(!shield)base.Stun();
    }
    public override void Explode()
    {
        if (shield)
        {
            shield = false;
            AudioPlayer.instance.Play("CrunchPunch", Random.Range(1, 1.3f));
            transform.GetChild(2).gameObject.SetActive(false);
            shieldExplodePreventer += .3f;
        }
        else
        {
            base.Explode();

        }
    }
}

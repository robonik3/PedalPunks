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

        }

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

        if (Physics2D.OverlapCircle(transform.position, .2f, LayerMask.GetMask("Player")))
        {
            PlayerScript.instance.Die();
            state = 0;
            return;
        }

        if (playsound) { playsound= false;// AudioPlayer.instance.Play("JetCharge");
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
    void AnimateTurning()
    {
        animator.SetFloat("SpeedY", mover.linearVelocityY);
    }
}

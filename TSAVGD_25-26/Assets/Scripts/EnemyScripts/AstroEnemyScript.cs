using UnityEngine;

public class AstroEnemyScript : EnemyScript
{
    private float timer;
    private float antitimer;
    private bool stunned = false;
    private bool playsound;
    private Animator animator;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        mover = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {if (slide)
        {

                //GetComponent<Animator>().Play("Drive");
                GetComponent<SpriteRenderer>().color = Color.white;
                state = 0;
                stunned = true;
                //Debug.Log("Stunned, driving");

        } else
            {
               // Debug.Log("not stunned");
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
                AnimateTurning();

                break;
            case 2:
                PrepareJet();
                break;
            case 3:
                BlastOff();
                break;
            case 4:
                Avoidance();
                AnimateTurning();

                break;
        }

    }
    void Drive()
    {
        if (PlayerScript.instance.transform.position.x-3 < transform.position.x)
        {
            mover.linearVelocityX = -2f;

            antitimer += Time.deltaTime;
            if (antitimer > 1)
            {
                antitimer = 0;
                state = 4;
            }
        }
        else
        {
            mover.linearVelocityX = 4;
            if (transform.position.x > 9) { mover.linearVelocityX = -4; }
        }

        mover.linearVelocityY = 0;
        if (Physics2D.BoxCast(transform.position, new Vector2(1, 1.3f), 0, Vector2.right, 6, LayerMask.GetMask("Pothole"))) 
        {
            mover.linearVelocityY = -2;
        }

        if(transform.position.x>-9)timer += Time.deltaTime;
        if ((timer > 1.5f) && !stunned)
        {
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
        if (PlayerScript.instance.transform.position.x > transform.position.x)
        {
            if(Mathf.Abs(PlayerScript.instance.transform.position.y-transform.position.y) > .7f)
            {
                mover.linearVelocityY = Mathf.Sign(PlayerScript.instance.transform.position.y - transform.position.y) * 2;
            }
            else { mover.linearVelocityY = 0; }

            mover.linearVelocityX = -1.25f;
            if(transform.position.x < -5f)
            {
                mover.linearVelocityX = -.75f;
                timer += Time.deltaTime;
                if (timer > .75f)
                {
                    playsound = true;
                    GetComponent<SpriteRenderer>().color = Color.red;
                    timer = 0;
                    animator.Play("Drive");
                    state = 2;
                    return;
                }
            }
        }
        else
        {
            antitimer += Time.deltaTime;
            if (antitimer > .75f)
            {
                antitimer = 0;
                state = 0;
                return;
            }
        }
    }
    void PrepareJet()
    {
         if (stunned)
        {
            return;
        }       
        timer += Time.deltaTime;
        mover.linearVelocityX = -.25f;
        mover.linearVelocityY = 0;
        if (playsound) { playsound= false; AudioPlayer.instance.Play("JetCharge"); }

        if (timer > 1.5f)
        {
            playsound = true;
            timer = 0;
            state = 3;
            animator.Play("Extra");

            return;
        }
    }
    void BlastOff()
    {
                if (stunned)
        {
            return;
        }
        mover.linearVelocityX = 8;
        if (Physics2D.OverlapCircle(transform.position, .35f, LayerMask.GetMask("Player")))
        {
            PlayerScript.instance.Die();
            state = 0;
            return;
        }

        if (playsound) { playsound = false; AudioPlayer.instance.Play("BlastOff"); }

        if (transform.position.x > 8)
        {
            playsound = true;
            GetComponent<SpriteRenderer>().color = Color.white;
            state = 4;
            animator.Play("Drive");

            return;
        }
    }
    void Avoidance()
    {
                if (stunned)
        {
            return;
        }
        if (PlayerScript.instance.transform.position.y > transform.position.y) 
        { 
            mover.linearVelocityY = -2; 
        }
        else
        {
            mover.linearVelocityY = 2;
        }
        mover.linearVelocityX = -3f;

        timer += Time.deltaTime;

        if (timer > 1)
        {
            timer = 0;
            state = 0;
            return;
        }
    }
    void AnimateTurning()
    {
        animator.SetFloat("SpeedY", mover.linearVelocityY);
    }
}

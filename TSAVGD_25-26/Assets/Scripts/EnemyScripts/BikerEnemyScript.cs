using UnityEngine;

public class BikerEnemyScript : EnemyScript
{

    private int state;
    private float timer;
    private float random;
    private bool playsound;
    private bool goBackwards;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        mover = GetComponent<Rigidbody2D>();
        random = Random.Range(1.5f, 3.5f);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        switch (state)
        {
            case 0:
                Drive();
                break;
            case 1:
                Target();
                break;
            case 2:
                Wheelie();
                break;
            case 3:
                Avoidance();
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
            mover.linearVelocityX = 3;
            if (transform.position.x > 9) { mover.linearVelocityX = -4; }
        }

        mover.linearVelocityY = 0;
        if (Physics2D.BoxCast(transform.position, new Vector2(1,1.3f), 0, Vector2.right, 6, LayerMask.GetMask("Pothole"))) 
        {
            mover.linearVelocityY = -3;
        }

        if(transform.position.x>-9)timer += Time.deltaTime;
        if (timer > random)
        {
            GetComponent<Animator>().Play("Wheelie");

            timer = 0;
            state = 1;
        }
    }
    void Target()
    {
        mover.linearVelocityX = -.75f;
        if (PlayerScript.instance.transform.position.x+2 < transform.position.x)
        {
            mover.linearVelocityX = -3;
        }

        if (Mathf.Abs(PlayerScript.instance.transform.position.y - transform.position.y) < 1)
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
            state = 2;
            return;
        }
    }
    void Wheelie()
    {
        GetComponent<SpriteRenderer>().color = Color.red;
        timer += Time.deltaTime;
        mover.linearVelocityX = 5;
        if(transform.position.x-1 > PlayerScript.instance.transform.position.x)
        {
            goBackwards = true;

        }
        if (goBackwards)
        {
            if (Mathf.Abs(PlayerScript.instance.transform.position.y - transform.position.y) < 1)
                {
                    mover.linearVelocityY = Mathf.Sign(PlayerScript.instance.transform.position.y - transform.position.y) * 1f;

                    mover.linearVelocityX = -.75f;

                }
            else {
                mover.linearVelocityY = Mathf.Sign(PlayerScript.instance.transform.position.y - transform.position.y) * 2;

                mover.linearVelocityX = 1;
            }
        }
        else { mover.linearVelocityY = 0; }

        if (Physics2D.OverlapCircle(transform.position + Vector3.left * .35f, .15f, LayerMask.GetMask("Player")))
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
            GetComponent<Animator>().Play("Drive");

            playsound = true;
            timer = 0;
            state = 3;
            return;
        }
    }

    void Avoidance()
    {
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
}

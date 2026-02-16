using UnityEngine;

public class ScooterEnemyScript : EnemyScript
{
    private float timer;
    private float random;
    private int stun;

    private bool stunned = false;
    private bool slide1;
    private bool playsound;
    private bool upOrDown;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        mover = GetComponent<Rigidbody2D>();
        random = Random.Range(1.5f, 3.5f);
    }

    // Update is called once per frame
    void FixedUpdate()
    {if (slide)
        {

                GetComponent<Animator>().Play("Drive");
                GetComponent<SpriteRenderer>().color = Color.white;
                state = 0;
                stunned = true;
                Debug.Log("Stunned, driving");

        } else
            {
                Debug.Log("not stunned");
                stunned = false;
            }
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
            case 4:
                Stunned();
                break;
            case 5:
                FinalStunned();
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
        if (Physics2D.BoxCast(transform.position,Vector2.one,0, Vector2.right, 6, LayerMask.GetMask("Pothole"))) 
        {
            mover.linearVelocityY = 4;
            timer -= Time.deltaTime;
        }

        if(transform.position.x>-9)timer += Time.deltaTime;
        if ((timer > random) && !stunned)
        {

            timer = 0;
            upOrDown = Random.Range(0, 2) == 0;
            state = 1;
        }
    }
    void Target()
    {
        if (stunned)
        {
            return;
        }
        if (Mathf.Abs(PlayerScript.instance.transform.position.x - transform.position.x) > 1)
        {
            mover.linearVelocityY = Mathf.Sign(PlayerScript.instance.transform.position.y - transform.position.y) * 3;
        }
        else
        {
            mover.linearVelocityX = 1;
        }
        if (upOrDown)
        {
            if (transform.position.y < 2)
            {
                mover.linearVelocityY = 4;
            }
            else
            {
                mover.linearVelocityY = 0;
                timer += Time.deltaTime;
            }
        }
        else
        {
            if (transform.position.y > -2)
            {
                mover.linearVelocityY = -4;
            }
            else
            {
                mover.linearVelocityY = 0;
                timer += Time.deltaTime;
            }
        }
        if (Physics2D.BoxCast(transform.position, Vector2.one, 0, Vector2.right, 6, LayerMask.GetMask("Pothole")))
        {
            mover.linearVelocityY = 4;

        }
        if (Physics2D.OverlapCircle(transform.position, .2f, LayerMask.GetMask("Player")))
        {
            timer-=Time.deltaTime;
        }
        if (timer > .75f)
        {
            playsound = true;
            upOrDown = (PlayerScript.instance.transform.position.y > transform.position.y);
            timer = 0;
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
        mover.linearVelocityX = .25f;
        if (upOrDown)
        {
            mover.linearVelocityY = 3;
            if (Physics2D.BoxCast(transform.position, Vector2.one, 0, Vector2.up, 6, LayerMask.GetMask("Pothole")))
            {
                mover.linearVelocityY = 0;
            }
        }
        else 
        { 
            mover.linearVelocityY = -3;
            if (Physics2D.BoxCast(transform.position, Vector2.one, 0, Vector2.down, 6, LayerMask.GetMask("Pothole")))
            {
                mover.linearVelocityY = 0;
            }
        }

        if (Physics2D.OverlapCircle(transform.position, .2f, LayerMask.GetMask("Player")))
        {
            PlayerScript.instance.Die();
            state = 0;
            return;
        }

        if (playsound) { playsound= false;// AudioPlayer.instance.Play("JetCharge");
        }

        if (timer > 1f)
        {
            GetComponent<SpriteRenderer>().color = Color.white;

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
    void Stunned()
    {
        mover.linearVelocityX = .25f;
        mover.linearVelocityY = 0;
        timer += Time.deltaTime;
        if (timer > 1.1f&&state!=5)
        {
            GetComponent<Animator>().Play("Drive");

            state = 0;
            stun = 0;
        }
    }
    void FinalStunned()
    {
        mover.AddForce(Vector2.left);

        mover.linearVelocityY = timer*5*(slide1?-1:1);
        timer = Mathf.Clamp01(timer - Time.deltaTime*1.8f);

    }
    public override void Shoved()
    {
        PlayerScript.instance.cooldown = 0;
        if (stun==5)
        {
            if (timer < .5f)
            {
                Instantiate(bike.prefab, transform.position, new Quaternion());
                bool above = PlayerScript.instance.transform.position.y > transform.position.y;
                GameObject r = Instantiate(RagdollPrefab, transform.position, new Quaternion());
                r.GetComponent<SpriteRenderer>().sprite = bike.enemyRagdoll[(above ? 0 : 1)];
                r.GetComponent<Rigidbody2D>().linearVelocity = mover.linearVelocity / 6 + new Vector2(0, (above ? -1 : 1) * 4);
                AudioPlayer.instance.StopSound("DazedWhistle");
                Dead();
            }


        }
        else
        {
            stun++;
            if (stun == 4)
            {
                AudioPlayer.instance.Play("CrunchPunch");
                AudioPlayer.instance.Play("DazedWhistle");
                
                slide1 = (PlayerScript.instance.transform.position.y > transform.position.y);
                timer = 1;
                state = 5;
                transform.GetChild(1).gameObject.SetActive(true);
                stun++;
            }
            else
            {
                AudioPlayer.instance.Play("Hit1");
                AudioPlayer.instance.Play("Duun", 1.3f + stun / 10f);
                AudioPlayer.instance.Play("Hit2", 1 + stun / 10f);
                GetComponent<Animator>().SetTrigger("Extra");
                timer = 0;
                state = 4;
            }
        }

    }
}

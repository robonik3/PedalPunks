using System.Collections;
using UnityEngine;

public class SkullRiderBossScript : EnemyScript
{
    public float HP=10;
    [SerializeField] GameObject fire;
    private float timer;
    private bool playsound;
    private float fireTime;
    private bool stunned = false;
    private Animator animator;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        mover = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
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
        

        mover.linearVelocityX = 0;
        mover.linearVelocityY = 0;
        if (Physics2D.BoxCast(transform.position, new Vector2(1,1.3f), 0, Vector2.right, 6, LayerMask.GetMask("Pothole"))) 
        {
            mover.linearVelocityY = -3;
        }
        timer += Time.deltaTime;
        if (timer > 1)
        {
            animator.Play("Wheelie");

            mover.linearVelocityY = (Random.Range(0, 2) == 0 ? 3 : -3);
            timer = 0;
            state = 2;
        }
    }
    void Target()
    {

    }
    void Wheelie()
    {
        fireTime = Mathf.Clamp01(fireTime -= Time.deltaTime);
        if(fireTime == 0)
        {
            Instantiate(fire,transform.position, new Quaternion(),GameObject.Find("Road Tilemap").transform);

            fireTime += .1f;
        }

        timer += Time.deltaTime;
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
        if (timer<.5f)
        {
            mover.linearVelocityX = -3f;

        }
        else
        {
            mover.linearVelocityX = 3f;

        }


        timer += Time.deltaTime;

        if (timer > 1)
        {
            timer = 0;
            state = 0;
            return;
        }
    }

    public override void Shoved()
    {
        
        //imagine player gets flung backward 

        HP--;
        if (HP < 1)
        {
            AudioPlayer.instance.Play("CrunchPunch");
            FindFirstObjectByType<ProgressBar>().CanComplete = true;
            base.Shoved();
        }
    }
    void BackwardsEntrance()
    {

        mover.linearVelocityX = -3;
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

using System.Collections;
using UnityEngine;

public class FloatieEnemyScript : EnemyScript
{
    private float timer;
    private float random;
    [SerializeField]private bool notcounted;
    private bool stunned = false;
    private Animator animator;
    [SerializeField] private GameObject urchin;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        mover = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        slide = false;
        random = Random.Range(.9f, 2);
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
                BackwardsEntrance();
                break;
            case 1:
                ThrowUrchin();
                break;
            case 2:
                MoveLeft();
                break;
        }

    }
    void BackwardsEntrance()
    {
        mover.linearVelocityX = -4;
        mover.linearVelocityY = 0;

        timer += Time.deltaTime;
        if ((timer > random) && !stunned)
        {
            animator.Play("Extra");

            timer = 0;
            state = 1;
        }
    }
    void ThrowUrchin()
    {
        mover.linearVelocityX = -1;
        mover.linearVelocityY = 0;
        timer += Time.deltaTime;
        if (timer > 1)
        {
            animator.Play("Drive");
            Instantiate(urchin,transform.position,new Quaternion());
            state = 2;
        }
    }
    void MoveLeft()
    {
        animator.speed = 1.5f;
        mover.linearVelocityX = -6;
        mover.linearVelocityY = 0;

    }
    public override void Dead()
    {
        if (EnemySpawner.instance != null&&!notcounted) EnemySpawner.instance.activeEnemies--;
        Destroy(gameObject);
    }
}

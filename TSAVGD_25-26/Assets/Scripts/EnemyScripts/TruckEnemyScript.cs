using System.Collections;
using UnityEngine;

public class TruckEnemyScript : EnemyScript
{
    private float timer;
    private bool playsound;
    private bool done;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        mover = GetComponent<Rigidbody2D>();
        slide = false;
        if (transform.position.x > 9)
        {
            state = 1;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        switch (state)
        {
            
            case 0:
                Drive();
                WaitToDeliver();

                break;
            case 1:
                BackwardsEntrance();
                WaitToDeliver();
                break;

        }

    }
    void Drive()
    {
        mover.linearVelocityX = 4;
        mover.linearVelocityY = 0;
    }

    void BackwardsEntrance()
    {

        mover.linearVelocityX = -4;
        mover.linearVelocityY = 0;


    }
    void WaitToDeliver()
    {
        timer += Time.deltaTime;
        if (timer>1.8f && !done)
        {
            done = true;
            AudioPlayer.instance.Play("Jump", Random.Range(.9f, 1.2f));
            for(int i = 0; i < 3; i++)
            {
                Transform nemy = transform.GetChild(1);
                nemy.parent = null;
                nemy.GetComponent<EnemyScript>().DepartTruck();

            }
            EnemySpawner.instance.truck = null;
        }
    }
    public override void Shoved()
    {
        
    }
    public override void Stun()
    {
        
    }
    public override void Explode()
    {
        
    }

}

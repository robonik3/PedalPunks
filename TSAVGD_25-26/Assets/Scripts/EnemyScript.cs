using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class EnemyScript : MonoBehaviour
{
    private Rigidbody2D mover;
    [SerializeField] private BikeType bike;
    [SerializeField] private BoxCollider2D back;
    private bool crash;
    public AudioSource explosion;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        mover = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!crash)
        {
            mover.linearVelocityX = 3;
            if (Mathf.Abs(PlayerScript.instance.transform.position.y - transform.position.y) < 1 
                && Vector3.Distance(PlayerScript.instance.transform.position,transform.position)<3
                && transform.position.x<PlayerScript.instance.transform.position.x)
            {
                mover.linearVelocityY = Mathf.Clamp((transform.position.y-PlayerScript.instance.transform.position.y +.01f)*10,-1,1)*3;
            }
            else
            {
                mover.linearVelocityY = 0;

            }

            if (transform.position.x > 10) { Dead(); }

            // if (Physics2D.OverlapCircle(transform.position + new Vector3(.2f, 0, 0), .2f, LayerMask.GetMask("Player","Default"))) //hits enemy and player and crashes them
            // {
            //     crash = true;
            //     gameObject.layer = 0;
            //     EnemySpawner.instance.activeEnemies--;
            //     mover.linearVelocityX = 0;
            // }
        }
        else
        {
            explosion.Play();
            Crashed();
        }
    }
    private void Crashed()
    {
        transform.eulerAngles += new Vector3(0, 0, (Time.deltaTime*-360)*2);
        mover.linearVelocityX = -6;
        if (transform.position.x < -10) { Destroy(gameObject); }

    }
    public void Shoved(float velocity)
    {
        Instantiate(bike.prefab, transform.position, new Quaternion());
        Dead();
        //add visual of guy flying off bike using the velocity
    }
    void Dead()
    {
        EnemySpawner.instance.activeEnemies--;
        Destroy(gameObject);
    }
}

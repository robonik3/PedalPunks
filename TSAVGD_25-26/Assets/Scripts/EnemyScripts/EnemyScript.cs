using System.Collections;
using System.Runtime.CompilerServices;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class EnemyScript : MonoBehaviour
{
    public Rigidbody2D mover;
    public BikeType bike;
    public GameObject explosionPrefab;
    public GameObject RagdollPrefab;
    public PlayerScript playerScript;
    public int state;
    private bool crash;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        mover = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //backoff = playerScript.invulnerable;
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
            AudioPlayer.instance.Play("explosion3");
            Crashed();
        }
    }
    private void Crashed()
    {
        transform.eulerAngles += new Vector3(0, 0, (Time.deltaTime*-360)*2);
        mover.linearVelocityX = -6;
        if (transform.position.x < -10) { Destroy(gameObject); }

    }

    //the keyword VIRTUAL allows for optional override of code in scripts that inhereit from this one
    public virtual void Shoved()
    {
        Instantiate(bike.prefab, transform.position, new Quaternion());
        bool above = PlayerScript.instance.transform.position.y > transform.position.y;
        GameObject r = Instantiate(RagdollPrefab, transform.position, new Quaternion());
        r.GetComponent<SpriteRenderer>().sprite = bike.enemyRagdoll[(above?0:1)];
        r.GetComponent<Rigidbody2D>().linearVelocity = mover.linearVelocity / 6 + new Vector2(0, (above?-1:1)*4);
        Dead();
    }
    public void Dead()
    {
        if(EnemySpawner.instance!=null)EnemySpawner.instance.activeEnemies--;
        Destroy(gameObject);
    }
    public void Explode()
    {
        StartCoroutine(ExplodeSequence());
    }
    IEnumerator ExplodeSequence()
    {
        AudioPlayer.instance.Play("explosion3");
        EnemySpawner.instance.activeEnemies--;
        this.gameObject.SetActive(false);
        Instantiate(explosionPrefab, transform.position, Quaternion.identity);
        yield return null;
        Destroy(gameObject);
    }
}

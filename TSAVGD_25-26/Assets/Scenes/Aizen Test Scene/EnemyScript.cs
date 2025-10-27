using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class EnemyScript : MonoBehaviour
{
    private Rigidbody2D mover;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        mover = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        mover.AddForce(Vector2.right);
        mover.AddForce(Vector2.up *(PlayerScript.instance.transform.position.y>transform.position.y?1:-1));
        if (transform.position.x > 10) { Dead(); Destroy(gameObject); }
    }

    void Dead()
    {
        //add code here to either have enemy crash or leave their bike when killed

        EnemySpawner.instance.activeEnemies--;
    }
}

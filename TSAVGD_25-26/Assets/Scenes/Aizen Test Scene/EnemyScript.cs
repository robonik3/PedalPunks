using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class EnemyScript : MonoBehaviour
{
    private Rigidbody2D mover;
    [SerializeField] private BikeType bike;
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
        if (transform.position.x > 10) { Dead(); }
    }
    private void Crashed()
    {
        transform.eulerAngles += new Vector3(0, 0, Time.deltaTime*30);
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

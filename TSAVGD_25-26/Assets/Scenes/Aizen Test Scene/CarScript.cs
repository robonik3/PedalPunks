using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class CarScript : MonoBehaviour
{
    private Rigidbody2D mover;
    //[SerializeField] private BikeType bike;
    //private bool crash;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        mover = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
            mover.linearVelocityX = -2;
            

            if (transform.position.x < -10) { Dead(); }

            
        }
    
    void Dead()
    {
        CarSpawner.instance.activeCars--;
        Destroy(gameObject);
    }
}

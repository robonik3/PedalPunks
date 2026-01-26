using UnityEngine;

public class BikeScript : MonoBehaviour
{
    public float fuel;
    public BikeType type;
    private Rigidbody2D mover;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        mover = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        mover.AddForce(Vector2.left);
        if (transform.position.x < -10)
        {
            Destroy(gameObject);
        }
    }
}

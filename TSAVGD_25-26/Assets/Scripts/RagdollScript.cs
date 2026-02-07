using System.Collections;
using UnityEngine;

public class RagdollScript : MonoBehaviour
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
        mover.AddForce(Vector2.left*40);
        if (transform.position.x < -10)
        {
            Destroy(gameObject);
        }
    }
}

using UnityEngine;

public class PotholeScript : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.x < -12f)
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<PlayerScript>().Die();
        }
        if (other.CompareTag("Bike"))
        {
            other.GetComponent<BikeScript>().Explode();
        }
        if (other.CompareTag("Enemy"))
        {
            other.GetComponent<EnemyScript>().Explode();
        }
    }
}

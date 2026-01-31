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
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("TOUCHING POTHOLE!");
            other.gameObject.GetComponent<PlayerScript>().Die();
        }
    }
}

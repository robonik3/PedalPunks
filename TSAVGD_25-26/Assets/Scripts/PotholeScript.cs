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
        if (other.CompareTag("Player")&&PlayerScript.instance.height==0)
        {
            //other.GetComponent<PlayerScript>().Die();
            PlayerScript.instance.fuel -= 3f / 15f;
            PlayerScript.instance.ultraBoost = 0;
            AudioPlayer.instance.Play("explosion2");
        }
        if (other.CompareTag("Bike"))
        {
            other.GetComponent<BikeScript>().Explode();
        }
        if (other.CompareTag("Enemy"))
        {
            other.GetComponent<EnemyScript>().Shoved();
        }
    }
}

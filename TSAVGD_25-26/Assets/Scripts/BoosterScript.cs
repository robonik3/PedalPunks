using UnityEngine;

public class BoosterScript : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.left * 8 * Time.deltaTime);

        if (transform.position.x < -12f)
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")&&PlayerScript.instance.height==0)
        {
            PlayerScript.instance.trickBoost += 1;
            AudioPlayer.instance.Play("BikerWheelie1", Random.Range(1, 1.2f));
        }

    }
}

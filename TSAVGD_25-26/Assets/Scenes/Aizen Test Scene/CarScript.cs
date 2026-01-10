using UnityEngine;
using UnityEngine.SceneManagement;

public class CarScript : MonoBehaviour
{
    public Sprite[] carSprites;
    public float minSpeed = 3f;
    public float maxSpeed = 7f;
    float speed;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        sr.sprite = carSprites[Random.Range(0, carSprites.Length)];

        Debug.Log("Car Spawned");
        speed = Random.Range(minSpeed, maxSpeed);
        //Spawn
        int randomNum = Random.Range(0,4);
        float Yposition = 0f;
        if(randomNum == 0)
        {
            Yposition = -2.2f;
        }
        if(randomNum == 1)
        {
            Yposition = -1.2f;
        }
        if(randomNum == 2)
        {
            Yposition = -0.1f;
        }    
        if(randomNum == 3)
        {
            Yposition = 0.8f;
        }

        transform.position = new Vector3(12f,Yposition, 0f);

    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.left * speed * Time.deltaTime);

        if(transform.position.x < -12f)
        {
            Destroy(gameObject);
        }
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
            {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

}

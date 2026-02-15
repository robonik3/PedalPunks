using System.Collections;
using System.Runtime.CompilerServices;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CarScript : MonoBehaviour
{
    public Sprite[] carSprites;
    public float minSpeed = 3f;
    public float maxSpeed = 7f;
    float speed;
        public GameObject explosionPrefab;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
     
        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        Debug.Log("Found SpriteRenderer: " + (sr != null));
        Debug.Log("Using SpriteRenderer: " + sr.name);


        if (sr != null && carSprites.Length > 0)
        {
            sr.sprite = carSprites[Random.Range(0, carSprites.Length-1)];
            Debug.Log("Chosen Sprite: " + sr.sprite.name);
        }
        else if (sr == null)
        {
            Debug.LogWarning("No SpriteRenderer found!");
        }
        else
        {
            Debug.LogWarning("No Car Sprites assigned!");
        }

        Debug.Log("Car Spawned");
        if (SceneManager.GetActiveScene().name == "Level 2")
        {
            speed = Random.Range(minSpeed, maxSpeed);
            //Spawn
            int randomNum = Random.Range(0, 4);
            float Yposition = 0f;
            if (randomNum == 0) { Yposition = -2.2f; }
            if (randomNum == 1) { Yposition = -1.2f; }
            if (randomNum == 2) { Yposition = -0.1f; }
            if (randomNum == 3) { Yposition = 0.8f;  }
            transform.position = new Vector3(12f, Yposition, 0f);
        }
        else //level 3
        {
            //Spawn
            int randomNum = Random.Range(0, 2);
            float Yposition = 0f;
            if (randomNum == 0)
            {
                Yposition = -1.2f;
                speed = Random.Range(minSpeed, maxSpeed);
            }
            if (randomNum == 1)
            {
                Yposition = -0.1f;
                Vector3 s = transform.localScale;
                s.x *= -1;
                transform.localScale = s;
                speed = 12f;
            }
            transform.position = new Vector3(12f, Yposition, 0f);
        }



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
        if (other.CompareTag("Player") && PlayerScript.instance.height == 0)
        {
            if(other.gameObject.layer == 9)
            {
                Explode();
            }
            else
            {
            other.GetComponent<PlayerScript>().Die();
            }

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
public void Explode()
    {
        StartCoroutine(ExplodeSequence());
    }
    IEnumerator ExplodeSequence()
    {
        AudioPlayer.instance.Play("explosion3");
        EnemySpawner.instance.activeEnemies--;
        this.gameObject.SetActive(false);
        Instantiate(explosionPrefab, transform.position, Quaternion.identity);
        yield return null;
        Destroy(gameObject);
    }
}

using System.Collections;
using UnityEngine;

public class BikeScript : MonoBehaviour
{
    public float fuel;
    public BikeType type;
    private Rigidbody2D mover;

    //exploding things
    [SerializeField] private GameObject explosionPrefab;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        mover = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        mover.AddForce(Vector2.left);
        if (transform.position.x < -15)
        {
            Destroy(gameObject);
        }
    }
    public void Explode()
    {
        StartCoroutine(ExplodeSequence());
    }
    IEnumerator ExplodeSequence()
    {
        gameObject.SetActive(false);
        Instantiate(explosionPrefab, transform.position, Quaternion.identity);
        yield return null;
        Destroy(gameObject);
    }
}

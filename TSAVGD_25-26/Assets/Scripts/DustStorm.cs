using System.Collections;
using UnityEngine;

public class DustStorm : MonoBehaviour
{
    [SerializeField] private ParticleSystem particles;
    [SerializeField] private Material black;
    [SerializeField] private Material regular;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void ActivateStorm()
    {
        StartCoroutine("Storm");
    }
    public void DeactivateStorm()
    {
        StartCoroutine("StormAway");
    }
    IEnumerator Storm()
    {
        particles.Play();
        yield return new WaitForSeconds(1);
        float timer = 1;
        GetComponent<SpriteRenderer>().enabled = true;
        GetComponent<BoxCollider2D>().enabled = true;
        while (timer > 0)
        {
            timer -= Time.deltaTime;
            transform.position = new Vector3(transform.localScale.x * timer, 0, 0);
            yield return null;
        }
        transform.position = Vector3.zero;
    } 
    IEnumerator StormAway()
    {
        float timer = 0;

        while (timer > -1)
        {
            timer -= Time.deltaTime;
            transform.position = new Vector3(transform.localScale.x * timer, 0, 0);
            yield return null;
        }
        transform.position = new Vector3(-18,0,0);
        GetComponent<SpriteRenderer>().enabled = false;
        GetComponent<BoxCollider2D>().enabled = false;
        yield return new WaitForSeconds(1);
        particles.Stop();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out SpriteRenderer sprite))
        {
            sprite.material = black;
        }        
        if (other.TryGetComponent(out PlayerScript p))
        {
            p.playerVisual.GetComponent<SpriteRenderer>().material = black;
            p.bikeVisual.GetComponent<SpriteRenderer>().material = black;
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.TryGetComponent(out SpriteRenderer sprite))
        {
            sprite.material = regular;
        }        
        if (other.TryGetComponent(out PlayerScript p))
        {
            p.playerVisual.GetComponent<SpriteRenderer>().material = regular;
            p.bikeVisual.GetComponent<SpriteRenderer>().material = regular;
        }
    }
}

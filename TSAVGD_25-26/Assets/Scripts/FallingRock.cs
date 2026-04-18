using UnityEngine;

public class FallingRock : MonoBehaviour
{
    private float timer = 1;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
            AudioPlayer.instance.Play("Wind",Random.Range(.9f,1.3f));

    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime/2.5f;

        transform.GetChild(0).transform.eulerAngles = new Vector3(0, 0, timer *720);
        GetComponent<SpriteRenderer>().color = new Color(0, 0, 0, 1-timer);
        transform.localScale = 2 * (1-timer) * Vector3.one ;
        transform.GetChild(0).transform.localPosition = new Vector3(0, timer*15/transform.localScale.x);
        if (timer < 0)
        {
            if (Physics2D.OverlapCircle(transform.position, .5f, LayerMask.GetMask("Player")))
            {
                PlayerScript.instance.Die();
            }
            transform.GetChild(1).GetComponent<ParticleSystem>().Play();
            transform.GetChild(1).parent = null;
            AudioPlayer.instance.Play("explosion2",Random.Range(.8f,1.1f));
            Destroy(gameObject);
        }
    }
}

using UnityEngine;

public class ThrownUrchin : MonoBehaviour
{
    private float timer = 1;
    private bool above;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
            AudioPlayer.instance.Play("Wind",Random.Range(.9f,1.3f));
        above = transform.position.y > 0;
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;
        Transform u = transform.GetChild(0);
        transform.position += (above?-2:2) * Time.deltaTime * Vector3.up;
        u.eulerAngles = new Vector3(0, 0, timer *720);
        GetComponent<SpriteRenderer>().color = new Color(0, 0, 0, (1-timer)/2);
        u.localScale = Vector3.one*(1.5f - (1-timer) / 2);
        if (timer < 0)
        {
            timer = 1;
            gameObject.AddComponent<PotholeScript>();
            if (Physics2D.OverlapCircle(transform.position, .25f, LayerMask.GetMask("Player")))
            {
                PlayerScript.instance.fuel -= 3f / 15f;
                PlayerScript.instance.ultraBoost = 0;
                AudioPlayer.instance.Play("explosion2");
            }
            Destroy(this);

        }
    }
}

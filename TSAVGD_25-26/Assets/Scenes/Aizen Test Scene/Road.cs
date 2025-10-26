using UnityEngine;

public class Road : MonoBehaviour
{
    private float timer;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime*8;
        while (timer < -.5f)
        {
            timer++;
        }
        transform.position = new Vector3(timer, -1.35f, 0);
    }
}

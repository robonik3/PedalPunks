using UnityEngine;

public class Road : MonoBehaviour
{
    [SerializeField] private float roadLength;
    private float timer;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (roadLength <= 0) { roadLength = 1; }
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime*8;
        while (timer < -roadLength && !(roadLength <= 0))
        {
            timer += roadLength;
        }
        transform.position = new Vector3(timer, transform.position.y, 0);
    }
}

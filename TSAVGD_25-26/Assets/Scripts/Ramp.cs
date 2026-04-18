using UnityEngine;

public class Ramp : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.TryGetComponent(out PlayerScript player))
        {
            player.fallingVelocity = Mathf.Sqrt(2 * 9.81f * 2);
        }
    }
}

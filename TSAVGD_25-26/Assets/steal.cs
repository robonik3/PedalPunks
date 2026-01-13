using UnityEngine;

public class steal : MonoBehaviour
{
    public float fuelTrig = 0;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name=="Enemy Biker(Clone)") {
        fuelTrig = 1;
        }
    }
}

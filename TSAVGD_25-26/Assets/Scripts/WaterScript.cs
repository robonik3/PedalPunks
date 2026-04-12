using UnityEngine;

public class WaterScript : MonoBehaviour
{
    [SerializeField] private GameObject waterOverlay;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        foreach(Collider2D c in Physics2D.OverlapBoxAll(transform.position, transform.localScale, 0))
        {
            if (c.GetComponent<EnemyScript>())
            {

            }
        }
    }
}

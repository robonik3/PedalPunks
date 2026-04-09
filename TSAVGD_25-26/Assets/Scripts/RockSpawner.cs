using System.Collections;
using UnityEngine;

public class RockSpawner : MonoBehaviour
{
    [SerializeField] GameObject rock;
    float cooldown;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        cooldown -= Time.deltaTime;
        if (cooldown < 0)
        {
            cooldown = 15;
            StartCoroutine(SpawnRocks());
        }
    }
    IEnumerator SpawnRocks()
    {
        float timer = 0;
        for(int i = 0; i < 4; i++)
        {
            Instantiate(rock, new Vector3(Random.Range(-8, 8), Random.Range(-2, 2), 0), new Quaternion());
            while (timer < 1)
            {
                timer += Time.deltaTime;
                yield return null;
            }
            timer = 0;
        }

    }
}

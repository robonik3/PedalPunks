using UnityEngine;

public class PotholeSpawner : MonoBehaviour
{
    [SerializeField] private Transform roadTransform;
    [SerializeField] private GameObject potholePrefab;
    public float spawnDelay = 5f;

    void Start()
    {
        InvokeRepeating(nameof(SpawnPothole), 1f, spawnDelay);
    }

    void SpawnPothole()
    {
        int randomNum = Random.Range(0, 4);
        float Yposition = 0f;
        if (randomNum == 0)
        {
            Yposition = -2.4f;
        }
        if (randomNum == 1)
        {
            Yposition = -1.4f;
        }
        if (randomNum == 2)
        {
            Yposition = -0.3f;
        }
        if (randomNum == 3)
        {
            Yposition = 1.1f;
        }
        GameObject pothole = Instantiate(potholePrefab, new Vector3(12f, Yposition, 0f), Quaternion.identity);
        pothole.transform.SetParent(roadTransform);
    }
}

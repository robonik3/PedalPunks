using UnityEngine;

public class CarSpawner : MonoBehaviour
{
    public GameObject carPrefab;
    public float spawnDelay = 3f;

    void Start()
    {
        InvokeRepeating(nameof(SpawnCar), 1f, spawnDelay);
    }

    void SpawnCar()
    {
        Instantiate(carPrefab);
    }
}
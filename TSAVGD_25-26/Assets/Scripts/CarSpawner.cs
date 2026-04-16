using UnityEngine;

public class CarSpawner : MonoBehaviour
{
    public GameObject carPrefab;
    public float spawnDelay = 3f;

    void Start()
    {
        InvokeRepeating(nameof(SpawnCar), 2f, spawnDelay);
    }

    void SpawnCar()
    {
        Instantiate(carPrefab,new Vector3(13,0,0),new Quaternion());
    }
    private void OnDisable()
    {
        CancelInvoke();
        Destroy(gameObject);
    }
}
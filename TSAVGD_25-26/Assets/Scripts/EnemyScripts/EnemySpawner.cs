using UnityEngine;
using System.Collections;
using UnityEngine.Events;
public class EnemySpawner : MonoBehaviour
{
    public static EnemySpawner instance;

    [SerializeField] private WaveInfo[] waves;
    private int runthrough;
    
    [HideInInspector] public int activeEnemies;
    private int completedWaves;
    private float timer;
    public bool readyToEndLevel;
    [HideInInspector] public GameObject truck;

    [System.Serializable]
    public class WaveInfo
    {
        public GameObject enemy;
        public float delaySpawnTime;
        public float ypos;
        public bool spawnOnRightSide;
    }
    public UnityEvent FinishLevel;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        instance = this;
        for(int i = 0; i < waves.Length; i++)
        {
            StartCoroutine("WaitToSpawn");
            runthrough++;
        }
    }
    private void Update()
    {
          timer += Time.deltaTime;
        if(activeEnemies < 1)
        {
            if(10 < timer && timer < 120)
            {
                timer += 5;
            }
            if(completedWaves == waves.Length)
            {
                if (readyToEndLevel)
                {
                    FinishLevel.Invoke();
                }
                else
                {
                    if (timer > 126)
                    {
                        timer -= 4;
                        Instantiate(waves[0].enemy, new Vector3(-10, 1f, 0), new Quaternion());
                        Instantiate(waves[0].enemy, new Vector3(-10, -2, 0), new Quaternion());

                    }
                }

            }
        }
    }
    IEnumerator WaitToSpawn()
    {
        int i = runthrough;
        while (timer < waves[i].delaySpawnTime)
        {
            yield return null;
        }
        activeEnemies++;
        completedWaves++;
        GameObject nemy = Instantiate(waves[i].enemy, new Vector3((waves[i].spawnOnRightSide ? 10 : -10), waves[i].ypos, 0), new Quaternion());
        if (truck&&truck.transform.childCount<4) { nemy.GetComponent<Rigidbody2D>().simulated=false; nemy.transform.parent = truck.transform; nemy.transform.localPosition = new Vector3(-.5f * (truck.transform.childCount-2), .5f, 0); }
        if (waves[i].enemy.name =="Enemy Truck") { truck = nemy; }
    }

}

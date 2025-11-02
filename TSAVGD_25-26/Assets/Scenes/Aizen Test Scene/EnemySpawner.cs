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

    [System.Serializable]
    public class WaveInfo
    {
        public GameObject enemy;
        public float delaySpawnTime;
        public float ypos;
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
            if(5 < timer && timer < 120)
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
        Instantiate(waves[i].enemy, new Vector3(-10, waves[i].ypos, 0), new Quaternion());
    }

}

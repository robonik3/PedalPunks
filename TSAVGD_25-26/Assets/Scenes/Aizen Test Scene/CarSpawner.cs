using UnityEngine;
using System.Collections;
using UnityEngine.Events;
public class CarSpawner : MonoBehaviour
{
    public static CarSpawner instance;

    [SerializeField] private WaveInfo[] waves;
    private int runthrough;

    [HideInInspector] public int activeCars;
    //private int completedWaves;
    private float timer;
    //public bool readyToEndLevel;

    [System.Serializable]
    public class WaveInfo
    {
        public GameObject car;
        public float delaySpawnTime;
        public float ypos;
    }
    public UnityEvent FinishLevel;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        instance = this;
        for (int i = 0; i < waves.Length; i++)
        {
            StartCoroutine("WaitToSpawn");
            runthrough++;
        }
    }
    private void Update()
    {
        timer += Time.deltaTime;
        if (activeCars < 1)
        {
            if (5 < timer && timer < 120)
            {
                timer += 3;
            }


                    if (timer > 126)
                    {
                        timer -= 4;
                        Instantiate(waves[0].car, new Vector3(10, 1f, 0), new Quaternion());
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
        activeCars++;
        Instantiate(waves[i].car, new Vector3(10, waves[i].ypos, 0), new Quaternion());
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public List<GameObject> enemySlimes;
    private float totalTime;
    private float spawnTime;
    public static bool endTime;

    public float MaxX;
    public float MinX;
    public float MaxY;
    public float MinY;

    public int waveTime;
    public float Lvl1_SpawnTime;
    public float Lvl2_SpawnTime;
    public float Lvl3_SpawnTime;
    public float Lvl4_SpawnTime;
    public float Lvl5_SpawnTime;
    public float Lvl6_SpawnTime;
    public float Lvl7_SpawnTime;
    public float Lvl8_SpawnTime;
    public float Lvl9_SpawnTime;
    public float Lvl10_SpawnTime;

    private bool Lvl3_Pattern_On;

    // Start is called before the first frame update
    void Start()
    {
        totalTime = 0;
        spawnTime = 0;
        endTime = false;

        Lvl3_Pattern_On = false;
    }

    // Update is called once per frame
    void Update()
    {
        totalTime += Time.deltaTime;
        spawnTime += Time.deltaTime;
        int currentWave = (int)totalTime / waveTime;
        switch (currentWave)
        {
            case 0:
                NormalPattern(Lvl1_SpawnTime, enemySlimes[0]);
                break;
            case 1:
                NormalPattern(Lvl2_SpawnTime, enemySlimes[0]);
                break;
            case 2:
                if (Lvl3_Pattern_On)
                {
                    NormalPattern(Lvl3_SpawnTime, enemySlimes[0]);
                }
                else
                {
                    StartCoroutine(Lvl3Pattern());
                    Lvl3_Pattern_On = true;
                }
                break;
            case 3:
                break;
            case 4:
                break;
            case 5:
                break;
            case 6:
                break;
            case 7:
                break;
            case 8:
                break;
            case 9:
                break;
            case 10:
                endTime = true;
                break;
            default:
                break;
        }
    }

    void NormalPattern(float spawnRoutine, GameObject obj)
    {
        if (spawnTime > spawnRoutine)
        {
            spawnTime = 0;
            Instantiate(obj, RandomSpawnPoint(), Quaternion.identity);
        }
    }

    Vector3 RandomSpawnPoint()
    {
        int rand = Random.Range(1, 4);

        switch (rand)
        {
            case 1:
                return new Vector3(MinX, Random.Range(MinY, MaxY), 0);
            case 2:
                return new Vector3(MaxX, Random.Range(MinY, MaxY), 0);
            case 3:
                return new Vector3(Random.Range(MinX, MaxX), MinY, 0);
            case 4:
                return new Vector3(Random.Range(MinX, MaxX), MaxY, 0);
            default:
                return new Vector3(MinX, Random.Range(MinY, MaxY), 0);
        }
    }

    IEnumerator Lvl3Pattern()
    {
        do
        {
            Instantiate(enemySlimes[1], RandomSpawnPoint(), Quaternion.identity);
            yield return new WaitForSeconds(9.5f);
        } while (totalTime < 180);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public List<GameObject> enemyList;
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

    public float Lvl3_Pattern_SpawnTime;
    public float Lvl5_Pattern_SpawnTime;
    public float Lvl9_Pattern_SpawnTime;

    private bool Lvl3_Pattern_On;
    private bool Lvl4_Pattern_On;
    private bool Lvl5_Pattern_On;
    private bool Lvl8_Pattern_On;
    private bool Lvl9_Pattern_On;

    // Start is called before the first frame update
    void Start()
    {
        spawnTime = 0;
        endTime = false;

        Lvl3_Pattern_On = false;
        Lvl4_Pattern_On = false;
        Lvl5_Pattern_On = false;
        Lvl8_Pattern_On = false;
        Lvl9_Pattern_On = false;
    }

    // Update is called once per frame
    void Update()
    {
        spawnTime += Time.deltaTime;
        int currentWave = (int)TimeCount.timePassed / waveTime;
        switch (currentWave)
        {
            case 0: // level 1
                NormalPattern(Lvl1_SpawnTime, enemyList[0], currentWave);
                break;
            case 1: // level 2
                NormalPattern(Lvl2_SpawnTime, enemyList[0], currentWave);
                break;
            case 2: // level 3
                if (Lvl3_Pattern_On)
                {
                    NormalPattern(Lvl3_SpawnTime, enemyList[0], currentWave);
                }
                else
                {
                    StartCoroutine(PatternSpawn(Lvl3_Pattern_SpawnTime, enemyList[1], currentWave + 1));
                    Lvl3_Pattern_On = true;
                }
                break;
            case 3: // level 4
                if (Lvl4_Pattern_On)
                {
                    NormalPattern(Lvl4_SpawnTime, enemyList[0], currentWave);
                }
                else
                {
                    Instantiate(enemyList[3], RandomSpawnPoint(), Quaternion.identity);
                    Lvl4_Pattern_On = true;
                }
                break;
            case 4: // level 5
                if (Lvl5_Pattern_On)
                {
                    NormalPattern(Lvl5_SpawnTime, enemyList[0], currentWave);
                }
                else
                {
                    StartCoroutine(PatternSpawn(Lvl5_Pattern_SpawnTime, enemyList[1], currentWave + 1));
                    Lvl5_Pattern_On = true;
                }
                break;
            case 5: // level 6
                NormalPattern(Lvl6_SpawnTime, enemyList[1], currentWave);
                break;
            case 6: // level 7
                NormalPattern(Lvl7_SpawnTime, enemyList[1], currentWave);
                break;
            case 7: // level 8
                if (Lvl8_Pattern_On)
                {
                    NormalPattern(Lvl8_SpawnTime, enemyList[1], currentWave);
                }
                else
                {
                    Instantiate(enemyList[4], RandomSpawnPoint(), Quaternion.identity);
                    Lvl8_Pattern_On = true;
                }
                break;
            case 8: // level 9
                if (Lvl9_Pattern_On)
                {
                    NormalPattern(Lvl9_SpawnTime, enemyList[1], currentWave);
                }
                else
                {
                    StartCoroutine(PatternSpawn(Lvl9_Pattern_SpawnTime, enemyList[5], currentWave + 1));
                    Lvl9_Pattern_On = true;
                }
                break;
            case 9: // level 10
                NormalPattern(Lvl10_SpawnTime, enemyList[5], currentWave);
                break;
            case 10: // gamekiller
                if (!endTime)
                {
                    Instantiate(enemyList[2], new Vector3(0, 5, 0), Quaternion.identity);
                    endTime = true;
                }
                break;
            default:
                break;
        }
    }

    void NormalPattern(float spawnRoutine, GameObject obj, int currentWave)
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

    IEnumerator PatternSpawn(float spawnRoutine, GameObject obj, int level)
    {
        do
        {
            Instantiate(obj, RandomSpawnPoint(), Quaternion.identity);
            yield return new WaitForSeconds(spawnRoutine);
        } while (TimeCount.timePassed < level * 60);
    }
}

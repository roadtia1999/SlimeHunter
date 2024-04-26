using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public List<GameObject> enemySlimes;
    private float totalTime;
    private float spawnTime;

    public float MaxX;
    public float MinX;
    public float MaxY;
    public float MinY;

    public int waveTime;
    public float Lvl1_SpawnTime;

    // Start is called before the first frame update
    void Start()
    {
        totalTime = 0;
        spawnTime = 0;
    }

    // Update is called once per frame
    void Update()
    {
        int rand = Random.Range(1, 4);
        Vector3 spawn = new Vector3();
        Quaternion qtn = Quaternion.identity;

        switch (rand)
        {
            case 1:
                spawn = new Vector3(MinX, Random.Range(MinY, MaxY), 0);
                break;
            case 2:
                spawn = new Vector3(MaxX, Random.Range(MinY, MaxY), 0);
                break;
            case 3:
                spawn = new Vector3(Random.Range(MinX, MaxX), MinY, 0);
                break;
            case 4:
                spawn = new Vector3(Random.Range(MinX, MaxX), MaxY, 0);
                break;
        }

        totalTime += Time.deltaTime;
        spawnTime += Time.deltaTime;
        int currentWave = (int)totalTime / waveTime;
        switch (currentWave)
        {
            case 0:
                if (spawnTime > Lvl1_SpawnTime)
                {
                    spawnTime = 0;
                    Instantiate(enemySlimes[0], spawn, qtn);
                }
                break;
            default:
                break;
        }
    }
}

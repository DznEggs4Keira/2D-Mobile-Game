using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp_Spawner : MonoBehaviour
{
    public float startIntervalTime = 3f;

    private float intervalTime;

    private void Start()
    {
        intervalTime = startIntervalTime;
    }

    // Update is called once per frame
    void Update()
    {
        intervalTime -= Time.deltaTime;

        if (intervalTime <= 0f)
        {
            SpawnPowerUps();
            intervalTime = startIntervalTime;
        }
    }

    void SpawnPowerUps()
    {
        //random element from enemy pool list
        int index = (int)Random.Range(0f, (Object_Pooler.Instance.Powerups.Count));

        //Spawn Enemies
        Object_Pooler.Instance.SpawnPowerUps(Object_Pooler.Instance.Powerups[index].tag,
            new Vector3(Random.Range(-2, 2), Random.Range(-2.8f, 3.5f), 0), Quaternion.identity);
    }
}

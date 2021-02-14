using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Object_Pooler : MonoBehaviour
{
    [System.Serializable]
    public class Pool
    {
        public string tag;
        public GameObject prefab;
        public int size;
    }

    #region Singleton
    public static Object_Pooler Instance;

    private void Awake()
    {
        Instance = this;
    }
    #endregion

    public List<Pool> Enemies;
    public List<Pool> Powerups;
    public List<Pool> Bullets;

    public Dictionary<string, Queue<GameObject>> bulletPool;
    public Dictionary<string, Queue<GameObject>> powerPool;
    public Dictionary<string, Queue<GameObject>> enemyPool;

    // Start is called before the first frame update
    void Start()
    {
        bulletPool = new Dictionary<string, Queue<GameObject>>();
        enemyPool = new Dictionary<string, Queue<GameObject>>();
        powerPool = new Dictionary<string, Queue<GameObject>>();

        //Bullets
        foreach (Pool pool in Bullets)
        {
            Queue<GameObject> objectPool = new Queue<GameObject>();

            for (int i = 0; i < pool.size; i++)
            {
                GameObject obj = Instantiate(pool.prefab);
                obj.SetActive(false);
                objectPool.Enqueue(obj);
            }

            bulletPool.Add(pool.tag, objectPool);
        }

        //Enemies
        foreach (Pool pool in Enemies)
        {
            Queue<GameObject> objectPool = new Queue<GameObject>();

            for (int i = 0; i < pool.size; i++)
            {
                GameObject obj = Instantiate(pool.prefab);
                obj.SetActive(false);
                objectPool.Enqueue(obj);
            }

            enemyPool.Add(pool.tag, objectPool);
        }

        //PowerUps
        foreach (Pool pool in Powerups)
        {
            Queue<GameObject> objectPool = new Queue<GameObject>();

            for (int i = 0; i < pool.size; i++)
            {
                GameObject obj = Instantiate(pool.prefab);
                obj.SetActive(false);
                objectPool.Enqueue(obj);
            }

            powerPool.Add(pool.tag, objectPool);
        }
    }

    public GameObject SpawnEnemies(string tag, Vector2 position, Quaternion rotation)
    {
        if (!enemyPool.ContainsKey(tag))
        {
            Debug.LogWarning("Pool with tag " + tag + "doesn't exist");
            return null;
        }

        GameObject objectToSpawn = enemyPool[tag].Dequeue();
        objectToSpawn.SetActive(true);
        objectToSpawn.transform.position = position;
        objectToSpawn.transform.rotation = rotation;

        enemyPool[tag].Enqueue(objectToSpawn);

        return objectToSpawn;
    }

    public GameObject SpawnPowerUps(string tag, Vector2 position, Quaternion rotation)
    {
        if (!powerPool.ContainsKey(tag))
        {
            Debug.LogWarning("Pool with tag " + tag + "doesn't exist");
            return null;
        }

        GameObject objectToSpawn = powerPool[tag].Dequeue();
        objectToSpawn.SetActive(true);
        objectToSpawn.transform.position = position;
        objectToSpawn.transform.rotation = rotation;

        powerPool[tag].Enqueue(objectToSpawn);

        return objectToSpawn;
    }

    public GameObject SpawnBullets(string tag, Vector2 position, Quaternion rotation)
    {
        if (!bulletPool.ContainsKey(tag))
        {
            Debug.LogWarning("Pool with tag " + tag + "doesn't exist");
            return null;
        }

        GameObject objectToSpawn = bulletPool[tag].Dequeue();
        objectToSpawn.SetActive(true);
        objectToSpawn.transform.position = position;
        objectToSpawn.transform.rotation = rotation;

        bulletPool[tag].Enqueue(objectToSpawn);

        return objectToSpawn;
    }
}

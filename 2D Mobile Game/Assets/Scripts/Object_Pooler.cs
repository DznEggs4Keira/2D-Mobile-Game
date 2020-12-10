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

    public List<Pool> Enemies;
    public List<Pool> Powerups;

    public Dictionary<string, Queue<GameObject>> powerPool;
    public Dictionary<string, Queue<GameObject>> enemyPool;

    // Start is called before the first frame update
    void Start()
    {
        enemyPool = new Dictionary<string, Queue<GameObject>>();
        powerPool = new Dictionary<string, Queue<GameObject>>();

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
    }
}

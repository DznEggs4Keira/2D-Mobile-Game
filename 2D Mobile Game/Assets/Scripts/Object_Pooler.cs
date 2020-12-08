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
    public Dictionary<string, Queue<GameObject>> enemyPool;

    // Start is called before the first frame update
    void Start()
    {
        enemyPool = new Dictionary<string, Queue<GameObject>>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

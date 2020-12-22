using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Spawner : MonoBehaviour
{
    float force = 300f;
    public float timer = 2f;
    [SerializeField]
    private float counter;

    #region Moving Spawner
    /*
    public float moveSpeed = -10f;

    float cameraY;
    float reposPoint = 5f; // y/height of the new placement
    
    void Awake()
    {
        cameraY = Camera.main.gameObject.transform.position.y - 15f;
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        Reposition();
    }

    void Move()
    {
        //move spawner
        Vector3 temp = transform.position;
        temp.y += moveSpeed * Time.deltaTime;
        transform.position = temp;
    }

    void Reposition()
    {
        if (transform.position.y < cameraY)
        {
            Vector3 temp = transform.position;
            temp.y = reposPoint;
            transform.position = temp;

            SpawnEnemies();
        }
    }
    */
    #endregion

    #region Counter Spawner

    private void Start()
    {
        counter = timer;
    }

    private void Update()
    {
        counter -= Time.deltaTime;

        if(counter <= 0)
        {
            //spawn enemy
            SpawnEnemies();

            //reset timer
            counter = timer;
        }
    }

    #endregion

    void SpawnEnemies()
    {
        //random element from enemy pool list
        int index = (int)Random.Range(0f, (Object_Pooler.Instance.Enemies.Count));

        //Spawn Enemies
        MoveEnemy(Object_Pooler.Instance.SpawnEnemies(Object_Pooler.Instance.Enemies[index].tag,
            new Vector3(Random.Range(-2, 2), transform.position.y, 0), Quaternion.identity));
    }

    void MoveEnemy(GameObject temp)
    {
        temp.GetComponent<Rigidbody2D>().velocity = (Vector2.down * force * Time.deltaTime);
    }
}
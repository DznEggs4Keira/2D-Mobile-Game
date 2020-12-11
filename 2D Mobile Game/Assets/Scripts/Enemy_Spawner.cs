using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Spawner : MonoBehaviour
{
    public float moveSpeed = -5f;

    float cameraY;
    float reposPoint = 5f; // y/height of the new placement
    float force = 300f;

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
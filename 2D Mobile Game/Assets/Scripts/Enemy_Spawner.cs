using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Spawner : MonoBehaviour
{
    public float moveSpeed = -5f;
    public GameObject[] enemies;

    float cameraY;
    float reposPoint = 5f; // y/height of the new placement

    Vector2 direction = new Vector2(0, -1);
    float force = 300f;

    int randomEnemyIndex;

    //for movement of enemies
    GameObject tempEnemy;

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

    void MoveEnemy()
    {
        Rigidbody2D temp = tempEnemy.GetComponent<Rigidbody2D>();
        temp.AddForce(direction * force);
    }

    void SpawnEnemies()
    {
        if (Random.Range(0, 10) > 5)
        {
            randomEnemyIndex = Random.Range(0, enemies.Length);
        }

        //Enemies
        tempEnemy = Instantiate(enemies[randomEnemyIndex], new Vector3(Random.Range(-2, 2), transform.position.y, 0), Quaternion.identity);

        MoveEnemy();
    }
}
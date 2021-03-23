using UnityEngine;

public class Enemy_Spawner : MonoBehaviour
{
    readonly float force = 300f;
    public float timer = 2f;
    [SerializeField]
    private float counter;

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
        int index = (int)Random.Range(0f, Object_Pooler.Instance.Enemies.Count);

        //Spawn Enemies
        MoveEnemy(Object_Pooler.Instance.SpawnEnemies(Object_Pooler.Instance.Enemies[index].tag,
            new Vector3(Random.Range(-2, 2), transform.position.y, 0), Quaternion.identity));
    }

    void MoveEnemy(GameObject temp)
    {
        temp.GetComponent<Rigidbody2D>().velocity = (Vector2.down * force * Time.deltaTime);
    }
}
using UnityEngine;
using System.Collections;

public class EnemyRespawn : MonoBehaviour
{

    [SerializeField]
    private GameObject[] enemies;

    private float[] arrPosY = { -13f, 13f };

    [SerializeField]
    private float respawnTime = 1.5f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartEnemyRoutine();
    }

    void StartEnemyRoutine()
    {
        StartCoroutine("EnemyRoutine");
    }

    public void StopEnemyRoutine()
    {
        StopCoroutine("EnemyRoutine");
    }

    IEnumerator EnemyRoutine()
    {
        yield return new WaitForSeconds(1.5f);
        while (true)
        {
            SpawnEnemy();
            yield return new WaitForSeconds(respawnTime);

        }
    }

    void SpawnEnemy()
    {
        int randomIndex = Random.Range(0, enemies.Length);
        float randomPosY = Random.Range(arrPosY[0], arrPosY[1]);

        Instantiate(enemies[randomIndex], new Vector3(transform.position.x, randomPosY, 0), Quaternion.identity);
    }
}

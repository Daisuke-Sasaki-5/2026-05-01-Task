using System;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private Transform plane;
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private float spawnInterval = 2f;

    private float timer;

    void Update()
    {
        timer += Time.deltaTime;

        if(timer >= spawnInterval)
        {
            Spawn();
            timer = 0f;
        }
    }

    /// <summary>
    /// スポーン処理
    /// 範囲と位置の決定
    /// </summary>
    void Spawn()
    {
        float width = 10f * plane.localScale.x;
        float height = 10f * plane.localScale.z;

        float x = UnityEngine.Random.Range(-width / 2f, width / 2f);
        float z = UnityEngine.Random.Range(-height / 2f, height / 2f);

        Vector3 pos = new Vector3(x,1,z) + plane.position;

        Instantiate(enemyPrefab, pos, Quaternion.identity);
    }
}

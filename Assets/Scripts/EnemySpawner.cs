using System;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private Transform plane;
    [SerializeField] private GameObject enemyPrefab;

    [Header("敵スポーン間隔")]
    [SerializeField] private float spawnInterval = 2f;

    private float timer;

    void Update()
    {
        timer += Time.deltaTime;

        if(timer >= spawnInterval)
        {
            Spawn();
            timer -= spawnInterval;
        }
    }

    /// <summary>
    /// スポーン処理
    /// 範囲と位置の決定
    /// </summary>
    void Spawn()
    {
        // Planeサイズ取得
        float width = 10f * plane.localScale.x;
        float height = 10f * plane.localScale.z;

        // Planeを中心にランダムにスポーン
        float x = UnityEngine.Random.Range(-width / 2f, width / 2f);
        float z = UnityEngine.Random.Range(-height / 2f, height / 2f);

        // Y座標を上げることで地面に埋まるのを防止
        Vector3 pos = new Vector3(x,1,z) + plane.position;

        // 敵生成
        Instantiate(enemyPrefab, pos, Quaternion.identity);
    }
}

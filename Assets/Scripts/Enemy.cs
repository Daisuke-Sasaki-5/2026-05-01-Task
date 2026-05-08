using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("最大移動速度")]
    [SerializeField] private int MaxMoveSpeed = 0;

    [Header("最少移動速度")]
    [SerializeField] private int MinMoveSpeed = 0;

    private float movespeed;
    private Transform player;

    private void Awake()
    {
        movespeed = Random.Range(MinMoveSpeed, MaxMoveSpeed);
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Update()
    {
        if(player != null)
        {
            // Playerの方向ベクトルを計算
            Vector3 direction = (player.position - transform.position).normalized;

            // 高さ固定
            direction.y = 0f;

            // 敵をPlayerの方向に移動
            transform.position += direction * movespeed * Time.deltaTime;

            // 敵をPlayerの方向に向かせる
            transform.LookAt(player);
        }
    }

    public void Hit()
    {
       Destroy(gameObject);
    }
}

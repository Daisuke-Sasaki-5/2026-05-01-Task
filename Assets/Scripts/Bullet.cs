using UnityEngine;
using UnityEngine.SceneManagement;

public class Bullet : MonoBehaviour
{
    /// <summary>
    /// 移動速度と生存時間の設定
    /// </summary>
    [SerializeField] public float speed = 0;
    [SerializeField] public float lifeTime = 0f;
    private Vector3 direction;

    private void Start()
    {
        // 時間経過で削除
       Destroy(gameObject,lifeTime);
    }

    public void Init(Vector3 dir)
    {
        direction = dir.normalized;
    }

    private void Update()
    {
        transform.position += direction * speed * Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        // EnemyTagにぶつかったらEnemyを消す
        if(other.CompareTag("Enemy"))
        {
            other.GetComponent<Enemy>()?.Hit();

            // スコアにプラスする
            GameManager.instance.AddScore(10);

            // 敵に当たったら弾を削除
            Destroy(gameObject);
        }
    }
}

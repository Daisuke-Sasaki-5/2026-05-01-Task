using UnityEngine;
using UnityEngine.SceneManagement;

public class Bullet : MonoBehaviour
{
    /// <summary>
    /// ˆÚ“®‘¬“x‚Æ¶‘¶ŠÔ‚Ìİ’è
    /// </summary>
    [SerializeField] public float speed = 0;
    [SerializeField] public float lifeTime = 0f;
    private Vector3 direction;

    private void Start()
    {
        // ŠÔŒo‰ß‚ÅDestroy
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
        // EnemyTag‚É‚Ô‚Â‚©‚Á‚½‚çEnemy‚ğÁ‚·
        if(other.CompareTag("Enemy"))
        {
            Destroy(other.gameObject);

            LoadScene();
        }
    }

    public void LoadScene()
    {
        SceneManager.LoadScene("EndScene");
    }
}

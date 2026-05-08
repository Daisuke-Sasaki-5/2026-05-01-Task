using System;
using UnityEngine;
using UnityEngine.InputSystem;
public class Player : MonoBehaviour
{
    //変数宣言
    [Header("移動速度")]
    [SerializeField] private float moveSpeed = 3.0f;

    [Header("HP")]
    [SerializeField] private int health = 0;

    private CharacterController characterController;
    private Vector3 moveVelocity;
    private InputAction move;
    private InputAction shoot;

    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform ShootPointer;

    [SerializeField] private Transform plane;

    /// <summary>
    /// 初期化
    /// </summary>
    void Awake()
    {
        characterController = GetComponent<CharacterController>();
        var input = GetComponent<PlayerInput>();
        input.currentActionMap.Enable();

        move = input.currentActionMap.FindAction("Move");
        shoot = input.currentActionMap.FindAction("Shoot");
    }
    void Update()
    {
        var moveValue = move.ReadValue<Vector2>();
        moveVelocity.x = moveValue.x * moveSpeed;
        moveVelocity.z = moveValue.y * moveSpeed;

        characterController.Move(moveVelocity * Time.deltaTime);

        ClampPosition();

        if(shoot.triggered)
        {
            Shoot();
        }
    }

    /// <summary>
    /// 移動制限
    /// </summary>
    private void ClampPosition()
    {
        float width = 10f * plane.localScale.x;
        float height = 10f * plane.localScale.z;

        Vector3 pos = transform.position;

        float minX = plane.position.x - width / 2f;
        float maxX = plane.position.x + width / 2f;
        float minZ = plane.position.z - width / 2f;
        float maxZ = plane.position.z + width / 2f;

        pos.x = Mathf.Clamp(pos.x, minX, maxX);
        pos.z = Mathf.Clamp(pos.z, minZ, maxZ);

        transform.position = pos;
    }

    /// <summary>
    /// 弾の生成処理
    /// </summary>
    void Shoot()
    {
        var bullet = Instantiate(bulletPrefab,ShootPointer.position, Quaternion.identity);
        bullet.GetComponent<Bullet>().Init(ShootPointer.forward);
    }

    public void OnTriggerEnter(Collider other)
    {
        // Enemyに触れたときダメージを受ける
        if(other.CompareTag("Enemy"))
        {
            TakeDamage(10);

            Destroy(other.gameObject);
        }
    }

    /// <summary>
    /// ダメージ処理
    /// </summary>
    /// <exception cref="NotImplementedException"></exception>
    private void TakeDamage(int damage)
    {
        health -= damage;

        // HPが0になったときGameOverを呼ぶ
        if(health <= 0)
        {
            GameManager.instance.GameOver();
        }
    }
}

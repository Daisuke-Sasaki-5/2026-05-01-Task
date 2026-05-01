using UnityEngine;
using UnityEngine.InputSystem;
public class Player : MonoBehaviour
{
    //ïœêîêÈåæ
    [SerializeField] private float moveSpeed = 3.0f;
    private CharacterController characterController;
    private Vector3 moveVelocity;
    private InputAction move;
    private InputAction shoot;

    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform ShootPointer;

    /// <summary>
    /// èâä˙âª
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

        if(shoot.triggered)
        {
            Shoot();
        }
    }

    /// <summary>
    /// íeÇÃê∂ê¨èàóù
    /// </summary>
    void Shoot()
    {
        var bullet = Instantiate(bulletPrefab,ShootPointer.position, Quaternion.identity);
        bullet.GetComponent<Bullet>().Init(ShootPointer.forward);
    }
}

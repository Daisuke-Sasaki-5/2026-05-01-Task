using System;
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

    [SerializeField] private Transform plane;

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

        ClampPosition();

        if(shoot.triggered)
        {
            Shoot();
        }
    }

    /// <summary>
    /// à⁄ìÆêßå¿
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
    /// íeÇÃê∂ê¨èàóù
    /// </summary>
    void Shoot()
    {
        var bullet = Instantiate(bulletPrefab,ShootPointer.position, Quaternion.identity);
        bullet.GetComponent<Bullet>().Init(ShootPointer.forward);
    }
}

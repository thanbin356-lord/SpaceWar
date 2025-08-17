using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;
using UnityEngine.InputSystem;

public class PlayerControll : MonoBehaviour
{
    public GameObject PlayerBullet;
    public GameObject BulletPosition1;
    public GameObject BulletPosition2;
    public float fireRate; // Adjust the fire rate as needed
    public float nextFireTime;
    public float moveSpeed = 5f;
    private Vector3 lastMousePosition;
    private Rigidbody2D rb;
    private Vector2 moveInput;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = false;

        lastMousePosition = Input.mousePosition;
    }
    void FixedUpdate()
    {
        Vector3 currentMousePosition = Input.mousePosition;
        Vector3 worldDelta = Camera.main.ScreenToWorldPoint(currentMousePosition)
                           - Camera.main.ScreenToWorldPoint(lastMousePosition);
        worldDelta.z = 0f;

        Vector2 newPosition = rb.position + (Vector2)(worldDelta * moveSpeed);
        rb.MovePosition(newPosition);

        lastMousePosition = currentMousePosition;
    }

    void OnFire()
    {
        if (Time.time >= nextFireTime)
        {
            GameObject bullet01 = (GameObject)Instantiate(PlayerBullet);
            bullet01.transform.position = BulletPosition1.transform.position;

            GameObject bullet02 = (GameObject)Instantiate(PlayerBullet);
            bullet02.transform.position = BulletPosition2.transform.position;

            nextFireTime = Time.time + fireRate;
        }
    }
    void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
    }
}

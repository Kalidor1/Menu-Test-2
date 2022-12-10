using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D body;

    private float horizontal;
    private float vertical;
    private float moveLimiter = 0.7f;
    public float runSpeed = 20.0f;
    Vector2 mousePos;
    public float invincibilityTime = 2.0f;

    private GameObject itemInReach;

    void Start()
    {
        body = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal"); // -1 is left
        vertical = Input.GetAxisRaw("Vertical"); // -1 is down


        if (GameController.Instance.playerHealth.IsDead)
        {
            GameController.Instance.PlayerHealthReachedZero();
        }

        if (Input.GetKeyDown(KeyCode.E) && itemInReach != null)
        {
            GameController.Instance.inventory.AddItem(new InventoryItem(itemInReach.GetComponent<Item>()));
            Destroy(itemInReach);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy") && gameObject.layer != 10)
        {
            StartCoroutine(Invincibility());
            GameController.Instance.playerHealth.TakeDamage(1);
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Item"))
        {
            itemInReach = collision.gameObject;
        }
    }

    void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy") && gameObject.layer != 10)
        {
            StartCoroutine(Invincibility());
            GameController.Instance.playerHealth.TakeDamage(1);
        }
    }

    IEnumerator Invincibility()
    {
        gameObject.layer = 10; // Invincible layer
        yield return new WaitForSeconds(invincibilityTime);
        gameObject.layer = 0; // Default layer

        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

    void FixedUpdate()
    {
        if (horizontal != 0 && vertical != 0)
        {
            // limit movement speed diagonally
            horizontal *= moveLimiter;
            vertical *= moveLimiter;
        }

        body.velocity = new Vector2(horizontal * runSpeed, vertical * runSpeed);

        RotateToMousePos();
    }

    public void RotateToMousePos()
    {

        Vector2 dirMousePos = mousePos - body.position;
    }
}

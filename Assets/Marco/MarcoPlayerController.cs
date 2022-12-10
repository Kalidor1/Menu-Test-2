using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarcoPlayerController : MonoBehaviour
{
    Rigidbody2D body;
    private float horizontal;
    private float vertical;
    private float moveLimiter = 0.7f;
    public float rotationSpeed = 0.5f;
    public bool useGamepadRotation = false;
    Vector2 mousePos;
    private GameObject itemInReach;

    void Start()
    {
        body = GetComponent<Rigidbody2D>();
    }

    void Update()
    {

        horizontal = Input.GetAxisRaw("Horizontal"); // -1 is left
        vertical = Input.GetAxisRaw("Vertical"); // -1 is down


        if (useGamepadRotation)
        {
            //Rotate with right stick
            var direction = new Vector2(Input.GetAxis("RightStickVertical"), Input.GetAxis("RightStickHorizontal"));

            // Check for deadzone
            if (direction.magnitude > 0.1)
            {
                // Look at direction
                var angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

                // Rotate towards direction
                body.rotation = angle;
            }
            else
            {
                //Prevent rotation
                body.rotation = 0;
            }
        }
        else
        {
            //Rotate towards mouse
            mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 lookDir = mousePos - body.position;
            float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - 90f;
            body.rotation = angle;
        }

        if (GameController.Instance.playerHealth.IsDead)
        {
            GameController.Instance.PlayerHealthReachedZero();
        }

        // if e is pressed or x on gamepad and item is in reach
        if ((Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.Joystick1Button2)) && itemInReach != null)
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
            if (collision.gameObject.GetComponent<Item>().isPickup)
            {
                GameController.Instance.playerHealth.Heal(1);
                Destroy(collision.gameObject);
            }
            else
            {
                itemInReach = collision.gameObject;
            }
        }


        else if (collision.gameObject.CompareTag("Altar"))
        {
            GameController.Instance.atAltar = true;
            GameController.Instance.UpdateUI();
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Item"))
        {
            itemInReach = null;
        }

        else if (collision.gameObject.CompareTag("Altar"))
        {
            GameController.Instance.atAltar = false;
            GameController.Instance.UpdateUI();
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
        yield return new WaitForSeconds(GameController.Instance.playerInvincibilityTime);
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

        body.velocity = new Vector2(horizontal * GameController.Instance.playerSpeed, vertical * GameController.Instance.playerSpeed);

        RotateToMousePos();
    }

    public void RotateToMousePos()
    {
        Vector2 dirMousePos = mousePos - body.position;
    }
}

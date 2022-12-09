using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarcoPlayerController : MonoBehaviour
{
Rigidbody2D body;

    private float horizontal;
    private float vertical;
    private float moveLimiter = 0.7f;

    public float runSpeed = 20.0f;

    public float rotationSpeed = 0.5f;
    public bool useGamepadRotation = false;

    void Start()
    {
        body = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
 
        horizontal = Input.GetAxisRaw("Horizontal"); // -1 is left
        vertical = Input.GetAxisRaw("Vertical"); // -1 is down
        // var direction = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        // float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        // Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        // transform.rotation = Quaternion.Slerp(transform.rotation, rotation, runSpeed * Time.deltaTime);
        
        Vector3 direction;
        if(useGamepadRotation)
        {
            direction = new Vector3(Input.GetAxis("RightStickHorizontal"), -Input.GetAxis("RightStickVertical"), 0);
            Debug.Log(direction.x);
            Debug.Log(direction.y);
        }
        else
        {
            direction = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
            Debug.Log(direction.x);
            Debug.Log(direction.y);
        }

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, runSpeed * Time.deltaTime);
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
    }
}

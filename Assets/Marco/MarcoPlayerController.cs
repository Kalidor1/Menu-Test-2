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
        
        
        if(useGamepadRotation)
        {
            var direction = new Vector2(Input.GetAxis("RightStickHorizontal"), Input.GetAxis("RightStickVertical"));
            if(direction.magnitude > 0.5f){
                Vector3 curRotation = Vector3.left * direction.x + Vector3.up * direction.y;
                Quaternion playerRotation = Quaternion.LookRotation(curRotation, Vector3.forward);
                body.SetRotation(playerRotation);
            }
        }
        else
        {
            var direction = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90;
            Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, runSpeed * Time.deltaTime);
        }

        
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
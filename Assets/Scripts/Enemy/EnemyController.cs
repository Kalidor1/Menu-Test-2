using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public GameObject target;
    //The tag of the player
    public string playerTag = null;
    public float speed = 1f;

    private Rigidbody2D rb;


    // Start is called before the first frame update
    void Start()
    {
        if (playerTag != null)
        {
            target = GameObject.FindGameObjectWithTag(playerTag);
        }

        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        //add force towards the target
        if (target != null)
        {
            // cap the speed
            rb.AddForce((target.transform.position - transform.position).normalized * speed);

            if (rb.velocity.magnitude > speed)
            {
                rb.velocity = rb.velocity.normalized * speed;
            }
        }
    }
}

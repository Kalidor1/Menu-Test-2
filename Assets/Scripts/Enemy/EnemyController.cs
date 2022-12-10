using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public GameObject target;
    //The tag of the player
    public string playerTag = null;
    public float speed = 1f;

    public float health = 10f;

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
        if(health <= 0){
            Destroy(gameObject);
        }
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

    void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.tag == "Weapon")
        {
            var damage = other.gameObject.GetComponent<PitchforkController>().isExtracting ? 10f : 2f;
            health -= damage;
            Debug.Log(damage);
        }
    }
}

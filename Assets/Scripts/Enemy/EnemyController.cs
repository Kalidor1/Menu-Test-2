using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class EnemyController : MonoBehaviour
{
    public float health = 10f;
    private Rigidbody2D rb;


    // Update is called once per frame
    void Update()
    {
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Trigger");

        if (other.gameObject.tag == "Weapon")
        {
            var damage = other.gameObject.GetComponent<PitchforkController>().isExtracting ? 10f : 2f;
            health -= damage;
            Debug.Log(damage);
        }
    }
}

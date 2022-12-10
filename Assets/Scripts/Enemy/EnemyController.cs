using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class EnemyController : MonoBehaviour
{
    public float health = 10f;
    private Rigidbody2D rb;
    public float cropCoolDown = 0.5f;
    private float _canAttackCrops = -1f;
    public ParticleSystem deathParticles;

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
    private void OnCollisionStay2D(Collision2D other)
    {
        if (other.gameObject.tag == "Crops" && Time.time > _canAttackCrops)
        {
            _canAttackCrops = Time.time + cropCoolDown;
            other.gameObject.GetComponent<CropController>().health -= 5f;
        }
    }

    // play death particles and destroy the enemy
    private void OnDestroy()
    {
        var particles = Instantiate(deathParticles, transform.position, Quaternion.identity);
        particles.Play();
    }
}

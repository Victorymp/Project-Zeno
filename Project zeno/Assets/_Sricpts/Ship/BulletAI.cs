using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletAI : MonoBehaviour
{
    public float damage = 10f;
    // public GameObject hit;
    // public GameObject fireParticle;
    // Start is called before the first frame update
    void Start()
    {
        // Debug.Log("Bullet fired");
        // Instantiate(fireParticle, transform.position, transform.rotation);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            // Debug.Log("Hit player");
            collision.gameObject.GetComponent<BattleShip>().TakeDamage(damage);
            // Instantiate(hit, transform.position, transform.rotation);
            Destroy(gameObject);
        }
        // Instantiate(hit, transform.position, transform.rotation);
    }

}

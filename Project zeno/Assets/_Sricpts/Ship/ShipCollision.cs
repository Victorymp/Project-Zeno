using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipCollision : MonoBehaviour
{
    public float health = 10f;
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Bullet")
        {
            Debug.Log("Hit by bullet");
            health -= 10f;
            Destroy(collision.gameObject);
            Debug.Log("Health: " + health);
            if (health <= 0)
            {
                Destroy(gameObject);
            }
        }
    }
}

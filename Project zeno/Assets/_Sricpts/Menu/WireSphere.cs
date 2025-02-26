using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WireSphere : MonoBehaviour
{
    public float radius = 1;
    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}

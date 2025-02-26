using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ShipBounacy : MonoBehaviour
{
    /// <summary>
    /// The rigidbody component of the ship.
    /// </summary>
    public new Rigidbody rigidbody;
    public float depth = 1.0f;
    public float diplacement = 1.0f;

    public int numberFloaters = 1;

    public float drag = 0.99f;

    public float angularDrag = 0.5f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    void FixedUpdate()
    {
        rigidbody.AddForceAtPosition(Physics.gravity/numberFloaters, transform.position,ForceMode.Acceleration);
        float waveHeight = WaterLogic.instance.GetWaveHeight(transform.position.x);
        if (transform.position.y < waveHeight)
        {
            float displacemtMultiplier = Mathf.Clamp01((waveHeight - transform.position.y) / depth) * diplacement;
            rigidbody.AddForceAtPosition(new Vector3 (0f, Mathf.Abs(Physics.gravity.y) * displacemtMultiplier, 0f), transform.position, ForceMode.Acceleration);
            rigidbody.AddForce(displacemtMultiplier * -rigidbody.velocity * drag * Time.fixedDeltaTime, ForceMode.VelocityChange);
            rigidbody.AddTorque(displacemtMultiplier * -rigidbody.angularVelocity * angularDrag * Time.fixedDeltaTime, ForceMode.VelocityChange);
        }
    }
}

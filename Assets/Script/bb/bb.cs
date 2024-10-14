using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BouncingBackspin : MonoBehaviour
{
    public float BackspinDrag = 0.001f;
    public float BounceForceMultiplier = 0.5f; // Adjust this value to control the bounce force
    public float DestroyAfterSeconds = 30f; // Time in seconds before destroying the object

    private Rigidbody rb;
    private float elapsedTime = 0f;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        // Calculate the force of sustentation with Backspin
        float velocityXZ = new Vector3(rb.velocity.x, 0f, rb.velocity.z).magnitude;
        Vector3 backspinForce = Vector3.up * Mathf.Sqrt(velocityXZ) * BackspinDrag * Time.deltaTime;
        rb.AddForce(backspinForce);

        // Increase elapsed time
        elapsedTime += Time.deltaTime;

        // Check if it's time to destroy the object
        if (elapsedTime >= DestroyAfterSeconds)
        {
            Destroy(gameObject); // Destroy the object
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Calculate the bounce force and apply it with reduced intensity
        Vector3 incomingVelocity = rb.velocity;
        Vector3 normal = collision.contacts[0].normal;
        Vector3 bounceForce = Vector3.Reflect(incomingVelocity, normal) * BounceForceMultiplier;
        rb.velocity = bounceForce;
    }
}

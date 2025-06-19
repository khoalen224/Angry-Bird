using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AngryBird : MonoBehaviour
{
    private Rigidbody2D rigidBody;
    private CircleCollider2D circleCollider;
    private bool hasLaunched;
    private bool shouldFaceVelDirection; 
    private void Awake()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        circleCollider = GetComponent<CircleCollider2D>();
    }
    public void Start()
    {
        rigidBody.bodyType = RigidbodyType2D.Kinematic;
        circleCollider.enabled = false;
    }
    private void FixedUpdate()
    {
        if (hasLaunched && shouldFaceVelDirection)
        { 
            transform.right = rigidBody.linearVelocity;
        }
    }
    public void LaunchBird(Vector2 direction, float force)
    {
        rigidBody.bodyType = RigidbodyType2D.Dynamic;
        circleCollider.enabled = true;

        //apply force
        rigidBody.AddForce(direction * force, ForceMode2D.Impulse);

        hasLaunched = true;
        shouldFaceVelDirection = true;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        shouldFaceVelDirection = false;
    }
}

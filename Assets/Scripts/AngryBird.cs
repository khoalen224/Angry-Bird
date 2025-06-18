using UnityEngine;

public class AngryBird : MonoBehaviour
{
      private Rigidbody2D rigidBody;
      private CircleCollider2D circleCollider;
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
    public void LaunchBird(Vector2 direction, float force)
    {
        rigidBody.bodyType = RigidbodyType2D.Dynamic;
        circleCollider.enabled = true;

        //apply force
        rigidBody.AddForce(direction * force, ForceMode2D.Impulse);
    }
}

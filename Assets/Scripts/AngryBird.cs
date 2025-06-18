using UnityEngine;

public class AngryBird : MonoBehaviour
{
      private Rigidbody2D _rb;
      private CircleCollider2D _circleCollider;
    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _circleCollider = GetComponent<CircleCollider2D>();
    }
    public void Start()
    {
        _rb.bodyType = RigidbodyType2D.Kinematic;
        _circleCollider.enabled = false;
    }
    public void LaunchBird(Vector2 direction, float force)
    {
        _rb.bodyType = RigidbodyType2D.Dynamic;
        _circleCollider.enabled = true;

        //apply force
        _rb.AddForce(direction * force, ForceMode2D.Impulse);
    }
}

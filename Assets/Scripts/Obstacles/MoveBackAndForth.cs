using UnityEngine;

public class MoveBackAndForth : MonoBehaviour
{
    [SerializeField]
    private float speedMove = 5f;
    [SerializeField]
    private bool isRight;
    [SerializeField]
    public float home;
    [SerializeField]
    public float target;
    [SerializeField]
    private LayerMask playerLayer;

    private Rigidbody2D rb;
    private int direction;


    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    void Start()
    {
        direction = isRight ? 1 : -1;
    }

    void Update()
    {
        if (rb.position.x >= target)
        {
            direction = -1;
        }
        else if (rb.position.x <= home)
        {
            direction = 1;
        }

        float newX = rb.position.x + (speedMove * direction * Time.deltaTime);
        rb.MovePosition(new Vector2(newX, rb.position.y));
    }
    private void OnCollisionEnter2D(Collision2D col)
    {
        if (((1 << col.gameObject.layer) & playerLayer) != 0)
        {
            Rigidbody2D ballRb = col.gameObject.GetComponent<Rigidbody2D>();
            if (ballRb != null && col.contacts != null && col.contacts.Length > 0)
            {
                Vector2 pushDirection = col.contacts[0].point - (Vector2)transform.position;
                ballRb.AddForce(pushDirection.normalized * 500f);
            }
        }
    }
}

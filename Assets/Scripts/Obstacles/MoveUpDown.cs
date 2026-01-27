using UnityEngine;

public class MoveUpDown : MonoBehaviour
{
    [SerializeField]
    private float speedMove = 5f;
    [SerializeField]
    private bool isUp;
    [SerializeField]
    private float distance = 4.0f;
    [SerializeField]
    private LayerMask playerLayer;

    private Rigidbody2D rb;


    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }


    private float startY;
    private int direction;

    void Start()
    {
        startY = rb.position.y;
        direction = isUp ? 1 : -1;
    }

    void Update()
    {
        float newY = rb.position.y + (speedMove * direction * Time.deltaTime);

        if (newY >= startY + distance)
        {
            newY = startY + distance;
            direction = -1;
        }
        else if (newY <= startY - distance)
        {
            newY = startY - distance;
            direction = 1;
        }

        rb.MovePosition(new Vector2(rb.position.x, newY));
    }
    private void OnCollisionEnter2D(Collision2D col)
    {
        if (((1 << col.gameObject.layer) & playerLayer) != 0)
        {
            Rigidbody2D ballRb = col.gameObject.GetComponent<Rigidbody2D>();
            if (ballRb != null)
            {
                Vector2 pushDirection = col.contacts[0].point - (Vector2)transform.position;

                ballRb.AddForce(pushDirection.normalized * 500f);
            }
        }
    }
}

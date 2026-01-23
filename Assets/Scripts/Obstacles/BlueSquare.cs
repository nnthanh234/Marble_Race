using UnityEngine;

public class BlueSquare : MonoBehaviour
{
    [SerializeField]
    private float speedMove = 5f;
    [SerializeField]
    private LayerMask playerLayer;

    private Rigidbody2D rb;


    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public float distance = 4.0f;   

    private float startX;
    private int direction;

    void Start()
    {
        startX = rb.position.x;
        direction = -1; 
    }

    void Update()
    {
        float newX = rb.position.x + (speedMove * direction * Time.deltaTime);

        if (newX >= startX + distance)
        {
            newX = startX + distance;
            direction = -1; 
        }
        else if (newX <= startX - distance)
        {
            newX = startX - distance;
            direction = 1; 
        }

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

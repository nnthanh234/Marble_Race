using UnityEngine;

public class MoveUp : MonoBehaviour
{
    [SerializeField]
    private float speedMove = 5f;
    [SerializeField]
    private float distance = 4.0f;
    [SerializeField]
    private bool isRight;

    private Rigidbody2D rb;
    private float startY;
    private int direction;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    void Start()
    {
        startY = rb.position.y;
        direction = isRight ? -1 : 1;
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
}

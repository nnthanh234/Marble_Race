using UnityEngine;

public class DoubleUnknow : MonoBehaviour
{
    [SerializeField]
    private float speedMove = 5f;
    [SerializeField]
    private bool isRight;

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
        direction = isRight ? 1 : -1;
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
}

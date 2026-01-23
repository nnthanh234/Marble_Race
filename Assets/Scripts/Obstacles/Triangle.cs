using UnityEngine;
using UnityEngine.U2D;

public class Triangle : MonoBehaviour
{
    [SerializeField]
    private SpriteShapeRenderer spr;
    [SerializeField]
    private EdgeCollider2D edgeCol;
    [SerializeField]
    private float speedMove = 5f;
    [SerializeField]
    private bool isMoveUp;
    [SerializeField]
    private bool isMoveRight;
    [SerializeField]
    private bool isMoveLeft;

    private Rigidbody2D rb;


    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        Vector2 dir = isMoveRight ? Vector2.right : isMoveLeft ? Vector2.left : Vector2.up;
        rb.linearVelocity = dir * speedMove;
    }

    private void Update()
    {
        if (isMoveUp)
        {
            if (transform.localPosition.y >= 2f)
            {
                if (spr != null) spr.enabled = false;
                if (edgeCol != null) edgeCol.enabled = false;

                transform.localPosition = new Vector2(transform.localPosition.x, -5f);

                if (spr != null) spr.enabled = true;
                if (edgeCol != null) edgeCol.enabled = true;
            }
        }
        else if (isMoveRight)
        {
            if (transform.localPosition.x >= 4f)
            {
                if (spr != null) spr.enabled = false;
                if (edgeCol != null) edgeCol.enabled = false;

                transform.localPosition = new Vector2(-11.36f, transform.localPosition.y);

                if (spr != null) spr.enabled = true;
                if (edgeCol != null) edgeCol.enabled = true;
            }
        }
        else if (isMoveLeft)
        {
            if (transform.localPosition.x <= -4.3f)
            {
                if (spr != null) spr.enabled = false;
                if (edgeCol != null) edgeCol.enabled = false;

                transform.localPosition = new Vector2(10.862f, transform.localPosition.y);

                if (spr != null) spr.enabled = true;
                if (edgeCol != null) edgeCol.enabled = true;
            }
        }
    }
}

using UnityEngine;

public class BigLongStick : MonoBehaviour
{
    [SerializeField]
    private float maxAngle = 25f;
    [SerializeField]
    private float period = 2f;
    [SerializeField]
    private float pauseDuration = 2f;
    [SerializeField]
    private LayerMask playerLayer;
    [SerializeField]
    private bool isRight;

    private Rigidbody2D rig;

    private float currentAngle;
    private int direction = 1; 
    private bool isPaused;
    private float pauseTimer;

    private void Awake()
    {
        rig = GetComponent<Rigidbody2D>();

        currentAngle = transform.eulerAngles.z;
        if (currentAngle > maxAngle) currentAngle = maxAngle;

        isPaused = false;
        pauseTimer = 0f;
    }

    private void FixedUpdate()
    {
        if (period <= 0f)
        {
            return;
        }

        float halfPeriod = period * 0.5f;
        float speed = maxAngle / halfPeriod; 

        if (isPaused)
        {
            pauseTimer -= Time.fixedDeltaTime;
            rig.MoveRotation(currentAngle);

            if (pauseTimer <= 0f)
            {
                isPaused = false;
                direction = -1;
            }

            return;
        }

        currentAngle += direction * speed * Time.fixedDeltaTime;

        if (isRight ? currentAngle >= maxAngle : currentAngle <= maxAngle)
        {
            currentAngle = maxAngle;
            isPaused = true;
            pauseTimer = pauseDuration;
        }
        else if (isRight && currentAngle <= 0f)
        {
            currentAngle = 0f;
            direction = 1;
        }
        else if (!isRight && currentAngle >= -150f)
        {
            currentAngle = -150f;
            direction = 1;
        }

        rig.MoveRotation(currentAngle);
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

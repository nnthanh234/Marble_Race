using UnityEngine;

public class LongStick : MonoBehaviour
{
    [SerializeField]
    private float rotB = 340f;
    [SerializeField]
    private float rotA = 20f;
    [SerializeField]
    public float rotationSpeed = 100f;
    [SerializeField]
    private LayerMask playerLayer;


    private Rigidbody2D rig;
    private float targetAngle;

    private void Awake()
    {
        rig = GetComponent<Rigidbody2D>();
        targetAngle = rotA;
    }

    private void FixedUpdate()
    {
        float currentAngle = rig.rotation;

        float nextAngle = Mathf.MoveTowardsAngle(currentAngle, targetAngle, rotationSpeed * Time.fixedDeltaTime);

        rig.MoveRotation(nextAngle);

        if (Mathf.Abs(Mathf.DeltaAngle(nextAngle, targetAngle)) < 0.1f)
        {
            targetAngle = (targetAngle == rotA) ? rotB : rotA;
        }
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

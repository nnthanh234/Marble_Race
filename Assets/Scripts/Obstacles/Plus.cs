using System.Collections;
using UnityEngine;

public class Plus : MonoBehaviour
{
    [SerializeField]
    public float speed = 100f;
    [SerializeField]
    private LayerMask playerLayer;

    private Rigidbody2D rb;
    private bool isWaiting = false;
    private int direction = 1;
    private float totalRotation = 0f;
    private float previousRotation = 0f;


    private void Start()
    {
        if (rb == null) rb = GetComponent<Rigidbody2D>();
        previousRotation = rb.rotation;
    }

    private void FixedUpdate() 
    {
        if (isWaiting)
        {
            rb.angularVelocity = 0f;
            return;
        }

        float deltaRotation = rb.rotation - previousRotation;

        if (deltaRotation > 180) deltaRotation -= 360;
        else if (deltaRotation < -180) deltaRotation += 360;

        totalRotation += Mathf.Abs(deltaRotation);

        rb.angularVelocity = direction * speed;

        if (totalRotation >= 360f)
        {
            StartCoroutine(ResetAndReverse());
            totalRotation = 0f; 
        }
        previousRotation = rb.rotation;
    }

    private IEnumerator ResetAndReverse()
    {
        isWaiting = true;
        rb.angularVelocity = 0f; 

        yield return new WaitForSeconds(1f); 

        direction *= -1; 
        isWaiting = false;
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

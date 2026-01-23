using UnityEngine;

public class ObstaclesRotate : MonoBehaviour
{
    [SerializeField]
    private float rotationSpeed = 300f;
    [SerializeField]
    private LayerMask playerLayer;

    private Rigidbody2D rig;


    private void Awake()
    {
        rig = GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        rig.angularVelocity = rotationSpeed;
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

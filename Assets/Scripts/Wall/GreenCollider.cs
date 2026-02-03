using UnityEngine;

public class GreenCollider : MonoBehaviour
{
    [SerializeField]
    private LayerMask playerLayer;

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (((1 << col.gameObject.layer) & playerLayer) != 0)
        {
            BallInfo ball = col.gameObject.GetComponent<BallInfo>();
            if (ball != null)
            {
                ball.DislayCountry();
            }

            col.gameObject.SetActive(false);

            GameManager.Instance.CheckResult();
        }
    }
}


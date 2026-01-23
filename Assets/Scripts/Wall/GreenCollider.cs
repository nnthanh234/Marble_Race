using UnityEngine;

public class GreenCollider : MonoBehaviour
{
    [SerializeField]
    private LayerMask playerLayer;

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (((1 << col.gameObject.layer) & playerLayer) != 0)
        {
            col.gameObject.SetActive(false);

            GameManager.Instance.CheckResult();
        }
    }
}


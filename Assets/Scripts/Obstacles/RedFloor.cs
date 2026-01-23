using UnityEngine;

public class RedFloor : MonoBehaviour
{
    [SerializeField]
    private LayerMask playerLayer;


    private void OnCollisionEnter2D(Collision2D col)
    {
        if (((1 << col.gameObject.layer) & playerLayer) != 0)
        {
            if (col.gameObject.TryGetComponent(out SpriteRenderer render))
            {
                UIResult.Instance.ShowResult(render);
            }
        }
    }
}

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
                col.gameObject.GetComponent<TrailRenderer>().enabled = false;

                UIResult.Instance.ShowResult(render);

                BallSpawner.Instance.RemoveFlag(render);

            }
        }
    }
}

using UnityEngine;
using UnityEngine.U2D;

public class Hexagonal : MonoBehaviour
{
    [SerializeField]
    private SpriteShapeRenderer spr;
    [SerializeField]
    private int amout = 5;
    [SerializeField]
    private LayerMask playerLayer;


    private void OnCollisionEnter2D(Collision2D col)
    {
        if (((1 << col.gameObject.layer) & playerLayer) != 0)
        {
            amout--;
            if (amout > 0)
            {
                spr.color = new Color(spr.color.r, spr.color.g, spr.color.b, spr.color.a - 0.2f);
            }
            else
            {
                gameObject.SetActive(false);
            }
        }
    }
}

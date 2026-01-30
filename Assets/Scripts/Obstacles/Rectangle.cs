using UnityEngine;

public class Rectangle : MonoBehaviour
{
    [SerializeField]
    private SpriteRenderer spr;
    [SerializeField]
    private int amout = 3;
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

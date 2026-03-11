using System.Collections.Generic;
using UnityEngine;

public class GreenCollider : MonoBehaviour
{
    [SerializeField]
    private LayerMask playerLayer;

    public static Stack<SpriteRenderer> finishLs;

    private void Awake()
    {
        finishLs = new Stack<SpriteRenderer>();
    }
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (((1 << col.gameObject.layer) & playerLayer) != 0)
        {
            BallInfo ball = col.gameObject.GetComponent<BallInfo>();
            if (ball != null)
            {
                ball.DislayCountry();
            }

            finishLs.Push(col.gameObject.GetComponent<SpriteRenderer>());

            col.gameObject.SetActive(false);

            GameManager.Instance.CheckResult();
        }
    }
}


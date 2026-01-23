using DG.Tweening;
using UnityEngine;

public class Trapezoid : MonoBehaviour
{
    [SerializeField]
    private float speed = 1f;
    [SerializeField]
    private float PosA = 1f;
    [SerializeField]
    private float PosB = 1f;

    Rigidbody2D rig;


    private void Awake()
    {
        rig = GetComponent<Rigidbody2D>();
    }
    void Start()
    {
        float distanceToA = Mathf.Abs(PosA - transform.localPosition.y);
        float durationToA = distanceToA / speed;

        float distanceAtoB = Mathf.Abs(PosB - PosA);
        float durationAtoB = distanceAtoB / speed;

        rig.DOMove(new Vector2(transform.localPosition.x, PosA), durationToA).OnComplete(() =>
        {
            rig.DOMove(new Vector2(transform.localPosition.x, PosB), durationAtoB).SetLoops(-1, LoopType.Yoyo);
        });
    }

}

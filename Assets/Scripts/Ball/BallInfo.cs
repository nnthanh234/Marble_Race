using TMPro;
using UnityEngine;

public class BallInfo : MonoBehaviour
{
    [SerializeField]
    private SpriteRenderer sprRender;
    [SerializeField]
    private SpriteMask mask;
    [SerializeField]
    private DisplayCountry canvas;

    public string NameBall { get; private set; }


    public void Init(Sprite spr)
    {
        sprRender.sprite = spr;
        NameBall = RemoveSuffix(spr.name);

        sprRender.sortingLayerName = NameBall;
        mask.frontSortingLayerID = SortingLayer.NameToID(NameBall);

        gameObject.GetComponent<TrailRenderer>().enabled = transform;
    }
    public void DislayCountry()
    {
        Vector3 pos = transform.position + Vector3.up;
        DisplayCountry cv = Instantiate(canvas, pos, Quaternion.identity);
        cv.RunText(NameBall);
    }
    public static string RemoveSuffix(string name) { const string suffix = "_0"; return name.EndsWith(suffix) ? name.Substring(0, name.Length - suffix.Length) : name; }
}

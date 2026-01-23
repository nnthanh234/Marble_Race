using UnityEngine;
using static UnityEditor.U2D.ScriptablePacker;

public class BallInfo : MonoBehaviour
{
    [SerializeField]
    private SpriteRenderer sprRender;
    [SerializeField]
    private SpriteMask mask;

    public string NameBall { get; private set; }


    public void Init(Sprite spr)
    {
        sprRender.sprite = spr;
        NameBall = RemoveSuffix(spr.name);

        sprRender.sortingLayerName = NameBall;
        mask.frontSortingLayerID = SortingLayer.NameToID(NameBall);
    }
    public static string RemoveSuffix(string name) { const string suffix = "_0"; return name.EndsWith(suffix) ? name.Substring(0, name.Length - suffix.Length) : name; }
}

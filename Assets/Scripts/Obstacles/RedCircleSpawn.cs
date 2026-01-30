using UnityEngine;

public class RedCircleSpawn : MonoBehaviour
{
    public static RedCircleSpawn Instance;
    private void Awake()
    {
        Instance = this;
    }
    public void Spawn(GameObject ball)
    {
        ball.GetComponent<TrailRenderer>().Clear();
        ball.transform.position = transform.localPosition;
        ball.SetActive(true);
        ball.GetComponent<Rigidbody2D>().AddForceY(Random.Range(5f, 15f), ForceMode2D.Impulse);
    }
}

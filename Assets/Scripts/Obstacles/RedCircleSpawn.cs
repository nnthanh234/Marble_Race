using UnityEngine;

public class RedCircleSpawn : Singleton<RedCircleSpawn>
{
    public void Spawn(GameObject ball)
    {
        ball.transform.position = transform.localPosition;
        ball.SetActive(true);
        ball.GetComponent<Rigidbody2D>().AddForceY(Random.Range(5f, 15f), ForceMode2D.Impulse);
    }
}

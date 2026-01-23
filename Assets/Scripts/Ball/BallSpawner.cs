using UnityEngine;
using System.Collections.Generic;

public class BallSpawner : Singleton<BallSpawner>
{
    [SerializeField]
    private BallInfo ballPrefab;
    [SerializeField]
    private Transform[] lsPos;
    [SerializeField]
    private List<Sprite> lsFlag;
    [SerializeField]
    private int ballCount = 50;

    private List<Sprite> flags;
    private bool isFirstStartGame = true;

    private void Start()
    {
        flags = new List<Sprite>(lsFlag);
    }
    public void SpawnAllBalls()
    {
        flags.Clear();
        flags.AddRange(lsFlag);

        for (int i = 0; i < ballCount; i++)
        {
            BallInfo ball = null;

            if (isFirstStartGame)
            {
                ball = Instantiate(ballPrefab, lsPos[i].position, Quaternion.identity);
                GameManager.Instance.AddBall(ball);
            }
            else
            {
                ball = GetFromPool();
                if (ball == null)
                {
                    ball = Instantiate(ballPrefab, lsPos[i].position, Quaternion.identity);
                    GameManager.Instance.AddBall(ball);
                }
                else
                {
                    ball.gameObject.SetActive(true);
                    ball.transform.position = lsPos[i].position;
                    ball.transform.rotation = Quaternion.identity;
                }
            }

            int randomIndex = Random.Range(0, flags.Count);
            Sprite spr = flags[randomIndex];

            ball.Init(spr);

            flags.RemoveAt(randomIndex);
        }

        isFirstStartGame = false;
    }
    private BallInfo GetFromPool()
    {
        for (int j = 0; j < GameManager.Instance.BallPool.Count; j++)
        {
            if (!GameManager.Instance.BallPool[j].gameObject.activeInHierarchy)
            {
                return GameManager.Instance.BallPool[j];
            }
        }
        return null;
    }
    public void BackToPool()
    {
        for (int i = 0; i < GameManager.Instance.BallPool.Count; i++)
        {
            GameManager.Instance.BallPool[i].gameObject.SetActive(false);
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    [SerializeField]
    private GameObject middleWall;
    [SerializeField]
    private GameObject redWall;
    [SerializeField]
    private GameObject obstacle;
    [SerializeField]
    private int round;
    [SerializeField]
    private bool isTest;
    [SerializeField]
    private int mapIndexTest = 0;

    public int CurrentMap { get; private set; }
    public List<BallInfo> BallPool { get; private set; }
    public int MapIndex => round - CurrentMap + 1;

    private List<int> lsMapIndex;


    private void Awake()
    {
        BallPool = new List<BallInfo>();

        lsMapIndex = new List<int>();
        for (int i = 1; i <= round; i++)
        {
            lsMapIndex.Add(i);
        }

        lsMapIndex = lsMapIndex.OrderBy(x => Random.value).ToList();
    }
    public async void StartNewMap()
    {
        redWall.SetActive(false);
        middleWall.SetActive(true);

        BallSpawner.Instance.SpawnAllBalls();

        if (isTest)
        {
            LoadMap.Instance.StartLoad($"Map {mapIndexTest}");
            mapIndexTest++;
        }
        else
            LoadMap.Instance.StartLoad($"Map {lsMapIndex[CurrentMap]}");

        CurrentMap++;

        obstacle.SetActive(true);
        await Task.Delay(3000);
        middleWall.SetActive(false);
    }
    public void AddBall(BallInfo ballInfo)
    {
        BallPool.Add(ballInfo);
    }
    public void CheckResult()
    {
        int activeCount = 0;
        GameObject ball = null;
        for (int i = 0; i < BallPool.Count; i++)
        {
            if (BallPool[i].gameObject.activeInHierarchy)
            {
                activeCount++;
                ball = BallPool[i].gameObject;
                if (activeCount > 1)
                    return;
            }
        }

        redWall.SetActive(true);
        obstacle.SetActive(false);

        if (CurrentMap >= round - 1)
        {
            UIResult.Instance.ShowWinner(ball.GetComponent<SpriteRenderer>());
        }
    }
}

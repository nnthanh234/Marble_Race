using System.Collections.Generic;
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
    private int mapIndexTest;

    public int CurrentMap { get; private set; }

    public List<BallInfo> BallPool { get; private set; }
    public int MapIndex => round - CurrentMap + 1;


    private void Awake()
    {
        BallPool = new List<BallInfo>();
    }
    public async void StartNewMap()
    {
        redWall.SetActive(false);
        middleWall.SetActive(true);

        CurrentMap++;

        BallSpawner.Instance.SpawnAllBalls();

        if (isTest)
        {
            LoadMap.Instance.StartLoad($"Map {mapIndexTest}");
            mapIndexTest++;
        }
        else
            LoadMap.Instance.StartLoad($"Map {CurrentMap}");

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
        for (int i = 0; i < BallPool.Count; i++)
        {
            if (BallPool[i].gameObject.activeInHierarchy)
            {
                activeCount++;
                if (activeCount > 1)
                    return;
            }
        }

        redWall.SetActive(true);
        obstacle.SetActive(false);
    }
}

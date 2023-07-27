using UnityEngine;
using UnityEngine.Pool;

public class GameManager : MonoBehaviour
{
    public GameObject ballPrefabs;
    public static IObjectPool<Ball> ballPool;

    //현재 스테이지 체크
    public GameObject[] stages;
    //현재 스테이지의 상호작용 숫자
    public int[] stageInInteraction;

    private void Awake()
    {
        ballPool = new ObjectPool<Ball>(CreateBall,OnGetBall,OnReleaseBall,OnDestroyBall,maxSize:2000);
    }

    private void Start()
    {
        stageInInteraction = new int[stages.Length];
        int stageNum = 0;
        foreach (var stage in stages)
        {
            int count = 0;
            int numChildren = stage.transform.childCount;

            for (int i = 0; i < numChildren; i++)
            {
                var child = stage.transform.GetChild(i);
                if (child.CompareTag("InteractionBlock"))
                {
                    child.GetComponent<InteractionBlock>().count = count;
                    count++;
                }
            }
            stageInInteraction[stageNum] = count;
            stageNum++;
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            var ball = ballPool.Get();
            ball.transform.position = Vector3.zero;
        }
    }

    private Ball CreateBall()
    {
        Ball ball = Instantiate(ballPrefabs).GetComponent<Ball>();
        ball.SetBalls(ballPool);
        return ball; 
    }

    private void OnGetBall(Ball ball)   
    {
        ball.gameObject.SetActive(true);
    }

    private void OnReleaseBall(Ball ball)
    {
        ball.gameObject.SetActive(false);
    }

    private void OnDestroyBall(Ball ball)
    {
        Destroy(ball.gameObject);
    }
}

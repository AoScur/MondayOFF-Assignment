using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class GameManager : MonoBehaviour
{
    public GameObject ballPrefabs;
    private IObjectPool<Ball> pool;

    private void Awake()
    {
        pool = new ObjectPool<Ball>(CreateBall,OnGetBall,OnReleaseBall,OnDestroyBall,maxSize:2000);
    }

    private void Update()
    {
        
    }

    private Ball CreateBall()
    {
        Ball ball = Instantiate(ballPrefabs).GetComponent<Ball>();
        ball.SetBalls(pool);
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

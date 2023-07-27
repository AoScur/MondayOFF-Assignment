using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class Ball : MonoBehaviour
{
    public Color ballColor;

    private IObjectPool<Ball> balls;


    public void SetBalls(IObjectPool<Ball> pool)
    {
        balls = pool;
    }

    public void DestroyBall()
    {
        balls.Release(this);
    }
}

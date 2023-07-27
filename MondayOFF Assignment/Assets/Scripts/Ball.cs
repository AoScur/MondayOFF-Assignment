using UnityEngine;
using UnityEngine.Pool;

public class Ball : MonoBehaviour
{
    public Color ballColor;

    private IObjectPool<Ball> balls;

    private int interractCount;

    public void SetBalls(IObjectPool<Ball> pool)
    {
        balls = pool;
    }

    public void DestroyBall()
    {
        balls.Release(this);
    }

    private void OnTriggerEnter(Collider collider)
    {
        var col = collider.GetComponent<InteractionBlock>();
        if (collider.CompareTag("InteractionBlock") && interractCount== col.count && col.multiple!=0)
        {
            interractCount++;
            for (int i = 1; i < col.multiple; i++)
            {
                var ball = GameManager.ballPool.Get();
                if (ball != null)
                {
                    Vector3 collisionPoint = transform.position;
                    float range = 2.0f;

                    Vector3 randomOffset = new Vector3(Random.Range(-range, range), 0f, 0f);
                    ball.transform.position = collisionPoint + randomOffset;
                    ball.interractCount = interractCount;
                }
            }
        }
        else if(collider.CompareTag("InteractionBlock") && interractCount == col.count && col.multiple == 0)
        {
            DestroyBall();
        }
    }
}

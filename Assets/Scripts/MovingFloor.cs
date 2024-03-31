using System.Collections;
using UnityEngine;

public class MovingFloor : MonoBehaviour
{
    private Vector3 OriginVector3;

    public bool yMove;

    public float delta;

    // Move speed
    public float speed;

    IEnumerator Start()
    {
        OriginVector3 = transform.position;

        while (true)
        {
            Vector3 velocity = OriginVector3;

            if (yMove)
            {
                velocity.y += delta * Mathf.Cos(Time.time * speed);
            }
            else
            {
                velocity.x += delta * Mathf.Sin(Time.time * speed);
            }

            transform.position = velocity;

            yield return null;
        }
    }
}

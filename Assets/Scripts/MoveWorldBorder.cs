using UnityEngine;

public class MoveWorldBorder : MonoBehaviour
{
    public Transform target;
    public bool isSky;

    void Update()
    {
        if (isSky)
        {
            transform.position = new Vector3(target.position.x, 20, target.position.z);
        }
        else
        {
            transform.position = new Vector3(target.position.x, -9, target.position.z);
        }
    }
}

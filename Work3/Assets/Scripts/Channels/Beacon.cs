using UnityEngine;

public class Beacon : MonoBehaviour
{
    private static Beacon Instance { get; }
    public BallWallCollision i_BallWallCollision;

    public static Beacon GetInstance()
    {
        return Instance;
    }

}

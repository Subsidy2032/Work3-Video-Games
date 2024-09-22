using System;
using UnityEngine;

[CreateAssetMenu(fileName = "BallWallCollisionChannel", menuName = "Channels/BallWallCollision", order = 1)]

public class BallHoleCollisionChannel : ScriptableObject
{
    public event Action<GameObject> CollisionDetected;

    public void InvokeCollisionDetected(GameObject hole)
    {
        CollisionDetected?.Invoke(hole);
    }
}
using System;
using UnityEngine;

[CreateAssetMenu(fileName = "BallWallCollisionChannel", menuName = "Channels/BallWallCollision", order = 1)]

public class BallWallCollision : ScriptableObject
{
    public event Action CollisionDetected;

    public void InvokeCollisionDetected()
    {
        CollisionDetected?.Invoke();
    }


}

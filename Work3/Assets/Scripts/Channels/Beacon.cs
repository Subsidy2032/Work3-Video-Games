using UnityEngine;

public class Beacon : MonoBehaviour
{
    private static Beacon instance;
    [SerializeField] public BallHoleCollisionChannel ballHoleCollisionChannel;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);  // Ensure there is only one Beacon instance
        }
    }

    public static Beacon GetInstance()
    {
        return instance;
    }
}
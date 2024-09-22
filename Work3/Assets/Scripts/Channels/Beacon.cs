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
            Destroy(gameObject);
        }
    }

    public static Beacon GetInstance()
    {
        return instance;
    }
}
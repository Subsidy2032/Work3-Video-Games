using UnityEngine;

[CreateAssetMenu(fileName = "New Ball", menuName = "Spawnables/Ball", order = 1)]
public class SO_Ball : ScriptableObject
{
    [SerializeField] public Sprite ballSprite;
    [SerializeField] public int score;

}

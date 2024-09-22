using UnityEngine;

[CreateAssetMenu(fileName = "New Booster", menuName = "Spawnables/Booster", order = 1)]
public class SO_Booster : ScriptableObject
{
    [SerializeField] Sprite boosterSprite;
    [SerializeField] string boostType;
}
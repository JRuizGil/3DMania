using UnityEngine;

[CreateAssetMenu(fileName = "ClickerEventData", menuName = "ScriptableObjects/ClickerEventData", order = 1)]
public class ClickerEventData : ScriptableObject
{
    public float timerDuration = 5f;
    public float cooldownTime = 5f;
    public float materialMultiplier = 10f;
    public float price = 0f;
    public float actualearn = 0f;
    public float door = 0;
    public float lvl = 0;
    public float initialrevenue = 0;
    public GameObject AutoMatePrefab;

    
}

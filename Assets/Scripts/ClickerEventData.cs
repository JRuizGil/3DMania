using UnityEngine;

[CreateAssetMenu(fileName = "ClickerEventData", menuName = "ScriptableObjects/ClickerEventData", order = 1)]
public class ClickerEventData : ScriptableObject
{
    public float timerDuration = 5f;
    public float cooldownTime = 5f;
    public int materialMultiplier = 10;
}

using System.Diagnostics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class UpdatetxtUpgrade : MonoBehaviour
{
    public ClickerEventData eventData;
    private Text text;
    [SerializeField]public float lvl;
    [SerializeField]public float actualearn;
    [SerializeField]public float door;
    [SerializeField]public float multiplier;
    [SerializeField]public float initialrevenue;
    [SerializeField]public float Cooldown;

    private void Start()
    {
        initialrevenue = eventData.initialrevenue;
        Cooldown = eventData.cooldownTime;
        text = GetComponent<Text>();
        lvl = eventData.lvl;
        actualearn = eventData.actualearn;
        door = eventData.door;
        multiplier = eventData.materialMultiplier;
        if (eventData != null && text != null)
        {
            // Actualiza el texto con los valores actuales
            text.text = $"Door:{door}| Cooldown:{Cooldown}s \n ActualEarn:{actualearn:F2}| LVL:{lvl}";
        }

    }
    private void FixedUpdate()
    {
        
    }
    public void SumCPSandLVL()
    {        
        actualearn = initialrevenue * Mathf.Log(lvl + 1);
        lvl++;
        text.text = $"Door:{door}| Cooldown:{Cooldown}s \n ActualEarn:{actualearn:F2}| LVL:{lvl}";
    }
}

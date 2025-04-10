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
    [SerializeField]public float initialrevenue;
    [SerializeField]public float Cooldown;

    private void Start()
    {
        initialrevenue = eventData.initialrevenue;
        text = GetComponent<Text>();
        lvl = eventData.lvl + 1;
        actualearn = eventData.actualearn;
        door = eventData.door;
        if (eventData != null && text != null)
        {
            // Actualiza el texto con los valores actuales
            text.text = $"Door:{door}|   Cooldown:{eventData.cooldownTime}s \nActualEarn:{actualearn:F2}|   LVL:{lvl}";
        }

    }
    public void SumCPSandLVL()
    {        
        actualearn = initialrevenue * Mathf.Log(lvl + 1);
        lvl++;
        text.text = $"Door:{door}|   Cooldown:{eventData.cooldownTime}s \nActualEarn:{actualearn:F2}|   LVL:{lvl}";
    }
}

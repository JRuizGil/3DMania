using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;

public class UpdatetxtUpgrade : MonoBehaviour
{
    public ClickerEventData eventData;
    private Text text;
    [SerializeField]public float lvl;
    [SerializeField]public float cps;
    [SerializeField]public float door;

    private void Start()
    {
        text = GetComponent<Text>();
        lvl = eventData.lvl;
        cps = eventData.cps;
        door = eventData.door;
        if (eventData != null && text != null)
        {
            // Actualiza el texto con los valores actuales
            text.text = $"Door:{door}CPS:{cps}LVL:{lvl}";
        }
    }
    private void FixedUpdate()
    {
        
    }
    public void SumCPSandLVL()
    {
        lvl++;
        cps = (cps+1)*1.1f;
        text.text = $"Door:{door}CPS:{cps:F2}LVL:{lvl}";
    }
}

using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeButtonManager : MonoBehaviour
{
    public Inventory inventory;
    public ClickerEventData ClickerEventData;
    public ClickerEvent ClickerEventMain;
    public Button button;
    public Text btntext;
    public Text txt;
    public Text FloorTxt;
    public GameObject nextCamPos;
    public Button uparrow;
    public Button downarrow;

    [SerializeField] public float lvl;
    [SerializeField] public double actualearn;
    [SerializeField] public float door;
    [SerializeField] public double initialrevenue;
    [SerializeField] private double price;
    [SerializeField] private float actualmultiplier;
    [SerializeField] public float timerduration;

    private void Start()
    {
        timerduration = ClickerEventData.timerDuration;
        lvl = 0;        
        door = ClickerEventData.door;
        initialrevenue = ClickerEventData.initialrevenue;
        button.enabled = true;
        actualearn = ClickerEventData.initialrevenue;

        Debug.Log("puerta " + ClickerEventData.door + "cuesta" + price + "ofrece" + actualearn);

        txt.text = $"DOOR LEVEL:{lvl} \nEarn:{FormatPrice(actualearn)}   Cooldown:{timerduration:F2}s";
        FloorTxt.text = $"{ClickerEventData.door}º";
        price = ClickerEventData.price;
        btntext.text = $"{FormatPrice(price)} Bu";

    }    
    public void BuyUpgrade()
    {
        if (inventory != null)
        {
            if (inventory.Mat1 > price) // Verifica si tiene suficiente dinero
            {
                inventory.Mat1 -= price; // Resta el precio de la mejora
                lvl++;
                SumEarnings();
                MultiplyPrice(); // Aumenta el precio para la siguiente compra                

            }
            else
            {
                Debug.LogWarning("No tienes suficiente dinero para comprar la mejora.");
            }
        }
    }    
    public void MultiplyPrice()
    {
        price = ClickerEventData.price * Mathf.Pow(ClickerEventData.materialMultiplier, lvl);
        timerduration = Mathf.Max(ClickerEventData.MinBaseTimerProduction, ClickerEventData.timerDuration / (1f + Mathf.Log(lvl + 1f)));
        float timerScale = 1f + Mathf.Log(lvl + 1f); 
        int scaledClicks = Mathf.RoundToInt(ClickerEventData.neededClicksToMultiply / timerScale);
        ClickerEventMain.MultiSlider.maxValue = Mathf.Max(3, scaledClicks); 


        if (btntext != null)
        {
            btntext.text = $"{FormatPrice(price)} Bu";
        }
    }
    public void SumEarnings()
    {
        ChangeMultiplyEarn();
        actualearn = (initialrevenue * lvl) * actualmultiplier;
        txt.text = $"DOOR LEVEL:{lvl} \nEarn:{FormatPrice(actualearn)}  Cooldown:{timerduration:F2}s";
    }
    public void ChangeMultiplyEarn()
    {
        if (lvl >= 200)
        {
            actualmultiplier = 64;
        }
        if (lvl >= 150)
        {
            actualmultiplier = 32;
        }
        if (lvl >= 100)
        {
            actualmultiplier = 16;
        }
        else if (lvl >= 50)
        {
            actualmultiplier = 8;
        }
        else if (lvl >= 25)
        {
            actualmultiplier = 4;
        }
        else if (lvl >= 10)
        {
            actualmultiplier = 2;
        }
        else
        {
            actualmultiplier = 1;
        }
    }
    string FormatPrice(double value)
    {
        string[] suffixes = { "", " K", " M", " B", " T", " Qa", " Qi", " Sx", " Sp", " Oc", " No", " Dc", " Ud", " Dd", " Td", " Qad", " Qid", " Sxd", " Spd", " Ocd", " Nod" };

        int suffixIndex = 0;

        while (value >= 1000 && suffixIndex < suffixes.Length - 1)
        {
            value /= 1000;
            suffixIndex++;
        }

        return value.ToString("F2") + suffixes[suffixIndex];
    }
    public void ActivateFloorsButtons()
    {
        if (lvl == 1)
        {
            nextCamPos.SetActive(true);
            uparrow.interactable = true;
        }
    }
    public void ResetAll()
    {
        lvl = 0;
        actualmultiplier = 1;
        door = ClickerEventData.door;
        initialrevenue = ClickerEventData.initialrevenue;
        actualearn = initialrevenue;
        timerduration = 1f;
        price = ClickerEventData.price;

        // Reinicia la UI
        txt.text = $"DOOR LEVEL:{lvl} \nEarn:{FormatPrice(actualearn)}   Cooldown:{timerduration:F2}s";
        FloorTxt.text = $"{door}º";
        btntext.text = $"{FormatPrice(price)} Bu";

        // Reactiva el botón de mejora
        button.interactable = true;

        // Opcional: desactiva elementos visuales si fue activado en otro punto
        nextCamPos.SetActive(false);
        uparrow.interactable = false;
        downarrow.interactable = false;

        Debug.Log("Se ha hecho reset de todas las variables.");
    }

}

using UnityEngine;
using UnityEngine.UI;

public class UpdateBtnUpgrade : MonoBehaviour
{
    public ClickerEventData ClickerEventData;
    public UpdatetxtUpgrade UpdatetxtUpgrade;
    private Text text;
    public GameObject Puerta;
    private float price;
    private float pricemultiplier;
    private float lvl;

    private void Start()
    {
        pricemultiplier = ClickerEventData.materialMultiplier;
        text = GetComponent<Text>();
        if (ClickerEventData != null && text != null)
        {
            price = ClickerEventData.price;
            text.text = $"Buy:{price:F2}€";
        }
    }

    private void Update()
    {
        lvl = UpdatetxtUpgrade.lvl;
        
    }

    public void MultiplyPrice()
    {
        price *= Mathf.Pow(pricemultiplier, lvl);
        if (text != null)
        {
            text.text = $"Buy:{price:F2}€"; 
        }
    }
    public float GetPrice()
    {
        return price;
    }

}


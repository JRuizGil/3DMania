using UnityEngine;
using UnityEngine.UI;

public class UpgradeButtonManager : MonoBehaviour
{
    public Inventory inventory;
    public ClickerEventData ClickerEventData;
    public Button button;
    public Text btntext;
    public Text txt;

    [SerializeField] public float lvl;
    [SerializeField] public float actualearn;
    [SerializeField] public float door;
    [SerializeField] public float initialrevenue;
    [SerializeField] public float Cooldown;
    [SerializeField] private float price;
    [SerializeField] private float actualmultiplier;

    private void Start()
    {
        lvl = 0;
        if (ClickerEventData.door == 1)
        {
            lvl++;
            actualmultiplier = 1;
            initialrevenue = ClickerEventData.initialrevenue;
            SumEarnings();
            MultiplyPrice();
        }
        door = ClickerEventData.door;
        initialrevenue = ClickerEventData.initialrevenue;
        button.enabled = true;
        actualearn = ClickerEventData.initialrevenue;
        
        Debug.Log("puerta "+ ClickerEventData.door + "cuesta" + price + "ofrece"+ actualearn);
        
        txt.text = $"Door:{ClickerEventData.door}  |   Cooldown:{ClickerEventData.timerDuration}s \nActualEarn:{actualearn}  |   LVL:{lvl}";            
        
        price = ClickerEventData.price;
        btntext.text = $"Buy:{price:F1}Bu";
        
    }
    private void Update()
    {
        Buyablebtn();
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
    private void Buyablebtn()
    {        
        if (inventory.Mat1 > price)
        {
            button.interactable = true;
        }
    }
    public void MultiplyPrice()
    {
        price = ClickerEventData.price * Mathf.Pow(ClickerEventData.materialMultiplier, lvl);
        if (btntext != null)
        {
            btntext.text = $"Buy:{price:F1}Bu";
        }
    }
    public void SumEarnings()
    { 
        ChangeMultiplyEarn();
        actualearn = (initialrevenue * lvl) * actualmultiplier;
        txt.text = $"Door:{door}|   Cooldown:{ClickerEventData.timerDuration}s \nActualEarn:{actualearn}|   LVL:{lvl}";        
    }
    public void ChangeMultiplyEarn()
    {
        if(lvl >= 200)
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

}

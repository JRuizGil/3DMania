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

    private void Start()
    {
        if (ClickerEventData != null && btntext != null)
        {
            // Actualiza el texto con los valores actuales
            txt.text = $"Door:{door}|   Cooldown:{ClickerEventData.cooldownTime}s \nActualEarn:{actualearn:F2}|   LVL:{lvl}";
        }
        lvl = 0;
        btntext.text = $"Buy:{price:F2}€";
        price = ClickerEventData.price;
        SumCPSandLVL();
        button.interactable = false;
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
                SumCPSandLVL();
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
        //if (inventory.Mat1 > price)
        //{
        //    button.interactable = true;
        //}
        //else
        //{
        //    button.interactable = false;    
        //}
    }
    public void MultiplyPrice()
    {
        price *= Mathf.Pow(ClickerEventData.materialMultiplier, lvl);
        if (btntext != null)
        {
            btntext.text = $"Buy:{price:F2}€";
        }
    }
    public void SumCPSandLVL()
    {
        actualearn = initialrevenue * Mathf.Log(lvl + 1);
        txt.text = $"Door:{door}|   Cooldown:{ClickerEventData.cooldownTime}s \nActualEarn:{actualearn:F2}|   LVL:{lvl}";
        lvl++;
    }
}

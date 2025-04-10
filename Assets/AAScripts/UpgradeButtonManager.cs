using UnityEngine;
using UnityEngine.UI;

public class UpgradeButtonManager : MonoBehaviour
{
    private Inventory inventory;
    public UpdateBtnUpgrade updateBtnUpgrade;
    public UpdatetxtUpgrade UpdatetxtUpgrade;
    private float actualMoney;
    private float price;
    private Button button;

    private void Start()
    {
        inventory = GetComponent<Inventory>();
        price = updateBtnUpgrade.GetPrice();
        button = GetComponent<Button>();
    }
    private void Update()
    {
        Buyablebtn();
    }
    public void BuyUpgrade(Inventory inventory)
    {
        this.inventory = inventory;

        if (updateBtnUpgrade != null && inventory != null)
        {
            if (inventory.Mat1 > price) // Verifica si tiene suficiente dinero
            {
                inventory.Mat1 -= price; // Resta el precio de la mejora
                updateBtnUpgrade.MultiplyPrice(); // Aumenta el precio para la siguiente compra
                UpdatetxtUpgrade.SumCPSandLVL();
                price = updateBtnUpgrade.GetPrice();
            }
            else
            {
                Debug.LogWarning("No tienes suficiente dinero para comprar la mejora.");
            }
        }
        else
        {
            Debug.LogWarning("No se encontró UpdateBtnUpgrade o Inventory en la escena.");
        }
    }
    private void Buyablebtn()
    {
        //actualMoney = inventory.Mat1;
        //if (actualMoney > price)
        //{
        //    button.interactable = true;
        //}
        //else
        //{
        //    button.interactable = false;    
        //}
    }
}

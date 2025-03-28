using UnityEngine;

public class UpgradeButtonManager : MonoBehaviour
{
    private Inventory inventory;
    public UpdateBtnUpgrade updateBtnUpgrade;
    public UpdatetxtUpgrade uptxt;

    private void Start()
    {
        inventory = GetComponent<Inventory>();
    }

    public void BuyUpgrade(Inventory inventory)
    {
        this.inventory = inventory;

        if (updateBtnUpgrade != null && inventory != null)
        {
            float price = updateBtnUpgrade.GetPrice(); // Obtener el precio actual de la mejora

            if (inventory.Mat1 > price) // Verifica si tiene suficiente dinero
            {
                inventory.Mat1 -= price; // Resta el precio de la mejora
                updateBtnUpgrade.MultiplyPrice(); // Aumenta el precio para la siguiente compra
                uptxt.SumCPSandLVL();
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
}

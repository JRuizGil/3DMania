using UnityEngine;
using UnityEngine.UI;

public class ActivateAuto : MonoBehaviour
{
    public GameObject AutomaterPrefab;
    public ClickerEventData ClickerEventData;
    private float automaterprice;
    private Text text;
    public Inventory inventory;

    void Start()
    {
        text = GetComponentInChildren<Text>();

        if (text != null)
        {
            text.text = $"Price: {ClickerEventData.AutomaterPrice}€";
        }
        else
        {
            Debug.LogWarning("No se encontró un componente Text en los hijos de este objeto.");
        }
    }    
    public void BuyAutomater()
    {
         // Asegurarse de obtener el valor actualizado

        if (inventory.Mat1 >= ClickerEventData.AutomaterPrice) // Verifica si el jugador tiene suficiente dinero
        {
            inventory.Mat1 -= ClickerEventData.AutomaterPrice; // Resta el dinero del inventario
            AutomaterPrefab.SetActive(true);  // Activa el automatizador
            Destroy(gameObject); // Destruye el objeto después de la compra
        }
        else
        {
            Debug.Log("No tienes suficiente dinero para comprar el automatizador.");
        }
    }
}


using UnityEngine;
using UnityEngine.UI;

public class ActivateAuto : MonoBehaviour
{
    public GameObject AutomaterPrefab;
    public ClickerEventData ClickerEventData;
    private float automaterprice;
    private Text text;
    public Inventory inventory;
    public float actualmoney;

    void Start()
    {
        automaterprice = ClickerEventData.AutomaterPrice;

        text = GetComponentInChildren<Text>();

        if (text != null)
        {
            text.text = $"Price: {automaterprice}€";
        }
        else
        {
            Debug.LogWarning("No se encontró un componente Text en los hijos de este objeto.");
        }
    }
    private void Update()
    {
        actualmoney = inventory.Mat1;
    }
    public void BuyAutomater()
    {
         // Asegurarse de obtener el valor actualizado

        if (actualmoney >= automaterprice) // Verifica si el jugador tiene suficiente dinero
        {
            inventory.Mat1 -= automaterprice; // Resta el dinero del inventario
            AutomaterPrefab.SetActive(true);  // Activa el automatizador
            Destroy(gameObject); // Destruye el objeto después de la compra
        }
        else
        {
            Debug.Log("No tienes suficiente dinero para comprar el automatizador.");
        }
    }
}


using UnityEngine;
using UnityEngine.UI;

public class ActivateAuto : MonoBehaviour
{
    public GameObject AutomaterPrefab;
    public ClickerEventData ClickerEventData;
    private Text text;
    public Inventory inventory;
    private double Autoprice;
    private void Awake()
    {
        Autoprice = ClickerEventData.AutomaterPrice;
        text = GetComponentInChildren<Text>();

        if (text != null)
        {
            text.text = $"{ClickerEventData.door}\n{FormatNumber(Autoprice)} Bu";
        }
        else
        {
            Debug.LogWarning("No se encontró un componente Text en los hijos de este objeto.");
        }
    }
    private void OnDisable()
    {
        Autoprice *= 1.1f;
        text.text = $"{ClickerEventData.door}\n{FormatNumber(Autoprice)} Bu";
    }
    public void BuyAutomater()
    {
        // Asegurarse de obtener el valor actualizado
        if (inventory.Mat1 >= ClickerEventData.AutomaterPrice) // Verifica si el jugador tiene suficiente dinero
        {
            AutomaterPrefab.SetActive(true);
            Debug.Log("Comprado");
            inventory.Mat1 -= ClickerEventData.AutomaterPrice; // Resta el dinero del inventario                                                       
            gameObject.SetActive(false); // Destruye el objeto después de la compra
        }
        else
        {
            Debug.Log("No tienes suficiente dinero para comprar el automatizador.");
        }
    }

    string FormatNumber(double value)
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
}

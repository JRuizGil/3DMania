using UnityEngine;
using UnityEngine.UI;

public class ActivateAuto : MonoBehaviour
{
    public GameObject AutomaterPrefab;
    public ClickerEventData ClickerEventData;
    private Text text;
    public Inventory inventory;
    private void Awake()
    {
        text = GetComponentInChildren<Text>();

        if (text != null)
        {
            text.text = $"{ClickerEventData.door}\n{FormatNumber(ClickerEventData.AutomaterPrice)} Bu";
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

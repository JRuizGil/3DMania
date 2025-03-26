using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public float Mat1;

    public Text Display;

    private void Start()
    {
        Mat1 = 0;
    }

    void Update()
    {
        // Actualizar la UI para mostrar los materiales actuales
        Display.text = $"Dinero:{Mat1}€";
    }

    public void AddMaterials(int totalMaterials)
    {        
        Mat1 += totalMaterials * 1f; // 50% materiales tipo 1

        Debug.Log($"Dinero añadido:{Mat1}€");
    }
}

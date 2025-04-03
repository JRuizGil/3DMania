using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public float Mat1;
    public Text Display;

    private bool wasNegative = false; // Para rastrear si Mat1 ya ha sido negativo

    private void Start()
    {
        Mat1 = 0;
    }

    void Update()
    {
        // Actualizar la UI para mostrar los materiales actuales con formato adaptativo
        Display.text = $"Dinero: {Mat1:F2}€";
    }
    public void AddMaterials(float totalMaterials)
    {
        Mat1 += totalMaterials;
        Debug.Log($"Dinero: {Mat1:F2}€");
    }
    public float GetMat1()
    {
        return Mat1;
    }
}


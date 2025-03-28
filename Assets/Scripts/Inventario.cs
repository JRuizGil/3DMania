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
        // Actualizar la UI para mostrar los materiales actuales con formato adaptativo
        Display.text = $"Dinero:{(Mat1 >= 1_000_000 ? Mat1.ToString("0.##E+0") : Mat1.ToString("F2"))}€";
    }

    public void AddMaterials(int totalMaterials)
    {
        Mat1 += totalMaterials;

        Debug.Log($"Dinero añadido: {(Mat1 >= 1_000_000 ? Mat1.ToString("0.##E+0") : Mat1.ToString("F2"))}€");
    }

}

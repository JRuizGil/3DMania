using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public float Mat1;
    public float Mat2;
    public float Mat3;

    public Text Display;

    private void Start()
    {
        Mat1 = 0;
        Mat2 = 0;
        Mat3 = 0;
    }

    void Update()
    {
        // Actualizar la UI para mostrar los materiales actuales
        Display.text = $"M1:{Mat1}/ M2:{Mat2}/ M3:{Mat3}";
    }

    public void AddMaterials(int totalMaterials)
    {
        // Dividir el total de materiales entre los 3 tipos de materiales
        // Esto lo puedes ajustar dependiendo de la proporción que quieras

        Mat1 += totalMaterials * 0.5f; // 50% materiales tipo 1
        Mat2 += totalMaterials * 0.3f; // 30% materiales tipo 2
        Mat3 += totalMaterials * 0.2f; // 20% materiales tipo 3

        Debug.Log($"Materiales añadidos -> Mat1: {Mat1}, Mat2: {Mat2}, Mat3: {Mat3}");
    }
}

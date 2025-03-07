using UnityEngine;

public class Inventory : MonoBehaviour
{
    public float Mat1;
    public float Mat2;
    public float Mat3;

    public void AddMaterials(float mat1, float mat2, float mat3)
    {
        Mat1 += mat1;
        Mat2 += mat2;
        Mat3 += mat3;

        Debug.Log($"Inventario actualizado - Mat1: {Mat1}, Mat2: {Mat2}, Mat3: {Mat3}");
    }
}


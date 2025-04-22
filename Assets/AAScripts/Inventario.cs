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
        Display.text = $"Dinero: {FormatNumber(Mat1)}Bu";
    }

    string FormatNumber(float number)
    {
        if (number >= 10000000f)
            return number.ToString("E2");
        else
            return number.ToString("F2");
    }


    public void AddMaterials(float totalMaterials)
    {
        Mat1 += totalMaterials;
        Debug.Log($"{Mat1:F2}Bu");
    }
    public float GetMat1()
    {
        return Mat1;
    }
}


using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public double Mat1; // <-- cambiado a double
    public Text Display;

    private void Start()
    {
        Mat1 = 0;
    }

    void Update()
    {
        Display.text = $"Funds: {FormatNumber(Mat1)}  Bu";
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

    public void AddMaterials(double totalMaterials)
    {
        Mat1 += totalMaterials;
        Debug.Log($"{Mat1:F2}_Bu");
    }

    public double GetMat1()
    {
        return Mat1;
    }
}

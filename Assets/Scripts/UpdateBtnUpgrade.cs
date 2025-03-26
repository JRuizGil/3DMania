using UnityEngine;
using UnityEngine.UI;

public class UpdateBtnUpgrade : MonoBehaviour
{
    public ClickerEventData eventData;
    private Text text;
    private float price;

    private void Start()
    {
        text = GetComponent<Text>();
        if (eventData != null && text != null)
        {
            price = eventData.price;
            text.text = $"{price}€";
        }
    }

    private void FixedUpdate()
    {
        
    }

    public void MultiplyPrice()
    {
        price *= 1.1f;
        if (text != null)
        {
            text.text = $"{price:F2}€"; // Formatea a 2 decimales
        }
    }
    public float GetPrice()
    {
        return price;
    }

}


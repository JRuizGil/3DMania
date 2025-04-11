using UnityEngine;
using UnityEngine.UI;

public class BossManager : MonoBehaviour
{
    public ClickerEventData ClickerEventData;

    public Inventory inventory;

    public ButtonManager buttonManager;

    public GameObject BossScene;

    private float BossPrice;
    private float mat1AtBossStart;


    private bool BossIsOn= false;

    public Text BossRemainingText;


    public Slider BossMoneySlider;

    private void Start()
    {
        
    }
    private void Update()
    {
        if (BossIsOn)
        {
            StartBoss();
        }
    }
    public void BuyBossStart()
    {        
        if (inventory.Mat1 >= ClickerEventData.BossEnterPrice)
        {
            BossMoneySlider.gameObject.SetActive(true);
            inventory.Mat1 -= ClickerEventData.BossEnterPrice;
            mat1AtBossStart = inventory.Mat1; 
            buttonManager.CloseAllMenus();
            BossIsOn = true;
        }        
    }

    public void StartBoss()
    {
        float generatedSinceStart = inventory.Mat1 - mat1AtBossStart;

        if (generatedSinceStart > 0)
        {
            float amountToSpend = Mathf.Min(generatedSinceStart, 1000f);

            inventory.Mat1 -= amountToSpend;
            mat1AtBossStart += amountToSpend;
            BossMoneySlider.value += amountToSpend;
        }

        // Mostrar cuánto falta
        float remaining = BossMoneySlider.maxValue - BossMoneySlider.value;
        BossRemainingText.text = $"Faltan {remaining:0}€";

        if (BossMoneySlider.value >= BossMoneySlider.maxValue)
        {
            EndBoss();
        }
    }

    public void EndBoss()
    {
        inventory.Mat1 += ClickerEventData.BossEnterPrice * ClickerEventData.BossEarnMultiplier;
        buttonManager.OpenBossMenu();
        BossMoneySlider.value = 0;
        BossScene.SetActive(false);
        Debug.Log("¡Boss terminado!");
        BossIsOn = false;
    }
}

using UnityEngine;
using UnityEngine.UI;

public class BossManager : MonoBehaviour
{
    public ClickerEventData ClickerEventData;

    public Inventory inventory;

    public ButtonManager buttonManager;

    public GameObject BossScene;

    private float mat1AtBossStart;

    private bool BossIsOn = false;

    public Text BossRemainingText;

    public Slider BossMoneySlider;

    private float bossTimer; // Nuevo: temporizador

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
            bossTimer = 0f; // Resetear el tiempo al iniciar
            BossScene.SetActive(true); // Mostrar la escena del boss si no estaba activa
        }
    }

    public void StartBoss()
    {
        bossTimer += Time.deltaTime; // Aumentar el temporizador cada frame

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

        // Si completó el boss
        if (BossMoneySlider.value >= BossMoneySlider.maxValue)
        {
            EndBoss(success: true);
        }
        // Si se acaba el tiempo y no completó el boss
        else if (bossTimer >= ClickerEventData.BossCountdownTime)
        {
            EndBoss(success: false);
        }
    }

    public void EndBoss(bool success)
    {
        if (success)
        {
            inventory.Mat1 += ClickerEventData.BossEnterPrice * ClickerEventData.BossEarnMultiplier;
            Debug.Log("¡Boss terminado exitosamente!");
        }
        else
        {
            inventory.Mat1 += ClickerEventData.BossEnterPrice / 2f;
            Debug.Log("Boss fallido, se devuelve la mitad.");
        }

        buttonManager.OpenBossMenu();
        BossMoneySlider.value = 0;
        BossScene.SetActive(false);
        BossIsOn = false;
    }
}

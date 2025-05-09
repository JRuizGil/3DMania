using UnityEngine;
using UnityEngine.UI;

public class BossManager : MonoBehaviour
{
    public ClickerEventData ClickerEventData;

    public Inventory inventory;

    public ButtonManager buttonManager;

    public GameObject BossScene;

    private double mat1AtBossStart;

    private bool BossIsOn;

    public Text BossRemainingText;

    public Slider BossMoneySlider;

    private float bossTimer; // Nuevo: temporizador

}

using UnityEngine.UI;
using UnityEngine;
using System.Collections;

public class ClickerEvent : MonoBehaviour
{
    private bool gameActive = false;
    private float timer;
    private int clickCount = 0;
    private float cooldownTimer = 0f;

    public GameObject PanelTxt;
    public Text timerText;
    public Text cooldownText;

    public ClickerEventData eventData;
    private Inventory inventory;
    public UpgradeButtonManager upgradeButtonManager;

    private Camera prefabCamera;
    private Camera mainCamera;
    private bool wasCameraInactive = false;
    private Animator animator;
    private Transform Fantasma;
    private Vector3 escalaOriginal;
    private void Start()
    {
        prefabCamera = GetComponentInChildren<Camera>(true);
        mainCamera = Camera.main;
        inventory = Object.FindFirstObjectByType<Inventory>();
        PanelTxt?.SetActive(false);
        Fantasma = transform.Find("Fantasma");

        // Guardar la escala original de Fantasma
        if (Fantasma != null)
        {
            escalaOriginal = Fantasma.localScale;
        }

        // Buscar el Animator en el hijo llamado "Estruct"
        Transform child = transform.Find("Estruct");
        if (child != null)
        {
            animator = child.GetComponent<Animator>();
        }
    }
    private void Update()
    {
        UpdateMain();
        
    }
    private void UpdateMain()
    {
        bool isPrefabCameraActive = prefabCamera != null && prefabCamera.gameObject.activeSelf;

        if (!isPrefabCameraActive)
        {
            if (gameActive) EndGame();
            PanelTxt?.SetActive(false);
            wasCameraInactive = true;
            return;
        }
        else if (wasCameraInactive)
        {
            PanelTxt?.SetActive(true);
            cooldownText?.gameObject.SetActive(true);
            timerText?.gameObject.SetActive(true);
            wasCameraInactive = false;
        }

        if (cooldownTimer > 0 && !gameActive)
        {
            cooldownTimer -= Time.deltaTime;
            UpdateCooldownText();
        }

        if (Input.GetMouseButtonDown(0) && cooldownTimer <= 0 && isPrefabCameraActive)
        {
            if (!gameActive) StartGame();
            clickCount++;

            if (Fantasma != null)
            {
                LeanTween.scale(Fantasma.gameObject, escalaOriginal * 0.8f, 0.05f) // Reducir tamaño relativo
                    .setEase(LeanTweenType.easeOutQuad)
                    .setOnComplete(() =>
                    {
                        LeanTween.scale(Fantasma.gameObject, escalaOriginal, 0.05f) // Volver a la escala original
                            .setEase(LeanTweenType.easeInQuad);
                    });
            }
        }

        if (gameActive)
        {
            timer -= Time.deltaTime;
            UpdateUIText(timerText, $"Tiempo restante: {timer:F1} s");
            if (timer <= 0) EndGame();
        }
    }
    private IEnumerator CooldownRoutine()
    {
        while (cooldownTimer > 0)
        {
            cooldownTimer -= Time.deltaTime;
            UpdateCooldownText();
            yield return null;
        }
        cooldownText.gameObject.SetActive(false);
    }
    private void StartGame()
    {
        animator.SetBool("Open", true);
        gameActive = true;
        timer = eventData.timerDuration;
        clickCount = 0;
        PanelTxt?.SetActive(true);
        UpdateUIText(timerText, $"Tiempo restante: {timer:F1} s");

    }
    private void EndGame()
    {
        animator.SetBool("Open", false);
        gameActive = false;
        if (timer <= 0f)
        {
            float totalMaterials = clickCount + upgradeButtonManager.actualearn;
            inventory.AddMaterials(totalMaterials);
        }
        else
        {
            Debug.Log("El juego terminó antes de que se agotara el tiempo. No se otorgan materiales.");
        }
        cooldownTimer = eventData.cooldownTime;
        StartCoroutine(CooldownRoutine());
    }
    private void UpdateUIText(Text uiText, string newText)
    {
        if (uiText != null && uiText.text != newText)
        {
            uiText.text = newText;
            uiText.gameObject.SetActive(true);
        }
    }

    public void UpdateCooldownText()
    {
        if (cooldownText != null)
        {
            cooldownText.text = $"Cooldown: {Mathf.Max(0, cooldownTimer):F1} s";
            cooldownText.gameObject.SetActive(true);
        }
    }
}
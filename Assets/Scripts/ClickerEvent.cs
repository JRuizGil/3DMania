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
    private Camera prefabCamera;
    private Camera mainCamera;
    private bool wasCameraInactive = false;
    private Animator animator;

    private void Start()
    {
        prefabCamera = GetComponentInChildren<Camera>(true);
        mainCamera = Camera.main;
        inventory = Object.FindFirstObjectByType<Inventory>();
        PanelTxt?.SetActive(false);

        // Buscar el Animator en el hijo llamado "Estruct"
        Transform child = transform.Find("Estruct");
        if (child != null)
        {
            animator = child.GetComponent<Animator>();
        }
    }
    private void Update()
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
        int totalMaterials = Mathf.RoundToInt((float)clickCount * eventData.materialMultiplier);
        inventory?.AddMaterials(totalMaterials);
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
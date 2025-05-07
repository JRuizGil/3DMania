using UnityEngine.UI;
using UnityEngine;
using System.Collections;

public class ClickerEvent : MonoBehaviour
{
    public bool gameActive = false;
    private float timer;
    private int clickCount = 0;
    private float cooldownTimer = 0f;
    private float multiplier = 1f;

    public GameObject PanelTxt;

    public Slider timerSlider;
    public Slider MultiSlider;

    public Text MultiText;

    public ClickerEventData eventData;
    private Inventory inventory;
    public UpgradeButtonManager upgradeButtonManager;

    public AudioSource audioSource;

    private Camera prefabCamera;
    private Camera mainCamera;

    private bool wasCameraInactive = false;

    private Animator animator;
    private Transform Fantasma;

    private Vector3 escalaOriginal;
    
    private void Start()
    {
        MultiText.text = $" X {multiplier}";
        MultiSlider.minValue = 0;
        MultiSlider.maxValue = eventData.neededClicksToMultiply;
        prefabCamera = GetComponentInChildren<Camera>(true);
        mainCamera = Camera.main;
        inventory = Object.FindFirstObjectByType<Inventory>();
        PanelTxt?.SetActive(true);
        Fantasma = transform.Find("Fantasma");
        audioSource = GetComponent<AudioSource>();
        // Guardar la escala original de Fantasma
        if (Fantasma != null)
        {
            escalaOriginal = Fantasma.localScale;
        }

        // Buscar el Animator en el hijo llamado "Estruct"
        Transform child = transform.Find("INV");
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
            wasCameraInactive = true;
            return;
        }
        else if (wasCameraInactive)
        {            
            wasCameraInactive = false;
        }
        if (cooldownTimer > 0 && !gameActive)
        {
            cooldownTimer -= Time.deltaTime;
        }
        if ((Input.GetMouseButtonDown(0) || (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)) && cooldownTimer <= 0 && isPrefabCameraActive)
        {
            if (!gameActive) StartGame();
            clickCount++;
            MultiSlider.value = clickCount;
            MultiText.text = $" X {multiplier}";
            if (clickCount >= eventData.neededClicksToMultiply)
            {
                multiplier *= 2;
                clickCount = 0;
            }
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
            timerSlider.value = timer;
            if (timer <= 0) EndGame();
        }
    }
    private IEnumerator CooldownRoutine()
    {
        while (cooldownTimer > 0)
        {
            cooldownTimer -= Time.deltaTime;
            yield return null;
        }
    }
    public void StartGame()
    {
        animator.SetBool("Open", true);
        audioSource.Play();
        gameActive = true;
        timer = eventData.timerDuration;
        timerSlider.maxValue = eventData.timerDuration;
        timerSlider.value = eventData.timerDuration;
        timerSlider.gameObject.SetActive(true);
        clickCount = 0;

    }
    public void EndGame()
    {
        animator.SetBool("Open", false);
        audioSource.Play();
        gameActive = false;
        multiplier = 1;
        if (timer <= 0f)
        {
            double totalMaterials = (multiplier * eventData.materialMultiplier) + upgradeButtonManager.actualearn;
            inventory.AddMaterials(totalMaterials);
        }
        else
        {
            Debug.Log("El juego terminó antes de que se agotara el tiempo. No se otorgan materiales.");
        }
        StartCoroutine(CooldownRoutine());
    }
}
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
    private double TotMat;

    public GameObject PanelTxt;

    public Slider timerSlider;
    public Slider MultiSlider;

    public Text MultiText;
    public Text BUtxt;

    public ClickerEventData eventData;
    private Inventory inventory;
    public UpgradeButtonManager upgradeButtonManager;

    public AudioSource audioSource;

    private Camera prefabCamera;

    private bool wasCameraInactive = false;

    private Animator animator;
    private Transform Fantasma;

    private Vector3 escalaOriginal;
    private Canvas canvas;
    private void Start()
    {
        canvas = GetComponentInChildren<Canvas>();
        MultiText.text = $"{FormatPrice(TotMat)} X <color=#FF0000>{multiplier}</color>";

        MultiSlider.minValue = 0;
        MultiSlider.maxValue = eventData.neededClicksToMultiply;
        prefabCamera = GetComponentInChildren<Camera>(true);
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
            SpawnText();
            MultiSlider.value = clickCount;
            MultiText.text = $"{FormatPrice(TotMat)} BU <color=#FF0000> X {multiplier}</color>";
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
            TotMat = totalMaterials;
        }
        else
        {
            Debug.Log("El juego terminó antes de que se agotara el tiempo. No se otorgan materiales.");
        }
        StartCoroutine(CooldownRoutine());
    }
    public void SpawnText()
    {
        StartCoroutine(SpawnTextRoutine());
    }
    private IEnumerator SpawnTextRoutine()
    {
        // Instancia el texto como hijo del canvas
        Text newText = Instantiate(BUtxt, canvas.transform);

        // Obtener tamaño del Canvas en mundo
        RectTransform canvasRect = canvas.GetComponent<RectTransform>();
        Vector2 size = canvasRect.sizeDelta;

        // Generar posición aleatoria dentro del área del Canvas
        float randomX = Random.Range(-size.x / 2f, size.x / 2f);
        float randomY = Random.Range(-size.y / 2f, size.y / 2f);

        Vector3 worldPosition = canvas.transform.TransformPoint(new Vector3(randomX, randomY, 0f));

        // Asignar posición mundial al RectTransform del texto
        RectTransform textRect = newText.GetComponent<RectTransform>();
        textRect.position = worldPosition;
        textRect.localScale = Vector3.one;

        newText.text = "BU";

        // Escalado progresivo hacia cero durante 1 segundo
        float duration = 1f;
        float elapsed = 0f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float t = elapsed / duration;
            float scale = Mathf.Lerp(1f, 0f, t);
            textRect.localScale = new Vector3(scale, scale, scale);
            yield return null;
        }

        Destroy(newText.gameObject);
    }
    public void ActivateDoor()
    {
        if (!gameObject.activeSelf && upgradeButtonManager.lvl == 1)
        {
            gameObject.SetActive(true);
        }
    }
    string FormatPrice(double value)
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
}
using UnityEngine;
using UnityEngine.UI;

public class BossClickerEvent : MonoBehaviour
{
    [Header("Movement")]
    public Transform awakePosTransform;
    public Transform firstBossPosTransform;
    public Transform secondBossPosTransform;
    public Transform thirdBossPosTransform;
    public Transform fourthBossPosTransform;
    public float moveSpeed = 1f;

    [Header("Clicker Event")]
    public Slider clickSlider;
    public Slider timeSlider;
    public float totalTime = 10f; // Tiempo límite en segundos
    public int clicksToFill = 10;

    private Vector3 startPos;
    private Vector3 targetPos;
    private Vector3 escalaOriginal;
    private float t;
    private bool isMoving = false;

    private bool isClickerActive = false;
    private float currentTime = 0f;
    private int currentClicks = 0;
    private int currentStage = 0;

    public Camera MainCamera;
    public AudioListener BosscamAudiolist;

    public GameObject GameHud;
    public GameObject BossScene;

    private Transform[] bossPositions;
    private void Start()
    {
        escalaOriginal = gameObject.transform.localScale;
    }
    private void Awake()
    {
        BosscamAudiolist.enabled = false;

        bossPositions = new Transform[] {
            firstBossPosTransform,
            secondBossPosTransform,
            thirdBossPosTransform,
            fourthBossPosTransform
            
        };
    }
    private void OnEnable()
    {
        currentStage = 0;
        MoveToStage(currentStage);

        clickSlider.maxValue = clicksToFill;
        clickSlider.value = 0;

        timeSlider.maxValue = totalTime;
        timeSlider.value = totalTime;

        currentTime = totalTime;
        currentClicks = 0;
        isClickerActive = true;

        BosscamAudiolist.enabled = true;
    }

    private void FixedUpdate()
    {
        if (!isMoving) return;

        t += Time.deltaTime * moveSpeed;
        float smoothT = Mathf.SmoothStep(0f, 1f, Mathf.Clamp01(t));
        transform.position = Vector3.Lerp(startPos, targetPos, smoothT);

        if (t >= 1f)
        {
            transform.position = targetPos;
            isMoving = false;
        }
    }

    private void Update()
    {
        if (!isClickerActive) return;

        currentTime -= Time.deltaTime;
        timeSlider.value = currentTime;

        if (Input.GetMouseButtonDown(0) || (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began))
        {
            currentClicks++;
            clickSlider.value = currentClicks;
            
            if (currentClicks >= clicksToFill)
            {
                LeanTween.scale(gameObject.gameObject, escalaOriginal * 0.8f, 0.5f) // Reducir tamaño relativo
                    .setEase(LeanTweenType.easeOutQuad)
                    .setOnComplete(() =>
                    {
                        LeanTween.scale(gameObject.gameObject, escalaOriginal, 0.5f) // Volver a la escala original
                            .setEase(LeanTweenType.easeInQuad);
                    });
                //disparo aqui
                Nextbosspos();
                currentClicks = 0;
                clickSlider.value = 0;
            }
        }
        if (currentTime <= 0f)
        {
            Debug.Log("Tiempo agotado.");
            ResetClickerEvent();
        }
    }
    private void Nextbosspos()
    {
        Debug.Log("¡Slider lleno! moviendo al boss.");

        // Avanzar a la siguiente posición si no es la última
        if (currentStage < bossPositions.Length - 1)
        {
            currentStage++;
            MoveToStage(currentStage);
        }
        else
        {
            Debug.Log("Has alcanzado la última posición.");
        }
    }
    private void MoveToStage(int stage)
    {
        startPos = transform.position;
        targetPos = bossPositions[stage].position;
        t = 0f;
        isMoving = true;
    }
    private void ResetClickerEvent()
    {        
        // Reinicia etapa
        currentStage = 0;
        MoveToStage(currentStage);

        // Reinicia sliders y contadores
        clickSlider.value = 0;
        timeSlider.value = totalTime;

        currentClicks = 0;
        currentTime = totalTime;

        isClickerActive = false;
        BossScene.gameObject.SetActive(false);
        MainCamera.gameObject.SetActive(true);
        GameHud.gameObject.SetActive(true);        
    }

}

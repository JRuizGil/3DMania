using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.VFX;
using UnityEngine.VFX.Utility;
using Unity.VisualScripting;

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
    public VisualEffect effect;
    public GameObject AtractionPos;
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
    private float currentClicks = 0f;
    private int currentStage = 0;

    public Camera MainCamera;
    public Camera BossCamera;
    public AudioListener BosscamAudiolist;
    public Inventory Inventory;
    public FloorManager FloorManager;
    public StartManager StartManager;
    public UpgradeButtonManager[] UpgradeButtonManager;
    public GameObject EndScene;

    public GameObject GameHud;
    public GameObject BossScene;

    private Transform[] bossPositions;

    public GameObject MainCamStartPos;
    private void Start()
    {
        escalaOriginal = gameObject.transform.localScale;
        effect.SetFloat("AtractionStrength", 1);
        effect.SetFloat("Rate", 100);
    }
    private void Awake()
    {
        gameObject.transform.position = awakePosTransform.position;

        effect.Play();
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
        currentClicks = 0f;
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
            
            effect.SetFloat("AtractionStrength", currentClicks);
            effect.SetFloat("Rate", currentClicks * 200f);


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
                TriggerAtractionBoost();
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
            EndingCinematic();           

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
        effect.SetFloat("AtractionStrength", 0);
        currentTime = totalTime;
        isClickerActive = false;
        MainCamera.gameObject.SetActive(true);
        GameHud.gameObject.SetActive(true);        
    }
    public void TriggerAtractionBoost()
    {     
        StartCoroutine(TemporaryAtractionChange());       
    }

    private IEnumerator TemporaryAtractionChange()
    {
        Vector3 originalPos = AtractionPos.transform.position;

        // Mover hacia la derecha
        Vector3 newPos = originalPos;
        newPos.x += 100f;
        AtractionPos.transform.position = newPos;

        // Cambiar valores del efecto
        effect.SetFloat("AtractionStrength", 1000f);
        effect.SetFloat("Radius", 0.1f);
        effect.SetFloat("Rate", 100000f);
        yield return new WaitForSeconds(0.5f);

        // Volver a la posición original
        AtractionPos.transform.position = originalPos;

        // Cambiar valores del efecto nuevamente
        effect.SetFloat("AtractionStrength", -1000f);
        effect.SetFloat("Radius", 0.5f);
        yield return new WaitForSeconds(1.5f);

        // Restaurar valores finales
        effect.SetFloat("AtractionStrength", 1f);
        effect.SetFloat("Rate", 100f);
    }
    private IEnumerator EndingCinematic()
    {
        Vector3 startPos = BossCamera.transform.position;
        Vector3 targetPos = MainCamStartPos.transform.position; //  acceso correcto

        float duration = 5f;
        float elapsed = 0f;

        while (elapsed < duration)
        {
            float t = elapsed / duration;
            BossCamera.transform.position = Vector3.Lerp(startPos, targetPos, t);
            elapsed += Time.deltaTime;
        }

        // Asegurarse que termine justo en la posición final
        BossCamera.transform.position = targetPos;

        // Luego de la cinemática
        BossScene.SetActive(false);
        ResetClickerEvent();
        Inventory.Mat1 = 4f;
        clickSlider.maxValue = +20;
        FloorManager.FloorDown();
        StartManager.StartUnable();
        StartManager.StartEnable();
        foreach (UpgradeButtonManager button in UpgradeButtonManager)
        {
            if (button != null)
                button.ResetAll();
        }
        yield break;
    }

}

using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class DoorInteractable : MonoBehaviour
{
    public Transform player;
    public Transform targetPosition;
    public float moveSpeed = 10f;
    private PlayerController controller;
    private bool isMoving = false;
    private bool isdooractive = false;

    [Header("Camera Settings")]
    private Camera mainCamera;
    public Camera prefabCamera;

    private ClickerEvent clickerEvent;

    public UpgradeButtonManager NextDoorButtonManager;
    public GameObject prfbnextDoor;

    private void Start()
    {
        controller = player.GetComponent<PlayerController>();
        mainCamera = Camera.main;

        prefabCamera = GetComponentInChildren<Camera>(true);
        if (prefabCamera != null)
            prefabCamera.gameObject.SetActive(false);

        clickerEvent = GetComponentInChildren<ClickerEvent>();
    }

    private void Update()
    {
        PlayerMove();
        HandleEscape();
        if(NextDoorButtonManager != null)
        {
            if (NextDoorButtonManager.lvl >= 1 && !isdooractive)
            {
                prfbnextDoor.SetActive(true);
                isdooractive = true;
            }
        }        
    }
    private bool IsCameraActive(Camera cam)
    {
        return cam != null && cam.gameObject.activeSelf;
    }

    public void OnInteractStart()
    {
        if (!IsCameraActive(mainCamera)) return;

        if (controller != null && Vector3.Distance(player.position, targetPosition.position) > 0.1f)
        {
            var childTransform = transform.GetChild(0);
            Vector3 originalScale = childTransform.localScale;

            LeanTween.scale(childTransform.gameObject, Vector3.one * 0.8f, 0.05f)
                .setEase(LeanTweenType.easeOutQuad)
                .setOnComplete(() => {
                    LeanTween.scale(childTransform.gameObject, originalScale, 0.05f);
                });

            CancelCurrentMovement();
            controller.StopNavMeshAgent();
            controller.currentInteractable = this;
            controller.isInteractingWithDoor = true; //  Bloquea el movimiento/interacción
            isMoving = true;
        }
    }

    private void CancelCurrentMovement()
    {
        if (isMoving)
        {
            isMoving = false;
            prefabCamera?.gameObject.SetActive(false);
            mainCamera?.gameObject.SetActive(true);
            controller.isInteractingWithDoor = false; //  Habilita nuevamente el movimiento/interacción
        }
    }

    private void PlayerMove()
    {
        if (isMoving)
        {
            float step = moveSpeed * Time.deltaTime;
            player.position = Vector3.Lerp(player.position, targetPosition.position, step);

            if (Vector3.Distance(player.position, targetPosition.position) < 0.4f)
            {
                isMoving = false;
                mainCamera?.gameObject.SetActive(false);
                prefabCamera?.gameObject.SetActive(true);
                clickerEvent?.UpdateCooldownText();
            }
        }
    }

    private void HandleEscape()
    {
        if (IsCameraActive(prefabCamera) && Input.GetKeyDown(KeyCode.Escape))
        {
            prefabCamera?.gameObject.SetActive(false);
            mainCamera?.gameObject.SetActive(true);
            controller.isInteractingWithDoor = false; //  Reactiva el movimiento al salir
        }
    }
}

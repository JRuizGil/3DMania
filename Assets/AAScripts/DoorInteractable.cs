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

    [Header("Camera Settings")]
    private Camera mainCamera;
    public Camera prefabCamera;

    private ClickerEvent clickerEvent;

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
    }
    private bool IsCameraActive(Camera cam)
    {
        return cam != null && cam.gameObject.activeSelf;
    }

    public void OnInteractStart()
    {
        if (!IsCameraActive(mainCamera)) return;

        if (controller != null && Vector3.Distance(player.position, targetPosition.position) > 0.3f)
        {           
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
            prefabCamera.gameObject.SetActive(false);
            mainCamera.gameObject.SetActive(true);
            controller.isInteractingWithDoor = false; //  Habilita nuevamente el movimiento/interacción
        }
    }

    private void PlayerMove()
    {
        if (isMoving)
        {
            float step = moveSpeed * Time.deltaTime;

            // Mantener el Y actual del jugador
            Vector3 targetPosXZ = new Vector3(targetPosition.position.x, player.position.y, targetPosition.position.z);
            player.position = Vector3.Lerp(player.position, targetPosXZ, step);

            // Rotación solo en el eje Y (ignorar inclinaciones)
            Quaternion targetRot = Quaternion.Euler(0, targetPosition.rotation.eulerAngles.y, 0);
            player.rotation = Quaternion.Lerp(player.rotation, targetRot, step);

            if (Vector3.Distance(player.position, targetPosXZ) <= 0.3f)
            {
                isMoving = false;
                mainCamera.gameObject.SetActive(false);
                prefabCamera.gameObject.SetActive(true);
            }
        }
    }

    private void HandleEscape()
    {
        if (IsCameraActive(prefabCamera) && Input.GetKeyDown(KeyCode.Escape))
        {
            prefabCamera.gameObject.SetActive(false);
            mainCamera.gameObject.SetActive(true);
            controller.isInteractingWithDoor = false; //  Reactiva el movimiento al salir
        }
    }
    
}

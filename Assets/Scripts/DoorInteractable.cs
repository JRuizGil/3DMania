using UnityEngine;

public class DoorInteractable : MonoBehaviour
{
    public Transform player;
    public Transform targetPosition;
    public float moveSpeed = 10f;
    private PlayerController controller;
    private bool isMoving = false;

    [Header("Camera Settings")]
    private Camera mainCamera;
    private Camera prefabCamera;

    private void Start()
    {
        controller = player.GetComponent<PlayerController>();

        // Obtener la cámara principal automáticamente
        mainCamera = Camera.main;
        if (mainCamera == null)
        {
            Debug.LogError("No se encontró la cámara principal (Main Camera).");
        }

        // Buscar la cámara del prefab entre los hijos
        prefabCamera = GetComponentInChildren<Camera>(true);
        if (prefabCamera == null)
        {
            Debug.LogError("No se encontró ninguna cámara como hijo del objeto interactuable.");
        }
        else
        {
            // Asegurarse de que esté desactivada inicialmente
            prefabCamera.gameObject.SetActive(false);
        }
    }

    private void Update()
    {
        PlayerMove();
        HandleEscape();
    }

    public void OnInteractStart()
    {
        if (controller != null && Vector3.Distance(player.position, targetPosition.position) > 0.1f)
        {
            CancelCurrentMovement(); // Cancelar el movimiento anterior
            Debug.Log("IsMoving true");
            controller.StopNavMeshAgent();
            isMoving = true;
        }
        else
        {
            Debug.Log("El jugador ya está en la posición, no se puede interactuar de nuevo.");
        }
    }

    private void CancelCurrentMovement()
    {
        if (isMoving)
        {
            isMoving = false;
            Debug.Log("Movimiento cancelado.");

            // Reactivar la cámara principal si estaba cambiada
            if (prefabCamera != null && prefabCamera.gameObject.activeSelf)
            {
                prefabCamera.gameObject.SetActive(false);
                if (mainCamera != null)
                {
                    mainCamera.gameObject.SetActive(true);
                    Debug.Log("Volviendo a la cámara principal tras cancelar.");
                }
            }
        }
    }

    void PlayerMove()
    {
        if (isMoving)
        {
            if (controller.IsNavMeshAgentActive())
            {
                CancelCurrentMovement();
                return;
            }

            // Mover al jugador hacia la posición objetivo
            player.position = Vector3.MoveTowards(player.position, targetPosition.position, moveSpeed * Time.deltaTime);

            // Hacer que el jugador mire hacia el objeto interactuable
            Vector3 lookDirection = (transform.position - player.position).normalized;
            lookDirection.y = 0;
            player.forward = Vector3.Slerp(player.forward, lookDirection, 0.1f);

            // Verificar si ha llegado a la posición
            if (Vector3.Distance(player.position, targetPosition.position) < 0.1f)
            {
                isMoving = false;

                // Desactivar la cámara principal y activar la cámara del prefab
                if (mainCamera != null)
                {
                    mainCamera.gameObject.SetActive(false);
                }

                if (prefabCamera != null)
                {
                    prefabCamera.gameObject.SetActive(true);
                    Debug.Log("Cambiando a la cámara del prefab.");
                }
            }
        }
    }

    void HandleEscape()
    {
        // Si la cámara del prefab está activa y se pulsa Escape, revertir el cambio
        if (prefabCamera != null && prefabCamera.gameObject.activeSelf && Input.GetKeyDown(KeyCode.Escape))
        {
            prefabCamera.gameObject.SetActive(false);
            if (mainCamera != null)
            {
                mainCamera.gameObject.SetActive(true);
                Debug.Log("Cambiando de vuelta a la cámara principal.");
            }
        }
    }
}

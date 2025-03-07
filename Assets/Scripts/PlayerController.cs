using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float mouseSensitivity = 2f;
    private float rotationX = 90f;
    public float detectionDistance = 3f;
    public Transform cameraTransform;

    private Interactable currentInteractable = null; 

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    void Update()
    {
        HandleMovement();
        HandleMouseLook();
        HandleExit();
        HandleClick();
    }
    void HandleMovement()
    {
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        Vector3 moveDirection = transform.right * moveX + transform.forward * moveZ;
        transform.position += moveDirection * moveSpeed * Time.deltaTime;
    }
    void HandleMouseLook()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;

        rotationX -= mouseY;
        rotationX = Mathf.Clamp(rotationX, -90f, 90f);

        cameraTransform.localRotation = Quaternion.Euler(rotationX, 0f, 0f);
        transform.Rotate(Vector3.up * mouseX);
    }
    void HandleExit()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
        }
    }
    void HandleClick()
    {
        if (Input.GetMouseButtonDown(0)) 
        {
            RaycastHit hit;
            if (Physics.Raycast(cameraTransform.position, cameraTransform.forward, out hit, detectionDistance))
            {
                Interactable interactable = hit.collider.GetComponent<Interactable>();
                if (interactable != null)
                {
                    currentInteractable = interactable; 
                    currentInteractable.OnInteractStart();
                }
            }
        }
        if (Input.GetMouseButtonUp(0)) 
        {
            if (currentInteractable != null)
            {
                currentInteractable.OnInteractEnd();
                currentInteractable = null; 
            }
        }
    }
}

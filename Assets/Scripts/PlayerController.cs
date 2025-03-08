using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float mouseSensitivity = 2f;
    private float rotationX = 0f;
    public float detectionDistance = 3f;
    public Transform cameraTransform;

    private DoorInteractable currentInteractable = null; 
    
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
        HandleClickDistance();
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
        if (Input.GetMouseButton(0)) 
        {
            RaycastHit hit;
            if (Physics.Raycast(cameraTransform.position, cameraTransform.forward, out hit, detectionDistance))
            {
                DoorInteractable interactable = hit.collider.GetComponent<DoorInteractable>();

                if (interactable != null)
                {
                    if (currentInteractable == null)
                    {                        
                        currentInteractable = interactable;
                        currentInteractable.OnInteractStart();
                    }
                }
                else if (currentInteractable != null)
                {                    
                    currentInteractable.OnInteractEnd();
                    currentInteractable = null;
                }
            }
            else if (currentInteractable != null) 
            {
                currentInteractable.OnInteractEnd();
                currentInteractable = null;
            }
        }

        if (Input.GetMouseButtonUp(0) && currentInteractable != null)
        {
            currentInteractable.OnInteractEnd();
            currentInteractable = null;
        }
    }


    private void HandleClickDistance()
    {
        if (currentInteractable != null)
        {
            float distance = Vector3.Distance(transform.position, currentInteractable.transform.position);
            if (distance > detectionDistance)
            {
                currentInteractable.OnInteractEnd();
                currentInteractable = null;
            }
        }
    }
    
}

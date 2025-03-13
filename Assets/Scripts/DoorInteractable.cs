using UnityEngine;

public class DoorInteractable : MonoBehaviour
{
    public Transform player;
    public Transform targetPosition;
    public float moveSpeed = 10f;
    private PlayerController controller;
    private bool isMoving = false;

    private void Start()
    {
        controller = player.GetComponent<PlayerController>();
        if (controller == null)
        {
            Debug.LogError("No se encontró PlayerController en el objeto player.");
        }
    }
    private void Update()
    {
        if (isMoving)
        {
            if (controller.IsNavMeshAgentActive())
            {
                isMoving = false;
                return;
            }            
            player.position = Vector3.MoveTowards(player.position, targetPosition.position, moveSpeed * Time.deltaTime);                        
            Vector3 lookDirection = (transform.position - player.position).normalized;
            lookDirection.y = 0; 
            player.forward = Vector3.Slerp(player.forward, lookDirection, 0.1f);                        
            if (Vector3.Distance(player.position, targetPosition.position) < 0.1f)
            {
                isMoving = false;
            }
        }
    }
    public void OnInteractStart()
    {
        if (!isMoving && controller != null && Vector3.Distance(player.position, targetPosition.position) > 0.1f)
        {
            Debug.Log("IsMoving true");
            controller.StopNavMeshAgent();
            isMoving = true;
        }
        else
        {
            Debug.Log("El jugador ya está en la posición, no se puede interactuar de nuevo.");
        }
    }
}

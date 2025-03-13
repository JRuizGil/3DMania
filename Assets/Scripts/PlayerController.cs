using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.AI;

public class PlayerController : MonoBehaviour
{
    const string IDLE = "Idle";

    CustomActions input;
    NavMeshAgent agent;
    Animator animator;

    [Header("Movement")]
    [SerializeField] ParticleSystem clickEffect;
    [SerializeField] LayerMask clickableLayers;

    float lookRotationSpeed = 8f;

    private DoorInteractable currentInteractable;

    public Transform cameraTransform;

    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        input = new CustomActions();  
        AssignInputs();
    }

    void AssignInputs()
    {
        input.Main.Move.performed += ctx => ClickToMove();
    }

    void ClickToMove()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit[] hits = Physics.RaycastAll(ray, 100f);
        float nearestValidDistance = Mathf.Infinity;
        RaycastHit? validHit = null;

        foreach (RaycastHit hit in hits)
        {
            if ((clickableLayers & (1 << hit.collider.gameObject.layer)) == 0)
            {
                return;
            }
            if (hit.distance < nearestValidDistance)
            {
                nearestValidDistance = hit.distance;
                validHit = hit;
            }
        }

        if (validHit.HasValue)
        {
            agent.isStopped = false;  
            agent.destination = validHit.Value.point;

            if (clickEffect != null)
            {
                ParticleSystem effectInstance = Instantiate(clickEffect, validHit.Value.point + new Vector3(0, 0.1f, 0), clickEffect.transform.rotation);
                Destroy(effectInstance.gameObject, effectInstance.main.duration);
            }
        }
    }
    public void StopNavMeshAgent()
    {
        agent.isStopped = true;
        agent.ResetPath();
    }
    public bool IsNavMeshAgentActive()
    {
        return !agent.isStopped && agent.hasPath; 
    }

    void FaceTarget()
    {
        if (!agent.isStopped && agent.velocity.sqrMagnitude > 0.1f)
        {
            Vector3 direction = (agent.destination - transform.position).normalized;
            Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * lookRotationSpeed);
        }
    }

    void OnEnable()
    {
        input.Enable();
    }
    void OnDisable()
    {
        input.Disable();
    }
    void Update()
    {
        FaceTarget();
        HandleClick();
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
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                DoorInteractable interactable = hit.collider.GetComponent<DoorInteractable>();

                if (interactable != null)  
                {
                    interactable.OnInteractStart();
                }
                else
                {
                    return;
                }
            }
        }
    }
}

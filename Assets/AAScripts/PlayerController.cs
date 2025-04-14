using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.AI;
using System.Linq;
using UnityEngine.EventSystems;

public class PlayerController : MonoBehaviour
{
    const string IDLE = "Idle";

    CustomActions input;
    public NavMeshAgent agent;
    Animator animator;

    [Header("Movement")]
    [SerializeField] ParticleSystem clickEffect;
    [SerializeField] LayerMask clickableLayers;

    public float lookRotationSpeed = 25f;

    public DoorInteractable currentInteractable;
    public Transform cameraTransform;

    public bool isInteractingWithDoor = false; //  Nueva variable para bloquear movimiento/interacción

    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        input = new CustomActions();
        AssignInputs();
    }

    void OnEnable() => input.Enable();
    void OnDisable() => input.Disable();

    void Update()
    {
        if (IsMainCameraActive() && !isInteractingWithDoor) //  Evita que el personaje se mueva si interactúa con la puerta
        {
            FaceTarget();
            HandleClick();
        }
    }

    void AssignInputs()
    {
        input.Main.Move.performed += ctx => StartCoroutine(DelayedClickToMove());
    }

    IEnumerator DelayedClickToMove()
    {
        yield return null;
        ClickToMove();
    }

    void ClickToMove()
    {
        if (!IsMainCameraActive() || isInteractingWithDoor) return; //  Bloquea el movimiento si está en una puerta

        if (EventSystem.current != null && EventSystem.current.IsPointerOverGameObject()) return;

        Camera activeCamera = Camera.allCameras.FirstOrDefault(cam => cam.isActiveAndEnabled);
        if (activeCamera == null || activeCamera.tag != "MainCamera") return;

        Ray ray = activeCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit[] hits = Physics.RaycastAll(ray, 100f);

        float nearestValidDistance = Mathf.Infinity;
        RaycastHit? validHit = null;

        foreach (RaycastHit hit in hits)
        {
            if (((1 << hit.collider.gameObject.layer) & clickableLayers) == 0) continue;

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
                ParticleSystem effectInstance = Instantiate(clickEffect, validHit.Value.point + Vector3.up * 0.1f, clickEffect.transform.rotation);
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

    public void HandleClick()
    {
        if (!IsMainCameraActive() || isInteractingWithDoor) return; //  Bloquea interacciones si está en una puerta

        if (EventSystem.current != null && EventSystem.current.IsPointerOverGameObject()) return;

        if (Input.GetMouseButtonDown(0))
        {
            Camera activeCamera = Camera.allCameras.FirstOrDefault(cam => cam.isActiveAndEnabled);
            if (activeCamera == null || activeCamera.tag != "MainCamera") return;

            Ray ray = activeCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                DoorInteractable interactable = hit.collider.GetComponent<DoorInteractable>();

                if (interactable != null)
                {
                    currentInteractable = interactable;
                    interactable.OnInteractStart();
                }
                else
                {
                    currentInteractable = null;
                }
            }
        }
    }

    private bool IsMainCameraActive()
    {
        Camera activeCamera = Camera.allCameras.FirstOrDefault(cam => cam.isActiveAndEnabled);
        return activeCamera != null && activeCamera.tag == "MainCamera";
    }
}

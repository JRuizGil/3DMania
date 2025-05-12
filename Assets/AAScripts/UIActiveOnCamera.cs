using UnityEngine;

public class UIActiveOnCamera : MonoBehaviour
{
    public GameObject uiElement;
    public GameObject ui2Element;
    public GameObject ui3Element;

    private GameObject lastActiveCamera;

    void Update()
    {
        Camera[] cameras = Camera.allCameras;
        GameObject currentCamGO = null;

        foreach (Camera cam in cameras)
        {
            if (cam.gameObject.activeInHierarchy)
            {
                currentCamGO = cam.gameObject;
                break;
            }
        }

        // Si no hay cambio de cámara activa, no hacer nada
        if (currentCamGO == lastActiveCamera) return;

        // Guardar la nueva cámara activa
        lastActiveCamera = currentCamGO;

        // Actualizar UI según la cámara activa
        uiElement.SetActive(false);
        ui2Element.SetActive(false);
        ui3Element.SetActive(false);

        if (currentCamGO != null)
        {
            if (currentCamGO.CompareTag("MainCamera"))
            {
                uiElement.SetActive(true);
            }
            else if (currentCamGO.CompareTag("CameraDoor"))
            {
                ui2Element.SetActive(true);
            }
            else
            {
                ui3Element.SetActive(true);
            }
        }
    }
}

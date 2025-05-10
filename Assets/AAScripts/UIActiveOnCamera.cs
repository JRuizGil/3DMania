using UnityEngine;

public class UIActiveOnCamera : MonoBehaviour
{
    public GameObject uiElement;    // Para Main Camera
    public GameObject ui2Element;   // Para segunda cámara activa
    public GameObject ui3Element;   // Para tercera cámara activa (o cualquier otra)

    //void Update()
    //{
    //    Camera[] cameras = Camera.allCameras;
    //
    //    GameObject camGO = null;
    //
    //    foreach (Camera cam in cameras)
    //    {
    //        if (cam.gameObject.activeInHierarchy)
    //        {
    //            camGO = cam.gameObject;
    //            break; // Usamos la primera cámara activa que encontremos
    //        }
    //    }        
    //
    //    uiElement.SetActive(false);
    //    ui2Element.SetActive(false);
    //    ui3Element.SetActive(false);
    //
    //    if (camGO != null)
    //    {
    //        if (camGO.CompareTag("MainCamera"))
    //        {
    //            uiElement.SetActive(true); // Si es la MainCamera
    //        }
    //        else if (camGO.CompareTag("CameraDoor"))
    //        {
    //            ui2Element.SetActive(true); 
    //        }
    //        else
    //        {
    //            ui3Element.SetActive(true); // Cualquier otra cámara activa
    //        }
    //    }
    //}
}

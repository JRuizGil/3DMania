using UnityEngine;

public class UIActiveOnCamera : MonoBehaviour
{
    public GameObject uiElement; // Asigna el objeto UI en el Inspector

    void Update()
    {
        if (Camera.main != null)
        {
            uiElement.SetActive(Camera.main.gameObject.activeInHierarchy);
        }
        else
        {
            uiElement.SetActive(false); // Desactivar UI si no hay una Main Camera
        }
    }
}

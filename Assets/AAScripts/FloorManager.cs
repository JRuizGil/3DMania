using UnityEngine;
using UnityEngine.UI;

public class FloorManager : MonoBehaviour
{
    public Camera Maincamera;
    public GameObject Player;
    public Transform[] camPos;
    public Transform[] playerposy;
    public int currentFloorIndex = 0;
    private Vector3 camtargetPosition; // Nuevo: posición objetivo
    public float moveSpeed = 5f; // Velocidad de movimiento
    


    private void Start()
    {
        Maincamera.gameObject.SetActive(true);
        if (camPos.Length > 0)
        {
            camtargetPosition = camPos[0].transform.position;
            Maincamera.transform.position = camtargetPosition;
        }
    }

    private void Update()
    {
        // Movimiento suave cada frame hacia el objetivo
        Maincamera.transform.position = Vector3.Lerp(Maincamera.transform.position, camtargetPosition, moveSpeed * Time.deltaTime);
        
        // Opcional: controles de prueba
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            FloorUp();
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            FloorDown();
        }
    }

    public void FloorUp()
    {
        int originalIndex = currentFloorIndex;

        // Buscar el siguiente piso activo
        do
        {
            currentFloorIndex++;
            if (currentFloorIndex >= camPos.Length)
            {
                currentFloorIndex = originalIndex; // Volver al original si no hay más
                return;
            }
        } while (!camPos[currentFloorIndex].gameObject.activeSelf);

        MoveCameraToCurrentFloor();
    }

    public void FloorDown()
    {
        int originalIndex = currentFloorIndex;

        // Buscar el piso activo anterior
        do
        {
            currentFloorIndex--;
            if (currentFloorIndex < 0)
            {
                currentFloorIndex = originalIndex; // Volver al original si no hay más
                return;
            }
        } while (!camPos[currentFloorIndex].gameObject.activeSelf);

        MoveCameraToCurrentFloor();
    }


    private void MoveCameraToCurrentFloor()
    {
        camtargetPosition = camPos[currentFloorIndex].position; // Solo cambiamos el destino, la Update se encarga del lerp
    }
    
}

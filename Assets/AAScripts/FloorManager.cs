using UnityEngine;

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
        if (currentFloorIndex < camPos.Length - 1)
        {
            currentFloorIndex++;
            MoveCameraToCurrentFloor();
        }
    }

    public void FloorDown()
    {
        if (currentFloorIndex > 0)
        {
            currentFloorIndex--;

            MoveCameraToCurrentFloor();
        }
    }

    private void MoveCameraToCurrentFloor()
    {
        camtargetPosition = camPos[currentFloorIndex].position; // Solo cambiamos el destino, la Update se encarga del lerp
    }
}

using UnityEngine;
using UnityEngine.UI;

public class ClickerEvent : MonoBehaviour
{
    private bool gameActive = false;
    private float timerDuration = 5f; // Tiempo del minijuego en segundos
    private float timer;
    private int clickCount = 0;

    private Camera prefabCamera;  // Referencia a la cámara del prefab
    private Inventory inventory;  // Referencia al inventario (al vuelo, sin asignarlo en el Inspector)

    public Text timerText; // Referencia al componente Text que muestra el tiempo

    void Start()
    {
        // Buscar la cámara en los hijos del objeto
        prefabCamera = GetComponentInChildren<Camera>();

        if (prefabCamera == null)
        {
            Debug.LogError("No se encontró la cámara en los hijos del objeto.");
        }

        // Buscar el componente Inventory en la escena utilizando FindFirstObjectByType
        inventory = Object.FindFirstObjectByType<Inventory>();

        if (inventory == null)
        {
            Debug.LogError("No se encontró el componente Inventory en la escena.");
        }

        // Asegúrate de que el texto esté oculto al inicio
        if (timerText != null)
        {
            timerText.gameObject.SetActive(false);
        }
    }

    void Update()
    {
        // Verificar si la cámara del prefab está activa
        if (prefabCamera == null || !prefabCamera.gameObject.activeSelf)
        {
            ResetGame(); // Si la cámara no está activa, reiniciar el minijuego
            return;
        }

        // Iniciar el minijuego con el primer clic
        if (Input.GetMouseButtonDown(0))
        {
            if (!gameActive)
            {
                StartGame();
            }

            clickCount++;
            Debug.Log("Click detectado. Total: " + clickCount);
        }

        // Contador en marcha
        if (gameActive)
        {
            timer -= Time.deltaTime;
            UpdateTimerText();  // Actualizar el texto del contador

            if (timer <= 0)
            {
                EndGame();
            }
        }
    }

    void StartGame()
    {
        gameActive = true;
        timer = timerDuration;
        clickCount = 0;

        // Activar el texto del contador
        if (timerText != null)
        {
            timerText.gameObject.SetActive(true);
        }

        Debug.Log("Minijuego iniciado! Tiempo: " + timerDuration + " segundos.");
    }

    void EndGame()
    {
        gameActive = false;

        // Calcular los materiales totales
        int totalMaterials = CalculateMaterials(clickCount, timerDuration);

        // Pasa el total de materiales al inventario
        if (inventory != null)
        {
            inventory.AddMaterials(totalMaterials);
            Debug.Log($"Minijuego terminado! {totalMaterials} materiales añadidos.");
        }
        else
        {
            Debug.LogError("Inventory es NULL. Asegúrate de que exista un componente Inventory en la escena.");
        }

        // Desactivar el texto del contador cuando termine el minijuego
        if (timerText != null)
        {
            timerText.gameObject.SetActive(false);
        }
    }

    void ResetGame()
    {
        if (gameActive)
        {
            gameActive = false;
            Debug.Log("Minijuego reiniciado porque la cámara no está activa.");

            // Desactivar el texto del contador cuando se reinicia
            if (timerText != null)
            {
                timerText.gameObject.SetActive(false);
            }
        }
    }

    void UpdateTimerText()
    {
        // Actualiza el texto del contador para que muestre el tiempo restante
        if (timerText != null)
        {
            timerText.text = $"Tiempo restante: {timer:F1}"; // Mostrar el tiempo restante con un decimal
        }
    }

    int CalculateMaterials(int clicks, float time)
    {
        return Mathf.RoundToInt(clicks / time * 10); // Proporcionalidad simple
    }
}

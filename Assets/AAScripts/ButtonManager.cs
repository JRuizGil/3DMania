using UnityEngine;
using UnityEngine.AI;

public class ButtonManager : MonoBehaviour
{
    public GameObject Player;    

    public GameObject ExitMenu;
    public GameObject UpgradeMenu;
    public GameObject ConfigMenu;
    public GameObject MaterialMenu;

    Vector3 lastPosition; // Almacena la última posición del jugador

    void Start()
    {
        ExitMenu.SetActive(false);
        UpgradeMenu.SetActive(false);
        ConfigMenu.SetActive(false);
        MaterialMenu.SetActive(false);
        if (Player != null)
        {
            lastPosition = Player.transform.position; // Guarda la posición inicial
        }
    }    
    void Update()
    {
        if (IsPlayerMoving())
        {
            CloseAllMenus();
        }
    }
    public void OpenExitMenu()
    {
        ExitMenu.SetActive(true);
        UpgradeMenu.SetActive(false);
        ConfigMenu.SetActive(false);
        MaterialMenu.SetActive(false);
    }
    public void OpenUpgradeMenu()
    {
        ExitMenu.SetActive(false);
        UpgradeMenu.SetActive(true);
        ConfigMenu.SetActive(false);
        MaterialMenu.SetActive(false);
    }
    public void OpenConfigMenu()
    {
        ExitMenu.SetActive(false);
        UpgradeMenu.SetActive(false);
        ConfigMenu.SetActive(true);
        MaterialMenu.SetActive(false);
    }
    public void OpenMatsMenu()
    {
        ExitMenu.SetActive(false);
        UpgradeMenu.SetActive(false);
        ConfigMenu.SetActive(false);
        MaterialMenu.SetActive(true);
    }
    bool IsPlayerMoving()
    {
        if (Player == null) return false;

        Vector3 currentPosition = Player.transform.position;
        
        if (currentPosition != lastPosition)
        {
            lastPosition = currentPosition; // Actualiza la última posición
            return true;
        }

        return false;
    }
    public void CloseAllMenus()
    {
        ExitMenu.SetActive(false);
        UpgradeMenu.SetActive(false);
        ConfigMenu.SetActive(false);
        MaterialMenu.SetActive(false);
    }
    public void ExitGame()
    {
        Application.Quit();
        // Si estás en el editor de Unity, esto detendrá la ejecución del juego
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}

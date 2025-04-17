using UnityEngine;
using UnityEngine.AI;

public class ButtonManager : MonoBehaviour
{
    public GameObject Player;    

    public GameObject ExitMenu;
    public GameObject UpgradeMenu;
    public GameObject ConfigMenu;
    public GameObject MaterialMenu;
    public GameObject BossMenu;
    private bool IsmenuOpened;

    void Start()
    {
        CloseAllMenus();
    }    
    void Update()
    {        
        if (IsmenuOpened && Input.GetKey(KeyCode.Escape))
        {
            CloseAllMenus();
        }        
    }
    public void OpenExitMenu()
    {
        BossMenu.SetActive(false);
        UpgradeMenu.SetActive(false);
        ConfigMenu.SetActive(false);
        MaterialMenu.SetActive(false);
        ExitMenu.SetActive(true);
        IsmenuOpened = true;
    }
    public void OpenUpgradeMenu()
    {
        BossMenu.SetActive(false);
        ExitMenu.SetActive(false);
        ConfigMenu.SetActive(false);
        MaterialMenu.SetActive(false);
        UpgradeMenu.SetActive(true);
        IsmenuOpened = true;
    }
    public void OpenConfigMenu()
    {
        BossMenu.SetActive(false);
        ExitMenu.SetActive(false);
        UpgradeMenu.SetActive(false);
        MaterialMenu.SetActive(false);
        ConfigMenu.SetActive(true);
        IsmenuOpened = true;
    }
    public void OpenMatsMenu()
    {
        ExitMenu.SetActive(false);
        BossMenu.SetActive(false);
        UpgradeMenu.SetActive(false);
        ConfigMenu.SetActive(false);
        MaterialMenu.SetActive(true);
        IsmenuOpened = true;
    }
    public void OpenBossMenu()
    {
        ExitMenu.SetActive(false);
        UpgradeMenu.SetActive(false);
        ConfigMenu.SetActive(false);
        MaterialMenu.SetActive(false);
        BossMenu.SetActive(true);
        IsmenuOpened = true;
    }
    
    public void CloseAllMenus()
    {        
        ExitMenu.SetActive(false);
        UpgradeMenu.SetActive(false);
        ConfigMenu.SetActive(false);
        MaterialMenu.SetActive(false);
        BossMenu.SetActive(false);
        IsmenuOpened = false;
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

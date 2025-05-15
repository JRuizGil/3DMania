using UnityEngine;

public class StartManager : MonoBehaviour
{
    public GameObject[] Disable;
    public GameObject[] Enable;
    void Start()
    {
        StartEnable();
        StartUnable();
    }
    public void StartUnable()
    {
        foreach (GameObject door in Disable)
        {
            if (door != null)
                door.SetActive(false);
        }
    }
    public void StartEnable()
    {
        foreach (GameObject door in Enable)
        {
            if (door != null)
                door.SetActive(true);
        }
    }
}


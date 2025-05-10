using UnityEngine;

public class StartManager : MonoBehaviour
{
    public GameObject[] doors;
    void Start()
    {
        foreach (GameObject door in doors)
        {
            if (door != null)
                door.SetActive(false);
        }
    }

}


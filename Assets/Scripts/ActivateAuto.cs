using UnityEngine;

public class ActivateAuto : MonoBehaviour
{
    public GameObject AutomaterPrefab;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void BuyAutomater()
    {
        AutomaterPrefab.SetActive(true);
    }
}

using UnityEngine;

public class AutoManager : MonoBehaviour
{
    public UpgradeButtonManager upgradeButtonManager;
    public Inventory Inventory;
    public ClickerEventData ClickerEventData;

    private float actualearn;
    private float cooldown;
    private float lvl;
    private float initialrevenue;

    private void Start()
    {        
        cooldown = ClickerEventData.timerDuration;
        initialrevenue = ClickerEventData.initialrevenue;
        gameObject.SetActive(false);
    }

    private void FixedUpdate()
    {
        actualearn = upgradeButtonManager.actualearn; // Actualiza cps en cada frame
        lvl = upgradeButtonManager.lvl;
    }
    void Update()
    {
        if (!IsInvoking("Automatizar"))
        {
            InvokeRepeating("Automatizar", cooldown, cooldown);
        }
    }

    public void Automatizar()
    {
        int totalMaterials = Mathf.RoundToInt((float)actualearn);
        Inventory.AddMaterials(totalMaterials); // Añade cps al inventario cada llamada
    }
}


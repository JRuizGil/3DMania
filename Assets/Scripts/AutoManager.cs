using UnityEngine;

public class AutoManager : MonoBehaviour
{
    public UpdatetxtUpgrade UpdatetxtUpgrade;
    public Inventory Inventory;
    public ClickerEventData ClickerEventData;

    private float actualearn;
    private float cooldown;
    private float lvl;
    private float initialrevenue;

    private void Start()
    {        
        cooldown = ClickerEventData.cooldownTime;
        initialrevenue = ClickerEventData.initialrevenue;
        gameObject.SetActive(false);
    }

    private void FixedUpdate()
    {
        actualearn = UpdatetxtUpgrade.actualearn; // Actualiza cps en cada frame
        lvl = UpdatetxtUpgrade.lvl;
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


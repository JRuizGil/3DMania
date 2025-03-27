using UnityEngine;

public class AutoManager : MonoBehaviour
{
    public UpdatetxtUpgrade UpdatetxtUpgrade;
    public Inventory Inventory;
    public ClickerEventData ClickerEventData;

    private float cps;

    private void Start()
    {
        InvokeRepeating("Automatizar", 1f, 1f); // Llama a Automatizar cada segundo
    }

    private void Update()
    {
        cps = UpdatetxtUpgrade.cps; // Actualiza cps en cada frame
    }

    public void Automatizar()
    {
        int totalMaterials = Mathf.RoundToInt((float)cps * ClickerEventData.materialMultiplier);
        Inventory.AddMaterials(totalMaterials); // Añade cps al inventario cada segundo
    }
}


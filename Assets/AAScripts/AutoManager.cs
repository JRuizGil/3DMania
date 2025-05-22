using UnityEngine;

using System.Collections;
using System.Linq;
public class AutoManager : MonoBehaviour
{
    public UpgradeButtonManager upgradeButtonManager;
    public Inventory Inventory;
    public ClickerEventData ClickerEventData;
    private float timerduration;
    public Animator animator;
    private double actualearn;

    private void Awake()
    {
        gameObject.SetActive(false);
    }
    private void FixedUpdate()
    {
        actualearn = upgradeButtonManager.actualearn; // Actualiza cps en cada frame
        timerduration = upgradeButtonManager.timerduration;
    }
    private void Start()
    {
        timerduration = ClickerEventData.timerDuration;
        StartCoroutine(AutoLoop());
    }

    private IEnumerator AutoLoop()
    {
        yield return new WaitForSeconds(0.5f); // Pequeño delay inicial
        while (true)
        {
            yield return AutomatizarCoroutine();
            yield return new WaitForSeconds(0.5f); // Espera entre automatizaciones
        }
    }
    private IEnumerator AutomatizarCoroutine()
    {        
        animator.SetBool("Open", true);
        yield return new WaitForSeconds(timerduration);

        double totalMaterials = (double)System.Math.Round(actualearn); // sin casteo a float
        Inventory.AddMaterials(totalMaterials);

        animator.SetBool("Open", false);
    }
}

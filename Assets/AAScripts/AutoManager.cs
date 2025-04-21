using UnityEngine;

using System.Collections;
public class AutoManager : MonoBehaviour
{
    public UpgradeButtonManager upgradeButtonManager;
    public Inventory Inventory;
    public ClickerEventData ClickerEventData;
    public Animator animator;

    private float actualearn;
    private float cooldown = 1f;
    private float lvl;
    private float initialrevenue;

    private void Awake()
    {
        gameObject.SetActive(false);
    }
    private void FixedUpdate()
    {
        actualearn = upgradeButtonManager.actualearn; // Actualiza cps en cada frame
        lvl = upgradeButtonManager.lvl;
    }
    private void Start()
    {
        StartCoroutine(AutoLoop());
    }

    private IEnumerator AutoLoop()
    {
        yield return new WaitForSeconds(ClickerEventData.timerDuration + 1); // Pequeño delay inicial
        while (true)
        {
            yield return AutomatizarCoroutine();
            yield return new WaitForSeconds(cooldown); // Espera entre automatizaciones
        }
    }

    private IEnumerator AutomatizarCoroutine()
    {
        animator.SetBool("Open", true);
        yield return new WaitForSeconds(ClickerEventData.timerDuration);

        int totalMaterials = Mathf.RoundToInt((float)actualearn);
        Inventory.AddMaterials(totalMaterials);

        animator.SetBool("Open", false);
    }
}

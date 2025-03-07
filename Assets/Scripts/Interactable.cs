using UnityEngine;

public class Interactable : MonoBehaviour
{
    public GameObject Child;
    public Transform StartPos;
    public Transform EndPos;
    public float moveSpeed = 2f;

    private bool isInteracting = false;
    private float progress = 0f; 

    private Inventory inventory;

    private void Start()
    {
        inventory = FindAnyObjectByType<Inventory>();
    }

    private void Update()
    {
        if (isInteracting && progress < 1f)
        {
            progress += Time.deltaTime * moveSpeed;
            progress = Mathf.Clamp01(progress); 
        }
        else if (!isInteracting && progress > 0f)
        {
            progress -= Time.deltaTime * moveSpeed;
            progress = Mathf.Clamp01(progress);
        }        

        Child.transform.localPosition = Vector3.Lerp(StartPos.localPosition, EndPos.localPosition, progress);
        
        if (progress >= 1f)
        {
            AddRandomValuesToInventory();
        }
    }
    public void OnInteractStart()
    {
        isInteracting = true;
    }
    public void OnInteractEnd()
    {
        isInteracting = false;
    }
    private void AddRandomValuesToInventory()
    {
        if (inventory != null)
        {
            float mat1 = Random.Range(2, 4);
            float mat2 = Random.Range(0, 3);
            float mat3 = Random.Range(0, 1);

            inventory.AddMaterials(mat1, mat2, mat3);
        }
    }
}

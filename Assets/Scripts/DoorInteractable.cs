using UnityEngine;

public class DoorInteractable : MonoBehaviour
{
    public GameObject Child;
    public Transform StartPos;
    public Transform EndPos;
    public float moveSpeed = 2f;

    private bool isInteracting = false;
    private float progress = 0f; 
    private bool isFinished = false;

    private Inventory inventory;

    private void Start()
    {
        inventory = FindAnyObjectByType<Inventory>();
    }
    private void FixedUpdate()
    {
        if (isInteracting && progress < 1f)
        {
            progress += Time.deltaTime * moveSpeed;
            progress = Mathf.Clamp01(progress);
        }
        Child.transform.localPosition = Vector3.Lerp(StartPos.localPosition, EndPos.localPosition, progress);
                
    }
    private void Update()
    {
        if (progress == 1f && !isFinished)
        {
            AddRandomValuesToInventory();
            isFinished = true;
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
            float mat1 = Random.Range(1, 5);
            float mat2 = Random.Range(1, 3);
            float mat3 = Random.Range(1, 1);

            inventory.AddMaterials(mat1, mat2, mat3);
        }
    }
}

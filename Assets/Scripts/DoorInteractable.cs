using UnityEngine;

public class DoorInteractable : MonoBehaviour
{
    public GameObject Child;
    public Transform StartPos;
    public Transform EndPos;
    public float moveSpeed = 2f;
    public Transform player;
    public Transform playerTargetPosition;

    private bool isInteracting = false;
    private float progress = 0f;
    private bool isFinished = false;
    private Inventory inventory;
    private PlayerController PlayerController;

    private void Start()
    {
        inventory = FindAnyObjectByType<Inventory>();
        PlayerController = player.GetComponent<PlayerController>();
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
            //ReleasePlayer();
        }
    }

    public void OnInteractStart()
    {
        //MovePlayerToDoor();
    }

    //private void MovePlayerToDoor()
    //{
    //    if (PlayerController != null)
    //    {
    //        PlayerController.MoveToPosition(playerTargetPosition.position, () => {
    //            FreezePlayer();
    //            isInteracting = true;
    //        });
    //    }
    //}

    //private void FreezePlayer()
    //{
    //    if (PlayerController != null)
    //    {
    //        PlayerController.SetFrozen(true);
    //    }
    //}

    //private void ReleasePlayer()
    //{
    //    if (PlayerController != null)
    //    {
    //        PlayerController.SetFrozen(false);
    //    }
    //}

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

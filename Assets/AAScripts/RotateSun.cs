using UnityEngine;

public class RotateSun : MonoBehaviour
{
    [SerializeField] private float rotationSpeed = 10f; // Velocidad en grados por segundo

    private void Update()
    {
        transform.Rotate(Vector3.right * rotationSpeed * Time.deltaTime);
    }
    
}

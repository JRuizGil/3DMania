using System.Collections;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    public float Mat1;
    public float Mat2;
    public float Mat3;

    public GameObject Child;
    public Transform StartPos;
    public Transform EndPos;
    public float moveDuration = 2f;

    private bool isMoving = false;
    private bool hasReachedEnd = false;

    public void OnInteract()
    {
        if (!isMoving && !hasReachedEnd)
        {
            StartCoroutine(MoveChild());
        }
    }
    private IEnumerator MoveChild()
    {
        isMoving = true;
        float elapsedTime = 0f;
        Vector3 startLocalPos = StartPos.localPosition;
        Vector3 endLocalPos = EndPos.localPosition;

        while (elapsedTime < moveDuration)
        {
            elapsedTime += Time.deltaTime;
            float t = elapsedTime / moveDuration;
            Child.transform.localPosition = Vector3.Lerp(startLocalPos, endLocalPos, t);
            yield return null;
        }

        Child.transform.localPosition = endLocalPos;
        isMoving = false;
        hasReachedEnd = true;

        AddRandomValues(); 
    }
    private void AddRandomValues()
    {
        Mat1 += Random.Range(2, 4);
        Mat2 += Random.Range(0, 3);
        Mat3 += Random.Range(0, 1);

        Debug.Log($"Nuevos valores - Mat1: {Mat1}, Mat2: {Mat2}, Mat3: {Mat3}");
    }
}


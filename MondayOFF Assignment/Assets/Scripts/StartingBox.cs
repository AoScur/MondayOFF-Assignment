using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class StartingBox : MonoBehaviour
{
    //드래그
    private bool isDragging = false;
    private Vector3 offset;

    //회전
    private Quaternion startRotation;
    private Quaternion targetRotation;
    private bool isRotated = false;

    public float compareAngle = 3f;
    public float rotationSpeed = 1f;

    private void Awake()
    {
        startRotation = transform.rotation;
        targetRotation = startRotation * Quaternion.Euler(180f, 0f, 0f);
    }

    private void Update()
    {
        if (Input.GetMouseButton(0))
        {
            isDragging = true;
            offset = transform.position - GetMouseWorldPosition();
        }

        if(Input.GetMouseButtonUp(0))
        {
            isDragging = false;
            if (!isRotated)
            {
                isRotated = true;
                StartCoroutine(StartingBoxRotateCoroutine());
            }
        }

        if (isDragging)
        {
            Vector3 targetPosition = offset;
            targetPosition.y = transform.position.y;
            targetPosition.z = transform.position.z;
            transform.position = targetPosition;
        }
    }

    private Vector3 GetMouseWorldPosition()
    {
        Vector3 mousePosition = Input.mousePosition;
        mousePosition.z = Camera.main.nearClipPlane;
        Vector3 correction = new Vector3(1f, 1f, -1f);
        mousePosition = Vector3.Scale(mousePosition, correction);

        return Camera.main.ScreenToWorldPoint(mousePosition);
    }

    public void OnRotateStartingBox()
    {
            StartCoroutine(StartingBoxRotateCoroutine());        
    }

    private IEnumerator StartingBoxRotateCoroutine()
    {
        while (Quaternion.Angle(transform.rotation, targetRotation) > compareAngle)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
            yield return null;
        }

        isRotated = true;
        transform.rotation = targetRotation;
    }
}

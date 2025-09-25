using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuizmoPoint : MonoBehaviour
{
    [SerializeField] private Vector3 startPosition = Vector3.zero;
    [SerializeField] private Vector3 startScale = Vector3.zero;

    [SerializeField] private Vector3 currentPosition = Vector3.zero;
    [SerializeField] private Vector3 currentScale = Vector3.zero;
    [SerializeField] private Vector3 currentRotation = Vector3.zero;
    [SerializeField, Range(0.25f, 1f)] private float radius = 0.5f;

    [SerializeField] private GuizmoPoint targetPosition1;
    [SerializeField] private GuizmoPoint targetPosition2;
    [SerializeField] private GuizmoPoint targetPosition3;


    public Vector3 CurrentPosition => currentPosition;

    private void Start()
    {
        currentPosition = startPosition;
        currentScale = startScale;

        Debug.Log(Mathf.Sin(90 * Mathf.Deg2Rad));
    }

    private void OnEnable()
    {
        //Transformations.Translate += TranslatePoint;

        Transformations.UpdatePosition += UpdatePoint;
        Transformations.Scale += ScalePoint;
        Transformations.Rotate += RotatePoint;
    }

    private void OnDisable()
    {
        Transformations.UpdatePosition -= UpdatePoint;
        Transformations.Scale -= ScalePoint;
        Transformations.Rotate -= RotatePoint;
    }

    private void UpdatePoint(Vector3 centerPosition, Vector3 translateVector)
    {
        currentPosition = Vector3.Scale(currentRotation, currentScale) + centerPosition + translateVector;
    }

    private void TranslatePoint(Vector3 translateVector)
    {
        currentPosition = currentPosition + translateVector;
    }

    private void RotatePoint(Vector3 translateVector)
    {
        currentRotation = RotationInX(translateVector.x) + RotationInY(translateVector.y) + RotationInZ(translateVector.z);
    }

    private void ScalePoint(Vector3 scaleVector)
    {
        currentScale = Vector3.Scale(startScale, scaleVector);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(currentPosition, radius);

        if(targetPosition1 != null || targetPosition2 != null || targetPosition3 != null)
        {
            Gizmos.DrawLine(currentPosition, targetPosition1.CurrentPosition);
            Gizmos.DrawLine(currentPosition, targetPosition2.CurrentPosition);
            Gizmos.DrawLine(currentPosition, targetPosition3.CurrentPosition);
        }
            
    }


    private Vector3 RotationInX(float angle)
    {
        angle = angle * Mathf.Deg2Rad;

        float sin = Mathf.Sin(angle);
        float cos = Mathf.Sin(angle);

        float y = startPosition.y * cos - startPosition.z * sin;
        float z = startPosition.y * sin + startPosition.z * cos;

        return new Vector3(startPosition.x, y, z);
    }

    private Vector3 RotationInY(float angle)
    {
        angle = angle * Mathf.Deg2Rad;

        float sin = Mathf.Sin(angle);
        float cos = Mathf.Sin(angle);

        float x = startPosition.x * cos - startPosition.z * sin;
        float z = startPosition.x * sin + startPosition.z * cos;

        return new Vector3(x, startPosition.y, z);
    }

    private Vector3 RotationInZ(float angle)
    {
        angle = angle * Mathf.Deg2Rad;

        float sin = Mathf.Sin(angle);
        float cos = Mathf.Sin(angle);

        float x = startPosition.x * cos - startPosition.y * sin;
        float y = startPosition.x * sin + startPosition.y * cos;

        return new Vector3(x, y, startPosition.z);
    }
}

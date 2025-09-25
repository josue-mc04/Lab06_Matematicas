using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Transformations : MonoBehaviour
{
    [Header("Start values")]
    [SerializeField] private Vector3 startPosition = Vector3.zero;
    
    [Header("Start values")]
    [SerializeField] private Vector3 currentPosition = Vector3.zero;

    [Header("Transformations")]
    [SerializeField] private Vector3 translationValue = Vector3.zero;
    [SerializeField] private Vector3 rotationValue = Vector3.zero;
    [SerializeField] private Vector3 scaleValue = Vector3.one;

    public static event Action<Vector3> Translate;
    public static event Action<Vector3> Rotate;
    public static event Action<Vector3> Scale;
    
    public static event Action<Vector3, Vector3> UpdatePosition;

    private void Update()
    {
        currentPosition = startPosition + translationValue;
        UpdatePosition?.Invoke(currentPosition, translationValue);
        Scale?.Invoke(scaleValue);
        Rotate?.Invoke(rotationValue);
    }

    [ContextMenu("Update Translate")]
    private void UpdateTranslate()
    {
        Translate?.Invoke(translationValue);
    }
}

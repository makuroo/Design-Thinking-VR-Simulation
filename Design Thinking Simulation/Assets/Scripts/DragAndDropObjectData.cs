using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BNG;

public class DragAndDropObjectData : MonoBehaviour
{
    [SerializeField] private Vector3 initialPos;
    [SerializeField] private Transform originalParent;
    [SerializeField] private Quaternion originalRotation;
    [SerializeField] private Vector3 originalScale;

    private void Start()
    {
        initialPos = transform.localPosition;
        originalParent = transform.parent;
        originalScale = transform.localScale;
        originalRotation = transform.localRotation;
    }

    public void Return(SnapZone snapZone)
    {
        StartCoroutine(ReturnPosition(snapZone));
    }

    private IEnumerator ReturnPosition(SnapZone snapZone)
    {
        yield return new WaitForSeconds(3);
        snapZone.ReleaseAll();
        transform.SetParent(originalParent);
        transform.localPosition = initialPos;
        transform.localRotation = originalRotation;
        transform.localScale = originalScale;
    }
}

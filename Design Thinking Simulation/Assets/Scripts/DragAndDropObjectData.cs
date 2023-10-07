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
    [SerializeField] private Vector3 scaleInSnapzone;

    private void Start()
    {
        initialPos = transform.localPosition;
        originalParent = transform.parent;
        originalScale = transform.localScale;
        originalRotation = transform.localRotation;
    }

    private void Update()
    {
        if (!gameObject.GetComponent<Grabbable>().BeingHeld && (transform.localPosition != initialPos || transform.localRotation != originalRotation || transform.localScale != originalScale) && gameObject.GetComponentInParent<SnapZone>() == null)
        {
            Return();
        }

        if (gameObject.GetComponentInParent<SnapZone>())
        {
            transform.localScale = scaleInSnapzone;
        }
    }

    public void Return(SnapZone snapZone)
    {
        StartCoroutine(ReturnPosition(snapZone));
    }

    public void Return()
    {
        transform.SetParent(originalParent);
        transform.localPosition = initialPos;
        transform.localRotation = originalRotation;
        transform.localScale = originalScale;
        if (GetComponent<SnapZoneOffset>() != null)
            Destroy(GetComponent<SnapZoneOffset>());
    }

    private IEnumerator ReturnPosition(SnapZone snapZone)
    {
        yield return new WaitForSeconds(1);
        snapZone.ReleaseAll();
        transform.SetParent(originalParent);
        transform.localPosition = initialPos;
        transform.localRotation = originalRotation;
        transform.localScale = originalScale;
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuPointer : MonoBehaviour
{
    LineRenderer lr;
    [Rename("Line length")]
    [SerializeField] float maxLen;
    float lineLen;

    public Button currButton { get; private set; }

    void Awake()
    {
        lr = GetComponent<LineRenderer>();
    }

    void OnEnable()
    {
        Vector3[] pointStorage = new Vector3[lr.positionCount];
        lr.GetPositions(pointStorage);
    }

    void FixedUpdate()
    {
        lr.SetPosition(0, Vector3.zero);
        RaycastHit hit;
        int layerMask = 1 << 6;
        if(Physics.Raycast(transform.position, transform.forward, out hit, maxLen, layerMask))
        {
            if(hit.collider && hit.collider.GetComponent<Button>())
            {
                currButton = hit.collider.GetComponent<Button>();
                lineLen = Vector3.Distance(lr.GetPosition(0), transform.InverseTransformPoint(hit.point));
                currButton.Colliding = true;
            }
        }
        else
        {
            lineLen = maxLen;
            if(currButton)
            {
                currButton.Colliding = false;
                currButton = null;
            }
        }
        lr.SetPosition(1, Vector3.forward * lineLen);
    }
}

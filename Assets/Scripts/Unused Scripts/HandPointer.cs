using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandPointer : MonoBehaviour
{
    
    [SerializeField] LineRenderer lr;
    //  public float lineWidth = 0.1f;
    [SerializeField] float maxLength;
    // int layermask = layer number (used as a mask of layers that cannot be interacted with)
    bool canGrab = false;
    GameObject grabbable = null;
 
    void Start()
    {
        Vector3[] initPointerPos = new Vector3[2] {Vector3.zero, Vector3.zero};
        lr.SetPositions(initPointerPos);
        //  lr.SetWidth(lineWidth, lineWidth);
    }
 
    void Update()
    {
        DrawPointer(transform.position, transform.forward, maxLength);
    }
 
    void DrawPointer(Vector3 targetPosition, Vector3 direction, float length)
    {
        Ray ray = new Ray(targetPosition, direction);
        RaycastHit raycastHit;
        Vector3 endPosition = targetPosition + (length * direction);

        if(Physics.Raycast(ray, out raycastHit, length))
        {
            if(!canGrab)
            {
                canGrab = true;
                grabbable = raycastHit.collider.gameObject;
            }
            endPosition = raycastHit.point;
        }
        else
        {
            if(canGrab) {
                canGrab = false;
                grabbable = null;
            }
        }
        lr.SetPosition(0, targetPosition);
        lr.SetPosition(1, endPosition);
    }

    public GameObject GetGrabbableObject()
    {
        return grabbable;
    }
}
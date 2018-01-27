using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandMovement : MonoBehaviour {

    //the layers the ray can hit
    public LayerMask positionLayer;
    public LayerMask grabLayer;

    public Vector3 handOffset;

    public GameObject grabable;

    void Update()
    {
        FindMousePosition();

        if (Input.GetMouseButtonDown(1))
        {
            CheckForGrabable();
        }
        if (Input.GetMouseButtonUp(1))
        {
            grabable = null;
        }

        if(grabable != null)
        {
            grabable.transform.position = new Vector3(transform.position.x, grabable.transform.position.y, transform.position.z);
        }

    }

    public void FindMousePosition()
    {
        Vector3 mouse = Input.mousePosition;
        Ray castPoint = Camera.main.ScreenPointToRay(mouse);
        RaycastHit hit;
        if (Physics.Raycast(castPoint, out hit, Mathf.Infinity, positionLayer))
        {
            if (hit.collider.gameObject.layer == 9)
                transform.position = hit.point + Vector3.up*.5f;
            else
                transform.position = hit.point + handOffset;
        }
    }

    public void CheckForGrabable()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position,Vector3.down, out hit, Mathf.Infinity, grabLayer))
        {
            grabable = hit.collider.gameObject;
        }
    }
}

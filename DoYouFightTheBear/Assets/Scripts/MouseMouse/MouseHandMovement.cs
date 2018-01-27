using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseHandMovement : MonoBehaviour {

    //the layers the ray can hit
    public LayerMask positionLayer;
    public LayerMask grabLayer;

    public Vector3 handOffset;

    public Mouse mouse;

    void Update()
    {
        FindMousePosition();

        if (Input.GetMouseButtonDown(1))
        {
            CheckForGrabable();
        }
        if (Input.GetMouseButtonDown(0))
        {
            if(mouse!= null)
                mouse.Click();
        }
        if (Input.GetMouseButtonUp(1))
        {
            mouse = null;
        }

        if(mouse != null)
        {
            mouse.transform.position = new Vector3(transform.position.x, mouse.transform.position.y, transform.position.z);
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
            mouse = hit.collider.gameObject.GetComponent<Mouse>();
        }
    }
}

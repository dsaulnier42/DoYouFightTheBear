using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class iPadHand : MonoBehaviour {

    //the layers the ray can hit
    public LayerMask positionLayer;
    public LayerMask grabLayer;

    public Vector3 handOffset;

    public GameObject grabbed;
    public Stamp stamp;
    

    void Update()
    {
        FindMousePosition();

        if (Input.GetMouseButtonDown(1))
        {
            CheckForGrabable();
        }
        if (Input.GetMouseButtonDown(0))
        {

        }
        if (Input.GetMouseButtonUp(1))
        {
            grabbed = null;
            stamp = null;
        }

        if (grabbed != null)
        {
            if(stamp != null)
                grabbed.transform.position = new Vector3(transform.position.x + Vector3.left.x, transform.position.y, transform.position.z);
            else
                grabbed.transform.position = new Vector3(transform.position.x, grabbed.transform.position.y, transform.position.z);
        }

    }

    public void FindMousePosition()
    {
        Vector3 mouse = Input.mousePosition;
        Ray castPoint = Camera.main.ScreenPointToRay(mouse);
        RaycastHit hit;
        if (Physics.Raycast(castPoint, out hit, Mathf.Infinity, positionLayer))
        {
            if (hit.collider.gameObject.layer == 9 && stamp == null)
            {
                transform.position = hit.point + Vector3.up * .5f;
                transform.eulerAngles = new Vector3(0, 0, 0);
            }
            else
            {
                transform.position = hit.point + handOffset;
            }
                
        }
    }

    public void CheckForGrabable()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position + Vector3.up*10, Vector3.down, out hit, Mathf.Infinity, grabLayer) 
            || Physics.Raycast(transform.position + Vector3.forward + Vector3.up * 10, Vector3.down, out hit, Mathf.Infinity, grabLayer)
            || Physics.Raycast(transform.position + -Vector3.forward + Vector3.up * 10, Vector3.down, out hit, Mathf.Infinity, grabLayer)
            || Physics.Raycast(transform.position + -Vector3.right + Vector3.up * 10, Vector3.down, out hit, Mathf.Infinity, grabLayer)
            || Physics.Raycast(transform.position + Vector3.right + Vector3.up * 10, Vector3.down, out hit, Mathf.Infinity, grabLayer))
        {
            grabbed = hit.transform.gameObject;
            if (hit.collider.gameObject.layer == 9)
            {
                transform.eulerAngles = new Vector3(0, 0, 0);
            }
            else if (hit.collider.gameObject.layer == 12)
            {
                transform.eulerAngles = new Vector3(0, 0, -90);
                stamp = hit.collider.gameObject.GetComponentInParent<Stamp>();

            }
        }
    }
}

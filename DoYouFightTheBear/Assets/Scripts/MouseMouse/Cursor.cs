using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cursor : MonoBehaviour {

    public float lag;
    public Vector3 startOffset;

    Vector3 start;
    Vector3 targetLocation;

    public Vector2 screenConstraints;
    public LayerMask closeMask;

    public Vector2 screenOffset;

    private void Start()
    {
        start = transform.position + new Vector3(-3, 1);
        targetLocation = start;
    }
	
	// Update is called once per frame
	void Update () {
        if(Vector3.Distance(transform.position,targetLocation) > .2f)
        {
            MovePointer();
        }
    }

    void MovePointer()
    {
        transform.position = Vector3.Lerp(transform.position, targetLocation, Time.deltaTime * lag);
    }

    public void AddToTargetLocation(Vector2 addPos)
    {

        targetLocation.x = Mathf.Clamp(targetLocation.x + addPos.x, -screenConstraints.x, screenConstraints.x);
        //targetLocation.y +=  addPos.y;
        targetLocation.y = Mathf.Clamp(targetLocation.y + addPos.y, -screenConstraints.y + screenOffset.y, screenConstraints.y + screenOffset.y);
    }

    public void CheckForCloseButton()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.forward, out hit, Mathf.Infinity, closeMask))
        {
            hit.transform.gameObject.GetComponentInParent<AD>().Close();
        }
    }
}

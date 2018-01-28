using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoffeeHand : MonoBehaviour {

    public LayerMask positionLayer;
    public LayerMask grabLayer;
    public LayerMask coffeLayer;

    public Vector3 handOffset;
    Vector3 startHandOffset;

    public GameObject grabbed;

    public CoffeManager coffeManager;

    private void Start()
    {
        startHandOffset = handOffset;
    }

    void Update()
    {
        FindMousePosition();

        if (Input.GetMouseButtonDown(1))
        {
            CheckForGrabable();
        }
        if (Input.GetMouseButtonUp(1))
        {
            Dropped();
        }

        if (grabbed != null)
        {
            grabbed.transform.position = new Vector3(transform.position.x + Vector3.left.x, transform.position.y, transform.position.z);
        }

    }

    void Dropped()
    {
        RaycastHit hit;
		if (grabbed != null && Physics.Raycast(transform.position + Vector3.up * 50, Vector3.down, out hit, Mathf.Infinity, coffeLayer))
        {
            coffeManager.SetCountdown(true);
			//grabbed.SendMessage ("TurnOnPartilces");
        }
		grabbed = null;
    }

    public void FindMousePosition()
    {
        Vector3 mouse = Input.mousePosition;
        Ray castPoint = Camera.main.ScreenPointToRay(mouse);
        RaycastHit hit;
        if (Physics.Raycast(castPoint, out hit, Mathf.Infinity, positionLayer))
        {
            if (hit.collider.gameObject.layer == 9)
            {
                transform.position = hit.point + Vector3.up * .5f;
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
        if (Physics.Raycast(transform.position + Vector3.up * 50, Vector3.down, out hit, Mathf.Infinity, grabLayer))
        {
            grabbed = hit.collider.gameObject;
            coffeManager.SetCountdown(false);
        }
    }
}

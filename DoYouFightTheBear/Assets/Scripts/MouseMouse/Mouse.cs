using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mouse : MonoBehaviour {

    public Cursor cursor;
    public Vector2 moveSpeed;

    public LayerMask mousePad;

    Vector3 lastPos;

    Vector3 start;

    // Update is called once per frame
    void Update () {

        Vector3 moved = transform.position - lastPos;

        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.down, out hit, Mathf.Infinity, mousePad))
        {
            cursor.AddToTargetLocation(new Vector2(moved.x * moveSpeed.x, moved.z * moveSpeed.y));
        }


        lastPos = transform.position;
    }

    public void Click()
    {
        cursor.CheckForCloseButton();
    }
}

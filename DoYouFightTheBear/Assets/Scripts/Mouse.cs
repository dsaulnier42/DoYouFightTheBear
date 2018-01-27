using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mouse : MonoBehaviour {

    public Transform pointer;

    Vector3 start;

    private void Start()
    {
        start = pointer.transform.position + new Vector3(-3,1);
    }

    // Update is called once per frame
    void Update () {
        pointer.transform.position = new Vector3(start.x + transform.position.x * .7f, start.y + transform.position.z * .7f, pointer.transform.position.z);
	}
}

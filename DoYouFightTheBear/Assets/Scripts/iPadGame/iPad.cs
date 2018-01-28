using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class iPad : MonoBehaviour {

    public bool marked;
    iPadManager iPadManager;
    public LayerMask dropLayer;

    public void Setup(iPadManager iPadManager)
    {
        this.iPadManager = iPadManager;
        SetRandomPosistion();
    }

    void SetRandomPosistion()
    {
        transform.position = new Vector3(Random.Range(-8f, 8f), 5 ,Random.Range(-8f, 8f));
    }


    public void Dropped()
    {
       
        RaycastHit hit;
        if (Physics.Raycast(transform.position + Vector3.up*50, Vector3.down, out hit, Mathf.Infinity, dropLayer) 
            || Physics.Raycast(transform.position + Vector3.forward + Vector3.up * 10, Vector3.down, out hit, Mathf.Infinity, dropLayer)
            || Physics.Raycast(transform.position + -Vector3.forward + Vector3.up * 10, Vector3.down, out hit, Mathf.Infinity, dropLayer)
            || Physics.Raycast(transform.position + -Vector3.right + Vector3.up * 10, Vector3.down, out hit, Mathf.Infinity, dropLayer)
            || Physics.Raycast(transform.position + Vector3.right + Vector3.up * 10, Vector3.down, out hit, Mathf.Infinity, dropLayer))
        {
            if (marked)
            {
                iPadManager.Stacked();
            }
        }
    }




}

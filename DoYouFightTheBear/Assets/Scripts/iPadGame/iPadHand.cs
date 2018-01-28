using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class iPadHand : MonoBehaviour {

    //the layers the ray can hit
    public LayerMask positionLayer;
    public LayerMask grabLayer;
    public LayerMask iPadLayer;

    public Vector3 handOffset;
    Vector3 startHandOffset;
    public GameObject grabbed;
    public Stamp stamp;

    public Material stampedMat;

	public AudioClip stampSound;
	private AudioSource source;

    bool stamping;

    private void Start()
    {
        startHandOffset = handOffset;
		source = GetComponent<AudioSource>();
    }

    void Update()
    {
        FindMousePosition();

        if (Input.GetMouseButtonDown(1))
        {
            CheckForGrabable();
        }
        if (Input.GetMouseButtonDown(0)&& stamp != null)
        {
            StampDown();

        }
        if (Input.GetMouseButtonUp(1))
        {
            if (stamp == null && grabbed != null)
                grabbed.GetComponent<iPad>().Dropped();
            grabbed = null;
            if (stamp != null)
                stamp.gameObject.transform.position = new Vector3(stamp.gameObject.transform.position.x, 3, stamp.gameObject.transform.position.z);
            stamp = null;
            
            transform.eulerAngles = new Vector3(0, 0, 0);
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

    void StampDown()
    {
        StartCoroutine(Stamping());

    }

    IEnumerator Stamping()
    {
        Renderer iPad =new Renderer();
        RaycastHit hit;

        if (Physics.Raycast(transform.position + Vector3.up, Vector3.down, out hit, Mathf.Infinity, iPadLayer))
        {
            iPad = hit.transform.gameObject.GetComponent<Renderer>();
            hit.transform.gameObject.GetComponent<iPad>().marked = true;
			source.PlayOneShot(stampSound);
        }
        while (handOffset.y > 2)
        {
            handOffset.y -= 10f * Time.deltaTime;
            yield return null;
        }
        handOffset = startHandOffset;
        if(iPad !=null)
            iPad.material = stampedMat;
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

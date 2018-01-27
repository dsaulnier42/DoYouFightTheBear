using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextBubble : MonoBehaviour
{

	RectTransform m_rectTransform;
	public RectTransform endPoint;
	Vector3 showPos, hidePos, moveBy;
	public float slideSpeed = 1, showForSeconds = 1;

	void Start ()
	{
		m_rectTransform = GetComponent<RectTransform> ();
		showPos = transform.position;
		hidePos = endPoint.transform.position;
		transform.position = hidePos;
		StartCoroutine (slideSomwhere ());
	}

	IEnumerator slideSomwhere ()
	{
		moveBy = (hidePos - showPos).normalized * slideSpeed;

		while (transform.position.y > showPos.y) {
			m_rectTransform.position -= moveBy;
			yield return null;
		}

		yield return new WaitForSeconds (showForSeconds);

		while (transform.position.y < hidePos.y) {
			m_rectTransform.position += moveBy;
			yield return null;
		}
	}
}

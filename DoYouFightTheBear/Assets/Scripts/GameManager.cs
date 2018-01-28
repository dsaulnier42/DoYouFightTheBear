using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
	void Update ()
	{
		if (Input.GetKeyDown (KeyCode.Alpha8))
			StartShowdown ();
	}

	public void StartShowdown ()
	{
		if (GODSEND.GameSceneManager.GSM is GODSEND.GameSceneManager)
			GODSEND.GameSceneManager.GSM.AddCustomSceneDirect ();
		else
			Debug.LogWarning ("Call for GameSceneManager to load level, but the GSM doesn't esist.");
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {



	void Start () {
		
	}

	void Update () {
		if (Input.GetKeyDown (KeyCode.Alpha8))
			StartShowdown ();
	}

	void StartShowdown(){
		GODSEND.GameSceneManager.GSM.AddCustomSceneDirect ();
	}
}

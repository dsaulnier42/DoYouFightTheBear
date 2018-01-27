using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GODSEND;

public class MainMenu : MonoBehaviour {

	public void LoadRandomMinigame(){
		if(GameSceneManager.GSM is GameSceneManager)
		GameSceneManager.GSM.LoadSpecificGameplayLevel (true);
	}

	public void QuitGame(){
		if(Application.isEditor){
			Debug.Break ();
		} else{
			Application.Quit ();
		}
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowdownManager : MonoBehaviour {

	public GameObject KillBearScreen, HugBearScreen;

	void Awake () {
		KillBearScreen.SetActive (false);
		HugBearScreen.SetActive (false);
	}

	void Start(){
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void FightBear(){
		KillBearScreen.SetActive (true);
	}

	public void DontFight(){
		GODSEND.GameSceneManager.GSM.UnloadLastScene ();
		GODSEND.GameSceneManager.GSM.LoadSpecificGameplayLevel (true);
	}

	public void BefriendBear(){
		HugBearScreen.SetActive (true);
	}
}

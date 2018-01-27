using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using GODSEND;

public class ShowdownManager : MonoBehaviour
{

	public GameObject DecisionScreen, KillBearScreen, HugBearScreen;

	void Awake ()
	{
		KillBearScreen.SetActive (false);
		HugBearScreen.SetActive (false);
	}

	void Start ()
	{
		
	}

	public void FightBear ()
	{
		DecisionScreen.SetActive (false);
		KillBearScreen.SetActive (true);
	}

	public void DontFight ()
	{
		if (GameSceneManager.GSM is GODSEND.GameSceneManager) {
			GameSceneManager.GSM.UnloadLastScene ();
			GameSceneManager.GSM.LoadSpecificGameplayLevel (true);
		} else
			DecisionScreen.SetActive (false);
	}

	public void BefriendBear ()
	{
		DecisionScreen.SetActive (false);
		HugBearScreen.SetActive (true);
	}

	public void ReturnToMenu ()
	{
		if (GameSceneManager.GSM is GODSEND.GameSceneManager) {
			GameSceneManager.GSM.UnloadLastScene ();
			GameSceneManager.GSM.QuitToMenuInGame ();
		}
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace GODSEND
{
	public class GameSceneManager : MonoBehaviour
	{
		public static GameSceneManager GSM;
		public LevelSceneReferences LevelSceneAsset;
		SceneInfo CurrentScene;
		List<int> LoadedSceneIndicies = new List<int> ();

		public KeyCode PauseKey = KeyCode.Escape;

		public bool InGame { get; private set; }

		public static bool GamePaused { get; private set; }

		public static bool HelpShowing { get; private set; }

		public static bool MainMenuShowing { get; private set; }

		public delegate void UIEvent ();

		public static event UIEvent TogglePauseScreen;

		private void Awake ()
		{
			if (GSM != null) {
				Debug.LogWarning ("Duplicate level manager removed", gameObject);
				this.enabled = false;
			} else {
				GSM = this;
				DontDestroyOnLoad (this.transform);
			}
		}

		private void Start ()
		{
			if (LevelSceneAsset == null)
				Debug.LogError ("Scene management asset not assigned to game scene manager.");

			CurrentScene = ValidateScene (LevelSceneAsset.GameplayScenes [0]);

			if (CurrentScene.BuildIndex < 1)
				for (int i = 0; i < LevelSceneAsset.GameplayScenes.Count; i++) {
					if (LevelSceneAsset.GameplayScenes [i].BuildIndex > 0) {
						CurrentScene = ValidateScene (LevelSceneAsset.GameplayScenes [i]);
						break;
					}
				}

			SceneManager.LoadScene (LevelSceneAsset.MenuScene.SceneName);
			LoadedSceneIndicies.Add (SceneManager.GetSceneByName (LevelSceneAsset.MenuScene.SceneName).buildIndex);
		}

		SceneInfo ValidateScene (SceneInfo checkScene)
		{
			if (checkScene.BuildIndex > 0)
				return checkScene;
			else
				return LevelSceneAsset.MenuScene;
		}


		public void ClosePauseMenuButton ()
		{
			Time.timeScale = 1;
			GamePaused = false;
			if (TogglePauseScreen != null)
				TogglePauseScreen ();
		}

		/*
        *
        * Load scenes
        * 
        */

		public void LoadSpecificGameplayLevel (bool loadRandom = false)
		{
			if (loadRandom)
				LoadSpecificGameplayLevel (Random.Range (0, LevelSceneAsset.GameplayScenes.Count));
			else
				LoadSpecificGameplayLevel (0);
		}

		public void LoadSpecificGameplayLevel (int levelIndexInArray)
		{
			if (levelIndexInArray < 0 || levelIndexInArray > LevelSceneAsset.GameplayScenes.Count)
				return;

			CurrentScene = ValidateScene (LevelSceneAsset.GameplayScenes [levelIndexInArray]);
			StartCoroutine (LoadYourAsyncSceneWithLoadScreen (CurrentScene.SceneName, true));
		}

		#region LoadScenesAsyncronously

		IEnumerator LoadYourAsyncSceneWithLoadScreen (string sceneName, bool loadingGameLevel)
		{
			//Coroutine sourced from http://blog.teamtreehouse.com/make-loading-screen-unity
			// The Application loads the Scene in the background at the same time as the current Scene.
			//This is particularly good for creating loading screens. You could also load the scene by build //number.

			//display load screen and save reference
			yield return SceneManager.LoadSceneAsync (LevelSceneAsset.LoadingScene.SceneName, LoadSceneMode.Additive);
			LoadedSceneIndicies.Add (SceneManager.GetSceneByName (LevelSceneAsset.LoadingScene.SceneName).buildIndex);

			//unload old scene and remove reference
			yield return SceneManager.UnloadSceneAsync (SceneManager.GetSceneByBuildIndex (LoadedSceneIndicies [0]));
			LoadedSceneIndicies.RemoveAt (0);

			//load new scene and add reference
			yield return SceneManager.LoadSceneAsync (sceneName, LoadSceneMode.Additive);
			LoadedSceneIndicies.Add (SceneManager.GetSceneByName (sceneName).buildIndex);

			//hide load screen and remove reference
			yield return SceneManager.UnloadSceneAsync (LevelSceneAsset.LoadingScene.SceneName);
			LoadedSceneIndicies.RemoveAt (0);

			InGame = loadingGameLevel;
		}

		public void AddCustomSceneDirect ()
		{
			StartCoroutine (LoadYourAsyncScene (ValidateScene (LevelSceneAsset.c_ShowdownScene).SceneName));
		}

		//load scenes without a loading screen
		public void AddSceneDirect (string sceneName)
		{
			StartCoroutine (LoadYourAsyncScene (sceneName));
		}

		IEnumerator LoadYourAsyncScene (string sceneName)
		{
			//Coroutine sourced from http://blog.teamtreehouse.com/make-loading-screen-unity
			// The Application loads the Scene in the background at the same time as the current Scene.
			//This is particularly good for creating loading screens. You could also load the scene by build //number.
			yield return SceneManager.LoadSceneAsync (sceneName, LoadSceneMode.Additive);
			LoadedSceneIndicies.Add (SceneManager.GetSceneByName (sceneName).buildIndex);
		}

		#endregion

		/*
         *
         * Unload scenes
         * 
         */

		//out of the list of currently loaded scenes, unload the most recently loaded one
		public void UnloadLastScene ()
		{
			if (LoadedSceneIndicies.Count > 0)
				DropScene (SceneManager.GetSceneByBuildIndex (LoadedSceneIndicies [LoadedSceneIndicies.Count - 1]));
			else
				Debug.Log ("idk fam");
		}

		#region Unload Scenes Asyncronisously

		public void DropCustomSceneDirect ()
		{
			DropScene ((ValidateScene (LevelSceneAsset.c_ShowdownScene).SceneName));
		}

		//start async load via coroutine (overload method for Unload Scene, so you can reference via Scene name instead of scene reference)
		public void DropScene (string sceneName)
		{
			DropScene (SceneManager.GetSceneByName (sceneName));
		}

		public void DropScene (Scene sceneName)
		{
			StartCoroutine (UnLoadYourAsyncScene (sceneName));
		}

		IEnumerator UnLoadYourAsyncScene (Scene sceneName)
		{
			//Coroutine sourced from http://blog.teamtreehouse.com/make-loading-screen-unity
			// The Application loads the Scene in the background at the same time as the current Scene.
			//This is particularly good for creating loading screens. You could also load the scene by build //number.
			yield return SceneManager.UnloadSceneAsync (sceneName);
			LoadedSceneIndicies.RemoveAt (LoadedSceneIndicies.Count - 1);
		}

		#endregion

		public void ShowSettingsScreenInGame ()
		{
			MainMenuShowing = true;
			AddSceneDirect (LevelSceneAsset.MenuScene.SceneName);
		}

		public void CloseMainMenuInGame (GameObject otherwiseHideThis)
		{
			if (InGame) {
				DropScene (LevelSceneAsset.MenuScene.SceneName);
				MainMenuShowing = false;
			} else {
				otherwiseHideThis.SetActive (false);
			}
		}

		public void QuitToMenuInGame ()
		{
			GamePaused = false;
			Time.timeScale = 1;
			CurrentScene = ValidateScene (LevelSceneAsset.MenuScene);
			StartCoroutine (LoadYourAsyncSceneWithLoadScreen (CurrentScene.SceneName, false));
		}
	}
}



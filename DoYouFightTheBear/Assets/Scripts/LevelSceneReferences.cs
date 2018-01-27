using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SceneReferenceAsset", menuName = "Scriptable Objects/SceneReferenceAsset")]
public class LevelSceneReferences : ScriptableObject {
    public List<string> ScenesInBuildSettings = new List<string>();
    [HideInInspector]
    public SceneInfo IndexScene;
    public SceneInfo MenuScene, LoadingScene;
	public SceneInfo c_ShowdownScene;
    public int DefaultGameplayIndex;
    public List<SceneInfo> GameplayScenes = new List<SceneInfo>();
}

[System.Serializable]
public class SceneInfo
{
    [HideInInspector]
    public string ElementName;
    public string SceneName;
    public int BuildIndex;

    public SceneInfo(string name)
    {
        SceneName = name;
        BuildIndex = -1;
    }
}
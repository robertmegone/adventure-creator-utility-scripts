using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;
using System.IO;
using AC;

//Check cutscenes across the project for any that do not have autosave enabled afterwards.

public class CheckCutsceneAutosave : EditorWindow
{
    [MenuItem("Adventure Creator/Check Cutscene Autosave")]
    public static void ShowWindow()
    {
        EditorWindow.GetWindow(typeof(CheckCutsceneAutosave));
    }

    void OnGUI()
    {
        GUILayout.Label("Check Cutscene Autosave", EditorStyles.boldLabel);
        if (GUILayout.Button("Check All Scenes"))
        {
            CheckAllScenes();
        }
    }

    void CheckAllScenes()
    {
        string[] scenes = AssetDatabase.FindAssets("t:Scene");
        string logPath = Path.Combine(Application.dataPath, "autosave_logs.txt");
        StreamWriter writer = new StreamWriter(logPath);
        foreach (string scene in scenes)
        {
            string path = AssetDatabase.GUIDToAssetPath(scene);
            EditorUtility.DisplayProgressBar("Checking Scenes", "Checking " + path, (float)System.Array.IndexOf(scenes, scene) / scenes.Length);
            bool sceneHasAutosave = CheckScene(path, writer);
            if (!sceneHasAutosave)
            {
                Debug.Log("Scene " + path + " does not have Autosave after? enabled");
            }
        }
        EditorUtility.ClearProgressBar();
        EditorUtility.DisplayDialog("Done", "All scenes have been checked. Check the autosave_logs.txt file for details.", "OK");
        writer.Close();
    }

    bool CheckScene(string scenePath, StreamWriter writer)
    {
        SceneAsset sceneAsset = AssetDatabase.LoadAssetAtPath<SceneAsset>(scenePath);
        if (sceneAsset != null)
        {
            string sceneName = sceneAsset.name;
            string sceneFullPath = AssetDatabase.GetAssetPath(sceneAsset);
            EditorSceneManager.OpenScene(sceneFullPath, OpenSceneMode.Single);
            Cutscene[] cutscenes = FindObjectsOfType<Cutscene>();
            bool sceneHasAutosave = false;
            foreach (Cutscene cutscene in cutscenes)
            {
                if (cutscene.name == "OnStart")
                {
                    bool autosaveAfter = cutscene.autosaveAfter;
                    if (autosaveAfter)
                    {
                        writer.WriteLine("Cutscene " + cutscene.name + " in scene " + sceneName + " (" + scenePath + ") has Autosave after? enabled");
                        sceneHasAutosave = true;
                    }
                }
            }
            EditorSceneManager.SaveOpenScenes();
            EditorSceneManager.CloseScene(EditorSceneManager.GetActiveScene(), true);
            return sceneHasAutosave;
        }
        return false;
    }
}

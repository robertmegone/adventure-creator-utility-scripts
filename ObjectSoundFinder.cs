using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine.SceneManagement;
using System.IO;

// Find sounds that are not in the sound manager

public class ObjectSoundFinder : EditorWindow
{
    [MenuItem("Window/Object Sound Finder")]
    public static void ShowWindow()
    {
        EditorWindow.GetWindow(typeof(ObjectSoundFinder));
    }

    void OnGUI()
    {
        if (GUILayout.Button("Find Object Sounds"))
        {
            FindObjectSounds();
        }
    }

    private static void FindObjectSounds()
    {
        string[] scenePaths = AssetDatabase.GetAllAssetPaths();
        string logFilePath = "Assets/ObjectSound.log";

        // Clear the log file if it already exists
        if (File.Exists(logFilePath))
        {
            File.Delete(logFilePath);
        }

        foreach (string scenePath in scenePaths)
        {
            if (scenePath.EndsWith(".unity"))
            {
                Scene scene = EditorSceneManager.OpenScene(scenePath, OpenSceneMode.Single);
                GameObject[] allObjects = Resources.FindObjectsOfTypeAll<GameObject>();

                foreach (GameObject go in allObjects)
                {
                    if (PrefabUtility.GetPrefabAssetType(go) == PrefabAssetType.NotAPrefab)
                    {
                        if (go.GetComponent<ObjectSound>() != null)
                        {
                            string logMessage = "Found Object Sound on GameObject '" + go.name + "' in scene '" + scene.name + "'";
                            Debug.Log(logMessage);
                            File.AppendAllText(logFilePath, logMessage + "\n");
                        }
                    }
                }

                EditorSceneManager.CloseScene(scene, true);
            }
        }

        Debug.Log("Object Sound search complete. Results saved to: " + logFilePath);
    }
}

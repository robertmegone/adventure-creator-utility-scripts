using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine.SceneManagement;
using AC;
using System.IO;

// Search scenes for action lists where a hotspot is renamed

public class FindActionRename : EditorWindow
{
    [MenuItem("Adventure Creator/Find ActionRename")]
    static void Init()
    {
        FindActionRename window = (FindActionRename)EditorWindow.GetWindow(typeof(FindActionRename));
        window.Show();
    }

    void OnGUI()
    {
        if (GUILayout.Button("Search"))
        {
            Search();
        }
    }

    void Search()
    {
        string[] guids = AssetDatabase.FindAssets("t:Scene");
        foreach (string guid in guids)
        {
            string path = AssetDatabase.GUIDToAssetPath(guid);
            SceneAsset sceneAsset = AssetDatabase.LoadAssetAtPath<SceneAsset>(path);
            Scene scene = EditorSceneManager.OpenScene(path, OpenSceneMode.Single);
            ActionList[] actionLists = FindObjectsOfType<ActionList>();
            foreach (ActionList actionList in actionLists)
            {
                foreach (AC.Action action in actionList.actions)
                {
                    if (action is ActionRename)
                    {
                        ActionRename actionRename = (ActionRename)action;
                        string newName = actionRename.newName;
                        Debug.Log("Found ActionRename in " + actionList.gameObject.name + " in scene " + sceneAsset.name + " with new name: " + newName);
                        using (StreamWriter writer = new StreamWriter("hotspotrename.txt", true))
                        {
                            writer.WriteLine("Found ActionRename in " + actionList.gameObject.name + " in scene " + sceneAsset.name + " with new name: " + newName);
                        }
                    }
                }
            }
            EditorSceneManager.CloseScene(scene, true);
        }
    }
}

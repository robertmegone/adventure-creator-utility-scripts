using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine.SceneManagement;
using AC;
using System.IO;

// Find scenes with actionlists that have dialog options renamed.

public class FindDialogOptionRename : EditorWindow
{
    [MenuItem("Adventure Creator/Find DialogOptionRename")]
    static void Init()
    {
        FindDialogOptionRename window = (FindDialogOptionRename)EditorWindow.GetWindow(typeof(FindDialogOptionRename));
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
                    if (action is ActionDialogOptionRename)
                    {
                        ActionDialogOptionRename actionRename = (ActionDialogOptionRename)action;
                        string newName = actionRename.newLabel;
                        Debug.Log("Found ActionDialogOptionRename in " + actionList.gameObject.name + " in scene " + sceneAsset.name + " with new label: " + newName);
                        using (StreamWriter writer = new StreamWriter("dialogoptionrename.txt", true))
                        {
                            writer.WriteLine("Found ActionDialogOptionRename in " + actionList.gameObject.name + " in scene " + sceneAsset.name + " with new label: " + newName);
                        }
                    }
                }
            }
            EditorSceneManager.CloseScene(scene, true);
        }
    }
}

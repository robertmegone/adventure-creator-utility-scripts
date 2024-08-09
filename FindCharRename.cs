using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine.SceneManagement;
using AC;
using System.IO;

//Find scenes with actionlists that rename a character

public class FindCharRename : EditorWindow
{
    [MenuItem("Adventure Creator/Find CharRename")]
    static void Init()
    {
        FindCharRename window = (FindCharRename)EditorWindow.GetWindow(typeof(FindCharRename));
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
                    if (action is ActionCharRename)
                    {
                        ActionCharRename charRename = (ActionCharRename)action;
                        string newName = charRename.newName;
                        Debug.Log("Found ActionCharRename in " + actionList.gameObject.name + " in scene " + sceneAsset.name + " with new name: " + newName);
                        using (StreamWriter writer = new StreamWriter("charrename.txt", true))
                        {
                            writer.WriteLine("Found ActionCharRename in " + actionList.gameObject.name + " in scene " + sceneAsset.name + " with new name: " + newName);
                        }
                    }
                }
            }
            EditorSceneManager.CloseScene(scene, true);
        }
    }
}

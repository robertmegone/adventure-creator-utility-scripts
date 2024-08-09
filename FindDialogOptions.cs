using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;
using AC;

public class FindDialogOptions : EditorWindow
{
    [MenuItem("Adventure Creator/Find Dialog Options")]
    static void Init()
    {
        FindDialogOptions window = (FindDialogOptions)EditorWindow.GetWindow(typeof(FindDialogOptions));
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
            AC.Conversation[] conversations = FindObjectsOfType<AC.Conversation>();
            foreach (AC.Conversation conversation in conversations)
            {
                foreach (ButtonDialog dialogOption in conversation.options)
                {
                    string label = dialogOption.label;
                    Debug.Log("Found Dialog Option " + "* " + label + " *" + " in Conversation " + conversation.gameObject.name + " in scene " + sceneAsset.name);
                    using (StreamWriter writer = new StreamWriter("dialogoptions.txt", true))
                    {
                        writer.WriteLine("Found Dialog Option " + label + " in Conversation " + conversation.gameObject.name + " in scene " + sceneAsset.name);
                    }
                }
            }
            EditorSceneManager.CloseScene(scene, true);
        }
    }
}

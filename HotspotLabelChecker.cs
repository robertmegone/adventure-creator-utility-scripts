using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;
using System.IO;
using System.Collections.Generic;
using AC;

// Search scenes and log hotspot labels.

public class HotspotLabelChecker : EditorWindow
{
    [MenuItem("Tools/Hotspot Label Checker")]
    public static void ShowWindow()
    {
        EditorWindow.GetWindow(typeof(HotspotLabelChecker));
    }

    void OnGUI()
    {
        GUILayout.Label("Hotspot Label Checker", EditorStyles.boldLabel);
        GUILayout.Space(20);

        if (GUILayout.Button("Check Hotspot Labels"))
        {
            CheckHotspotLabels();
        }
    }

    private void CheckHotspotLabels()
    {
        string[] sceneGuids = AssetDatabase.FindAssets("t:Scene");

        using (StreamWriter writer = new StreamWriter("HotspotLabels.txt"))
        {
            foreach (string guid in sceneGuids)
            {
                string scenePath = AssetDatabase.GUIDToAssetPath(guid);
                SceneAsset scene = AssetDatabase.LoadAssetAtPath<SceneAsset>(scenePath);

                if (scene == null)
                {
                    continue;
                }

                EditorSceneManager.OpenScene(scenePath);

                Hotspot[] hotspots = Object.FindObjectsOfType<Hotspot>();

                foreach (Hotspot hotspot in hotspots)
                {
                    string hotspotName = hotspot.name;
                    string hotspotLabel = hotspot.hotspotName;

                    writer.WriteLine($"Scene: {scenePath}, Hotspot Name: {hotspotName}, Hotspot Label: {hotspotLabel}");
                }
            }
        }

        Debug.Log("Hotspot labels checked.");
    }
}

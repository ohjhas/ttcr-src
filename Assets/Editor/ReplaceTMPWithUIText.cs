using UnityEngine;
using UnityEditor;
using UnityEngine.UI;

#if UNITY_EDITOR
public class ReplaceTMPWithUIText : EditorWindow
{
    private Font _font;
    private int _replacedCount = 0;

    [MenuItem("Tools/Replace TMP with UI Text")]
    public static void ShowWindow()
    {
        GetWindow<ReplaceTMPWithUIText>("Replace TMP");
    }

    void OnGUI()
    {
        GUILayout.Label("Replace TextMeshProUGUI with UI Text", EditorStyles.boldLabel);
        GUILayout.Space(5);

        _font = (Font)EditorGUILayout.ObjectField("GROBOLD Font", _font, typeof(Font), false);

        GUILayout.Space(5);
        EditorGUILayout.HelpBox(
            "Replaces ALL TextMeshProUGUI in loaded scenes + prefabs under Assets/Resources/ui/.\nMake backups first.",
            MessageType.Warning);
        GUILayout.Space(10);

        if (GUILayout.Button("Run Replacement"))
        {
            if (_font == null)
                _font = AssetDatabase.LoadAssetAtPath<Font>("Assets/Font/GROBOLD.ttf");

            if (_font == null)
            {
                EditorUtility.DisplayDialog("Error",
                    "Could not find GROBOLD.ttf at Assets/Font/GROBOLD.ttf.\nPlease assign it in the Font field.",
                    "OK");
                return;
            }

            RunReplacement();
        }
    }

    void RunReplacement()
    {
        _replacedCount = 0;

        // --- 1. Process all GameObjects in currently loaded scenes ---
        GameObject[] allObjects = Resources.FindObjectsOfTypeAll<GameObject>();
        foreach (GameObject go in allObjects)
        {
            if (!go.scene.IsValid()) continue;
            ProcessGameObject(go);
        }

        // Mark all open scenes dirty so changes save
        for (int i = 0; i < UnityEditor.SceneManagement.EditorSceneManager.sceneCount; i++)
        {
            var scene = UnityEditor.SceneManagement.EditorSceneManager.GetSceneAt(i);
            if (scene.isLoaded)
                UnityEditor.SceneManagement.EditorSceneManager.MarkSceneDirty(scene);
        }

        // --- 2. Process prefabs under Assets/Resources/ui/ ---
        string[] guids = AssetDatabase.FindAssets("t:Prefab", new string[] { "Assets/Resources/ui" });
        foreach (string guid in guids)
        {
            string path = AssetDatabase.GUIDToAssetPath(guid);
            GameObject prefab = AssetDatabase.LoadAssetAtPath<GameObject>(path);
            if (prefab == null) continue;

            // Instantiate a temporary copy, modify it, then write back
            GameObject instance = PrefabUtility.InstantiatePrefab(prefab) as GameObject;
            if (instance == null) continue;

            bool changed = false;
            Transform[] transforms = instance.GetComponentsInChildren<Transform>(true);
            foreach (Transform t in transforms)
            {
                if (ProcessGameObject(t.gameObject))
                    changed = true;
            }

            if (changed)
            {
                // Write changes back to the prefab asset directly
                #pragma warning disable 0618
                PrefabUtility.ReplacePrefab(instance, prefab, ReplacePrefabOptions.ReplaceNameBased);
                #pragma warning restore 0618
                EditorUtility.SetDirty(prefab);
            }

            Object.DestroyImmediate(instance);
        }

        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();

        EditorUtility.DisplayDialog("Done",
            "Replaced " + _replacedCount + " TextMeshProUGUI components with UI Text.\n\nRemember to save your open scenes (Ctrl+S).",
            "OK");
    }

    // Returns true if a replacement was made
    bool ProcessGameObject(GameObject go)
    {
        Component[] comps = go.GetComponents<Component>();
        foreach (Component comp in comps)
        {
            if (comp == null) continue;
            if (comp.GetType().Name != "TextMeshProUGUI") continue;

            System.Type t = comp.GetType();

            string text    = GetProp<string>(t, comp, "text", "");
            float fontSize = GetProp<float>(t, comp, "fontSize", 24f);
            Color color    = GetProp<Color>(t, comp, "color", Color.white);
            bool autoSize  = GetProp<bool>(t, comp, "enableAutoSizing", false);
            float minSize  = GetProp<float>(t, comp, "fontSizeMin", 10f);
            float maxSize  = GetProp<float>(t, comp, "fontSizeMax", fontSize);
            TextAnchor anchor = ReadAlignment(t, comp);

            Undo.DestroyObjectImmediate(comp);

            Text ui = Undo.AddComponent<Text>(go);
            ui.text                  = text;
            ui.font                  = _font;
            ui.fontSize              = Mathf.RoundToInt(fontSize);
            ui.color                 = color;
            ui.alignment             = anchor;
            ui.resizeTextForBestFit  = autoSize;
            ui.resizeTextMinSize     = Mathf.RoundToInt(minSize);
            ui.resizeTextMaxSize     = Mathf.RoundToInt(maxSize);
            ui.horizontalOverflow    = HorizontalWrapMode.Overflow;
            ui.verticalOverflow      = VerticalWrapMode.Overflow;
            ui.raycastTarget         = false;
            ui.supportRichText       = true;

            UpdateLocalizeTarget(go, ui);

            EditorUtility.SetDirty(go);
            _replacedCount++;
            return true;
        }
        return false;
    }

    T GetProp<T>(System.Type type, object obj, string propName, T fallback)
    {
        try
        {
            var prop = type.GetProperty(propName);
            if (prop != null)
                return (T)System.Convert.ChangeType(prop.GetValue(obj, null), typeof(T));
        }
        catch {}
        return fallback;
    }

    TextAnchor ReadAlignment(System.Type type, object obj)
    {
        try
        {
            var prop = type.GetProperty("alignment");
            if (prop == null) return TextAnchor.MiddleCenter;
            int v = (int)prop.GetValue(obj, null);
            bool left   = (v & 1)    != 0;
            bool center = (v & 2)    != 0;
            bool right  = (v & 4)    != 0;
            bool top    = (v & 256)  != 0;
            bool middle = (v & 512)  != 0;
            bool bottom = (v & 1024) != 0;

            if (top    && left)   return TextAnchor.UpperLeft;
            if (top    && right)  return TextAnchor.UpperRight;
            if (top)              return TextAnchor.UpperCenter;
            if (bottom && left)   return TextAnchor.LowerLeft;
            if (bottom && right)  return TextAnchor.LowerRight;
            if (bottom)           return TextAnchor.LowerCenter;
            if (middle && left)   return TextAnchor.MiddleLeft;
            if (middle && right)  return TextAnchor.MiddleRight;
        }
        catch {}
        return TextAnchor.MiddleCenter;
    }

    void UpdateLocalizeTarget(GameObject go, Text newText)
    {
        foreach (Component comp in go.GetComponents<Component>())
        {
            if (comp == null) continue;
            if (comp.GetType().Name != "Localize") continue;

            System.Type lt = comp.GetType();
            var flags = System.Reflection.BindingFlags.Public |
                        System.Reflection.BindingFlags.NonPublic |
                        System.Reflection.BindingFlags.Instance;

            var field = lt.GetField("mTarget", flags);
            if (field != null) field.SetValue(comp, newText);

            var prop = lt.GetProperty("Target", flags);
            if (prop != null && prop.CanWrite) prop.SetValue(comp, newText, null);

            EditorUtility.SetDirty(comp);
        }
    }
}
#endif

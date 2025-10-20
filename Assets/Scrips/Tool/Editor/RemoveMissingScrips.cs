using UnityEngine;
using UnityEditor;
using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;

public class RemoveMissingScriptsTool : OdinEditorWindow
{
    [MenuItem("Tools/Odin/Remove Missing Scripts")]
    private static void OpenWindow()
    {
        GetWindow<RemoveMissingScriptsTool>("Remove Missing Scripts").Show();
    }

    [InfoBox("🧹 Tool này giúp bạn xóa tất cả script bị Missing trong các Prefab, Scene hoặc thư mục Project.\n" +
             "👉 Chỉ xóa Component bị mất script, KHÔNG xóa GameObject hoặc file nào.")]
    [FoldoutGroup("Tùy chọn quét")]
    [LabelText("Chọn thư mục để quét (chứa Prefab)")]
    public Object scanFolder;

    [FoldoutGroup("Tùy chọn quét")]
    [LabelText("Bao gồm cả thư mục con")]
    public bool includeSubfolders = true;

    [FoldoutGroup("Tùy chọn quét")]
    [LabelText("Hiển thị log chi tiết")]
    public bool verboseLog = false;

    [Button(ButtonSizes.Large), GUIColor(0.3f, 0.8f, 0.4f)]
    [LabelText("🧹 Xóa Missing Scripts trong GameObject đang chọn (trong Scene)")]
    private void RemoveMissingScriptsInSelection()
    {
        var selectedObjects = Selection.gameObjects;
        if (selectedObjects == null || selectedObjects.Length == 0)
        {
            EditorUtility.DisplayDialog("Thông báo", "Vui lòng chọn ít nhất một GameObject trong Scene!", "OK");
            return;
        }

        int totalRemoved = 0;
        foreach (var go in selectedObjects)
        {
            totalRemoved += RemoveMissingComponentsRecursive(go);
        }

        EditorUtility.DisplayDialog("Kết quả",
            $"✅ Đã xóa {totalRemoved} script bị Missing trong {selectedObjects.Length} đối tượng được chọn.",
            "Đóng");
    }

    [Button(ButtonSizes.Large), GUIColor(0.8f, 0.4f, 0.3f)]
    [LabelText("🧭 Quét & Làm sạch tất cả Prefab trong thư mục")]
    private void ScanAndCleanFolder()
    {
        if (scanFolder == null)
        {
            EditorUtility.DisplayDialog("Lỗi", "Vui lòng chọn một thư mục để quét!", "OK");
            return;
        }

        string folderPath = AssetDatabase.GetAssetPath(scanFolder);
        string[] prefabGUIDs = AssetDatabase.FindAssets("t:Prefab", includeSubfolders ? new[] { folderPath } : null);

        int totalRemoved = 0;

        foreach (var guid in prefabGUIDs)
        {
            string assetPath = AssetDatabase.GUIDToAssetPath(guid);
            totalRemoved += CleanPrefab(assetPath);
        }

        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();

        EditorUtility.DisplayDialog("Hoàn tất",
            $"🧩 Đã quét {prefabGUIDs.Length} Prefab và xóa {totalRemoved} Missing Script.",
            "OK");
    }

    /// <summary>
    /// Dọn sạch Missing Script trong Prefab bằng cách quét đệ quy toàn bộ cây con
    /// </summary>
    private int CleanPrefab(string assetPath)
    {
        GameObject prefabRoot = PrefabUtility.LoadPrefabContents(assetPath);
        int removed = RemoveMissingComponentsRecursive(prefabRoot);

        if (removed > 0)
        {
            PrefabUtility.SaveAsPrefabAsset(prefabRoot, assetPath);
            if (verboseLog)
                Debug.Log($"✅ Đã xóa {removed} Missing Script trong prefab: {assetPath}");
        }

        PrefabUtility.UnloadPrefabContents(prefabRoot);
        return removed;
    }

    /// <summary>
    /// Hàm quét đệ quy tất cả con của GameObject để xóa Missing Scripts
    /// </summary>
    private int RemoveMissingComponentsRecursive(GameObject root)
    {
        int total = 0;

        // Xóa missing scripts trên chính GameObject này
        int beforeCount = GameObjectUtility.GetMonoBehavioursWithMissingScriptCount(root);
        if (beforeCount > 0)
        {
            Undo.RegisterFullObjectHierarchyUndo(root, "Remove Missing Scripts");
            GameObjectUtility.RemoveMonoBehavioursWithMissingScript(root);
            total += beforeCount;
        }

        // Lặp qua tất cả các con
        foreach (Transform child in root.transform)
        {
            total += RemoveMissingComponentsRecursive(child.gameObject);
        }

        return total;
    }
}

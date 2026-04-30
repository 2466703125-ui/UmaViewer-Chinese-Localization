using UnityEngine;
using TMPro;
using UnityEditor;

public class BatchFontChanger : EditorWindow
{
    private TMP_FontAsset newFont;
    private Vector2 scrollPos;
    private int totalFound = 0;
    private int uiTextCount = 0;
    private int text3DCount = 0;

    [MenuItem("Tools/批量修改 TMP 字体")]
    static void ShowWindow()
    {
        GetWindow<BatchFontChanger>("批量字体修改器");
    }

    void OnGUI()
    {
        GUILayout.Space(10);
        GUILayout.Label("批量修改 TextMeshPro 字体", EditorStyles.boldLabel);
        GUILayout.Space(10);

        // 字体选择
        EditorGUILayout.BeginHorizontal();
        GUILayout.Label("新字体:", GUILayout.Width(60));
        newFont = (TMP_FontAsset)EditorGUILayout.ObjectField(newFont, typeof(TMP_FontAsset), false);
        EditorGUILayout.EndHorizontal();

        GUILayout.Space(10);

        // 统计信息
        if (GUILayout.Button("扫描场景中的文本"))
        {
            ScanTexts();
        }

        if (totalFound > 0)
        {
            EditorGUILayout.HelpBox($"找到 {totalFound} 个文本对象:\n- UI 文本: {uiTextCount} 个\n- 3D 文本: {text3DCount} 个", MessageType.Info);
        }

        GUILayout.Space(10);

        // 操作按钮
        GUI.enabled = (newFont != null);
        
        if (GUILayout.Button("应用到所有 TextMeshPro", GUILayout.Height(30)))
        {
            if (EditorUtility.DisplayDialog("确认", 
                $"确定要将 {totalFound} 个文本对象的字体修改为 {newFont.name} 吗？", 
                "确定", "取消"))
            {
                ApplyFont();
            }
        }

        GUI.enabled = true;

        GUILayout.Space(10);

        // 快捷选择字体
        GUILayout.Label("快捷操作:", EditorStyles.boldLabel);
        
        EditorGUILayout.BeginHorizontal();
        
        if (GUILayout.Button("选择 MSYH 字体"))
        {
            FindAndSelectFont("MSYH");
        }
        
        if (GUILayout.Button("选择 ChineseFont"))
        {
            FindAndSelectFont("ChineseFont");
        }
        
        EditorGUILayout.EndHorizontal();

        GUILayout.Space(10);

        // 帮助信息
        EditorGUILayout.HelpBox(
            "使用说明:\n" +
            "1. 从 Project 窗口拖拽字体资源到上方的\"新字体\"字段\n" +
            "2. 点击\"扫描\"查看场景中有多少文本\n" +
            "3. 点击\"应用\"批量修改所有文本的字体", 
            MessageType.None);
    }

    void ScanTexts()
    {
        TextMeshProUGUI[] uiTexts = Resources.FindObjectsOfTypeAll<TextMeshProUGUI>();
        TextMeshPro[] texts3D = Resources.FindObjectsOfTypeAll<TextMeshPro>();

        // 过滤掉预制件资源，只统计场景中的对象
        uiTextCount = 0;
        foreach (var text in uiTexts)
        {
            if (text.gameObject.scene.isLoaded)
            {
                uiTextCount++;
            }
        }

        text3DCount = 0;
        foreach (var text in texts3D)
        {
            if (text.gameObject.scene.isLoaded)
            {
                text3DCount++;
            }
        }

        totalFound = uiTextCount + text3DCount;

        Debug.Log($"[BatchFontChanger] 扫描完成: 找到 {totalFound} 个文本对象 (UI: {uiTextCount}, 3D: {text3DCount})");
    }

    void ApplyFont()
    {
        if (newFont == null)
        {
            EditorUtility.DisplayDialog("错误", "请先选择一个字体资源！", "确定");
            return;
        }

        int count = 0;

        // 修改 UI 文本
        TextMeshProUGUI[] uiTexts = Resources.FindObjectsOfTypeAll<TextMeshProUGUI>();
        foreach (var text in uiTexts)
        {
            if (text.gameObject.scene.isLoaded)
            {
                Undo.RecordObject(text, "Change TMP Font");
                text.font = newFont;
                EditorUtility.SetDirty(text);
                count++;
            }
        }

        // 修改 3D 文本
        TextMeshPro[] texts3D = Resources.FindObjectsOfTypeAll<TextMeshPro>();
        foreach (var text in texts3D)
        {
            if (text.gameObject.scene.isLoaded)
            {
                Undo.RecordObject(text, "Change TMP Font");
                text.font = newFont;
                EditorUtility.SetDirty(text);
                count++;
            }
        }

        Debug.Log($"[BatchFontChanger] 已修改 {count} 个文本对象的字体为 {newFont.name}");
        EditorUtility.DisplayDialog("完成", $"已成功修改 {count} 个文本对象的字体为 {newFont.name}！", "确定");
    }

    void FindAndSelectFont(string fontName)
    {
        // 在项目中查找字体资源
        string[] guids = AssetDatabase.FindAssets("t:TMP_FontAsset");
        
        foreach (string guid in guids)
        {
            string path = AssetDatabase.GUIDToAssetPath(guid);
            TMP_FontAsset font = AssetDatabase.LoadAssetAtPath<TMP_FontAsset>(path);
            
            if (font != null && font.name.Contains(fontName))
            {
                newFont = font;
                Selection.activeObject = font;
                EditorGUIUtility.PingObject(font);
                Debug.Log($"[BatchFontChanger] 已选择字体: {font.name}");
                return;
            }
        }

        Debug.LogWarning($"[BatchFontChanger] 未找到名为 '{fontName}' 的字体资源");
    }
}

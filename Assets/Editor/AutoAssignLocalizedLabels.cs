#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;
using UnityEditor.SceneManagement;

/// <summary>
/// 自动挂载LocalizedLabel组件的编辑器工具
/// 通过日文原文自动匹配本地化键
/// </summary>
public class AutoAssignLocalizedLabels : EditorWindow
{
    // Scene中的日文原文 → 翻译key的映射表
    static readonly Dictionary<string, string> japaneseToKey = new Dictionary<string, string>
    {
        // 侧边栏标签
        { "モデル", "tab_model" },
        { "環境", "tab_environment" },
        { "その他", "tab_other" },
        { "サウンド", "tab_sound" },
        { "スクリーンショット", "tab_screenshot" },
        { "アニメーション", "tab_animation" },
        
        // 顶部子标签
        { "キャラ", "tab_character" },
        { "表情", "tab_expression" },
        { "動作", "tab_action" },
        { "耳", "tab_ear" },
        { "眉", "tab_eyebrow" },
        { "目", "tab_eye" },
        { "口", "tab_mouth" },
        
        // 其他标签
        { "モブ", "tab_mob" },
        { "ミニ", "tab_mini" },
        { "ライブ", "tab_live" },
        { "ポーズ", "tab_pose" },
        
        // 英文标签（备用）
        { "Model", "tab_model" },
        { "Environment", "tab_environment" },
        { "Other", "tab_other" },
        { "Sound", "tab_sound" },
        { "Screenshot", "tab_screenshot" },
        { "Animation", "tab_animation" },
        { "Animations", "tab_animations" },
        { "Character", "tab_character" },
        { "Characters", "tab_characters" },
        { "Expression", "tab_expression" },
        { "Action", "tab_action" },
        { "Ear", "tab_ear" },
        { "Eyebrow", "tab_eyebrow" },
        { "Eye", "tab_eye" },
        { "Mouth", "tab_mouth" },
        { "Mob", "tab_mob" },
        { "Mini", "tab_mini" },
        { "Live", "tab_live" },
        { "Pose", "tab_pose" },
        { "Loaded Assets", "tab_loaded_assets" },
        { "Settings", "tab_settings" },
        { "ColorSet", "tab_colorset" },
        { "Camera", "tab_camera" },
        { "About", "tab_about" },
        { "Facial Morphs", "tab_facial_morphs" },
        { "Shows (WIP)", "tab_shows" },
        { "Sounds", "tab_sounds" },
        { "Props", "tab_props" },
        { "Scenes", "tab_scenes" },
    };
    
    // 中文标签（备用）
    static readonly Dictionary<string, string> chineseToKey = new Dictionary<string, string>
    {
        { "模型", "tab_model" },
        { "环境", "tab_environment" },
        { "其他", "tab_other" },
        { "声音", "tab_sound" },
        { "截图", "tab_screenshot" },
        { "人物", "tab_character" },
        { "表情", "tab_expression" },
        { "动作", "tab_action" },
        { "耳朵", "tab_ear" },
        { "眉毛", "tab_eyebrow" },
        { "眼睛", "tab_eye" },
        { "嘴", "tab_mouth" },
        { "NPC", "tab_mob" },
        { "Q版", "tab_mini" },
        { "Live", "tab_live" },
        { "姿势", "tab_pose" },
    };

    [MenuItem("Tools/Localization/Auto Assign Labels")]
    static void ShowWindow()
    {
        GetWindow<AutoAssignLocalizedLabels>("Auto Assign Labels");
    }
    
    void OnGUI()
    {
        GUILayout.Label("自动挂载 LocalizedLabel 组件", EditorStyles.boldLabel);
        GUILayout.Space(10);
        
        GUILayout.Label("此工具会扫描场景中所有的 Text 和 TextMeshProUGUI 组件，");
        GUILayout.Label("根据文字内容自动匹配本地化键并挂载 LocalizedLabel 组件。");
        GUILayout.Space(10);
        
        if (GUILayout.Button("开始自动挂载", GUILayout.Height(30)))
        {
            AssignLabels();
        }
        
        GUILayout.Space(10);
        
        if (GUILayout.Button("清除所有 LocalizedLabel", GUILayout.Height(25)))
        {
            ClearAllLabels();
        }
        
        GUILayout.Space(20);
        GUILayout.Label("映射表:", EditorStyles.boldLabel);
        
        EditorGUILayout.BeginVertical(EditorStyles.helpBox);
        foreach (var kvp in japaneseToKey)
        {
            GUILayout.Label($"{kvp.Key} → {kvp.Key}");
        }
        EditorGUILayout.EndVertical();
    }
    
    /// <summary>
    /// 自动挂载LocalizedLabel组件
    /// </summary>
    static void AssignLabels()
    {
        int count = 0;
        int skipped = 0;
        
        Debug.Log("[AutoAssignLocalizedLabels] 开始扫描场景...");
        
        // 合并所有映射表
        var allMappings = new Dictionary<string, string>(japaneseToKey);
        foreach (var kvp in chineseToKey)
        {
            if (!allMappings.ContainsKey(kvp.Key))
                allMappings.Add(kvp.Key, kvp.Value);
        }
        
        // 遍历旧版 Text
        Text[] allTexts = FindObjectsOfType<Text>(true);
        Debug.Log($"[AutoAssignLocalizedLabels] 找到 {allTexts.Length} 个 Text 组件");
        
        foreach (var text in allTexts)
        {
            string textContent = text.text.Trim();
            
            if (string.IsNullOrEmpty(textContent))
            {
                skipped++;
                continue;
            }
            
            if (allMappings.TryGetValue(textContent, out var key))
            {
                var label = text.gameObject.GetComponent<LocalizedLabel>();
                if (label == null)
                {
                    label = text.gameObject.AddComponent<LocalizedLabel>();
                    Undo.RegisterCreatedObjectUndo(label, "Add LocalizedLabel");
                }
                
                label.key = key;
                EditorUtility.SetDirty(label);
                count++;
                
                Debug.Log($"[Assign] {text.gameObject.name}: \"{textContent}\" → {key}");
            }
            else
            {
                skipped++;
            }
        }
        
        // 遍历 TextMeshProUGUI
        TextMeshProUGUI[] allTMP = FindObjectsOfType<TextMeshProUGUI>(true);
        Debug.Log($"[AutoAssignLocalizedLabels] 找到 {allTMP.Length} 个 TextMeshProUGUI 组件");
        
        foreach (var tmp in allTMP)
        {
            string textContent = tmp.text.Trim();
            
            if (string.IsNullOrEmpty(textContent))
            {
                skipped++;
                continue;
            }
            
            if (allMappings.TryGetValue(textContent, out var key))
            {
                var label = tmp.gameObject.GetComponent<LocalizedLabel>();
                if (label == null)
                {
                    label = tmp.gameObject.AddComponent<LocalizedLabel>();
                    Undo.RegisterCreatedObjectUndo(label, "Add LocalizedLabel");
                }
                
                label.key = key;
                EditorUtility.SetDirty(label);
                count++;
                
                Debug.Log($"[Assign] {tmp.gameObject.name}: \"{textContent}\" → {key}");
            }
            else
            {
                skipped++;
            }
        }
        
        // 保存场景
        EditorSceneManager.SaveOpenScenes();
        
        Debug.Log($"[AutoAssignLocalizedLabels] 完成！成功挂载 {count} 个，跳过 {skipped} 个");
        
        EditorUtility.DisplayDialog("完成", 
            $"自动挂载完成！\n\n成功挂载: {count} 个\n跳过: {skipped} 个\n\n场景已自动保存。", 
            "确定");
    }
    
    /// <summary>
    /// 清除所有LocalizedLabel组件
    /// </summary>
    static void ClearAllLabels()
    {
        if (!EditorUtility.DisplayDialog("确认", 
            "确定要清除所有 LocalizedLabel 组件吗？\n此操作不可撤销。", 
            "确定", "取消"))
        {
            return;
        }
        
        LocalizedLabel[] allLabels = FindObjectsOfType<LocalizedLabel>(true);
        int count = 0;
        
        foreach (var label in allLabels)
        {
            Undo.DestroyObjectImmediate(label);
            count++;
        }
        
        EditorSceneManager.SaveOpenScenes();
        
        Debug.Log($"[AutoAssignLocalizedLabels] 已清除 {count} 个 LocalizedLabel 组件");
        
        EditorUtility.DisplayDialog("完成", 
            $"已清除 {count} 个 LocalizedLabel 组件。\n场景已自动保存。", 
            "确定");
    }
}
#endif
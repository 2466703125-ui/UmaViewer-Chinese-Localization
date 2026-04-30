#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

/// <summary>
/// 在Language下面添加角色名语言Dropdown
/// </summary>
public class AddCharacterNameDropdown : EditorWindow
{
    private Vector2 scrollPos;
    
    [MenuItem("Tools/Localization/Add Character Name Language")]
    static void ShowWindow()
    {
        GetWindow<AddCharacterNameDropdown>("Add Character Name Language");
    }
    
    void OnGUI()
    {
        GUILayout.Label("添加角色名语言Dropdown", EditorStyles.boldLabel);
        GUILayout.Space(10);
        
        GUILayout.Label("请选择Language Dropdown，然后点击创建按钮");
        GUILayout.Space(10);
        
        // 显示当前选择
        var selected = Selection.activeGameObject;
        if (selected != null)
        {
            GUILayout.Label($"当前选择: {selected.name}");
            
            var dropdown = selected.GetComponent<TMP_Dropdown>();
            if (dropdown != null)
            {
                GUILayout.Space(10);
                if (GUILayout.Button("基于此创建角色名语言Dropdown", GUILayout.Height(30)))
                {
                    CreateDropdown(selected);
                }
            }
            else
            {
                GUILayout.Label("选择的对象没有TMP_Dropdown组件");
            }
        }
        else
        {
            GUILayout.Label("请先选择Language Dropdown");
        }
        
        GUILayout.Space(20);
        
        // 列出所有Dropdown
        GUILayout.Label("所有Dropdown对象:");
        GUILayout.Space(5);
        
        scrollPos = GUILayout.BeginScrollView(scrollPos, GUILayout.Height(200));
        
        var allDropdowns = FindObjectsOfType<TMP_Dropdown>(true);
        foreach (var dd in allDropdowns)
        {
            EditorGUILayout.BeginHorizontal();
            
            if (GUILayout.Button(dd.gameObject.name, GUILayout.Width(200)))
            {
                Selection.activeGameObject = dd.gameObject;
            }
            
            // 显示Label文字
            var labels = dd.GetComponentsInChildren<TextMeshProUGUI>(true);
            foreach (var label in labels)
            {
                if (label.gameObject.name == "Label" && !string.IsNullOrEmpty(label.text))
                {
                    GUILayout.Label($"[{label.text}]");
                    break;
                }
            }
            
            EditorGUILayout.EndHorizontal();
        }
        
        GUILayout.EndScrollView();
    }
    
    static void CreateDropdown(GameObject languageDropdown)
    {
        // 检查是否已存在
        var existingObj = GameObject.Find("CharacterNameLanguage");
        if (existingObj != null)
        {
            EditorUtility.DisplayDialog("提示", "CharacterNameLanguage已存在！", "确定");
            return;
        }
        
        // 复制Language对象
        var newObj = Instantiate(languageDropdown, languageDropdown.transform.parent);
        newObj.name = "CharacterNameLanguage";
        
        // 调整位置（放在Language下面）
        var rectTransform = newObj.GetComponent<RectTransform>();
        var langRect = languageDropdown.GetComponent<RectTransform>();
        
        // 获取Language的高度
        float langHeight = langRect.sizeDelta.y;
        if (langHeight <= 0) langHeight = 40;
        
        rectTransform.anchoredPosition = langRect.anchoredPosition - new Vector2(0, langHeight + 5);
        
        // 修改Label文字
        var labels = newObj.GetComponentsInChildren<TextMeshProUGUI>(true);
        foreach (var label in labels)
        {
            if (label.gameObject.name == "Label" || label.text == "Language" || label.text == "语言")
            {
                label.text = "角色名语言";
                Debug.Log($"[AddCharacterNameDropdown] 修改Label: {label.text}");
                break;
            }
        }
        
        // 设置Dropdown选项
        var dropdown = newObj.GetComponent<TMP_Dropdown>();
        if (dropdown != null)
        {
            dropdown.ClearOptions();
            dropdown.options.Add(new TMP_Dropdown.OptionData("日本語"));
            dropdown.options.Add(new TMP_Dropdown.OptionData("English"));
            dropdown.options.Add(new TMP_Dropdown.OptionData("中文"));
            dropdown.value = 0;
            Debug.Log("[AddCharacterNameDropdown] 设置Dropdown选项完成");
        }
        
        // 引用到UISettingsOther
        var settingsOther = FindObjectOfType<UISettingsOther>();
        if (settingsOther != null)
        {
            var serializedObject = new SerializedObject(settingsOther);
            var property = serializedObject.FindProperty("CharacterNameLanguageDropdown");
            if (property != null)
            {
                property.objectReferenceValue = dropdown;
                serializedObject.ApplyModifiedProperties();
                Debug.Log("[AddCharacterNameDropdown] 已设置CharacterNameLanguageDropdown引用");
            }
        }
        
        // 选中新对象
        Selection.activeGameObject = newObj;
        
        EditorUtility.DisplayDialog("完成", 
            "角色名语言Dropdown已创建！\n\n" +
            "名称: CharacterNameLanguage\n" +
            "位置: Language下方\n" +
            "选项: 日本語、English、中文\n\n" +
            "请检查并保存场景！", 
            "确定");
        
        Debug.Log("[AddCharacterNameDropdown] 完成！");
    }
}
#endif
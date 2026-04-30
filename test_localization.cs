// 本地化系统测试脚本
// 这个脚本用于验证本地化系统是否正常工作
// 注意：这个文件不能直接运行，需要在Unity环境中测试

using UnityEngine;

public class TestLocalization : MonoBehaviour
{
    void Start()
    {
        // 测试本地化系统
        Debug.Log("=== 本地化系统测试 ===");
        
        // 测试英文
        LocalizationManager.Instance.SetLanguage(Language.En);
        Debug.Log("英文测试:");
        Debug.Log("Generic: " + LocalizationManager.Get("ui.generic"));
        Debug.Log("Upgraded: " + LocalizationManager.Get("costume.upgraded"));
        Debug.Log("Select Folder: " + LocalizationManager.Get("msg.select_folder"));
        
        // 测试中文
        LocalizationManager.Instance.SetLanguage(Language.ZhCn);
        Debug.Log("\n中文测试:");
        Debug.Log("通用: " + LocalizationManager.Get("ui.generic"));
        Debug.Log("突破服装: " + LocalizationManager.Get("costume.upgraded"));
        Debug.Log("选择文件夹: " + LocalizationManager.Get("msg.select_folder"));
        
        // 测试角色名映射
        Debug.Log("\n角色名映射测试:");
        Debug.Log("1001: " + LocalizationManager.GetCharacterName("1001", "Default Name"));
        Debug.Log("1002: " + LocalizationManager.GetCharacterName("1002", "Default Name"));
        
        // 测试格式化字符串
        Debug.Log("\n格式化字符串测试:");
        Debug.Log("文件复制消息: " + LocalizationManager.Get("msg.files_copied", 5));
        Debug.Log("颜色集未找到: " + LocalizationManager.Get("ui.color_set_not_found", "123"));
        
        Debug.Log("\n=== 测试完成 ===");
    }
}
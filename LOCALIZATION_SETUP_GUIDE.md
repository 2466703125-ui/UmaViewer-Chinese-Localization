# UmaViewer 中文本地化设置指南

## 概述

本指南将帮助你设置UmaViewer的中文本地化功能。我们已经创建了完整的本地化系统，支持中英文切换。

## 已创建的文件

### 1. 核心文件
- `LocalizationManager.cs` - 本地化管理器（已修改）
- `UILocalizationUpdater.cs` - UI本地化更新器（新创建）
- `SceneLocalizationSetup.cs` - 场景本地化设置（新创建）
- `QuickLocalizationTest.cs` - 快速测试脚本（新创建）
- `TestLocalization.cs` - 测试脚本（已创建）

### 2. 语言文件
- `Assets/Resources/Localization/en.json` - 英文语言文件
- `Assets/Resources/Localization/zh_cn.json` - 中文语言文件
- `Assets/Resources/Localization/character_names_zh.json` - 角色中文名映射

### 3. 配置文件
- `Config.cs` - 已修改Language枚举
- `UISettingsOther.cs` - 已修改语言切换逻辑
- `UmaViewerUI.cs` - 已添加本地化支持

## 快速设置步骤

### 步骤1：在Unity中设置测试场景

1. **创建测试Canvas**：
   - 在Hierarchy中右键 → `UI` → `Canvas`
   - 命名为 `LocalizationTestCanvas`

2. **创建测试UI**：
   - 在Canvas下创建5个TextMeshPro文本：
     - 右键 → `UI` → `Text - TextMeshPro`（创建5次）
   - 创建2个Button：
     - 右键 → `UI` → `Button - TextMeshPro`（创建2次）

3. **设置测试脚本**：
   - 创建空对象：右键 → `Create Empty`
   - 命名为 `LocalizationTester`
   - 添加组件：`QuickLocalizationTest`
   - 将UI组件拖拽到对应字段

### 步骤2：配置UI组件

1. **设置文本组件**：
   - 将5个TextMeshPro文本拖拽到QuickLocalizationTest的对应字段
   - 将2个Button拖拽到对应字段

2. **设置按钮文本**：
   - 第一个Button的文本改为："切换语言"
   - 第二个Button的文本改为："更新UI"

### 步骤3：测试本地化

1. **运行项目**（点击播放按钮 ▶️）

2. **使用测试功能**：
   - 按 **T键** - 测试本地化系统
   - 按 **L键** - 切换语言
   - 按 **R键** - 更新所有UI
   - 点击按钮测试

3. **查看控制台输出**：
   - 打开Unity控制台：`Window` → `General` → `Console`
   - 查看本地化测试结果

## 完整设置流程

### 方案A：自动设置（推荐）

1. **添加场景本地化设置**：
   - 在场景中创建空对象
   - 添加 `SceneLocalizationSetup` 组件
   - 勾选 `Auto Setup On Start`
   - 勾选 `Auto Find All Texts`

2. **添加UI本地化更新器**：
   - 在场景中创建空对象
   - 添加 `UILocalizationUpdater` 组件
   - 勾选 `Auto Find All Texts`

3. **运行项目**：
   - 系统会自动查找并本地化所有UI文本

### 方案B：手动设置

1. **选择需要本地化的文本**：
   - 在Hierarchy中选择TextMeshPro组件
   - 在Inspector中查看文本内容

2. **添加本地化配置**：
   - 在SceneLocalizationSetup组件中
   - 点击 `Localization Configs` 旁边的 `+` 按钮
   - 将文本组件拖拽到 `Text Component` 字段
   - 在 `Localization Key` 中输入本地化键名

3. **常见本地化键名**：
   ```
   settings.camera          - 相机
   settings.model           - 模型
   settings.sound           - 声音
   settings.animation       - 动画
   settings.screenshot      - 截图
   settings.assets          - 资源
   settings.other           - 其他
   ui.generic               - 通用
   ui.tail                  - 尾巴
   ui.ear                   - 耳朵
   costume.upgraded         - 突破服装
   costume.race_shorts      - 比赛短裤
   ```

## 验证本地化效果

### 测试1：基础功能测试

1. 运行项目
2. 按 **T键** 查看测试结果
3. 检查控制台输出是否正确

### 测试2：语言切换测试

1. 在UmaViewer中进入"Other"设置
2. 切换语言到"中文"
3. 重启项目
4. 检查UI文字是否显示中文

### 测试3：角色名测试

1. 查看角色列表
2. 确认角色名显示为中文
3. 切换语言后角色名应显示英文

## 常见问题解决

### 问题1：UI文字显示为英文

**解决方案**：
1. 检查本地化系统是否初始化
2. 按 **L键** 切换语言
3. 按 **R键** 更新UI
4. 检查控制台是否有错误

### 问题2：某些文字显示为方框

**解决方案**：
1. 配置中文字体（参考字体配置指南）
2. 确保TextMeshPro组件使用支持中文的字体
3. 重新生成字体图集

### 问题3：语言切换后没有生效

**解决方案**：
1. 确保重启了应用
2. 检查Config.json文件中的Language设置
3. 手动调用UpdateUILocalization()

### 问题4：角色名没有显示中文

**解决方案**：
1. 检查character_names_zh.json文件是否存在
2. 确认文件格式正确
3. 检查角色ID是否在映射中

## 调试工具

### 使用控制台调试

在Unity控制台中查看以下日志：
```
[Localization] 加载语言文件成功: zh_cn (58 个条目)
[UILocalization] 找到 50 个TextMeshPro组件
[SceneLocalization] 场景本地化设置完成
```

### 使用快捷键调试

- **T键** - 测试本地化系统
- **L键** - 切换语言
- **R键** - 更新所有UI
- **U键** - 更新UI本地化

### 使用测试脚本

1. 运行 `QuickLocalizationTest` 脚本
2. 查看测试文本是否正确显示
3. 使用按钮切换语言

## 高级配置

### 自定义本地化键

1. 在语言文件中添加新键值对：
   ```json
   {
     "custom.key": "自定义文本",
     "custom.greeting": "你好，{0}！"
   }
   ```

2. 在代码中使用：
   ```csharp
   string text = LocalizationManager.Get("custom.key");
   string greeting = LocalizationManager.Get("custom.greeting", "玩家");
   ```

### 添加新语言

1. 创建新的语言文件：`Resources/Localization/ja.json`
2. 在LocalizationManager中添加语言支持
3. 在Language枚举中添加新语言

### 批量更新UI

使用UILocalizationUpdater批量更新：
```csharp
UILocalizationUpdater updater = FindObjectOfType<UILocalizationUpdater>();
if (updater != null)
{
    updater.UpdateAllUILocalization();
}
```

## 完整工作流程

1. **准备阶段**：
   - 确保所有文件已创建
   - 配置中文字体

2. **设置阶段**：
   - 添加测试场景
   - 配置UI组件

3. **测试阶段**：
   - 运行基础测试
   - 测试语言切换
   - 验证显示效果

4. **部署阶段**：
   - 应用到主场景
   - 测试完整功能

## 技术支持

如果遇到问题，请检查：

1. **控制台错误**：查看Unity控制台的错误信息
2. **文件路径**：确保语言文件在正确位置
3. **字体配置**：确保中文字体已正确配置
4. **组件引用**：确保所有UI组件已正确引用

## 总结

通过以上步骤，你应该能够成功设置UmaViewer的中文本地化功能。系统支持：
- ✅ 中英文切换
- ✅ 角色名中文显示
- ✅ UI文字本地化
- ✅ 自动查找UI组件
- ✅ 快速测试和调试

现在可以享受中文版的UmaViewer了！
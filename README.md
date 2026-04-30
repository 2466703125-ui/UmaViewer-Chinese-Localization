# UmaViewer 中文本地化

UmaViewer 的中文本地化修改，包括角色语言切换、UI 翻译和字体支持。

## 功能特性

### 1. 角色语言切换
- 独立的"角色名语言"下拉菜单
- 支持三种语言：日本語、English、中文
- 独立于全局 UI 语言设置
- 角色名语言设置保存到 Config，重启后生效

### 2. UI 本地化
- 完整的中文翻译（408+ 条）
- 所有导航栏、按钮、标签已翻译
- 支持运行时语言切换

### 3. 服装名翻译
- 根据角色名语言设置显示对应语言的服装名
- 支持日文、英文、中文

### 4. 字体支持
- 使用项目自带的中文字体（MSYH SDF）
- 自动设置 Fallback，解决方框问题
- 包含中文字符集文件

## 安装方法

### 方法 1：替换文件
1. 下载本仓库
2. 替换项目中的对应文件：
   - `Assets/Scripts/Settings/UISettingsOther.cs`
   - `Assets/Scripts/UmaViewerUI.cs`
   - `Assets/Scripts/UmaViewerMain.cs`
   - `Assets/Scripts/LocalizationManager.cs`
   - `Assets/Resources/Localization/zh_cn.json`

### 方法 2：使用工具
1. 复制 `Assets/Scripts/Editor/` 目录下的所有工具脚本到项目
2. 在 Unity 中运行工具：
   - **Tools > 设置中文字体 Fallback** - 自动设置中文字体
   - **Tools > 测试本地化** - 检查并添加缺失翻译
   - **Tools > 修复 Dropdown 标签** - 修复 Dropdown 选项显示问题

## 使用方法

### 角色语言切换
1. 进入 **Other** 设置面板
2. 找到 **"角色名语言"** 下拉菜单
3. 选择语言：日本語、English、中文
4. 角色名和服装名会立即更新

### UI 语言切换
1. 进入 **Other** 设置面板
2. 找到 **"Language"** 下拉菜单
3. 选择语言：English 或 中文
4. UI 文字会立即更新

## 文件说明

### 核心文件
- `UISettingsOther.cs` - 角色语言选择器核心代码
- `UmaViewerUI.cs` - UI 刷新和角色名刷新
- `UmaViewerMain.cs` - 翻译文件加载逻辑
- `LocalizationManager.cs` - 本地化管理器
- `zh_cn.json` - 中文翻译文件（408+ 条）

### 工具脚本
- `AutoLocalizeUI.cs` - 自动本地化 UI 工具
- `ExtractChineseChars.cs` - 提取中文字符集工具
- `CleanDuplicateKeys.cs` - 清理重复翻译 Key 工具
- `FindMissingTranslations.cs` - 查找缺失翻译工具
- `SetChineseFontFallback.cs` - 设置中文字体 Fallback 工具
- `TestLocalization.cs` - 测试本地化工具
- `FindMissingChars.cs` - 查找缺失中文字符工具
- `FixDropdownLabels.cs` - 修复 Dropdown 标签工具

### 字体文件
- `ChineseChars.txt` - 中文字符集文件
- `Fonts/` - 字体目录

## 技术细节

### 角色名语言切换原理
- 使用 `Config.Instance.CharacterNameLanguage` 保存设置
- `LocalizationManager.GetCharacterName()` 根据设置返回对应语言的角色名
- `UmaViewerUI.RefreshCharacterNames()` 刷新所有角色名显示

### UI 本地化原理
- 使用 `LocalizedLabel` 组件标记需要翻译的 UI 文字
- `LocalizationManager.Get()` 根据 key 返回翻译后的文字
- 支持运行时语言切换

### 字体问题解决
- 使用项目自带的 MSYH SDF 中文字体
- 设置 Fallback，遇到中文字符时自动回退到中文字体
- 包含所有需要的中文字符

## 已知问题

1. **贡献人员名单重叠** - 需要手动调整 UI 布局
2. **部分 Dropdown 选项显示"选项A"** - 需要运行"修复 Dropdown 标签"工具
3. **字体粗细不一致** - 需要重新生成字体图集

## 更新日志

### 2026-04-29
- 完成角色语言切换功能
- 完成 UI 本地化（408+ 条翻译）
- 添加服装名翻译
- 添加字体支持
- 添加工具脚本

## 贡献者

- 原始项目：[UmaViewer](https://github.com/katboi01/UmaViewer)
- 中文本地化：[2466703125-ui](https://github.com/2466703125-ui)

## 许可证

MIT License

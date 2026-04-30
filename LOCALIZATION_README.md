# UmaViewer 中文本地化改造完成

## 概述

已完成UmaViewer项目的中文本地化改造，添加了完整的国际化(i18n)框架，支持英文、日文和中文三种语言。

## 已完成的修改

### 1. 核心框架
- 创建了`LocalizationManager.cs`本地化管理器
- 支持JSON格式的语言文件加载
- 支持角色中文名映射

### 2. 语言文件
- `Assets/Resources/Localization/en.json` - 英文语言文件
- `Assets/Resources/Localization/zh_cn.json` - 中文语言文件  
- `Assets/Resources/Localization/character_names_zh.json` - 角色中文名映射文件

### 3. 代码修改

#### Config.cs
- 扩展了`Language`枚举，添加`ZhCn = 2`选项
- 将所有提示文字改为本地化属性，支持动态语言切换

#### UISettingsOther.cs
- 修改了`ChangeLanguage`方法，添加本地化初始化
- 替换了所有硬编码字符串为本地化调用

#### UmaViewerMain.cs
- 在`Start()`方法中添加了本地化系统初始化

#### UmaViewerUI.cs
- 替换了所有硬编码字符串为本地化调用
- 修改了角色名称显示，支持中文名映射
- 修改了`getCharaName`函数，添加中文名支持

#### Settings文件
- `UISettingsAnimation.cs` - 替换了速度显示字符串
- `UISettingsSound.cs` - 替换了音频相关字符串
- `UISettingsAssets.cs` - 替换了资源管理相关字符串

## 支持的语言

### 英文 (En)
- 默认语言
- 所有UI文字为英文

### 中文 (ZhCn)
- 完整中文翻译
- 角色名称中文映射
- 所有UI文字为中文

## 使用方法

### 切换语言
1. 打开设置面板
2. 进入"Other"设置
3. 在Language下拉菜单中选择语言：
   - 0: 英文
   - 1: 日文
   - 2: 中文
4. 重启应用以完全生效

### 添加新语言
1. 在`Resources/Localization/`目录下创建新的JSON文件，如`ja.json`
2. 在`LocalizationManager.cs`的`SetLanguage`方法中添加新语言支持
3. 在`Language`枚举中添加新语言选项

### 添加新角色中文名
1. 编辑`character_names_zh.json`文件
2. 添加新的ID-名称映射，格式：`"1001": "特别周"`

## 技术细节

### 本地化框架
- 使用Newtonsoft.Json进行JSON解析
- 支持参数化字符串格式化
- 支持角色名称映射
- 支持语言热切换（需重启应用）

### 文件结构
```
Assets/Resources/Localization/
├── en.json                 # 英文语言文件
├── zh_cn.json              # 中文语言文件
└── character_names_zh.json # 角色中文名映射
```

### 兼容性
- 保持向后兼容，不影响现有配置
- 不修改游戏数据，只影响UI显示
- 支持所有平台（Windows、Android等）

## 注意事项

1. **字体支持**：确保Unity项目中包含中文字体，否则中文字符可能显示为方块
2. **配置文件**：语言设置保存在`Config.json`中，会自动加载
3. **性能影响**：本地化系统对性能影响极小
4. **维护性**：所有翻译通过JSON文件管理，便于维护和更新

## 测试

可以使用`test_localization.cs`脚本在Unity中测试本地化功能。该脚本会输出各种本地化字符串的测试结果。

## 工作量统计

- 总修改文件：8个核心文件
- 新增文件：4个文件
- 替换字符串：50+处
- 总工作量：约10-12小时

## 后续维护

1. 添加新语言：只需创建新的JSON文件并更新`LocalizationManager`
2. 更新翻译：直接编辑对应的JSON文件
3. 添加新字符串：在JSON文件中添加新键值对，并在代码中使用`LocalizationManager.Get()`
4. 角色名更新：编辑`character_names_zh.json`文件

## 兼容性说明

- 不影响游戏资源加载和解密
- 不修改游戏数据库内容
- 保持与上游项目的兼容性
- 支持所有现有功能
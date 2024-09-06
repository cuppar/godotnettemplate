# Godot C# 项目模板

| [English](https://github.com/cuppar/godotnettemplate?tab=readme-ov-file#godot-c-project-template) | 中文 |

## 特性

- 场景切换功能
    - 异步淡入淡出切换场景到`tscn`文件，带有加载资源进度界面
    - 异步淡入淡出切换场景到`tscn`文件，带有加载资源进度界面，切换时暂停游戏
    - 同步淡入淡出切换场景到`tscn`文件
    - 同步淡入淡出切换场景到`tscn`文件，切换时暂停游戏
    - 同步淡入淡出切换场景到`PackedScene`
    - 同步淡入淡出切换场景到`PackedScene`，切换时暂停游戏
- 统一自动加载管理器
    - 支持在`C#`中统一访问`Godot`中注册的`Autoload`
    - 支持包装GDScript编写的插件暴露的`Autoload`
- 声音管理器
    - BGM播放器
    - 音效播放器
    - UI音效统一注册
    - Audio Bus管理
        - 默认内置`Master`/`BGM`/`SFX`三条`Bus`
        - Bus分贝线性化转换
- 内置常用组件
    - 攻击盒
    - 受伤盒
    - 不规则图片按钮（支持只用一张纹理图，自动生成鼠标`hover`高亮效果）
    - 单层枚举状态机
- 内置常用预制体
    - 相机
- 常量管理
    - 场景文件路径
    - BGM文件路径
    - Prefab文件路径
    - Groups名称
- 通用UI
    - HUD，可开关显示与否
    - 主界面(默认为主场景)
- 常用Helpers和Extensions

## 使用方式

1. 使用`Godot .Net`编辑器生成新项目，并创建一个`C#`脚本（触发自动创建`.Net` `Solution`和`Project`）。
2. 关闭`Godot`编辑器，以方便手动修改`project.godot`内容。
3. 复制本模板中所有非`.`开头的文件夹，以及`.gitignore`和`project.godot`文件，到目标项目中。
4. 修改`project.godot`文件中的`application/config/name`和`dotnet/project/assembly_name`为新项目的名称。
5. 重新打开`Godot`编辑器, 并测试功能。
6. 新建`git`仓库。
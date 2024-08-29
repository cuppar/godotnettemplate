# Godot C# 项目模板

## 使用方式

1. 使用Godot .Net客户端生成新项目，并创建一个C# 脚本（触发自动创建.Net Solution和Project）。
2. 关闭Godot客户端，以方便手动修改`project.godot`内容。
3. 复制本模板中所有非`.`开头的文件夹，以及`.gitignore`和`project.godot`文件，到目标项目中。
4. 修改`project.godot`文件中的`application/config/name`和`dotnet/project/assembly_name`为新项目的名称。
5. 重新打开Godot客户端, 并测试功能。
6. 新建git仓库。
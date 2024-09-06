# Godot C# Project Template
| English | [中文](https://github.com/cuppar/godotnettemplate/blob/main/readme.zh.md#godot-c-%E9%A1%B9%E7%9B%AE%E6%A8%A1%E6%9D%BF) |

## Features

- Scene Switching Functionality
    - Asynchronous fade-in/fade-out scene transition to `tscn` file, with resource loading progress screen
    - Asynchronous fade-in/fade-out scene transition to `tscn` file, with resource loading progress screen, and game pause during transition
    - Synchronous fade-in/fade-out scene transition to `tscn` file
    - Synchronous fade-in/fade-out scene transition to `tscn` file, with game pause during transition
    - Synchronous fade-in/fade-out scene transition to `PackedScene`
    - Synchronous fade-in/fade-out scene transition to `PackedScene`, with game pause during transition
- Unified `Autoload` Manager
    - Supports unified access to Godot registered `Autoloads` in `C#`
    - Supports wrapping `Autoloads` exposed by `GDScript` plugins
- Sound Manager
    - BGM player
    - Sound effects player
    - Unified registration for UI sounds
    - Audio Bus management
        - Default built-in `Master`/`BGM`/`SFX` buses
        - Linear conversion of bus decibels
- Built-in Common Components
    - Hit box
    - Hurt box
    - Irregular image button (supports using a single texture to automatically generate mouse `hover` highlight effect)
    - Single-layer enum state machine
- Constants Management
    - Scene file paths
    - BGM file paths
    - Prefab file paths
    - Group names
- General UI
    - HUD, toggleable display
    - Title screen (default is the main scene)
- Helpers and Extensions

## Usage

1. Generate a new project using `Godot .Net` Editor and create a `C#` script (this will trigger the automatic creation of the `.Net` `Solution` and `Project`).
2. Close the Godot Editor to manually modify the `project.godot` content.
3. Copy all non-`.` prefixed folders from this template, as well as the `.gitignore` and `project.godot` files, to the target project.
4. Modify the `project.godot` file's `application/config/name` and `dotnet/project/assembly_name` to the new project's name.
5. Reopen the `Godot` Editor and test the functionality.
6. Create a new `git` repository.
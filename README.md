# Afterhigh Unity Project

This is a Unity project configured to show only the Scripts folder in the file explorer, similar to Visual Studio's behavior.

## Project Structure

The project has been configured to hide all Unity-generated folders and non-script assets from the file explorer:

### Visible Folders
- `Assets/Scripts/` - Contains all C# scripts for the project
  - `Global/` - Global scripts and utilities
  - `Player/` - Player-related scripts
  - `RPG_Stuff/` - RPG system scripts
  - `Particle/` - Particle system scripts

### Hidden Folders (Unity-generated and asset folders)
- `Library/` - Unity's internal cache
- `Temp/` - Temporary files
- `obj/` - Build objects
- `Logs/` - Unity logs
- `UserSettings/` - User-specific settings
- `Assets/Scenes/` - Unity scenes
- `Assets/Animations/` - Animation files
- `Assets/Materials/` - Material files
- `Assets/Prefabs/` - Prefab files
- `Assets/Audio/` - Audio files
- `Assets/Textures/` - Texture files
- And other asset folders...

## Configuration Files

### .gitignore
- Hides Unity-generated folders and files from version control
- Only tracks the Scripts folder and its contents
- Keeps all meta files in version control (Unity needs them for asset references)
- Excludes build artifacts and temporary files

### .vscode/settings.json
- Configures VS Code to hide non-script folders from the file explorer
- Hides all meta files (Unity's metadata files) from the file explorer
- Excludes Unity-generated folders from search and file watching
- Optimizes performance by reducing file system monitoring

### .vscode/extensions.json
- Recommends essential Unity development extensions
- Includes C# support and Unity debugging tools

## Development Setup

1. Open the project in VS Code
2. Install the recommended extensions when prompted
3. Only the Scripts folder will be visible in the file explorer
4. Use Unity Editor for managing other assets (scenes, materials, etc.)

## Script Organization

The scripts are organized into logical folders:
- **Global**: Shared utilities, managers, and global systems
- **Player**: Player character scripts, input handling, movement
- **RPG_Stuff**: RPG mechanics, inventory, quest systems
- **Particle**: Particle effects and visual systems
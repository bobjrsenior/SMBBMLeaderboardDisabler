# SMBBM Leaderboard Disabler

A BepInEx plugin for Super Monkey Ball Banana Mania that disables leaderboard upload. This aims to be a library mod of sorts for other mods to depend on if they do things that affect gameplay.

Note: Based on what MorsGames did for [BananaModManager](https://github.com/MorsGames/BananaModManager)

## Installation

### Installing BepInEx

This plugin uses [BepInEx](https://github.com/BepInEx/BepInEx) as a mod loader so that needs to be installed first.

1. Download a bleeding edge build of "BepInEx Unity IL2CPP for Windows x64 games" [here](https://builds.bepinex.dev/projects/bepinex_be) (Only the bleeding edge builds support Il2CPP games which is what Banana Mania is)
2. Extract it in your game folder

### Installing This Plugin

1. Download the SMBBMAssetBundleLoader.zip file from the Releases page
2. Extract it in your main game folder (the zip file structure should put the plugin in the right place)

## Integration with other mods

If your making a mod that breaks gameplay in some way, it needs to disable the leaderboard upload. To do that, you can depend on this mod.

1. Add a project reference to the SMBBMLeaderboardDisabler.dll file from the releases to your project
2. At the top of your main BepInEx plugin class, add the annotation below

```csharp
[BepInDependency(SMBBMLeaderboardDisabler.PluginInfo.PLUGIN_GUID, BepInDependency.DependencyFlags.HardDependency)]
```
You can optionally hardcode the PLUGIN_GUID instead (it shouldn't ever change)
```csharp
[BepInDependency("com.bobjrsenior.SMBBMLeaderboardDisabler", BepInDependency.DependencyFlags.HardDependency)]
```

3. Tell this plugin to disable the leaderboards before you affect the gameplay

```csharp
SMBBMLeaderboardDisabler.Plugin.DisableLeaderboards(PluginInfo.PLUGIN_NAME);
```
The parameter for the DisableLeaderboards is only used for debugging purposes. One example is if someone is trying to make a set of mods that still allows leaderboard upload and has a plugin disabling it they didn't think would. 

## Building

## Setup

I use Visual Studio 2022  for development although I beleive it can also be compiled via command line. Additionally, make sure you setup your enviroment for BepInEx plugin development: https://docs.bepinex.dev/master/articles/dev_guide/plugin_tutorial/1_setup.html

## Configuration

In the .csproj, there is an element called `<SMBBMDir>`. You should edit this to point to your game installation. The project references are determined based on that. You will need to have run your game at least once with BepInEx installed for the references to be populated on disk.

## Post-build event

The project includes Post-build events that will automatically copy the plugin into "$(SMBBMDir)\BepInEx\plugins". That way you can immediately run the game after building is complete for testing.
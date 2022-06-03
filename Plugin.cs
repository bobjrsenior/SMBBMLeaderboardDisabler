using BepInEx;
using BepInEx.IL2CPP;
using BepInEx.Logging;
using Il2CppSystem;
using System.Runtime.InteropServices;
using UnhollowerRuntimeLib;

namespace SMBBMLeaderboardDisabler
{
    [BepInPlugin(PluginInfo.PLUGIN_GUID, PluginInfo.PLUGIN_NAME, PluginInfo.PLUGIN_VERSION)]
    public class Plugin : BasePlugin
    {
        [DllImport("kernel32.dll", CharSet = CharSet.Auto)]
        public static extern System.IntPtr GetModuleHandle(string lpModuleName);

        /// <summary>
        /// For logging convienence
        /// </summary>
        internal static new ManualLogSource Log;


        public delegate void LeaderboardsDelegate();
        public static LeaderboardsDelegate DelegateInstance;

        public static bool LeaderboardDisabled = false;

        public static void Dummy()
        {
            Log.LogInfo("Leaderboards are disabled when mods are used.");
        }

        /// <summary>
        /// Disables the leaderboards
        /// Note: Based on MorsGames work in https://github.com/MorsGames/BananaModManager/blob/main/BananaModManager.Loader.IL2Cpp/Loader.cs
        /// </summary>
        /// <param name="disabler">Name to print in the logs for debugging purposes</param>
        public static void DisableLeaderboards(string disabler)
        {
            Log.LogDebug($"Leaderboard requested to be disabled by {disabler}!");
            DisableLeaderboards();
        }

        /// <summary>
        /// Disabled the leaderboards (can't be undone except by game restart)
        /// </summary>
        public static void DisableLeaderboards()
        {
            if (!LeaderboardDisabled)
            {
                DelegateInstance = Dummy;
                ClassInjector.Detour.Detour(System.IntPtr.Add(GetModuleHandle("GameAssembly.dll"), 0xa9d990),
                                    DelegateInstance);
                LeaderboardDisabled = true;
                Log.LogInfo($"Leaderboards disabled!");
            }
        }

        public override void Load()
        {
            Plugin.Log = base.Log;
            Log.LogInfo($"Plugin {PluginInfo.PLUGIN_GUID} is loaded!");
        }
    }
}

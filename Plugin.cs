using BepInEx;
using BepInEx.IL2CPP;
using BepInEx.Logging;
using HarmonyLib;
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

        public delegate void LeaderboardsDelegate();
        public static LeaderboardsDelegate DelegateInstance;

        internal static new ManualLogSource Log;

        public static bool LeaderboardDisabled = false;

        public static void Dummy()
        {
            Log.LogInfo("Leaderboards are disabled when mods are used.");
        }

        public static void DiableLeaderboards()
        {
            DelegateInstance = Dummy;
            ClassInjector.Detour.Detour(IntPtr.Add(GetModuleHandle("GameAssembly.dll"), 0xa9d990),
                                DelegateInstance);
            LeaderboardDisabled = true;
        }

        public override void Load()
        {
            Plugin.Log = base.Log;
            Log.LogInfo($"Plugin {PluginInfo.PLUGIN_GUID} is loaded!");
        }
    }
}

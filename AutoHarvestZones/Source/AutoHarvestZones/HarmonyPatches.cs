using HarmonyLib;
using Verse;

namespace AutoHarvestZones
{
    [StaticConstructorOnStartup]
    public static class HarmonyPatches
    {
        static HarmonyPatches()
        {
            var harmony = new Harmony("com.automatic.harvest.zones");
            harmony.PatchAll();
        }
    }
}
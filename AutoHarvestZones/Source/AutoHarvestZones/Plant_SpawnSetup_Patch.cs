// using System;
// using HarmonyLib;
// using RimWorld;
// using Verse;
//
// namespace AutoHarvestZones
// {
//     [HarmonyPatch(typeof(Plant))]
//     [HarmonyPatch("SpawnSetup")]
//     public static class Plant_SpawnSetup_Patch
//     {
//         static void Postfix(Plant __instance)
//         {
//             if(!__instance.def.plant.Harvestable) return;
//             if (__instance.def.plant.IsTree) return;
//             if(__instance.TryGetComp<AutoHarvestComp>(out var comp)) return;
//             
//             __instance.def.comps.Add(new CompProperties(typeof(AutoHarvestComp)));
//             var thingComp = (ThingComp) Activator.CreateInstance(typeof(AutoHarvestComp));
//             thingComp.parent = __instance;
//             thingComp.parent.def.tickerType = TickerType.Normal;
//             __instance.AllComps.Add(thingComp);
//             thingComp.Initialize(__instance.def.comps[0]);
//             __instance.InitializeComps();
//             comp = __instance.GetComp<AutoHarvestComp>();
//             if (comp == null)
//             {
//                 Log.Message("[AutoHarvestZones] failed to add component to " + __instance);
//             }
//         }
//     }
// }
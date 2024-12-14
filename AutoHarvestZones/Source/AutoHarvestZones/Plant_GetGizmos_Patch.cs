// using System.Collections.Generic;
// using System.Linq;
// using HarmonyLib;
// using RimWorld;
// using UnityEngine;
// using Verse;
//
// namespace AutoHarvestZones
// {
//     [HarmonyPatch(typeof(Plant), "GetGizmos")]
//     public static class Plant_GetGizmos_Patch
//     {
//         static void Postfix(Plant __instance, ref IEnumerable<Gizmo> __result)
//         {
//             if(!__instance.def.plant.Harvestable) return;
//             if (__instance.def.plant.IsTree) return;
//             var gizmos = __result.ToList();
//
//             var customButton = new Command_Action
//             {
//                 defaultLabel = "AutoHarvestLabel".Translate(),
//                 defaultDesc = "AutoHarvestDescription".Translate(),
//                 icon = ContentFinder<Texture2D>.Get("AutoHarvest"),
//                 action = () =>
//                 {
//                     var comp = __instance.GetComp<AutoHarvestComp>();
//                     if (comp == null)
//                     {
//                         Log.Error("[AutoHarvestZones] failed to get component from " + __instance);
//                         return;
//                     }
//                     comp.IsHarvestedAutomatically = !comp.IsHarvestedAutomatically;
//                     if (comp.IsHarvestedAutomatically) return;
//                     var designation = __instance.Map.designationManager.DesignationOn(__instance, DesignationDefOf.HarvestPlant);
//                     if (designation != null)
//                         __instance.Map.designationManager.RemoveDesignation(designation);
//                 }
//             };
//
//             gizmos.Add(customButton);
//
//             __result = gizmos;
//         }
//     }
// }
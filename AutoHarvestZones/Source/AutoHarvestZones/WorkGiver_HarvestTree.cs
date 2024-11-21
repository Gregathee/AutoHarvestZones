using System.Collections.Generic;
using System.Linq;
using RimWorld;
using Verse;
using Verse.AI;

namespace AutoHarvestZones
{
    public class WorkGiver_HarvestTree : WorkGiver_Scanner
    {
        public override PathEndMode PathEndMode => PathEndMode.Touch;

        public override IEnumerable<IntVec3> PotentialWorkCellsGlobal(Pawn pawn)
        {
            var zones = pawn.Map.zoneManager.AllZones.FindAll(zone => zone is Zone_HarvestTree);
            foreach (var cell in zones.SelectMany(zone => zone.Cells)) { yield return cell; }
        }

        public override bool ShouldSkip(Pawn pawn, bool forced = false) => 
            pawn.Map.zoneManager.AllZones.FindAll(zone => zone is Zone_HarvestTree).Count == 0;

        public override bool HasJobOnCell(Pawn pawn, IntVec3 c, bool forced = false)
        {
            var plant = c.GetPlant(pawn.Map);
            if (plant == null)
                return false;
            
            var wantedPlantDef = WorkGiver_Grower.CalculateWantedPlantDef(c, pawn.Map);
            var designation = plant.HarvestableNow ? DesignationDefOf.HarvestPlant :DesignationDefOf.CutPlant;
            var zone = pawn.Map.zoneManager.ZoneAt(c) as Zone_HarvestTree;
            
            if (zone == null 
                || plant.def == wantedPlantDef 
                || !plant.def.plant.IsTree 
                || plant.Map.designationManager.DesignationOn(plant, designation) != null 
                || c.IsForbidden(pawn) 
                || plant.IsForbidden(pawn)
                || !pawn.CanReserve(plant, ignoreOtherReservations: forced))
                return false;

            if (zone.Settings.HarvestPartiallyGrown && plant.HarvestableNow)
            {
                plant.Map.designationManager.AddDesignation(new Designation(plant, designation));
                return false;
            }

            if (zone.Settings.HarvestFullyGrown && plant.Growth >= 1f)
            {
                plant.Map.designationManager.AddDesignation(new Designation(plant, designation));
                return false;
            }

            if (zone.Settings.HarvestPartiallyGrown || zone.Settings.HarvestFullyGrown) return false;
            
            plant.Map.designationManager.AddDesignation(new Designation(plant, designation));
            return false;


        }

        public override Job JobOnCell(Pawn pawn, IntVec3 c, bool forced = false)
        {
            var plant = c.GetPlant(pawn.Map);
            if (plant == null) return null;
            
            var wantedPlantDef = WorkGiver_Grower.CalculateWantedPlantDef(c, pawn.Map);
            var zone = pawn.Map.zoneManager.ZoneAt(c) as Zone_HarvestTree;
            
            if (zone == null 
                || plant.def == wantedPlantDef 
                || !plant.def.plant.IsTree 
                || c.IsForbidden(pawn)  
                || plant.IsForbidden(pawn)
                || !pawn.CanReserve(plant, ignoreOtherReservations: forced))
                return null;
            
            return new Job(plant.HarvestableNow ? JobDefOf.Harvest : JobDefOf.CutPlant, plant);
        }
    }
}
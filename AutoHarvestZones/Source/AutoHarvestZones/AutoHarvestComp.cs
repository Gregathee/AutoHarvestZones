using RimWorld;
using Verse;

namespace AutoHarvestZones
{
    public class AutoHarvestComp : ThingComp
    {
        public bool IsHarvestedAutomatically;

        public override void CompTick()
        {
            base.CompTick();
            if (!IsHarvestedAutomatically) return;
            if (parent is Plant plant && plant.def.plant.Harvestable && !plant.def.plant.IsTree && plant.Map.designationManager.DesignationOn(plant, DesignationDefOf.HarvestPlant) == null)
            {
                plant.Map.designationManager.AddDesignation(new Designation(plant, DesignationDefOf.HarvestPlant));
            }
        }

        public override void PostExposeData()
        {
            Scribe_Values.Look(ref IsHarvestedAutomatically, "IsHarvestedAutomatically");
        }
    }
}
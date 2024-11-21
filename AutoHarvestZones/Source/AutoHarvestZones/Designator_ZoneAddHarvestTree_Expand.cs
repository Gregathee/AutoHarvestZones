using RimWorld;
using Verse;

namespace AutoHarvestZones
{
    public class Designator_ZoneAddHarvestTree_Expand : Designator_ZoneAddHarvestTree
    {
        public Designator_ZoneAddHarvestTree_Expand()
        {
            this.defaultLabel = "DesignatorHarvestTreeZoneExpand".Translate();
            this.hotKey = KeyBindingDefOf.Misc6;
        }
    }
}
using RimWorld;
using UnityEngine;
using Verse;

namespace AutoHarvestZones
{
    public class Designator_ZoneAddHarvestTree : Designator_ZoneAdd
    {
        public Designator_ZoneAddHarvestTree()
        {
            zoneTypeToPlace = typeof (Zone_HarvestTree);
            defaultLabel = "HarvestTreeZoneLabel".Translate();
            defaultDesc = "HarvestTreeZoneDesc".Translate();
            icon = ContentFinder<Texture2D>.Get("ClearTreeExpand");
            hotKey = KeyBindingDefOf.Misc2;
            soundSucceeded = SoundDefOf.Designate_ZoneAdd_Growing;
        }

        protected override string NewZoneLabel => "HarvestTreeZoneLabel".Translate();

        protected override Zone MakeNewZone() => new Zone_HarvestTree(Find.CurrentMap.zoneManager);
    }
}
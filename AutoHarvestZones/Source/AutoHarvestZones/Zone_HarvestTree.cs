using System.Collections.Generic;
using RimWorld;
using UnityEngine;
using Verse;
using Verse.Sound;

namespace AutoHarvestZones
{
    public class HarvestTreeZoneSettings
    {
        public bool HarvestPartiallyGrown;
        public bool HarvestFullyGrown = true;
    }
    
    public class Zone_HarvestTree : Zone
    {
        private static HarvestTreeZoneSettings _copySettings;
        public readonly HarvestTreeZoneSettings Settings = new HarvestTreeZoneSettings();
        
        public Zone_HarvestTree() { }
        public Zone_HarvestTree(ZoneManager zoneManager) : base("HarvestTreeZoneLabel".Translate(), zoneManager) { }
        protected override Color NextZoneColor => ZoneColorUtility.NextGrowingZoneColor();
        
        public override void ExposeData()
        {
            base.ExposeData();
            Scribe_Values.Look(ref Settings.HarvestPartiallyGrown, "HarvestPartiallyGrown", true);
            Scribe_Values.Look(ref Settings.HarvestFullyGrown, "HarvestFullyGrown", true);
        }
        
        public override IEnumerable<Gizmo> GetGizmos()
        {
            foreach (var gizmo in base.GetGizmos())
                yield return gizmo;
            
            var harvestPartialGizmo = new Command_Toggle
            {
                defaultLabel = "HarvestPartiallyGrownLabel".Translate(),
                defaultDesc = "HarvestPartiallyGrownDesc".Translate(),
                icon = TexCommand.ForbidOff,
                isActive = () => Settings.HarvestPartiallyGrown,
                toggleAction = () =>
                {
                    Settings.HarvestPartiallyGrown = !Settings.HarvestPartiallyGrown;
                    if (Settings.HarvestPartiallyGrown && Settings.HarvestFullyGrown)
                        Settings.HarvestFullyGrown = false;
                }
            };
            yield return harvestPartialGizmo;
            
            var harvestFullGizmo = new Command_Toggle
            {
                defaultLabel = "HarvestFullyGrownLabel".Translate(),
                defaultDesc = "HarvestFullyGrownDesc".Translate(),
                icon = TexCommand.ForbidOff,
                isActive = () => Settings.HarvestFullyGrown,
                toggleAction = () =>
                {
                    Settings.HarvestFullyGrown = !Settings.HarvestFullyGrown;
                    if (Settings.HarvestPartiallyGrown && Settings.HarvestFullyGrown)
                        Settings.HarvestPartiallyGrown = false;
                }
            };
            yield return harvestFullGizmo;
            
            
            var copySettingsGizmo = new Command_Action
            {
                icon = ContentFinder<Texture2D>.Get("UI/Commands/CopySettings"),
                defaultLabel = "CopyZoneSettingsLabel".Translate(),
                defaultDesc = "CopyZoneSettingsDesc".Translate(),
                action = () =>
                {
                    SoundDefOf.Tick_High.PlayOneShotOnCamera();
                    _copySettings = Settings;
                }
            };

            yield return copySettingsGizmo;
            
            var pasteSettingsGizmo = new Command_Action
            {
                icon = ContentFinder<Texture2D>.Get("UI/Commands/PasteSettings"),
                defaultLabel = "PasteZoneSettingsLabel".Translate(),
                defaultDesc = "PasteZoneSettingsDesc".Translate(),
                action = () =>
                {
                    SoundDefOf.Tick_High.PlayOneShotOnCamera();
                    if (_copySettings == null) return;
                    Settings.HarvestPartiallyGrown = _copySettings.HarvestPartiallyGrown;
                    Settings.HarvestFullyGrown = _copySettings.HarvestFullyGrown;
                }
            };

            yield return pasteSettingsGizmo;
        }
        
        public override IEnumerable<Gizmo> GetZoneAddGizmos()
        {
            yield return DesignatorUtility.FindAllowedDesignator<Designator_ZoneAddHarvestTree_Expand>();
        }
    }
}
using Rage;
using Rage.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SirenSettingEdtor.Forms
{
    class MainEditor : GwenForm
    {
        private Gwen.Control.TextBox tName,
            tBPM, tTimeMultiplier,
            tFalloffMax, tFalloffExp,
            tAngleInner, tAngleOuter, tOffset,
            tLeftHeadSeq, tLeftHeadMod, tRightHeadSeq, tRightHeadMod,
            tLeftTailSeq, tLeftTailMod, tRightTailSeq, tRightTailMod;
        private DateTime lastChange = DateTime.MinValue;

        private bool BlockApply = false;

        public MainEditor() : base(typeof(MainEditorTemplate))
        {
            GameFiber.StartNew(delegate
            {
                while (true)
                {
                    GameFiber.Yield();
                    if (lastChange != DateTime.MinValue && (DateTime.Now - lastChange).TotalMilliseconds >= Plugin.EditorTimeout)
                    {
                        lastChange = DateTime.MinValue;
                        Apply();
                        LoadLights();
                    }
                }
            });
        }

        public override void InitializeLayout()
        {
            base.InitializeLayout();
            Window.ClampMovement = false;
            Window.DisableResizing();
            Window.BoundsChanged += Window_BoundsChanged;

            tName.TextChanged += TriggerLastChange;
            tBPM.TextChanged += TriggerLastChange;
            tTimeMultiplier.TextChanged += TriggerLastChange;
            tFalloffMax.TextChanged += TriggerLastChange;
            tFalloffExp.TextChanged += TriggerLastChange;
            tAngleInner.TextChanged += TriggerLastChange;
            tAngleOuter.TextChanged += TriggerLastChange;
            tOffset.TextChanged += TriggerLastChange;
            tLeftHeadSeq.TextChanged += TriggerLastChange;
            tLeftTailSeq.TextChanged += TriggerLastChange;
            tRightHeadSeq.TextChanged += TriggerLastChange;
            tRightTailSeq.TextChanged += TriggerLastChange;
            tLeftHeadMod.TextChanged += TriggerLastChange;
            tLeftTailMod.TextChanged += TriggerLastChange;
            tRightHeadMod.TextChanged += TriggerLastChange;
            tRightTailMod.TextChanged += TriggerLastChange;
        }

        private void Window_BoundsChanged(Gwen.Control.Base sender, EventArgs arguments)
        {
            Plugin.MEPos = Position;
        }

        private void TriggerLastChange(Gwen.Control.Base sender, EventArgs arguments)
        {
            if (BlockApply) return;
            lastChange = DateTime.Now;
        }

        public void LoadLights()
        {
            EmergencyLighting e = Plugin.EL;
            BlockApply = true;
            tName.Text = e.Name;
            tBPM.Text = e.SequencerBpm.ToString();
            tTimeMultiplier.Text = e.TimeMultiplier.ToString("");
            tFalloffMax.Text = e.LightFalloffMax.ToString("");
            tFalloffExp.Text = e.LightFalloffExponent.ToString("");
            tAngleInner.Text = e.LightInnerConeAngle.ToString("");
            tAngleOuter.Text = e.LightOuterConeAngle.ToString("");
            tOffset.Text = e.LightOffset.ToString("");
            tLeftHeadSeq.Text = e.LeftHeadLightSequence;
            tLeftTailSeq.Text = e.LeftTailLightSequence;
            tRightHeadSeq.Text = e.RightHeadLightSequence;
            tLeftHeadMod.Text = e.LeftHeadLightMultiples.ToString();
            tLeftTailMod.Text = e.LeftTailLightMultiples.ToString();
            tRightHeadMod.Text = e.RightHeadLightMultiples.ToString();
            tRightTailMod.Text = e.RightTailLightMultiples.ToString();
            Plugin.Logger.Log($"EmergencyLighting '{e.Name}' has been loaded");
            BlockApply = false;
        }

        public void Apply(bool alert = true)
        {
            if (BlockApply) return;
            BlockApply = true;
            string txt = "";//tName.Text.Trim();
            int incr = 1;
            if (txt.Length < 1) txt = "New Siren Settings";
            tName.Text = txt;
            while (EmergencyLighting.GetByName(txt).Exists())
            {
                txt = tName.Text + " " + incr;
                incr++;
            }
            Plugin.EL.Name = txt;
            txt = Regex.Replace(tBPM.Text, @"[^\d]", "");
            if (txt.Length < 1) txt = "0";
            tBPM.Text = MathHelper.Clamp(int.Parse(txt), 1, 1000).ToString();

            Plugin.EL.SequencerBpm = uint.Parse(tBPM.Text);
            Plugin.EL.TimeMultiplier = Validate.ValidateFloat(tTimeMultiplier);
            Plugin.EL.LightFalloffMax = Validate.ValidateFloat(tFalloffMax);
            Plugin.EL.LightFalloffExponent = Validate.ValidateFloat(tFalloffExp);
            Plugin.EL.LightInnerConeAngle = Validate.ValidateFloat(tAngleInner);
            Plugin.EL.LightOuterConeAngle = Validate.ValidateFloat(tAngleOuter);
            Plugin.EL.LightOffset = Validate.ValidateFloat(tOffset);
            Plugin.EL.LeftHeadLightSequence = Validate.ValidateSequence(tLeftHeadSeq);
            Plugin.EL.LeftTailLightSequence = Validate.ValidateSequence(tLeftTailSeq);
            Plugin.EL.RightHeadLightSequence = Validate.ValidateSequence(tRightHeadSeq);
            Plugin.EL.RightTailLightSequence = Validate.ValidateSequence(tRightTailSeq);
            Plugin.EL.LeftHeadLightMultiples = Validate.ValidateByte(tLeftHeadMod);
            Plugin.EL.LeftTailLightMultiples = Validate.ValidateByte(tLeftTailMod);
            Plugin.EL.RightHeadLightMultiples = Validate.ValidateByte(tRightHeadMod);
            Plugin.EL.RightTailLightMultiples = Validate.ValidateByte(tRightTailMod);

            //if (Plugin.Vehicle) Plugin.Vehicle.EmergencyLightingOverride = Plugin.EL;

            Plugin.Logger.Log($"EmergencyLighting '{Plugin.EL.Name}' has been applied");

            if (alert)
            {
                Game.DisplaySubtitle("~g~Saved");
                Rage.Native.NativeFunction.Natives.PlaySoundFrontend(-1, "YES", "HUD_FRONTEND_DEFAULT_SOUNDSET", false);
            }
            BlockApply = false;
        }
    }
}

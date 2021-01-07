using Rage;
using Rage.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SirenSettingEdtor.Forms
{
    class LightEditor : GwenForm
    {
        private Gwen.Control.TextBox tRotStart, tRotDelta, tRotSpeed, tRotMulti, tRotSeq,
            tFlaStart, tFlaDelta, tFlaSpeed, tFlaMulti, tFlaSeq,
            tCoronaSize, tCoronaPull, tCoronaIntensity, tLightGroup, tIntensity, tScale, tColor;
        private Gwen.Control.Label lIndex;
        private Gwen.Control.Button bPrev, bNext, bDisable;
        private Gwen.Control.CheckBox cScale, cCast, cSpot, cLight, cFace, cRot, cRotInvert, cRotBPM, cFla, cFlaInvert, cFlaBPM;

        public int Index = 0;

        private DateTime lastChange = DateTime.MinValue;

        private bool BlockApply = false;

        public LightEditor() : base(typeof(LightEditorTemplate))
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
            Window.BoundsChanged += Window_BoundsChanged; ;

            bPrev.Clicked += delegate { lastChange = DateTime.MinValue; Apply(false); Index--; LoadLights(); };
            bNext.Clicked += delegate { lastChange = DateTime.MinValue; Apply(false); Index++; LoadLights(); };
            bDisable.Clicked += BDisable_Clicked;

            cScale.CheckChanged += TriggerLastChangeNow;
            cCast.CheckChanged += TriggerLastChangeNow;
            cSpot.CheckChanged += TriggerLastChangeNow;
            cLight.CheckChanged += TriggerLastChangeNow;
            cFace.CheckChanged += TriggerLastChangeNow;
            cRot.CheckChanged += TriggerLastChangeNow;
            cRotInvert.CheckChanged += TriggerLastChangeNow;
            cRotBPM.CheckChanged += TriggerLastChangeNow;
            cFla.CheckChanged += TriggerLastChangeNow;
            cFlaInvert.CheckChanged += TriggerLastChangeNow;
            cFlaBPM.CheckChanged += TriggerLastChangeNow;

            tRotStart.TextChanged += TriggerLastChange;
            tRotDelta.TextChanged += TriggerLastChange;
            tRotSpeed.TextChanged += TriggerLastChange;
            tRotMulti.TextChanged += TriggerLastChange;
            tRotSeq.TextChanged += TriggerLastChange;
            tFlaStart.TextChanged += TriggerLastChange;
            tFlaDelta.TextChanged += TriggerLastChange;
            tFlaSpeed.TextChanged += TriggerLastChange;
            tFlaMulti.TextChanged += TriggerLastChange;
            tFlaSeq.TextChanged += TriggerLastChange;
            tCoronaSize.TextChanged += TriggerLastChange;
            tCoronaPull.TextChanged += TriggerLastChange;
            tCoronaIntensity.TextChanged += TriggerLastChange;
            tLightGroup.TextChanged += TriggerLastChange;
            //Color
            tIntensity.TextChanged += TriggerLastChange;
            tScale.TextChanged += TriggerLastChange;
        }

        private void BDisable_Clicked(Gwen.Control.Base sender, Gwen.Control.ClickedEventArgs arguments)
        {
            BlockApply = true;
            cRot.IsChecked = false;
            cRotInvert.IsChecked = false;
            cRotBPM.IsChecked = false;
            cFla.IsChecked = false;
            cFlaInvert.IsChecked = false;
            cFlaBPM.IsChecked = false;
            cFace.IsChecked = false;
            cLight.IsChecked = false;
            cSpot.IsChecked = false;
            cCast.IsChecked = false;
            cScale.IsChecked = false;
            BlockApply = false;
            TriggerLastChangeNow(null, null);
        }

        private void Window_BoundsChanged(Gwen.Control.Base sender, EventArgs arguments)
        {
            Plugin.LEPos = Position;
        }

        private void TriggerLastChange(Gwen.Control.Base sender, EventArgs arguments)
        {
            if (BlockApply) return;
            lastChange = DateTime.Now;
        }

        private void TriggerLastChangeNow(Gwen.Control.Base sender, EventArgs arguments)
        {
            if (BlockApply) return;
            lastChange = DateTime.MinValue;
            Apply();
            LoadLights();
        }

        public void LoadLights()
        {
            EmergencyLighting e = Plugin.EL;
            BlockApply = true;
            if (Index >= e.Lights.Length) Index = 0;
            if (Index < 0) Index = e.Lights.Length - 1;
            EmergencyLight x = e.Lights[Index];
            lIndex.Text = $"{Index + 1:00}/{e.Lights.Length:00}";
            Game.LogTrivial($"=== LIGHT #{Index + 1} ===");

            tRotStart.Text = x.RotationStart.ToString("");
            tRotDelta.Text = x.RotationDelta.ToString("");
            tRotSpeed.Text = x.RotationSpeed.ToString("");
            tRotMulti.Text = x.RotationMultiples.ToString();
            cRot.IsChecked = x.Rotate;
            cRotInvert.IsChecked = x.RotationDirection;
            cRotBPM.IsChecked = x.RotationSynchronizeToBpm;
            tRotSeq.Text = x.RotationSequence;
            Game.LogTrivial($"ROT (Rotate/Start/Delta/DeltaRAD/Spped/Multi/Direction/SyncToBPM/Sequence/SequenceRaw):");
            Game.LogTrivial($"{x.Rotate}/{x.RotationStart}/{x.RotationDelta}/{MathHelper.ConvertDegreesToRadians(x.RotationDelta)}/{x.RotationSpeed}/{x.RotationMultiples}/{x.RotationDirection}/{x.RotationSynchronizeToBpm}/{x.RotationSequence}/{x.RotationSequenceRaw}");

            tFlaStart.Text = x.FlashinessStart.ToString("");
            tFlaDelta.Text = x.FlashinessDelta.ToString("");
            tFlaSpeed.Text = x.FlashinessSpeed.ToString("");
            tFlaMulti.Text = x.FlashinessMultiples.ToString();
            cFla.IsChecked = x.Flash;
            cFlaInvert.IsChecked = x.FlashinessDirection;
            cFlaBPM.IsChecked = x.FlashinessSynchronizeToBpm;
            tFlaSeq.Text = x.FlashinessSequence;
            Game.LogTrivial($"FLA (Flash/Start/Delta/DeltaRAD/Spped/Multi/Direction/SyncToBPM/Sequence/SequenceRaw):");
            Game.LogTrivial($"{x.Flash}/{x.FlashinessStart}/{x.FlashinessDelta}/{MathHelper.ConvertDegreesToRadians(x.FlashinessDelta)}/{x.FlashinessSpeed}/{x.FlashinessMultiples}/{x.FlashinessDirection}/{x.FlashinessSynchronizeToBpm}/{x.FlashinessSequence}/{x.FlashinessSequenceRaw}");

            tCoronaSize.Text = x.CoronaSize.ToString("");
            tCoronaPull.Text = x.CoronaPull.ToString("");
            tCoronaIntensity.Text = x.CoronaIntensity.ToString("");
            cFace.IsChecked = x.CoronaFaceCamera;
            Game.LogTrivial($"CORONA (Size/Pull/Intensity/FaceCamera):");
            Game.LogTrivial($"{x.CoronaSize}/{x.CoronaPull}/{x.CoronaIntensity}/{x.CoronaFaceCamera}");

            tLightGroup.Text = x.LightGroup.ToString();
            tColor.Text = "#" + x.Color.R.ToString("X2") + x.Color.G.ToString("X2") + x.Color.B.ToString("X2") + x.Color.A.ToString("X2");
            tIntensity.Text = x.Intensity.ToString();
            cLight.IsChecked = x.Light;
            cSpot.IsChecked = x.SpotLight;
            cCast.IsChecked = x.CastShadows;
            Game.LogTrivial($"LIGHT (Group/Color/Intensity/Light/Spolight/CastShadows):");
            Game.LogTrivial($"{x.LightGroup}/0x{x.Color.R.ToString("X2") + x.Color.G.ToString("X2") + x.Color.B.ToString("X2") + x.Color.A.ToString("X2")}/{x.Intensity}/{x.Light}/{x.SpotLight}/{x.CastShadows}");

            tScale.Text = x.ScaleFactor.ToString();
            cScale.IsChecked = x.Scale;
            Game.LogTrivial($"SCALE (Scale/Factor):");
            Game.LogTrivial($"{x.Scale}/{x.ScaleFactor}");

            Plugin.Logger.Log($"Light '{Index}' has been loaded");
            BlockApply = false;
        }

        public void Apply(bool alert = true)
        {
            if (BlockApply) return;
            BlockApply = true;

            Plugin.EL.Lights[Index].RotationStart = Validate.ValidateFloat(tRotStart);
            Plugin.EL.Lights[Index].RotationDelta = Validate.ValidateFloat(tRotDelta);
            Plugin.EL.Lights[Index].RotationSpeed = Validate.ValidateFloat(tRotSpeed);
            Plugin.EL.Lights[Index].RotationMultiples = Validate.ValidateByte(tRotMulti);
            Plugin.EL.Lights[Index].RotationSequence = Validate.ValidateSequence(tRotSeq);
            Plugin.EL.Lights[Index].Rotate = cRot.IsChecked;
            Plugin.EL.Lights[Index].RotationDirection = cRotInvert.IsChecked;
            Plugin.EL.Lights[Index].RotationSynchronizeToBpm = cRotBPM.IsChecked;

            Plugin.EL.Lights[Index].FlashinessStart = Validate.ValidateFloat(tFlaStart);
            Plugin.EL.Lights[Index].FlashinessDelta = Validate.ValidateFloat(tFlaDelta);
            Plugin.EL.Lights[Index].FlashinessSpeed = Validate.ValidateFloat(tFlaSpeed);
            Plugin.EL.Lights[Index].FlashinessMultiples = Validate.ValidateByte(tFlaMulti);
            Plugin.EL.Lights[Index].FlashinessSequence = Validate.ValidateSequence(tFlaSeq);
            Plugin.EL.Lights[Index].Flash = cFla.IsChecked;
            Plugin.EL.Lights[Index].FlashinessDirection = cFlaInvert.IsChecked;
            Plugin.EL.Lights[Index].FlashinessSynchronizeToBpm = cFlaBPM.IsChecked;

            Plugin.EL.Lights[Index].CoronaSize = Validate.ValidateFloat(tCoronaSize);
            Plugin.EL.Lights[Index].CoronaPull = Validate.ValidateFloat(tCoronaPull);
            Plugin.EL.Lights[Index].CoronaIntensity = Validate.ValidateFloat(tCoronaIntensity);
            Plugin.EL.Lights[Index].CoronaFaceCamera = cFace.IsChecked;

            Plugin.EL.Lights[Index].LightGroup = Validate.ValidateByte(tLightGroup);
            //Color
            Plugin.EL.Lights[Index].Intensity = Validate.ValidateFloat(tIntensity);
            Plugin.EL.Lights[Index].Light = cLight.IsChecked;
            Plugin.EL.Lights[Index].SpotLight = cSpot.IsChecked;
            Plugin.EL.Lights[Index].CastShadows = cCast.IsChecked;

            Plugin.EL.Lights[Index].ScaleFactor = Validate.ValidateByte(tScale);
            Plugin.EL.Lights[Index].Scale = cScale.IsChecked;

            //if (Plugin.Vehicle) Plugin.Vehicle.EmergencyLightingOverride = Plugin.EL;

            Plugin.Logger.Log($"Light '{Index}' has been applied");

            if (alert)
            {
                Game.DisplaySubtitle("~g~Saved");
                Rage.Native.NativeFunction.Natives.PlaySoundFrontend(-1, "YES", "HUD_FRONTEND_DEFAULT_SOUNDSET", false);
            }

            BlockApply = false;
        }
    }
}

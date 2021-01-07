using Rage;
using Rage.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SirenSettingEdtor.Forms
{
    class Settings : GwenForm
    {
        private Gwen.Control.CheckBox cCam, cDrag;
        private Gwen.Control.Button bCopy, bSave;

        private static bool BlockApply = false;

        public Settings() : base(typeof(SettingsFormTemplate))
        {

        }

        public override void InitializeLayout()
        {
            base.InitializeLayout();
            Window.ClampMovement = false;
            Window.DisableResizing();
            Window.BoundsChanged += Window_BoundsChanged; ;

            cCam.IsChecked = Plugin.AllowCameraMovement;
            cDrag.IsChecked = Plugin.DragCamera;
            cCam.CheckChanged += CCam_CheckChanged;
            cDrag.CheckChanged += CDrag_CheckChanged;

            bCopy.Clicked += BCopy_Clicked;
        }

        private void Window_BoundsChanged(Gwen.Control.Base sender, EventArgs arguments)
        {
            Plugin.SEPos = Position;
        }

        private void BCopy_Clicked(Gwen.Control.Base sender, Gwen.Control.ClickedEventArgs arguments)
        {
            Rage.Game.SetClipboardText(XML.Write(Plugin.EL).InnerXML());
            Game.DisplaySubtitle("~b~Copied to Clipboard");
            Rage.Native.NativeFunction.Natives.PlaySoundFrontend(-1, "YES", "HUD_FRONTEND_DEFAULT_SOUNDSET", false);
        }

        private void CDrag_CheckChanged(Gwen.Control.Base sender, EventArgs arguments)
        {
            if (BlockApply) return;
            if (cDrag.IsChecked)
            {
                BlockApply = true;
                cCam.IsChecked = false;
                Plugin.AllowCameraMovement = false;
                BlockApply = false;
            }
            Plugin.DragCamera = cDrag.IsChecked;
        }

        private void CCam_CheckChanged(Gwen.Control.Base sender, EventArgs arguments)
        {
            if (BlockApply) return;
            if (cCam.IsChecked)
            {
                BlockApply = true;
                cDrag.IsChecked = false;
                Plugin.DragCamera = false;
                BlockApply = false;
            }
            Plugin.AllowCameraMovement = cCam.IsChecked;
        }
    }
}

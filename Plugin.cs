using BadMusician.Common;
using Rage;
using RAGENativeUI;
using RAGENativeUI.Elements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;
using System.Drawing;

[assembly: Rage.Attributes.Plugin("SirenSetting Editor",
    Description = "Edit your SirenSetting on the fly",
    Author = "BadMusician",
    EntryPoint = "SirenSettingEdtor.Plugin.OnLoad",
    ExitPoint = "SirenSettingEdtor.Plugin.OnUnload",
    PrefersSingleInstance = true,
    SupportUrl = "",
    ShouldTickInPauseMenu = false)]

namespace SirenSettingEdtor
{
    public class Plugin
    {
        public const int FormsMargin = 50;
        public const int EditorTimeout = 500;

        private static CultureInfo IC = CultureInfo.InvariantCulture;

        internal static bool AllowCameraMovement = false;
        internal static bool DragCamera = true;

        internal static bool DisableUI = false;

        internal static Logger Logger { get; private set; }
        internal static Vehicle Vehicle = null;
        internal static EmergencyLighting EL { get; set; } = new Model("police").EmergencyLighting.Clone();
        internal static int EditingIndex { get; set; } = 0;
        internal static EmergencyLight Light { get { return EL.Lights[EditingIndex]; } }


        private static GameFiber fiber;
        private static Forms.LightEditor LE;
        internal static Point LEPos;
        private static Forms.MainEditor ME;
        internal static Point MEPos;
        private static Forms.Settings SE;
        internal static Point SEPos;
        private static bool Pos;

        public static void OnLoad()
        {
            Logger = new Logger("SirenSetting Editor", true, false, "[TOOLS]");

            fiber = GameFiber.StartNew(delegate
            {
                while (true)
                {
                    GameFiber.Yield();
                    if (Game.IsKeyDown(System.Windows.Forms.Keys.Add))
                    {
                        DisableUI = !DisableUI;
                        Game.DisplayHelp($"SirenSetting Editor~n~UI " + (DisableUI ? "~r~Disabled" : "~g~Enabled"));
                    }
                    if (!DisableUI && Game.LocalPlayer.Character &&
                    Game.LocalPlayer.Character.CurrentVehicle &&
                    Game.LocalPlayer.Character.CurrentVehicle.HasSiren)
                    {
                        if (Vehicle == null)
                        {
                            Vehicle = Game.LocalPlayer.Character.CurrentVehicle;
                            Vehicle.IsSirenOn = true;
                            Vehicle.IsSirenSilent = true;
                            EL = Vehicle.EmergencyLighting.Clone();
                            Vehicle.EmergencyLightingOverride = EL;
                            if (LE != null) LE?.Window?.Close();
                            if (ME != null) ME?.Window?.Close();
                            if (SE != null) SE?.Window?.Close();
                            LE = new Forms.LightEditor();
                            ME = new Forms.MainEditor();
                            SE = new Forms.Settings();
                            LE.Show();
                            ME.Show();
                            SE.Show();
                            if (!Pos)
                            {
                                Pos = true;
                                ME.Position = new Point(FormsMargin, FormsMargin);
                                LE.Position = new Point(ME.Position.X, ME.Position.Y + ME.Size.Height + FormsMargin);
                                SE.Position = new Point(LE.Position.X + LE.Size.Width + FormsMargin, LE.Position.Y);
                            }
                            else
                            {
                                ME.Position = MEPos;
                                LE.Position = LEPos;
                                SE.Position = SEPos;
                            }
                            ME.LoadLights();
                            LE.LoadLights();
                        }
                    }
                    else
                    {
                        Vehicle = null;
                        if (LE != null) LE?.Window?.Close();
                        if (ME != null) ME?.Window?.Close();
                        if (SE != null) SE?.Window?.Close();
                        LE = null;
                        ME = null;
                        SE = null;
                        AllowCameraMovement = true;
                    }

                    if (Vehicle != null)
                    {
                        if (AllowCameraMovement || (DragCamera && Game.GetMouseState().IsLeftButtonDown))
                        {
                            Rage.Native.NativeFunction.Natives.EnableControlAction(0, (int)GameControl.LookLeftRight);
                            Rage.Native.NativeFunction.Natives.EnableControlAction(0, (int)GameControl.LookUpDown);
                        }
                        else
                        {
                            Rage.Native.NativeFunction.Natives.DisableControlAction(0, (int)GameControl.LookLeftRight);
                            Rage.Native.NativeFunction.Natives.DisableControlAction(0, (int)GameControl.LookUpDown);
                        }
                    }
                }
            }, "SirenSettingEdtor: Main");
        }

        public static void OnUnload(bool isCrashing)
        {
            Rage.Native.NativeFunction.Natives.EnableControlAction(0, (int)GameControl.LookLeftRight);
            Rage.Native.NativeFunction.Natives.EnableControlAction(0, (int)GameControl.LookUpDown);
        }
    }
}

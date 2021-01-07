using Rage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Globalization;
using System.Drawing;
using XE = System.Xml.Linq.XElement;
using XA = System.Xml.Linq.XAttribute;
using System.Xml.Linq;

namespace SirenSettingEdtor
{
    static class XML
    {
        public static EmergencyLighting Read(XmlNode n)
        {
            Plugin.Logger.Log($"Reading EmergencyLighting from XML...");
            EmergencyLighting e = new Model("police").EmergencyLighting.Clone();
            if (n == null) return e;
            e.Name = "EmergencyLighting";//
            e.TimeMultiplier = readNodeValue(n, "timeMultiplier", e.TimeMultiplier);//
            e.LightFalloffMax = readNodeValue(n, "lightFalloffMax", e.LightFalloffMax);//
            e.LightFalloffExponent = readNodeValue(n, "lightFalloffExponent", e.LightFalloffExponent);//
            e.LightInnerConeAngle = readNodeValue(n, "lightInnerConeAngle", e.LightInnerConeAngle);//
            e.LightOuterConeAngle = readNodeValue(n, "lightOuterConeAngle", e.LightOuterConeAngle);//
            e.LightOffset = readNodeValue(n, "lightOffset", e.LightOffset);
            e.TextureHash = Game.GetHashKey(readNodeValue(n, "textureName", "VehicleLight_sirenlight", "sameAsName", true));
            e.SequencerBpm = readNodeValue(n, "sequencerBpm", e.SequencerBpm);//
            e.LeftHeadLightSequence = readNodeValue(n, "leftHeadLightSequence", e.LeftHeadLightSequence, "leftHeadLight/sequencer");//
            e.RightHeadLightSequence = readNodeValue(n, "rightHeadLightSequence", e.RightHeadLightSequence, "rightHeadLight/sequencer");//
            e.LeftTailLightSequence = readNodeValue(n, "leftTailLightSequence", e.LeftTailLightSequence, "leftTailLight/sequencer");//
            e.RightTailLightSequence = readNodeValue(n, "rightTailLightSequence", e.RightTailLightSequence, "rightTailLight/sequencer");//
            e.LeftHeadLightMultiples = readNodeValue(n, "leftHeadLightMultiples", e.LeftHeadLightMultiples);//
            e.RightHeadLightMultiples = readNodeValue(n, "rightHeadLightMultiples", e.RightHeadLightMultiples);//
            e.LeftTailLightMultiples = readNodeValue(n, "leftTailLightMultiples", e.LeftTailLightMultiples);//
            e.RightTailLightMultiples = readNodeValue(n, "rightTailLightMultiples", e.RightTailLightMultiples);//
            e.UseRealLights = readNodeValue(n, "useRealLights", e.UseRealLights);
            int i = 0;
            foreach (XmlNode s in n.SelectNodes("sirens/Item"))
            {
                if (e.Lights.Length > i)
                {
                    e.Lights[i].RotationDelta = readNodeValue(s, "delta", e.Lights[i].RotationDelta, "rotation/delta");//
                    e.Lights[i].RotationStart = readNodeValue(s, "start", e.Lights[i].RotationStart, "rotation/start");//
                    e.Lights[i].RotationSpeed = readNodeValue(s, "speed", e.Lights[i].RotationSpeed, "rotation/speed");//
                    e.Lights[i].RotationSequence = readNodeValue(s, "sequencer", e.Lights[i].RotationSequence, "rotation/sequencer");//
                    e.Lights[i].RotationMultiples = readNodeValue(s, "multiples", e.Lights[i].RotationMultiples, "rotation/multiples");//
                    e.Lights[i].RotationDirection = readNodeValue(s, "direction", e.Lights[i].RotationDirection, "rotation/direction");//
                    e.Lights[i].RotationSynchronizeToBpm = readNodeValue(s, "syncToBpm", e.Lights[i].RotationSynchronizeToBpm, "rotation/syncToBpm");//
                    e.Lights[i].FlashinessDelta = readNodeValue(s, "delta", e.Lights[i].FlashinessDelta, "flashiness/delta");//
                    e.Lights[i].FlashinessStart = readNodeValue(s, "start", e.Lights[i].FlashinessStart, "flashiness/start");//
                    e.Lights[i].FlashinessSpeed = readNodeValue(s, "speed", e.Lights[i].FlashinessSpeed, "flashiness/speed");//
                    e.Lights[i].FlashinessSequence = readNodeValue(s, "sequencer", e.Lights[i].FlashinessSequence, "flashiness/sequencer");//
                    e.Lights[i].FlashinessMultiples = readNodeValue(s, "multiples", e.Lights[i].FlashinessMultiples, "flashiness/multiples");//
                    e.Lights[i].FlashinessDirection = readNodeValue(s, "direction", e.Lights[i].FlashinessDirection, "flashiness/direction");//
                    e.Lights[i].FlashinessSynchronizeToBpm = readNodeValue(s, "syncToBpm", e.Lights[i].FlashinessSynchronizeToBpm, "flashiness/syncToBpm");//
                    e.Lights[i].CoronaIntensity = readNodeValue(s, "intensity", e.Lights[i].CoronaIntensity, "corona/intensity");//
                    e.Lights[i].CoronaSize = readNodeValue(s, "size", e.Lights[i].CoronaSize, "corona/size");//
                    e.Lights[i].CoronaPull = readNodeValue(s, "pull", e.Lights[i].CoronaPull, "corona/pull");//
                    e.Lights[i].CoronaFaceCamera = readNodeValue(s, "faceCamera", e.Lights[i].CoronaFaceCamera, "corona/faceCamera");//
                    e.Lights[i].Color = readNodeValue(s, "color", e.Lights[i].Color);//
                    e.Lights[i].Intensity = readNodeValue(s, "intensity", e.Lights[i].Intensity);//
                    e.Lights[i].LightGroup = readNodeValue(s, "lightGroup", e.Lights[i].LightGroup);//
                    e.Lights[i].Rotate = readNodeValue(s, "rotate", e.Lights[i].Rotate);//
                    e.Lights[i].Scale = readNodeValue(s, "scale", e.Lights[i].Scale);
                    e.Lights[i].ScaleFactor = readNodeValue(s, "scaleFactor", e.Lights[i].ScaleFactor);
                    e.Lights[i].Flash = readNodeValue(s, "flash", e.Lights[i].Flash);//
                    e.Lights[i].Light = readNodeValue(s, "light", e.Lights[i].Light);//
                    e.Lights[i].SpotLight = readNodeValue(s, "spotLight", e.Lights[i].SpotLight);//
                    e.Lights[i].CastShadows = readNodeValue(s, "castShadows", e.Lights[i].CastShadows);//
                    i++;
                }
                else break;
            }
            return e;
        }

        private static T readNodeValue<T>(XmlNode parent, string name, T defaultValue, string path = "sameAsName", bool innerText = false)
        {
            if (path == "sameAsName") path = name;
            CultureInfo c = CultureInfo.InvariantCulture;
            bool fail = true;
            object ret = null;
            string value = innerText ? parent.SelectSingleNode(path).InnerText : parent.SelectSingleNode(path).Attributes.GetNamedItem("value").Value;
            switch (typeof(T).Name.ToLowerInvariant())
            {
                case "string":
                    fail = value.Length < 1;
                    if (!fail) ret = value;
                    break;
                case "float":
                    fail = float.TryParse(value, NumberStyles.Float, c, out float xfloat);
                    if (!fail) ret = xfloat;
                    break;
                case "int":
                    fail = int.TryParse(value, NumberStyles.Integer, c, out int xint);
                    if (!fail) ret = xint;
                    break;
                case "byte":
                    fail = int.TryParse(value, NumberStyles.Integer, c, out int xbyte);
                    if (!fail) ret = (byte)(xbyte < 0 ? 0 : xbyte > 7 ? 7 : xbyte);
                    break;
                case "uint":
                    fail = int.TryParse(value, NumberStyles.Integer, c, out int xuint);
                    if (!fail) ret = (uint)Math.Abs(xuint);
                    break;
                case "bool":
                    fail = value.ToLowerInvariant() != "true" && value.ToLowerInvariant() != "false";
                    if (!fail) ret = value.ToLowerInvariant() == "true";
                    break;
                case "color":
                    fail = value.Length != 10 || !value.StartsWith("0x");
                    if (!fail) ret = ColorTranslator.FromHtml($"#{value.Remove(0, 2)}");
                    break;

            }
            if (ret == null) ret = defaultValue;
            if (fail)
                Plugin.Logger.Log($"Error reading '{name}'. Using default value: {ret}.");
            else
                Plugin.Logger.Log($"Found '{name}'. Value: ${ret}.");
            return (T)ret;
        }


        public static XE Write(EmergencyLighting e)
        {
            XE root = new XE("Item");
            //root.Add(new XE("name", e.Name));
            root.Add(new XE("timeMultiplier", new XA("value", e.TimeMultiplier.ToString(""))));
            root.Add(new XE("lightFalloffMax", new XA("value", e.LightFalloffMax.ToString(""))));
            root.Add(new XE("lightFalloffExponent", new XA("value", e.LightFalloffExponent.ToString(""))));
            root.Add(new XE("lightInnerConeAngle", new XA("value", e.LightInnerConeAngle.ToString(""))));
            root.Add(new XE("lightOuterConeAngle", new XA("value", e.LightOuterConeAngle.ToString(""))));
            root.Add(new XE("lightOffset", new XA("value", e.LightOffset.ToString(""))));
            root.Add(new XE("textureName", "VehicleLight_sirenlight"));
            root.Add(new XE("sequencerBpm", new XA("value", e.SequencerBpm.ToString())));
            root.Add(new XE("leftHeadLight", new XE("sequencer", new XA("value", e.LeftHeadLightSequenceRaw.ToString()))));
            root.Add(new XE("rightHeadLight", new XE("sequencer", new XA("value", e.RightHeadLightSequenceRaw.ToString()))));
            root.Add(new XE("leftTailLight", new XE("sequencer", new XA("value", e.LeftTailLightSequenceRaw.ToString()))));
            root.Add(new XE("rightTailLight", new XE("sequencer", new XA("value", e.RightTailLightSequenceRaw.ToString()))));
            root.Add(new XE("leftHeadLightMultiples", new XA("value", e.LeftHeadLightMultiples.ToString())));
            root.Add(new XE("rightHeadLightMultiples", new XA("value", e.RightHeadLightMultiples.ToString())));
            root.Add(new XE("leftTailLightMultiples", new XA("value", e.LeftTailLightMultiples.ToString())));
            root.Add(new XE("rightTailLightMultiples", new XA("value", e.RightTailLightMultiples.ToString())));
            root.Add(new XE("useRealLights", new XA("value", e.UseRealLights.ToString().ToLower())));
            XE sirens = new XE("sirens");
            int idx = 1;
            foreach (EmergencyLight x in e.Lights)
            {
                XE i = new XE("Item");
                i.Add(new XComment($"Siren {idx}"));
                i.Add(new XE("rotation",
                    new XE("delta", new XA("value", MathHelper.ConvertDegreesToRadians(x.RotationDelta).ToString(""))),
                    new XE("start", new XA("value", x.RotationStart.ToString(""))),
                    new XE("speed", new XA("value", x.RotationSpeed.ToString(""))),
                    new XE("sequencer", new XA("value", x.RotationSequenceRaw.ToString())),
                    new XE("multiples", new XA("value", x.RotationMultiples.ToString())),
                    new XE("direction", new XA("value", x.RotationDirection.ToString().ToLower())),
                    new XE("syncToBpm", new XA("value", x.RotationSynchronizeToBpm.ToString().ToLower()))));
                Game.LogTrivial($"FL Delta: {x.FlashinessDelta}");
                i.Add(new XE("flashiness",
                    new XE("delta", new XA("value", MathHelper.ConvertDegreesToRadians(x.FlashinessDelta).ToString(""))),
                    new XE("start", new XA("value", x.FlashinessStart.ToString(""))),
                    new XE("speed", new XA("value", x.FlashinessSpeed.ToString(""))),
                    new XE("sequencer", new XA("value", x.FlashinessSequenceRaw.ToString())),
                    new XE("multiples", new XA("value", x.FlashinessMultiples.ToString())),
                    new XE("direction", new XA("value", x.FlashinessDirection.ToString().ToLower())),
                    new XE("syncToBpm", new XA("value", x.FlashinessSynchronizeToBpm.ToString().ToLower()))));
                i.Add(new XE("corona",
                    new XE("intensity", new XA("value", x.CoronaIntensity.ToString(""))),
                    new XE("size", new XA("value", x.CoronaSize.ToString(""))),
                    new XE("pull", new XA("value", x.CoronaPull.ToString(""))),
                    new XE("faceCamera", new XA("value", x.CoronaFaceCamera.ToString().ToLower()))));
                i.Add(new XE("color", new XA("value", "0x" + x.Color.R.ToString("X2") + x.Color.G.ToString("X2") + x.Color.B.ToString("X2") + x.Color.A.ToString("X2"))));
                i.Add(new XE("intensity", new XA("value", x.Intensity.ToString(""))));
                i.Add(new XE("lightGroup", new XA("value", x.LightGroup.ToString())));
                i.Add(new XE("rotate", new XA("value", x.Rotate.ToString().ToLower())));
                i.Add(new XE("scale", new XA("value", x.Scale.ToString().ToLower())));
                i.Add(new XE("scaleFactor", new XA("value", x.ScaleFactor.ToString())));
                i.Add(new XE("flash", new XA("value", x.Flash.ToString().ToLower())));
                i.Add(new XE("light", new XA("value", x.Light.ToString().ToLower())));
                i.Add(new XE("spotLight", new XA("value", x.SpotLight.ToString().ToLower())));
                i.Add(new XE("castShadows", new XA("value", x.CastShadows.ToString().ToLower())));
                sirens.Add(i);
                idx++;
            }
            root.Add(sirens);
            return root;
        }
    }

    public static class XElementExtension
    {
        public static string InnerXML(this XE el)
        {
            var reader = el.CreateReader();
            reader.MoveToContent();
            string x = reader.ReadInnerXml();
            reader.Dispose();
            return x;
        }
    }
}

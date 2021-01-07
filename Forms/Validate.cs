using Rage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Gwen.Control;

namespace SirenSettingEdtor.Forms
{
    internal static class Validate
    {
        public static float ValidateFloat(TextBox t)
        {
            string txt = Regex.Replace(t.Text, @"[^.\d]", "");
            if (txt.StartsWith(".")) txt = "0" + txt;
            if (txt.Replace(".", "").Length < 1) txt = "0";
            float val = float.Parse(txt);
            t.Text = val.ToString("");
            return val;
        }
        public static byte ValidateByte(TextBox t)
        {
            string txt = Regex.Replace(t.Text, @"[^\d]", "");
            if (txt.Length < 1) txt = "0";
            byte val = (byte)MathHelper.Clamp(int.Parse(txt), 0, 255);
            t.Text = val.ToString();
            return val;
        }
        public static string ValidateSequence(TextBox t)
        {
            string txt = Regex.Replace(t.Text, @"[^01]", "");
            txt += "00000000000000000000000000000000";
            string val = new string(txt.Take(32).ToArray());
            t.Text = val;
            return val;
        }
    }
}

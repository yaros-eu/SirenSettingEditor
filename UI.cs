using BadMusician.Common;
using Rage;
using RAGENativeUI;
using RAGENativeUI.Elements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SirenSettingEdtor
{
    internal class UI : Initializable
    {
        private GameFiber fiber = null;
        private MenuPool pool = new MenuPool();

        public UIMenu Menu;

        public UIMenuItem sequencerBPM;

        protected override void OnDeinitialize()
        {
            if (fiber != null && fiber.IsAlive) fiber.Abort();
            pool.Clear();
        }

        protected override void OnInitialize()
        {
            Menu = new UIMenu("SirenSetting Edtor", "by BadMusician");
            sequencerBPM = new UIMenuItem("Sequencer BPM", "sequencerBpm ~b~<int>");
            fiber = GameFiber.StartNew(delegate
            {
                while (true)
                {
                    GameFiber.Yield();
                    pool.ProcessMenus();
                }
            }, "SirenSettingEdtor: UI");
        }
    }
}

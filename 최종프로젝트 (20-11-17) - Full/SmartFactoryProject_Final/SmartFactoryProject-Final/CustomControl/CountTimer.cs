using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SmartFactoryProject_Final.CustomControl
{
    class CountTimer : Timer
    {
        static int[] SpeedLevel = { 500, 250, 100 };
        int currentLevel { get; set; } = 0;

        static int[] LevelUpCount = { 2, 6 };
        int tickCount { get; set; } = 0;

        EventHandler tickEvent;

        public CountTimer()
        {
            this.Enabled = false;
            this.Interval = SpeedLevel[0];
            this.Tick += CountTick;
        }

        public void Start(EventHandler tickEvent)
        {
            ChangeSpeedLevel(0);
            this.tickEvent = tickEvent;
            this.Tick += this.tickEvent;
            this.Enabled = true;
        }

        public new void Stop()
        {
            this.Enabled = false;
            this.Tick -= tickEvent;
            tickEvent = null;
        }

        private void CountTick(object sender, EventArgs e)
        {
            if (currentLevel >= LevelUpCount.Length)
                return;

            tickCount++;
            if (tickCount > LevelUpCount[currentLevel])
                ChangeSpeedLevel(currentLevel + 1);
        }

        private void ChangeSpeedLevel(int level)
        {
            if (level < 0 || level >= SpeedLevel.Length)
                return;

            currentLevel = level;
            tickCount = 0;
            this.Interval = SpeedLevel[currentLevel];
        }
    }
}
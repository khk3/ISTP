using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Bullseye
{
    public class DateTimeClass
    {
        private Timer timer;

        public DateTimeClass() { }

        public void Timer()
        {
            
            timer = new Timer();
            timer.Interval = 1000;
            timer.Tick += Timer_Tick;
            timer.Start();
        }
        private void Timer_Tick(object sender, EventArgs e)
        {       
        }

    }
}

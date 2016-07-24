using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectDaedalus
{
    public class Updater
    {
        public delegate void Update(int timeElapsed, int deltaMilli, Dictionary<string, object> state, ref bool finished);

        private Dictionary<string, object> state;

        private int timeElapsed;

        private Update updater;

        private bool finished;

        public Updater(Update updater)
        {
            this.updater = updater;
            this.finished = false;
            this.timeElapsed = 0;
            this.state = new Dictionary<string, object>();
        }

        public void Do(int deltaMilli)
        {
            timeElapsed += deltaMilli;
            updater.Invoke(timeElapsed, deltaMilli, state, ref finished);
        }

        public bool IsFinished
        {
            get { return finished; }
        }
    }
}

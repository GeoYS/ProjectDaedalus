using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ProjectDaedalus
{
    public static class Repo
    {

        private static volatile Queue<Action> updates = new Queue<Action>();

        public static void QueueUpdate(Action update)
        {
            updates.Enqueue(update);
        }

        public static void PerformNextUpdate()
        {
            updates.Dequeue().Invoke();
        }

        /*
            Update Firebase
        */
        public static void PerformAllUpdates()
        {
            while(updates.Any())
            {
                updates.Dequeue().Invoke();
            }
        }
    }
}

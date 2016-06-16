using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectDaedalus
{
    public static class Repo
    {
        private static List<Tuple<Guid, string, string>> changes = new List<Tuple<Guid, string, string>>();

        public static void Track(Tuple<Guid, string, string> change)
        {
            changes.Add(change);
        }

        // TODO: Update firebase
    }
}

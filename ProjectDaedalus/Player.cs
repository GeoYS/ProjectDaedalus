using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectDaedalus
{
    public class Player
    {
        public string Name { get; set; }
        public List<Guid> PlayerTechnologies { get; set; }
        public HashSet<string> VisibleTiles { get; set; }
        public Dictionary<string, object> Properties { get; }

        public Player(string name)
        {
            this.Name = name;
            PlayerTechnologies = new List<Guid>();
            VisibleTiles = new HashSet<string>();
            Properties = new Dictionary<string, object>();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectDaedalus
{
    public class Player
    {
        public string name { get; set; }
        public List<Guid> playerTechnologies { get; set; }
        public HashSet<string> visibleTiles { get; set; }

        public Player(string name)
        {
            this.name = name;
            playerTechnologies = new List<Guid>();
            visibleTiles = new HashSet<string>();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectDaedalus
{
    public class World
    {
        Map map;
        Dictionary<Guid, Entity> establishment;
        Dictionary<Guid, Entity> armies;
        Dictionary<Guid, Entity> battles;
        Dictionary<string, Player> players;

        public World()
        {
        }

        public void GenerateWorld(List<string> playerNames, int size = 30)
        {
            map = new Map(size, size);
            establishment = new Dictionary<Guid, Entity>();
            armies = new Dictionary<Guid, Entity>();
            battles = new Dictionary<Guid, Entity>();
            players = new Dictionary<string, Player>();

        }

        public void Update(int deltaMilli)
        {

        }

        public class Map
        {
            public Map(int tilesHigh, int tilesWide)
            {
                this.tilesHigh = tilesHigh;
                this.tilesWide = tilesWide;
                this.tiles = new Dictionary<string, Entity>();
            }

            public int tilesHigh { get; set; }
            public int tilesWide { get; set; }

            public Dictionary<string, Entity> tiles { get; set; }
        }
    }
}

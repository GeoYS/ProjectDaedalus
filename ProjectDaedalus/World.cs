using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectDaedalus
{
    public class World
    {
        public Map map { get; set; }
        public Dictionary<Guid, Entity> establishment { get; set; }
        public Dictionary<Guid, Entity> armies { get; set; }
        public Dictionary<Guid, Entity> battles { get; set; }
        public Dictionary<string, Player> players { get; set; }

        private World(int size)
        {
            map = new Map(size, size);
            establishment = new Dictionary<Guid, Entity>();
            armies = new Dictionary<Guid, Entity>();
            battles = new Dictionary<Guid, Entity>();
            players = new Dictionary<string, Player>();
        }

        public static World GenerateWorld(List<string> playerNames, int size = 30)
        {
            World world = new World(size);

            foreach (var playerName in playerNames)
            {
                world.players.Add(playerName, new Player(playerName));
            }

            int tilesHigh = size;
            int tilesWide = size;

            Random rnd = new Random();

            for (int i = 0; i < tilesHigh; i++)
            {
                for (int j = 0; j < tilesWide; j++)
                {
                    string key = string.Format("{0},{1}", i, j);
                    Area tile = new Area();

                    tile["tileType"] = rnd.NextDouble() > 0.5 ? "grassland" : "water";

                    if((string)tile["tileType"] == "grassland")
                    {
                        if (rnd.NextDouble() > 0.95) {
                            Area.Resources res = (Area.Resources)tile["resources"];
                            res.amount["wood"] = 100;
                            res.amount["food"] = 200;
                            res.amount["ore"] = 300;
                        }
                    }

                    world.map.tiles[key] = tile;
                }
            }

            return world;
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

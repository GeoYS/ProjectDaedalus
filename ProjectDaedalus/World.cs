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
        public Dictionary<Guid, Area> areas { get; }
        public Dictionary<Guid, Entity> armies { get; }
        public Dictionary<Guid, Entity> battles { get; }
        public Dictionary<string, Player> players { get; }

        private List<Updater> toUpdate { get; }

        private World()
        {
            areas = new Dictionary<Guid, Area>();
            armies = new Dictionary<Guid, Entity>();
            battles = new Dictionary<Guid, Entity>();
            players = new Dictionary<string, Player>();
            toUpdate = new List<Updater>();
        }

        private World(int size) : this()
        {
            map = new Map(size, size);
        }

        private World(int sizeX, int sizeY) : this()
        {
            map = new Map(sizeX, sizeY);
        }

        public void Update(int deltaMilli)
        {
            List<Updater> finished = new List<Updater>();
            foreach(Updater u in toUpdate)
            {
                if(u.IsFinished)
                {
                    finished.Add(u);
                    continue;
                }
                u.Do(deltaMilli);
            }
            foreach(Updater u in finished)
            {
                finished.Remove(u);
            }
        }

        public void AddUpdater(Updater updater)
        {
            toUpdate.Add(updater);
        }

        public void ProcessUserRequest(Request request)
        {
            switch (request.Type)
            {
                case Request.RequestType.DEPLOY_TROOP:
                    {
                        var numTroops = int.Parse(request.Data["number"]);
                        var x = int.Parse(request.Data["x"]);
                        var y = int.Parse(request.Data["y"]);
                        var unitType = (Units.UnitType) int.Parse(request.Data["unitType"]);
                        var player = players.Where(kv => kv.Key == request.Player).FirstOrDefault().Value;
                        
                        if(player.Properties[request.Data["unitType"]] != null)
                        {
                            var numAvail = (int)player.Properties[request.Data["unitType"]];

                            if(numAvail >= numTroops)
                            {
                                var area = areas[map.tiles[x + "," + y]];

                                if(area["occupier"] == player.Name && area["isDeployment"] == "1")
                                {
                                    AddUnitsToArea(area, unitType, numTroops);
                                    player.Properties[request.Data["unitType"]] = numAvail - numTroops;
                                }
                            }
                        }

                        break;
                    }
                case Request.RequestType.MOVE_ARMY:
                    {
                        var numTroopTypes = int.Parse(request.Data["troopTypes"]);

                        // determine if troops to move is valid amount

                        // remove troops from area, create army and add troops

                        // set army destination, add updater to world

                        break;
                    }
                default:
                    {
                        Console.WriteLine("Unexpected user request type!");
                        break;
                    }
            }
        }

        private void AddUnitsToArea(Area area, Units.UnitType unitType, int number)
        {
            if(area.units.unallocated[unitType] == null)
            {
                area.units.unallocated[unitType] = Units.NewUnitGroup(unitType, number);
            }
            else
            {
                var unitGroup = area.units.unallocated[unitType];
                unitGroup.number += number;
            }
        }

        public class Map
        {
            public Map(int tilesHigh, int tilesWide)
            {
                this.tilesHigh = tilesHigh;
                this.tilesWide = tilesWide;
                this.tiles = new Dictionary<string, Guid>();
            }

            public int tilesHigh { get; set; }
            public int tilesWide { get; set; }

            public Dictionary<string, Guid> tiles { get; set; }
        }

        private static World currentWorld = null;

        public static World CurrentWorld
        {
            get
            {
                return currentWorld;
            }
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
                    string coord = string.Format("{0},{1}", i, j);
                    Area tile = new Area();

                    tile["tileType"] = rnd.NextDouble() > 0.5 ? "grassland" : "water";

                    if ((string)tile["tileType"] == "grassland")
                    {
                        Area.Resources res = tile.resources;
                        res.amount["wood"] = 100;
                        res.amount["food"] = 200;
                        res.amount["ore"] = 300;
                    }

                    world.areas[tile.id] = tile;
                    world.map.tiles[coord] = tile.id;
                }
            }

            currentWorld = world;
            return world;
        }
    }
}

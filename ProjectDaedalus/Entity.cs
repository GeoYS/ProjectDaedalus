using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectDaedalus
{
    public class Entity
    {
        public Dictionary<string, string> properties { get; }

        public Guid id { get; }

        public Player owningPlayer { get; }

        private List<Entity> toSpawn;

        private bool remove;

        public Entity()
        {
            id = Guid.NewGuid();
            remove = false;
            properties = new Dictionary<string, string>();
            toSpawn = new List<Entity>();
        }

        public Entity(params Tuple<string, string>[] properties ) : this()
        {
            foreach (var property in properties)
            {
                this.properties[property.Item1] = property.Item2;
            }
        }

        public string this[string key]
        {
            get { return properties[key]; }
            set
            {
                properties[key] = value;
                //Repo.Track(Tuple.Create(id, key, value.ToString()));
            }
        }

        public List<Entity> spawnEntities()
        {
            if (toSpawn.Count == 0)
            {
                return toSpawn;
            }
            else
            {
                var tempSpawn = toSpawn;
                toSpawn = new List<Entity>();
                return tempSpawn;
            }
        }

        public void FlagRemove()
        {
            remove = true;
        }

        public bool ToRemove()
        {
            return remove;
        }
    }
}

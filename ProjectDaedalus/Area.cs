using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectDaedalus
{
    public class Area : Entity
    {
        public Area() : base()
        {
            this.properties["resources"] = new Resources();
            this.properties["buildings"] = new Buildings();
            this.properties["areaUnits"] = new AreaUnits();
        }

        public Area(params Tuple<string, string>[] properties) : base(properties)
        {
            this.properties["resources"] = new Resources();
            this.properties["buildings"] = new Buildings();
            this.properties["areaUnits"] = new AreaUnits();
        }

        public Area(Resources res, Buildings buildings, params Tuple<string, string>[] properties) : base(properties)
        {
            this.properties["resources"] = res;
            this.properties["buildings"] = buildings;
            this.properties["areaUnits"] = new AreaUnits();
        }

        public class AreaUnits
        {
            public Dictionary<Guid, Army.UnitGroup> allocated { get; set; }
            public Dictionary<string, Army.UnitGroup> unallocated { get; set; }

            public AreaUnits()
            {
                allocated = new Dictionary<Guid, Army.UnitGroup>();
                unallocated = new Dictionary<string, Army.UnitGroup>();
            }
        }

        public class Resources
        {
            public Dictionary<string, int> amount { get; set; }
            public Resources()
            {
                amount = new Dictionary<string, int>();
            }
        }

        public class Buildings
        {
            public Dictionary<string, Building> buildings { get; set; }
            public Buildings()
            {
                buildings = new Dictionary<string, Building>();
            }
        }

        public class Building : Entity
        {
        }
    }
}

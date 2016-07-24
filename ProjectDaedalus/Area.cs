using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectDaedalus
{
    public class Area : Entity
    {
        public Resources resources { get; }
        public Buildings buildings { get; }
        public AreaUnits units { get; }

        public Area() : base()
        {
            this.resources = new Resources();
            this.buildings = new Buildings();
            this.units = new AreaUnits();
        }
 
        public Area(params Tuple<string, string>[] properties) : base(properties)
        {
            this.resources = new Resources();
            this.buildings = new Buildings();
            this.units = new AreaUnits();
    }

        public Area(Resources res, Buildings buildings, params Tuple<string, string>[] properties) : base(properties)
        {
            this.resources = new Resources();
            this.buildings = new Buildings();
            this.units = new AreaUnits();
        }

        public class AreaUnits
        {
            public Dictionary<Guid, Army.UnitGroup> allocated { get; set; }
            public Dictionary<Units.UnitType, Army.UnitGroup> unallocated { get; set; }

            public AreaUnits()
            {
                allocated = new Dictionary<Guid, Army.UnitGroup>();
                unallocated = new Dictionary<Units.UnitType, Army.UnitGroup>();
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

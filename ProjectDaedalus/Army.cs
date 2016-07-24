using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectDaedalus
{
    public class Army : Entity
    {
        public Dictionary<Units.UnitType, UnitGroup> units { get; set; }

        public Army(double x, double y) : base()
        {
            units = new Dictionary<Units.UnitType, UnitGroup>();
            this["x"] = x.ToString();
            this["y"] = y.ToString();
        }

        public class UnitGroup : Entity
        {
            public int type { get; set; }
            public int number { get; set; }
            public int tier { get; set; }

            public UnitGroup(Units.UnitType type, int number, int tier) : base()
            {
                this.type = (int)type;
                this.number = number;
                this.tier = tier;
            }
        }
    }
}

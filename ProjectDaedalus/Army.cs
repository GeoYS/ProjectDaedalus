using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectDaedalus
{
    public class Army : Entity
    {
        public Dictionary<string, UnitGroup> units { get; set; }

        public Army(double x, double y) : base()
        {
            units = new Dictionary<string, UnitGroup>();
            this["x"] = x;
            this["y"] = y;
        }

        public class UnitGroup : Entity
        {
            public string type { get; set; }
            public int number { get; set; }
            public int tier { get; set; }

            public UnitGroup(string type, int number, int tier) : base()
            {
                this.type = type;
                this.number = number;
                this.tier = tier;
            }
        }
    }
}

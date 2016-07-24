using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectDaedalus
{
    public class Units
    {
        public enum UnitType
        {
            PIKER, NOCKER, RIDER
        }
        
        public static Army.UnitGroup NewUnitGroup(UnitType unitType, int number)
        {
            switch(unitType)
            {
                case UnitType.PIKER:
                    {
                        return new Piker(number);
                    }
                case UnitType.NOCKER:
                    {
                        return new Nocker(number);
                    }
                case UnitType.RIDER:
                    {
                        return new Rider(number);
                    }
                default:
                    return null;
            }
        }

        public class Piker : Army.UnitGroup
        {
            public Piker(int number) : base(UnitType.PIKER, number, 1)
            {
            }
        }

        public class Nocker : Army.UnitGroup
        {
            public Nocker(int number) : base(UnitType.NOCKER, number, 1)
            {
            }
        }

        public class Rider : Army.UnitGroup
        {
            public Rider(int number) : base(UnitType.RIDER, number, 1)
            {
            }
        }
    }
}

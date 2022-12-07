using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lb3
{
    public class Workshop : ICloneable
    {
        public readonly Guid ID;
        public int CountOfWorkPlaces { get; set; }
        public const int MaxCapacity = 33;
        public const int MaxCapacityOfMasters = 3;
        public const int MaxCapacityOfEmployees = 30;
        public int CountOfMasters { get; set; }
        public int CountOfEmployees { get; set; }
        public const int MaxDetailsFromMaster = 300;
        public const int MaxDetailsFromEmployee = 200;
        public const int CostPriceOnDetail = 25;
        public readonly int PriceOnDetail = 45;
        public Workshop() //конструктор
        {
            ID = Guid.NewGuid();
            CountOfWorkPlaces = MaxCapacity - (CountOfMasters + CountOfEmployees);
        }
        public Workshop(string id)
        {
            ID = Guid.Parse(id);
        }
        public object Clone()
        {
            Workshop newWorkshop = (Workshop)this.MemberwiseClone();
            return newWorkshop;
        }
    }
}

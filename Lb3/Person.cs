using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lb3
{
    public abstract class Person : ICloneable
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string IndividualTaxNumber { get; set; }
        public string ID_Workshop { get; set; }
        public Person() { }
        public Person(string fstName, string lstName) //конструктор
        {
            FirstName = fstName;
            LastName = lstName;
            DateTime centuryBegin = new DateTime(2021, 4, 29); //событие от которого рассчитывается количество тактов
            DateTime currentDate = DateTime.Now;
            string str = (currentDate.Ticks - centuryBegin.Ticks).ToString();
            IndividualTaxNumber = str.Remove(10);
        }
        public abstract object Clone();
    }
}
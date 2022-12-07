using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lb3
{
    public sealed class Worker : Person
    {
        public const int Salary = 15000;
        public bool HavingHigherTechnikalEducation { get; set; }
        public Worker(string fstName, string lstName, bool HaveHigherTechnikalEducation) : base(fstName, lstName)
        {
            HavingHigherTechnikalEducation = HaveHigherTechnikalEducation;
        }
        public override object Clone()
        {
            Worker newWorker = (Worker)this.MemberwiseClone();
            return newWorker;
        }
    }
}

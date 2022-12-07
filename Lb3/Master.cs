using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lb3
{
    public sealed class Master : Person
    {
        public const int Salary = 40000;
        public ushort CountOfCertificates { get; set; }
        public Master(string fstName, string lstName, ushort NumberOfCertificates) : base(fstName, lstName)
        {
            CountOfCertificates = NumberOfCertificates;
        }
        public override object Clone()
        {
            Master newMaster = (Master)this.MemberwiseClone();
            return newMaster;
        }
    }
}

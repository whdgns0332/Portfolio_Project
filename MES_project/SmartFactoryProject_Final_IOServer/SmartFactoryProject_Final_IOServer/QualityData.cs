using OPCAutomation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartFactoryProject_Final_IOServer
{
    class QualityData
    {
        public string MachCode;
        public string QualCode;
        public string Name;
        public string TagAddr;
        public double Minimum;
        public double Maximum;
        public double Value;

        public QualityData(string machCode, string qualCode, string name, string tagaddr,
                           double minimum, double maximum)
        {
            this.MachCode = machCode;
            this.QualCode = qualCode;
            this.Name = name;
            this.TagAddr = tagaddr;
            this.Minimum = minimum;
            this.Maximum = maximum;
            this.Value = 0;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xbim.Ifc;

namespace IFCMapper.TeklaModelObjects
{
    class ModelOptions
    {
        public Model_info.Environment Environment { get; set; }
        public IfcStore Model { get; set; }

        public ModelOptions(IfcStore model, Model_info.Environment environment )
        {
            Model = model;
            this.Environment = environment;
        }
    }
}

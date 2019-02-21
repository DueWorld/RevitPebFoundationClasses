using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xbim.Ifc;
using Xbim.Ifc2x3.Kernel;

namespace IFCMapper
{
    interface ISchemaEntity
    {
        IfcProduct IfcProduct { get; }
        IfcStore Model { get; set; }
    }
}

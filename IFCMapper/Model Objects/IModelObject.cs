using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xbim.Ifc;
using Xbim.Ifc2x3.Kernel;

namespace IFCMapper.Model_Objects
{
    interface IModelObject
    {
        string Name { get; }
        string Description { get; }
        string ObjectType { get; }
        IfcProduct IfcProduct { get; }
        IfcStore Model { get; set; }
    }
}

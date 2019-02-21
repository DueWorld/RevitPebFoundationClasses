using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xbim.Ifc2x3.ProfileResource;

namespace IFCMapper.Geomterical_Entities.ExtrudedCrossSections
{
    interface IProfile
    {
        IfcProfileTypeEnum ProfileType { get; }
        IfcProfileDef ProfileDef { get; }
        string ProfileName { get; }
    }
}

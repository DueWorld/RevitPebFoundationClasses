using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xbim.Ifc;
using Xbim.Ifc2x3.ActorResource;

namespace IFCMapper.Model_info
{
    class Organization
    {
        private IfcOrganization ifcOrganization;
        private string name;

        public string Name => name;
        public IfcOrganization IfcOrganization => ifcOrganization;

        public Organization(IfcStore model, string name)
        {
            ifcOrganization = model.Instances.New<IfcOrganization>(p =>
            {
                p.Name = name;
            });
            this.name = name;
        }
    }
}

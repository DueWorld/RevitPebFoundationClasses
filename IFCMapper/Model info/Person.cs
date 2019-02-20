using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xbim.Common;
using Xbim.Ifc;
using Xbim.Ifc2x3.ActorResource;

namespace IFCMapper.Model_info
{
    class Person
    {
        private IfcPerson ifcPerson;
        private string name;
        private string familyName;

        public IfcPerson IfcPerson => ifcPerson;  
        public string Name => name;
        public string FamilyName => familyName;

        public Person(IfcStore model, string name, string familyName)
        {
            ifcPerson = model.Instances.New<IfcPerson>(p =>
            {
                p.Id = name;
                p.FamilyName = familyName;
            });
            this.name = name;
            this.familyName = familyName;
        }

    }
}

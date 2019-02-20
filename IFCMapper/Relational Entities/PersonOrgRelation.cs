using IFCMapper.Model_info;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xbim.Ifc;
using Xbim.Ifc2x3.ActorResource;
using Xbim.Ifc2x3.UtilityResource;

namespace IFCMapper.Relational_Entities
{
    class PersonOrgRelation
    {
        private Person person;
        private Organization organization;
        private IfcPersonAndOrganization ifcPersonAndOrganization;

        public Person Person => person;
        public Organization Organization => organization;
        public IfcPersonAndOrganization PersonAndOrganization => ifcPersonAndOrganization;

        public PersonOrgRelation(IfcStore model, Organization organization, Person person)
        {
            ifcPersonAndOrganization = model.Instances.New<IfcPersonAndOrganization>(p =>
            {
                p.ThePerson = person.IfcPerson;
                p.TheOrganization = organization.IfcOrganization;

            });
            this.person = person;
            this.organization = organization;
        }
    }
}

using IFCMapper.Model_info;
using IFCMapper.Relational_Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xbim.Ifc;
using Xbim.Ifc2x3.ActorResource;
using Xbim.Ifc2x3.MeasureResource;
using Xbim.Ifc2x3.UtilityResource;

namespace IFCMapper
{
    class OwnerHistory
    {
        private IfcOwnerHistory ifcOwnerHistory;
        private PersonOrgRelation personOrgRelation;
        private Application application;
        private IfcChangeActionEnum changeAction;
        private IfcTimeStamp creationDate;


        public IfcOwnerHistory IfcOwnerHistory => ifcOwnerHistory;
        public PersonOrgRelation PersonOrgRelation => personOrgRelation;
        public Application Application => application;
        public IfcChangeActionEnum IfcChangeAction => changeAction;
        public IfcTimeStamp CreationDate => creationDate;


        public OwnerHistory(IfcStore model, PersonOrgRelation personOrgRelation, Application application, IfcChangeActionEnum changeAction, int creationDate)
        {
            ifcOwnerHistory = model.Instances.New<IfcOwnerHistory>(p =>
            {
                p.OwningUser = personOrgRelation.PersonAndOrganization;
                p.OwningApplication = application.IfcApplication;
                p.ChangeAction = changeAction;
                p.CreationDate = creationDate;
            });

            this.personOrgRelation = personOrgRelation;
            this.application = application;
            this.changeAction = changeAction;
            this.creationDate = creationDate;
        }



    }
}

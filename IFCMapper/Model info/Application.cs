using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xbim.Ifc;
using Xbim.Ifc2x3.UtilityResource;

namespace IFCMapper.Model_info
{
    class Application
    {
        IfcApplication ifcApplication;
        private Organization applicationDeveloper;
        private string version;
        private string applicationFullName;
        private string applicationIdentifier;


        public IfcApplication IfcApplication => ifcApplication;
        public Organization ApplicationDeveloper => applicationDeveloper;
        public string Version => version;
        public string ApplicationFullName => applicationFullName;
        public string ApplicationIdentifier => applicationIdentifier;


        public Application(IfcStore model, Organization developer,string version,string appName,string appIdentifier)
        {
            ifcApplication = model.Instances.New<IfcApplication>(p =>
            {
                p.ApplicationDeveloper = developer.IfcOrganization;
                p.Version = version;
                p.ApplicationFullName = appName;
                p.ApplicationIdentifier = appIdentifier;

            });
            applicationDeveloper = developer;
            this.version = version;
            applicationFullName = appName;
            applicationIdentifier = appIdentifier;
        }
    }
}

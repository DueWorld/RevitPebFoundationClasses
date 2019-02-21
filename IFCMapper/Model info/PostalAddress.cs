using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xbim.Ifc;
using Xbim.Ifc2x3.ActorResource;
using Xbim.Ifc2x3.MeasureResource;

namespace IFCMapper.Model_info
{
    class PostalAddress
    {
        private IfcPostalAddress ifcPostalAddress;
        private string adress;

        public string Adress => adress;
        public IfcPostalAddress IfcPostalAdress => ifcPostalAddress;

        public PostalAddress(IfcStore model, string adress)
        {
            ifcPostalAddress = model.Instances.New<IfcPostalAddress>(p =>
            {
                p.AddressLines.Add(adress);
            });
            this.adress = adress;
        }
    }
}

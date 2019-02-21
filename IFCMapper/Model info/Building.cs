using IFCMapper.Geomterical_Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xbim.Ifc;
using Xbim.Ifc2x3.ProductExtension;

namespace IFCMapper.Model_info
{
    class Building
    {
        private IfcBuilding ifcBuilding;
        private OwnerHistory ownerHistory;
        private string name;
        private LocalPlacement localPlacement;
        private IfcElementCompositionEnum compositionType;
        private PostalAddress postalAdress;


        public IfcBuilding IfcBuilding => ifcBuilding;
        public OwnerHistory OwnerHistory => ownerHistory;
        public String Name => name;
        public LocalPlacement LocalPlacement => localPlacement;
        public IfcElementCompositionEnum CompositionType => compositionType;
        public PostalAddress PostalAddress => postalAdress;


        

        public Building(IfcStore model, OwnerHistory ownerHistory, string name, LocalPlacement localPlacement, IfcElementCompositionEnum compositionType, PostalAddress postalAdress)
        {
            ifcBuilding = model.Instances.New<IfcBuilding>(p =>
            {
                p.OwnerHistory = ownerHistory.IfcOwnerHistory;
                p.Name = name;
                p.ObjectPlacement = localPlacement.IfcLocalPlacement;
                p.CompositionType = compositionType;
                p.BuildingAddress = postalAdress.IfcPostalAdress;
            });

            this.ownerHistory = ownerHistory;
            this.name = name;
            this.localPlacement = localPlacement;
            this.compositionType = compositionType;
            this.postalAdress = postalAdress;
        }
    }
}

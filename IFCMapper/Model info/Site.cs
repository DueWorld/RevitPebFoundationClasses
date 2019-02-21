using IFCMapper.Geomterical_Entities;
using IFCMapper.Geomterical_Entities.ExtrudedCrossSections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xbim.Ifc;
using Xbim.Ifc2x3.ProductExtension;

namespace IFCMapper.Model_info
{
    class Site
    {
        private IfcSite ifcSite;
        private OwnerHistory ownerHistory;
        private string name;
        private LocalPlacement localPlacement;
        private IfcElementCompositionEnum compositionType;
        private double relativeElevation;


        public IfcSite IfcSite => ifcSite;
        public OwnerHistory OwnerHistory => ownerHistory;
        public String Name => name;
        public LocalPlacement LocalPlacement => localPlacement;
        public IfcElementCompositionEnum CompositionType =>compositionType;
        public double RelativeElevation=>relativeElevation;




        public Site(IfcStore model, OwnerHistory ownerHistory, string name, LocalPlacement localPlacement, IfcElementCompositionEnum compositionType,double relativeElevation)
        {
            ifcSite = model.Instances.New<IfcSite>(p =>
            {
                p.OwnerHistory = ownerHistory.IfcOwnerHistory;
                p.Name = name;
                p.ObjectPlacement = localPlacement.IfcLocalPlacement;
                p.CompositionType = compositionType;
                p.RefElevation = relativeElevation;
            });

            this.ownerHistory = ownerHistory;
            this.name = name;
            this.localPlacement = localPlacement;
            this.compositionType = compositionType;
            this.relativeElevation = relativeElevation;
        }
    }
}

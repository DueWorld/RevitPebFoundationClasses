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
    class BuildingStorey
    {
        private IfcBuildingStorey ifcBuildingStorey;
        private OwnerHistory ownerHistory;
        private string name;
        private LocalPlacement localPlacement;
        private IfcElementCompositionEnum compositionType;
        private double elevation;


        public IfcBuildingStorey IfcBuildingStorey => ifcBuildingStorey;
        public OwnerHistory OwnerHistory => ownerHistory;
        public String Name => name;
        public LocalPlacement LocalPlacement => localPlacement;
        public IfcElementCompositionEnum CompositionType => compositionType;
        public double Elevation => elevation;




        public BuildingStorey(IfcStore model, OwnerHistory ownerHistory, string name, LocalPlacement localPlacement, IfcElementCompositionEnum compositionType, double elevation)
        {
            ifcBuildingStorey = model.Instances.New<IfcBuildingStorey>(p =>
            {
                p.OwnerHistory = ownerHistory.IfcOwnerHistory;
                p.Name = name;
                p.ObjectPlacement = localPlacement.IfcLocalPlacement;
                p.CompositionType = compositionType;
                p.Elevation = elevation;
            });

            this.ownerHistory = ownerHistory;
            this.name = name;
            this.localPlacement = localPlacement;
            this.compositionType = compositionType;
            this.elevation = elevation;
        }
    }
}

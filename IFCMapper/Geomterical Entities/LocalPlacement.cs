using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xbim.Ifc;
using Xbim.Ifc2x3.Interfaces;
using Xbim.Ifc2x3.MeasureResource;
using Xbim.Ifc2x3.PropertyResource;
using Xbim.Common.Step21;
using Xbim.Ifc2x3.GeometryResource;
using Xbim.Ifc2x3.GeometricConstraintResource;

namespace IFCMapper.Geomterical_Entities
{
    class LocalPlacement
    {
        public IfcStore Model { get; set; }


        private LocalPlacement relativeToPlacement;


        private PlacementAxis3D relativePlacement;


        public IfcLocalPlacement IfcLocalPlacement => ifcLocalPlacement;
        private IfcLocalPlacement ifcLocalPlacement;



        public LocalPlacement(IfcStore model, LocalPlacement relativeToPlacement, PlacementAxis3D relativePlacement)
        {
            this.relativeToPlacement = relativeToPlacement;
            this.relativePlacement = relativePlacement;

            ifcLocalPlacement = model.Instances.New<IfcLocalPlacement>(p =>
             {
                 if (ifcLocalPlacement != null)
                 {
                     p.PlacementRelTo = relativeToPlacement.ifcLocalPlacement;
                 }

                 p.RelativePlacement = relativePlacement.IfcAxis2Placement3D;
             }
             );


        }




    }
}

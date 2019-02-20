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

        private LocalPlacement relativeToPlacement;
        private PlacementAxis3D relativePlacement;


        public IfcStore Model { get; set; }


        public IfcLocalPlacement IfcLocalPlacement => ifcLocalPlacement;


        private IfcLocalPlacement ifcLocalPlacement;



        public LocalPlacement(IfcStore model, LocalPlacement relativeToPlacement, PlacementAxis3D relativePlacement)
        {
            this.relativeToPlacement = relativeToPlacement;
            this.relativePlacement = relativePlacement;


            IfcLocalPlacement result = model.Instances.OfType<IfcLocalPlacement>()
                .Where(p =>
                {
                    if (relativeToPlacement != null && p.PlacementRelTo != null)
                    {
                        return p.PlacementRelTo.Equals(relativeToPlacement.ifcLocalPlacement) && p.RelativePlacement.Equals(relativePlacement.IfcAxis2Placement3D);
                    }
                    else if (relativeToPlacement == null && p.PlacementRelTo != null)
                    {
                        return false;
                    }
                    else if (relativeToPlacement != null && p.PlacementRelTo == null)
                    {
                        return false;
                    }
                    else
                    {
                        return p.RelativePlacement.Equals(relativePlacement.IfcAxis2Placement3D);
                    }
                })
                .FirstOrDefault();


            if (result == null)
                ifcLocalPlacement = model.Instances.New<IfcLocalPlacement>(p =>
                 {
                     if (relativeToPlacement != null)
                     {
                         p.PlacementRelTo = relativeToPlacement.ifcLocalPlacement;
                     }

                     p.RelativePlacement = relativePlacement.IfcAxis2Placement3D;
                 }
                 );
            else
                ifcLocalPlacement = result;


        }




    }
}

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

namespace IFCMapper.Geomterical_Entities
{
    class PlacementAxis2D
    {
        private CartesianPoint2D location;
        private DirectionVector2D refAxis;
        private IfcAxis2Placement2D ifcAxis2Placement2D;


        public IfcStore Model { get; set; }
        public IfcAxis2Placement2D IfcAxis2Placement2D => ifcAxis2Placement2D;


        public PlacementAxis2D(IfcStore model, CartesianPoint2D location, DirectionVector2D refAxis)
        {
            this.location = location;
            this.refAxis = refAxis;

            IfcAxis2Placement2D result = model.Instances.OfType<IfcAxis2Placement2D>().Where(p => p.Location == location.IfcPoint && p.RefDirection.Equals(refAxis.IfcDirection)).FirstOrDefault();

            if (result == null)

                ifcAxis2Placement2D = model.Instances.New<IfcAxis2Placement2D>(p =>
                {
                    p.Location = location.IfcPoint;
                    p.RefDirection = refAxis.IfcDirection;
                });

            else
                ifcAxis2Placement2D = result;
        }


    }
}

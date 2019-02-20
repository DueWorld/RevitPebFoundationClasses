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
    class PlacementAxis3D
    {
        private CartesianPoint3D location;
        private DirectionVector3D axis;
        private DirectionVector3D refAxis;
        private IfcAxis2Placement3D ifcAxis2Placement3D;


        public IfcStore Model { get; set; }

        public IfcAxis2Placement3D IfcAxis2Placement3D => ifcAxis2Placement3D;


        public PlacementAxis3D(IfcStore model, CartesianPoint3D location, DirectionVector3D axis, DirectionVector3D refAxis)
        {
            this.location = location;
            this.axis = axis;
            this.refAxis = refAxis;

            IfcAxis2Placement3D result = model.Instances.OfType<IfcAxis2Placement3D>().Where(p => p.Location == location.IfcPoint && p.Axis.Equals(axis.IfcDirection) && p.RefDirection.Equals(refAxis.IfcDirection)).FirstOrDefault();

            if (result == null)
                ifcAxis2Placement3D = model.Instances.New<IfcAxis2Placement3D>(p =>
                {
                    p.Location = location.IfcPoint;
                    p.Axis = axis.IfcDirection;
                    p.RefDirection = refAxis.IfcDirection;
                });
            else
                ifcAxis2Placement3D = result;




        }
    }
}

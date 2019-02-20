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
    class CartesianPoint3D
    {
        private double x;
        private double y;
        private double z;
        private IfcCartesianPoint ifcPoint;

        public double X => x;
        public double Y => y;
        public double Z => z;

        public IfcStore Model { get; set; }
        public IfcCartesianPoint IfcPoint => ifcPoint;

        public CartesianPoint3D(IfcStore model, double x, double y, double z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
            this.Model = model;
            IfcCartesianPoint result = model.Instances.OfType<IfcCartesianPoint>().Where(p => p.X == x && p.Y == y && p.Z == z).FirstOrDefault();

            if (result == null)
                ifcPoint = model.Instances.New<IfcCartesianPoint>(p =>
                {
                    p.SetXYZ(x, y,z);
                }
                 );
            else
                ifcPoint = result;

            
        }
    }
}

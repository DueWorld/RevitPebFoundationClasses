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
    class DirectionVector3D
    {
        private double x;
        private double y;
        private double z;
        private IfcDirection ifcDirection;

        public double X => x;
        public double Y => y;
        public double Z => z;

        public IfcStore Model { get; set; }
        public IfcDirection IfcDirection => ifcDirection;

        public DirectionVector3D(IfcStore model, double x, double y, double z)
        {
            this.Model = model;
            this.x = x;
            this.y = y;
            this.z = z;
            IfcDirection result = model.Instances.OfType<IfcDirection>().Where(d => d.X == x && d.Y == y && d.Z == z).FirstOrDefault();

            if (result == null)
                ifcDirection = model.Instances.New<IfcDirection>(d =>
                {
                    d.SetXYZ(x, y,z);
                }
                 );
            else
                ifcDirection = result;
        }



        public static DirectionVector3D Origin(IfcStore model)
        {
            return new DirectionVector3D(model, 0, 0, 0);
        }

        public static DirectionVector3D UnitX(IfcStore model)
        {
            return new DirectionVector3D(model, 1, 0, 0);

        }

        public static DirectionVector3D UnitY(IfcStore model)
        {
            return new DirectionVector3D(model, 0, 1, 0);

        }

        public static DirectionVector3D UnitZ(IfcStore model)
        {
            return new DirectionVector3D(model, 0, 0, 1);

        }



    }


}


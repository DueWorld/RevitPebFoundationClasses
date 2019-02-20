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
    class DirectionVector
    {
        private double x;
        private double y;
        private double z;
        private IfcDirection direction;

        public double X => x;
        public double Y => y;
        public double Z => z;

        public IfcStore Model { get; set; }
        public IfcDirection IfcDirection => direction;

        public DirectionVector(IfcStore model, double x, double y, double z)
        {
            this.Model = model;
            this.x = x;
            this.y = y;
            this.z = z;
            direction = model.Instances.New<IfcDirection>(p =>
              {
                  p.SetXYZ(x, y, z);
              }
             );
        }
    }


}


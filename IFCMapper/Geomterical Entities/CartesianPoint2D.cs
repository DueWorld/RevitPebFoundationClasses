
using System.Linq;
using Xbim.Ifc;
using Xbim.Ifc2x3.GeometryResource;

namespace IFCMapper.Geomterical_Entities
{
    class CartesianPoint2D
    {
        private double x;
        private double y;
        private IfcCartesianPoint ifcPoint;

        public double X => x;
        public double Y => y;

        public IfcStore Model { get; set; }

        public IfcCartesianPoint IfcPoint => ifcPoint;

        public CartesianPoint2D(IfcStore model, double x, double y)
        {
            this.x = x;
            this.y = y;
            this.Model = model;
            IfcCartesianPoint result = model.Instances.OfType<IfcCartesianPoint>().Where(p => p.X == x && p.Y == y && p.Z == 0 && p.Dim.Value.Equals(2)).FirstOrDefault();

            if (result == null)
                ifcPoint = model.Instances.New<IfcCartesianPoint>(p =>
                  {
                      p.SetXY(x, y);
                     var test= p.Dim.Value;
                  }
                 );
            else
                ifcPoint = result;
        }


        public static CartesianPoint2D Origin(IfcStore model)
        {
            return new CartesianPoint2D(model, 0, 0);
        }

        public static CartesianPoint2D UnitX(IfcStore model)
        {
            return new CartesianPoint2D(model, 1, 0);

        }

        public static CartesianPoint2D UnitY(IfcStore model)
        {
            return new CartesianPoint2D(model, 0, 1);

        }

    }
}

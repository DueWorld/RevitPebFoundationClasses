using IFCMapper.Geomterical_Entities;
using IFCMapper.RevitRetreiver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xbim.Ifc;

namespace IFCMapper.TeklaModelObjects
{
    class TeklaPlatePlacementInitializer
    {
        private CartesianPoint3D origin;
        private DirectionVector3D main;
        private DirectionVector3D reff;
        private LocalPlacement localPlacement;

        public CartesianPoint3D Origin => origin;
        public DirectionVector3D Main => main;
        public DirectionVector3D Reff => reff;
        public LocalPlacement LocalPlacement => localPlacement;

        public TeklaPlatePlacementInitializer(IfcStore model, Model_info.Environment env, Point origin, Vector main, Vector reff)
        {
            this.origin = new CartesianPoint3D(model, origin.X, origin.Y, origin.Z);
            this.main = new DirectionVector3D(model, main.X, main.Y, main.Z);
            this.reff = new DirectionVector3D(model, reff.X, reff.Y, reff.Z);

            PlacementAxis3D axis = new PlacementAxis3D(model, this.origin, this.main, this.reff);
            localPlacement = new LocalPlacement(model, env.Stories.FirstOrDefault().LocalPlacement, axis);
        }

    }
}

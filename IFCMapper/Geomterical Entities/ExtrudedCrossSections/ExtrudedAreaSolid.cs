using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xbim.Ifc;
using Xbim.Ifc2x3.GeometricModelResource;
using Xbim.Ifc2x3.GeometryResource;
using Xbim.Ifc2x3.MeasureResource;
using Xbim.Ifc2x3.RepresentationResource;

namespace IFCMapper.Geomterical_Entities.ExtrudedCrossSections
{
    class ExtrudedAreaSolid
    {
        private double depth;
        private IProfile profile;
        private DirectionVector3D extrudedDirection;
        private PlacementAxis3D position;
        private IfcExtrudedAreaSolid ifcExtrusionSolid;

        public double Depth => depth;
        public IProfile Profile => profile;
        public DirectionVector3D ExtrudedDirection => extrudedDirection;
        public PlacementAxis3D Position => position;
        public IfcExtrudedAreaSolid IfcExtrusionSolid => ifcExtrusionSolid;
        IfcStore Model { get; set; }

        public ExtrudedAreaSolid(IfcStore model, double depth, IProfile profile, DirectionVector3D direction, PlacementAxis3D placement)
        {
            this.Model = model;
            this.depth = depth;
            this.profile = profile;
            this.extrudedDirection = direction;
            this.position = placement;

            ifcExtrusionSolid = model.Instances.New<IfcExtrudedAreaSolid>(e =>
             {
                 e.Depth = depth;
                 e.ExtrudedDirection = extrudedDirection.IfcDirection;
                 e.Position = position.IfcAxis2Placement3D;
                 e.SweptArea = profile.ProfileDef;
             });
        }
    }
}

using IFCMapper.Geomterical_Entities;
using IFCMapper.Geomterical_Entities.ExtrudedCrossSections;
using IFCMapper.TeklaModelObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xbim.Ifc;

namespace IFCMapper.TeklaModelObjects.ExtrudedCrossSections
{
    class RectangularCrossSection
    {
        public ProductionDefinitionShape ProductShape { get; private set; }
        public RectangularCrossSection(ModelOptions modelOption, double x, double y)
        {
            //Position of the 2D rectangular section (Will remain fixed).
            CartesianPoint2D p = CartesianPoint2D.Origin(modelOption.Model);
            DirectionVector2D v = DirectionVector2D.UnitX(modelOption.Model);
            PlacementAxis2D postion = new PlacementAxis2D(modelOption.Model, p, v);
            RectangleProfile rectProfile = new RectangleProfile(modelOption.Model, x, y, Xbim.Ifc2x3.ProfileResource.IfcProfileTypeEnum.AREA, postion);


            //Local placement of the 3D definition shape of the column(Will remain fixed).
            CartesianPoint3D Expoint = CartesianPoint3D.Origin(modelOption.Model);
            DirectionVector3D exMain = DirectionVector3D.UnitZ(modelOption.Model);
            DirectionVector3D exReff = DirectionVector3D.UnitX(modelOption.Model);
            PlacementAxis3D Exaxis = new PlacementAxis3D(modelOption.Model, Expoint, exMain, exReff);
            DirectionVector3D extVec = DirectionVector3D.UnitZ(modelOption.Model);
            ExtrudedAreaSolid solid = new ExtrudedAreaSolid(modelOption.Model, 3000, rectProfile, extVec, Exaxis);
            ShapeRepresentation representation = new ShapeRepresentation(modelOption.Model, modelOption.Environment.SubContext, new List<ExtrudedAreaSolid>() { solid });
            ProductShape = new ProductionDefinitionShape(modelOption.Model, new List<ShapeRepresentation>() { representation });




            //Local placement of the object placement of the column (This will be manipulated according to Autodesk Revit readings).
            CartesianPoint3D point = CartesianPoint3D.Origin(modelOption.Model);
            DirectionVector3D main = DirectionVector3D.UnitZ(modelOption.Model);
            DirectionVector3D reff = DirectionVector3D.UnitX(modelOption.Model);
            PlacementAxis3D axis = new PlacementAxis3D(modelOption.Model, point, main, reff);
            LocalPlacement placement = new LocalPlacement(modelOption.Model, modelOption.Environment.Stories.FirstOrDefault().LocalPlacement, axis);

        }



    }
}

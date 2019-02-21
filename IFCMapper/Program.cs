using System.Linq;
using Xbim.Ifc;
using Xbim.Ifc2x3.Interfaces;
using Xbim.Ifc2x3.MeasureResource;
using Xbim.Ifc2x3.PropertyResource;
using Xbim.Common.Step21;
using IFCMapper.Geomterical_Entities;
using IFCMapper.Model_info;
using IFCMapper.Relational_Entities;
using IFCMapper.Geomterical_Entities.ExtrudedCrossSections;
using System.Collections.Generic;
using Xbim.Ifc2x3.ProductExtension;
using IFCMapper.Model_Objects;
using Xbim.Ifc2x3.Kernel;

namespace IFCMapper
{
    class Program
    {
        static void Main(string[] args)
        {
            //CHANGE PATH HERE.
            string path = @"C:\Users\Scorias\Desktop\IFC trails";

            var editor = new XbimEditorCredentials
            {
                ApplicationDevelopersName = "xBIM Team",
                ApplicationFullName = "xBIM Toolkit",
                ApplicationIdentifier = "xBIM",
                ApplicationVersion = "4.0",
                EditorsFamilyName = "sane",
                EditorsGivenName = "sane",
                EditorsOrganisationName = "Independent"
            };

            using (IfcStore model = IfcStore.Create(editor, IfcSchemaVersion.Ifc2X3, XbimStoreType.InMemoryModel))
            {
                using (var txn = model.BeginTransaction("Initialise Model"))
                {
                    Environment env = Environment.Create(model);

                    LocalPlacement storeyPlacement = new LocalPlacement(model, env.Building.LocalPlacement, env.ProjectAxis);
                    BuildingStorey story = new BuildingStorey(model, env.OwnerHistory, "Story", storeyPlacement, IfcElementCompositionEnum.ELEMENT, 0);


                    env.AddStorey(model, story);



                    CartesianPoint3D Expoint = new CartesianPoint3D(model, 0, 0, 0);
                    DirectionVector3D Exmain = new DirectionVector3D(model, 0, 0, 1);
                    DirectionVector3D Exreff = new DirectionVector3D(model, 1, 0, 0);
                    PlacementAxis3D Exaxis = new PlacementAxis3D(model, Expoint, Exmain, Exreff);
                    CartesianPoint2D p = new CartesianPoint2D(model, 0, 0);
                    DirectionVector2D v = new DirectionVector2D(model, 1, 0);
                    PlacementAxis2D postion = new PlacementAxis2D(model, p, v);

                    DirectionVector3D extVec = new DirectionVector3D(model, 0, 0, 1);

                    RectangleProfile rectProfile = new RectangleProfile(model, 5, 500, "Sec1", Xbim.Ifc2x3.ProfileResource.IfcProfileTypeEnum.AREA, postion);
                    ExtrudedAreaSolid solid = new ExtrudedAreaSolid(model, 3000, rectProfile, extVec, Exaxis);


                    ShapeRepresentation representation = new ShapeRepresentation(model, env.SubContext, new List<ExtrudedAreaSolid>() { solid });
                    ProductionDefinitionShape productShape = new ProductionDefinitionShape(model, new List<ShapeRepresentation>() { representation });


                    CartesianPoint3D point = new CartesianPoint3D(model, 0, 0, 0);
                    DirectionVector3D main = new DirectionVector3D(model, 0, 0, 1);
                    DirectionVector3D reff = new DirectionVector3D(model, 1, 0, 0);
                    PlacementAxis3D axis = new PlacementAxis3D(model, point, main, reff);

                    LocalPlacement placement = new LocalPlacement(model, env.Stories.FirstOrDefault().LocalPlacement, axis);


                    PrimitiveColumn column = new PrimitiveColumn(model, "pl500x5", "pl500x5", "column", "plswurk", placement, productShape);










                    txn.Commit();
                }






                model.SaveAs($"{path}\\REEEEEEE.ifcxml");
            }

        }
    }
}

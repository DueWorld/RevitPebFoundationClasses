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
using IFCMapper.Material_Resources;
using IFCMapper.TeklaModelObjects;

namespace IFCMapper
{
    class Program
    {
        static void Main(string[] args)
        {
            //CHANGE PATH HERE.
            string path = @"C:\Users\world\Downloads";

            var editor = new XbimEditorCredentials
            {
                ApplicationDevelopersName = "xBIM Team",
                ApplicationFullName = "xBIM Toolkit",
                ApplicationIdentifier = "xBIM",
                ApplicationVersion = "4.0",
                EditorsFamilyName = "MoSalah",
                EditorsGivenName = "sane",
                EditorsOrganisationName = "Independent"
            };

            using (IfcStore model = IfcStore.Create(editor, IfcSchemaVersion.Ifc2X3, XbimStoreType.InMemoryModel))
            {
                using (var txn = model.BeginTransaction("Initialise Model"))
                {
                    Environment env = Environment.Create(model);
                    ModelOptions option = new ModelOptions(model, env);



                    //Local placement of the main storey.
                    LocalPlacement storeyPlacement = new LocalPlacement(model, env.Building.LocalPlacement, env.ProjectAxis);
                    BuildingStorey storey = new BuildingStorey(model, "Story", storeyPlacement, IfcElementCompositionEnum.ELEMENT, 0);
                    env.AddStorey(model, storey);


                    //Local placement of the object placement of the column (This will be manipulated according to Autodesk Revit readings).
                    CartesianPoint3D point = CartesianPoint3D.Origin(model);
                    DirectionVector3D main = DirectionVector3D.UnitZ(model);
                    DirectionVector3D reff = DirectionVector3D.UnitX(model);
                    PlacementAxis3D axis = new PlacementAxis3D(model, point, main, reff);
                    LocalPlacement placement = new LocalPlacement(model, env.Stories.FirstOrDefault().LocalPlacement, axis);


                    //Local placement of the object placement of the column (This will be manipulated according to Autodesk Revit readings).
                    CartesianPoint3D point2 = new CartesianPoint3D(model, 6000, 6000, 0);
                    DirectionVector3D main2 = DirectionVector3D.UnitZ(model);
                    DirectionVector3D reff2 = DirectionVector3D.UnitX(model);
                    PlacementAxis3D axis2 = new PlacementAxis3D(model, point2, main2, reff2);
                    LocalPlacement placement2 = new LocalPlacement(model, env.Stories.FirstOrDefault().LocalPlacement, axis2);




                    //Assigning the material and the main column.
                    Material material = new Material(model, "S235JR");

                    TeklaPlate plate = new TeklaPlate(option, placement, 5, 500);

                    TeklaPlate plate2 = new TeklaPlate(option, placement2, 5, 500);

                    storey.AddModelObject(plate, plate2);



                    plate.AssignMaterial(material);



                    plate2.AssignMaterial(material);





                    txn.Commit();
                }
                model.SaveAs($"{path}\\TESTPLSSS.ifcxml");
            }

        }
    }
}

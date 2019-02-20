using System.Linq;
using Xbim.Ifc;
using Xbim.Ifc2x3.Interfaces;
using Xbim.Ifc2x3.MeasureResource;
using Xbim.Ifc2x3.PropertyResource;
using Xbim.Common.Step21;
using IFCMapper.Geomterical_Entities;

namespace IFCMapper
{
    class Program
    {
        static void Main(string[] args)
        {
            //CHANGE PATH HERE.
            string path = @"C:\TeklaStructuresModels\IFC\IFC";

            var editor = new XbimEditorCredentials
            {
                ApplicationDevelopersName = "xBIM Team",
                ApplicationFullName = "xBIM Toolkit",
                ApplicationIdentifier = "xBIM",
                ApplicationVersion = "4.0",
                EditorsFamilyName = "sane",
                EditorsGivenName = "Johann blast",
                EditorsOrganisationName = "Independent Architecture"
            };

            using (IfcStore model = IfcStore.Create(editor, IfcSchemaVersion.Ifc2X3, XbimStoreType.InMemoryModel))
            {
                using (var txn = model.BeginTransaction("Initialise Model"))
                {
                    //ALL CODE HERE.

                    CartesianPoint3D point = new CartesianPoint3D(model, 10, 10, 10);
                    CartesianPoint3D pointx = new CartesianPoint3D(model, 10, 10, 10);
                    CartesianPoint2D point2 = new CartesianPoint2D(model, 10, 10);
                    DirectionVector vector1 = new DirectionVector(model, 5, 5, 5);
                    DirectionVector vector2 = new DirectionVector(model, 20, 20, 20);


                    PlacementAxis3D axis = new PlacementAxis3D(model, point, vector1, vector2);
                    PlacementAxis3D axis2 = new PlacementAxis3D(model, pointx, vector2, vector2);
                    LocalPlacement placement = new LocalPlacement(model, null, axis);
                    LocalPlacement placement2 = new LocalPlacement(model, placement, axis2);
                    txn.Commit();
                }






                model.SaveAs($"{path}\\outSelectedBimXML.ifcxml");
            }

        }
    }
}

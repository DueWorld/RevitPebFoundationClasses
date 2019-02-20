using System.Linq;
using Xbim.Ifc;
using Xbim.Ifc2x3.Interfaces;
using Xbim.Ifc2x3.MeasureResource;
using Xbim.Ifc2x3.PropertyResource;
using Xbim.Common.Step21;
using IFCMapper.Geomterical_Entities;
using IFCMapper.Material_Resources;
using IFCMapper.Geomterical_Entities.ExtrudedCrossSections;

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
                    CartesianPoint3D p = new CartesianPoint3D(model, 1, 2, 3);
                    DirectionVector3D axis = new DirectionVector3D(model,1, 1, 1);

                    GeometricRepresentationContext geoCont = new GeometricRepresentationContext(model,"model",3,0.0000001,new PlacementAxis3D(model,p,axis,axis));


                    txn.Commit();
                }






                model.SaveAs($"{path}\\outSelectedBimXML.ifcxml");
            }

        }
    }
}

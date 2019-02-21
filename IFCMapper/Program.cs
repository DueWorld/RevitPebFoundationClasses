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
using IFCMapper.Model_Objects;
using Xbim.Ifc2x3.ProductExtension;
using Xbim.Ifc2x3.Kernel;

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
                EditorsGivenName = "sane",
                EditorsOrganisationName = "Independent"
            };

            using (IfcStore model = IfcStore.Create(editor, IfcSchemaVersion.Ifc2X3, XbimStoreType.InMemoryModel))
            {
                using (var txn = model.BeginTransaction("Initialise Model"))
                {
                    DirectionVector3D vec3 = new DirectionVector3D(model, 1, 2, 3);
                    PlacementAxis3D place3 = new PlacementAxis3D(model, new CartesianPoint3D(model, 1, 2, 3), vec3, vec3);
                    RectangleProfile rectProfile = new RectangleProfile(model, 1, 1, "name", Xbim.Ifc2x3.ProfileResource.IfcProfileTypeEnum.AREA);
                    ExtrudedAreaSolid solid = new ExtrudedAreaSolid(model, 1000, rectProfile, vec3, place3);
                    GeometricRepresentationContext cont = new GeometricRepresentationContext(model, "contType", 3, 0.000001, place3);
                    GeometricRepresentationSubContext subCont = new GeometricRepresentationSubContext(model, "contId", "contType", Xbim.Ifc2x3.RepresentationResource.IfcGeometricProjectionEnum.MODEL_VIEW, cont);
                    ShapeRepresentation representation = new ShapeRepresentation(model, subCont, new List<ExtrudedAreaSolid>() { solid });
                    ProductionDefinitionShape productShape = new ProductionDefinitionShape(model, new List<ShapeRepresentation>() { representation });
                    LocalPlacement placement = new LocalPlacement(model, null, place3);
                    Organization org = new Organization(model, "orgname");
                    Person p = new Person(model, "personName", "familyName");
                    Application app = new Application(model,org, "version", "appName", "appId");
                    PersonOrgRelation relOrgPer = new PersonOrgRelation(model, org, p);
                    OwnerHistory owner = new OwnerHistory(model,relOrgPer,app, Xbim.Ifc2x3.UtilityResource.IfcChangeActionEnum.NOCHANGE, 5);
                    //PrimitiveColumn column = new PrimitiveColumn(model, "a7a", "a7a kbera neek", "type of a7a", "tag a7a", placement,owner, productShape);
                    
                   
                    
                    txn.Commit();
                }






                model.SaveAs($"{path}\\outSelectedBimXML.ifcxml");
            }

        }
    }
}

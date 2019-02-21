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
                EditorsGivenName = "Johann blast",
                EditorsOrganisationName = "Independent Architecture"
            };

            using (IfcStore model = IfcStore.Create(editor, IfcSchemaVersion.Ifc2X3, XbimStoreType.InMemoryModel))
            {
                using (var txn = model.BeginTransaction("Initialise Model"))
                {
                    //ALL CODE HERE.
                    Person person = new Person(model, "Scorias", "");


                    Organization org = new Organization(model, "SilkRoad");



                    PersonOrgRelation relation = new PersonOrgRelation(model, org, person);


                    Application app = new Application(model, org, "1.00", "MMORPG", "696969");


                    OwnerHistory OH = new OwnerHistory(model, relation, app, Xbim.Ifc2x3.UtilityResource.IfcChangeActionEnum.NOCHANGE, 21102004);

                    UnitAssignmentRelation units = new UnitAssignmentRelation(model);



                    CartesianPoint3D point = new CartesianPoint3D(model, 0, 0, 0);
                    DirectionVector3D main = new DirectionVector3D(model, 0, 0, 1);
                    DirectionVector3D reff = new DirectionVector3D(model, 1, 0, 0);
                    PlacementAxis3D ProjAxis = new PlacementAxis3D(model, point, main, reff); //i10


                    GeometricRepresentationContext GeoRep = new GeometricRepresentationContext(model, "Model", 3, 0.000001, ProjAxis);


                    Project proj = new Project(model, OH, "PTSA", GeoRep,units);









                    LocalPlacement sitePlacement = new LocalPlacement(model, null, ProjAxis);
                    Site site = new Site(model, OH, "Site", sitePlacement, Xbim.Ifc2x3.ProductExtension.IfcElementCompositionEnum.ELEMENT, 0);




                    PostalAddress post = new PostalAddress(model, "IN YOUR MOMMA ROOM");

                    LocalPlacement buildPlacement = new LocalPlacement(model, sitePlacement, ProjAxis);

                    Building building = new Building(model, OH, "Building", buildPlacement, Xbim.Ifc2x3.ProductExtension.IfcElementCompositionEnum.ELEMENT, post);
                    

                    LocalPlacement storeyPlacement = new LocalPlacement(model, buildPlacement, ProjAxis);
                    BuildingStorey story = new BuildingStorey(model, OH, "Story", storeyPlacement, Xbim.Ifc2x3.ProductExtension.IfcElementCompositionEnum.ELEMENT, 0);

                    proj.IfcProject.AddSite(site.IfcSite);
                    site.IfcSite.AddBuilding(building.IfcBuilding);


                    txn.Commit();
                }






                model.SaveAs($"{path}\\REEEEEEEEEEEEEEEE.ifcxml");
            }

        }
    }
}

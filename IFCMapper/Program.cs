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
using IFCMapper.RevitRetreiver;

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
                EditorsFamilyName = "MoSalah",
                EditorsGivenName = "sane",
                EditorsOrganisationName = "Independent"
            };

            RevitSeeker seeker;

            string filename = "RF";
            string filepath = @"C:\Users\Scorias\Desktop\IFC trails";
            using (var stepModel = IfcStore.Open($"{filepath}\\{filename}.ifc"))
            {
                seeker = new RevitSeeker(stepModel);
            }



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


                    //Assigning the material and the main column.
                    Material material = new Material(model, "S235JR");


                    var RevitColumns = seeker.RevitColumns;
                    List<TeklaPlate> StoryComponants = new List<TeklaPlate>();
                    TeklaPlatePlacementInitializer plateInitializer;

                    foreach (var rColumn in RevitColumns)
                    {
                        List<RevitPlate> rColumnComp = rColumn.Components;
                        foreach (var revitPlate in rColumnComp)
                        {
                            plateInitializer = new TeklaPlatePlacementInitializer(model, env, revitPlate.Origin, revitPlate.Axis, revitPlate.ReffDirection);
                            TeklaPlate plate = new TeklaPlate(option, plateInitializer.LocalPlacement, revitPlate.OverallWidth, revitPlate.OverallDepth,revitPlate.Height);
                            StoryComponants.Add(plate);
                            plate.AssignMaterial(material);
                        }
                    }
                    storey.AddModelObject(StoryComponants.ToArray());
                    txn.Commit();
                }
                model.SaveAs($"{path}\\TheRealMindWurks.ifcxml");
            }
        }
    }
}

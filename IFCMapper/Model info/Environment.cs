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
using Xbim.Common;
using Xbim.Ifc2x3.UtilityResource;
using IFCMapper.Material_Resources;

namespace IFCMapper.Model_info
{
    class Environment
    {
        private static Environment instance = null;
        private static readonly object padlock = new object();
        private Building building;
        private Project project;
        private OwnerHistory ownerHistory;
        private Site site;
        private Person person;
        private Organization organization;
        private PersonOrgRelation relation;
        private Application application;
        private PlacementAxis3D projectAxis;
        private LocalPlacement sitePlacement;
        private LocalPlacement buildingPlacement;
        private UnitAssignmentRelation units;
        private List<BuildingStorey> stories;
        private GeometricRepresentationContext context;
        private GeometricRepresentationSubContext subContext;
        private Material material;

        public Building Building => building;
        public Project Project => project;
        public OwnerHistory OwnerHistory => ownerHistory;
        public Site Site => site;
        public Person Person => person;
        public Organization Organization => organization;
        public PersonOrgRelation Relation => relation;
        public Application Application => application;
        public PlacementAxis3D ProjectAxis => projectAxis;
        public LocalPlacement SitePlacement => sitePlacement;
        public LocalPlacement BuildingPlacement => buildingPlacement;
        public UnitAssignmentRelation Units => units;
        public List<BuildingStorey> Stories => stories;
        public GeometricRepresentationContext Context => context;
        public GeometricRepresentationSubContext SubContext => subContext;
        public Material Material => Material;




        private Environment(IfcStore model)
        {
            Initialize(model);
        }

        public static Environment Create(IfcStore model)
        {
            lock (padlock)
            {
                if (instance == null)
                {
                    instance = new Environment(model);
                }
                return instance;
            }

        }

        private void Initialize(IfcStore model)
        {
            //person = new Person(model, "Company", "");
            //organization = new Organization(model, "SilkRoad");
            //relation = new PersonOrgRelation(model, organization, person);
            //application = new Application(model, organization, "1.00", "IFCRevitToTekla", "1");
            //ownerHistory = new OwnerHistory(model, relation, application, IfcChangeActionEnum.NOCHANGE, 18022019);
            units = new UnitAssignmentRelation(model);
            material = new Material(model, "S235J0");

            //creating main project axis
            CartesianPoint3D point = new CartesianPoint3D(model, 0, 0, 0);
            DirectionVector3D mainAxis = new DirectionVector3D(model, 0, 0, 1);
            DirectionVector3D refAxis = new DirectionVector3D(model, 1, 0, 0);
            projectAxis = new PlacementAxis3D(model, point, mainAxis, refAxis);
            context = new GeometricRepresentationContext(model, "Model", 3, 0.00001, projectAxis);
            subContext = new GeometricRepresentationSubContext(model, "Body", "Model", Xbim.Ifc2x3.RepresentationResource.IfcGeometricProjectionEnum.MODEL_VIEW, context);

            //creating project
            project = new Project(model, "RevitProject", context, units);

            //creating site
            sitePlacement = new LocalPlacement(model, null, projectAxis);
            site = new Site(model, ownerHistory, "Site", sitePlacement, IfcElementCompositionEnum.ELEMENT, 0);




            PostalAddress post = new PostalAddress(model, "IN YOUR MOMMA ROOM");
            buildingPlacement = new LocalPlacement(model, sitePlacement, projectAxis);
            building = new Building(model, ownerHistory, "Building", buildingPlacement, IfcElementCompositionEnum.ELEMENT, post);

            stories = new List<BuildingStorey>();

            InitializeRelations();
        }



        private void InitializeRelations()
        {
            project.IfcProject.AddSite(site.IfcSite);
            site.IfcSite.AddBuilding(building.IfcBuilding);
        }

        public void AddStorey(IfcStore model, BuildingStorey storey)
        {
            IfcRelAggregates StoryBuildingRel = model.Instances.New<IfcRelAggregates>(p =>
            {
                //p.OwnerHistory = ownerHistory.IfcOwnerHistory;
                p.RelatingObject = building.IfcBuilding;
                p.RelatedObjects.Add(storey.IfcBuildingStorey);
            });
            stories.Add(storey);
        }










    }





}

using IFCMapper.Geomterical_Entities;
using IFCMapper.Geomterical_Entities.ExtrudedCrossSections;
using IFCMapper.Material_Resources;
using System.Collections.Generic;
using Xbim.Ifc;
using Xbim.Ifc2x3.SharedBldgElements;
using Xbim.Ifc2x3.Kernel;
using IFCMapper.Relational_Entities;
using IFCMapper.Model_Objects;
using IFCMapper.TeklaModelObjects.ExtrudedCrossSections;

namespace IFCMapper.TeklaModelObjects
{
    class TeklaPlate : IModelObject
    {
        private string name;
        private string description;
        private string objectType;
        private ProductionDefinitionShape productRepresentation;
        private LocalPlacement objectPlacement;
         private IfcColumn ifcColumn;

        public string Name => name;
        public string Description => description;
        public string ObjectType => objectType;
        public ProductionDefinitionShape ProductRepresentation => productRepresentation;
        public LocalPlacement ObjectPlacement => objectPlacement;
        public IfcStore Model { get; set; }
        public IfcProduct IfcProduct => ifcColumn;

        public TeklaPlate(ModelOptions modelOption,LocalPlacement objectPlacement,double xDimension, double yDimension, string name = "COLUMN")
        {
            this.Model = modelOption.Model;
            RectangularCrossSection section = new RectangularCrossSection(modelOption, xDimension, yDimension);
            this.productRepresentation = section.ProductShape;
            this.name = name;
            this.description = productRepresentation.Representations[0].ExtrudedSolidList[0].Profile.ProfileName;
            this.objectType = description;
            this.objectPlacement = objectPlacement;


            ifcColumn = Model.Instances.New<IfcColumn>(c =>
             {
                 c.Name = name;
                 c.Representation = productRepresentation.IfcProductionDefShape;
                 c.ObjectPlacement = ObjectPlacement.IfcLocalPlacement;
                 c.ObjectType = objectType;
                 c.Description = description;

             }
            );

        }
      

        public void AssignMaterial(Material material)
        {
            MaterialRelation relation = new MaterialRelation(Model, material, new List<IModelObject>() { this });
        }

    }
}

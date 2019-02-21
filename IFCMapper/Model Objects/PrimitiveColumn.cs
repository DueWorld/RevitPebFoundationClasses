using IFCMapper.Geomterical_Entities;
using IFCMapper.Geomterical_Entities.ExtrudedCrossSections;
using IFCMapper.Material_Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xbim.Ifc;
using Xbim.Ifc2x3.MeasureResource;
using Xbim.Ifc2x3.SharedBldgElements;
using Xbim.Ifc2x3.Kernel;
using IFCMapper.Relational_Entities;

namespace IFCMapper.Model_Objects
{
    class PrimitiveColumn : IModelObject, ISchemaEntity
    {
        private string name;
        private string description;
        private string objectType;
        private ProductionDefinitionShape productRepresentation;
        private LocalPlacement objectPlacement;
        private string tag;
        private IfcColumn ifcColumn;

        public string Name => name;
        public string Description => description;
        public string ObjectType => objectType;
        public ProductionDefinitionShape ProductRepresentation => productRepresentation;
        public LocalPlacement ObjectPlacement => objectPlacement;
        public string Tag => tag;
        public IfcColumn IfcColumn => ifcColumn;
        public IfcStore Model { get; set; }
        public IfcProduct IfcProduct => IfcColumn;

        public PrimitiveColumn(IfcStore model, string name, string description, string objectType, string tag, LocalPlacement objectPlacement, ProductionDefinitionShape productRepresentation)
        {
            this.Model = model;
            this.productRepresentation = productRepresentation;
            this.name = name;
            this.description = description;
            this.objectType = objectType;
            this.tag = tag;
            this.objectPlacement = objectPlacement;


            ifcColumn = Model.Instances.New<IfcColumn>(c =>
             {
                 c.Name = name;
                 c.Representation = productRepresentation.IfcProductionDefShape;
                 c.ObjectPlacement = ObjectPlacement.IfcLocalPlacement;
                 c.ObjectType = objectType;
                 c.Tag = tag;
                 c.Description = description;

             }
            );

        }
        public PrimitiveColumn(IfcStore model, string name, string description, string objectType, string tag, LocalPlacement objectPlacement, Material material, ProductionDefinitionShape productRepresentation)
        {
            this.Model = model;
            this.productRepresentation = productRepresentation;
            this.name = name;
            this.description = description;
            this.objectType = objectType;
            this.tag = tag;
            this.objectPlacement = objectPlacement;

            ifcColumn = Model.Instances.New<IfcColumn>(c =>
            {
                c.Name = name;
                c.Representation = productRepresentation.IfcProductionDefShape;
                c.ObjectPlacement = ObjectPlacement.IfcLocalPlacement;
                c.ObjectType = objectType;
                c.Tag = tag;
                c.Description = description;
            }
            );

            MaterialRelation relation = new MaterialRelation(model, material, new List<ISchemaEntity>() { this });
        }

        public void AssignMaterial(Material material)
        {
            MaterialRelation relation = new MaterialRelation(Model, material, new List<ISchemaEntity>() { this });
        }

    }
}

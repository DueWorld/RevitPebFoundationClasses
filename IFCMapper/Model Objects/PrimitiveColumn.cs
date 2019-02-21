using IFCMapper.Geomterical_Entities;
using IFCMapper.Geomterical_Entities.ExtrudedCrossSections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xbim.Ifc;
using Xbim.Ifc2x3.MeasureResource;
using Xbim.Ifc2x3.SharedBldgElements;

namespace IFCMapper.Model_Objects
{
    class PrimitiveColumn
    {
        private OwnerHistory owner;
        private string name;
        private string description;
        private string objectType;
        private ProductionDefinitionShape productRepresentation;
        private LocalPlacement objectPlacement;
        private string tag;
        private IfcColumn ifcColumn;

        public OwnerHistory Owner => owner;
        public string Name => name;
        public string Description;
        public string ObjectType;
        public ProductionDefinitionShape ProductRepresentation => productRepresentation;
        public LocalPlacement ObjectPlacement => objectPlacement;
        public string Tag => tag;
        public IfcColumn IfcColumn => ifcColumn;
        public IfcStore Model { get; set; }

        public PrimitiveColumn(IfcStore model, string name, string description, string objectType, string tag, LocalPlacement objectPlacement, OwnerHistory owner, ProductionDefinitionShape productRepresentation)
        {
            this.Model = model;
            this.productRepresentation = productRepresentation;
            this.owner = owner;
            this.name = name;
            this.description = description;
            this.objectType = objectType;
            this.tag = tag;
            this.objectPlacement = objectPlacement;


            ifcColumn = Model.Instances.New<IfcColumn>(c =>
             {
                 c.OwnerHistory = owner.IfcOwnerHistory;
                 c.Name = name;
                 c.Representation = productRepresentation.IfcProductionDefShape;
                 c.ObjectPlacement = ObjectPlacement.IfcLocalPlacement;
                 c.ObjectType = objectType;
                 c.Tag = tag;
                 c.Description = description;

             }
            );

        }
    }
}

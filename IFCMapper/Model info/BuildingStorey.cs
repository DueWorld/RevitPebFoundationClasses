using IFCMapper.Geomterical_Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xbim.Ifc;
using Xbim.Ifc2x3.ProductExtension;
using Xbim.Ifc2x3.Kernel;
using IFCMapper.Relational_Entities;
using IFCMapper.Model_Objects;

namespace IFCMapper.Model_info
{
    class BuildingStorey : ISchemaEntity
    {
        private IfcBuildingStorey ifcBuildingStorey;
        private string name;
        private LocalPlacement localPlacement;
        private IfcElementCompositionEnum compositionType;
        private double elevation;


        public IfcBuildingStorey IfcBuildingStorey => ifcBuildingStorey;
        public String Name => name;
        public LocalPlacement LocalPlacement => localPlacement;
        public IfcElementCompositionEnum CompositionType => compositionType;
        public double Elevation => elevation;

        public IfcProduct IfcProduct => ifcBuildingStorey;
        public IfcStore Model {get;set;}

        public BuildingStorey(IfcStore model, OwnerHistory ownerHistory, string name, LocalPlacement localPlacement, IfcElementCompositionEnum compositionType, double elevation)
        {
            this.Model = model;

            ifcBuildingStorey = model.Instances.New<IfcBuildingStorey>(p =>
            {
                p.Name = name;
                p.ObjectPlacement = localPlacement.IfcLocalPlacement;
                p.CompositionType = compositionType;
                p.Elevation = elevation;
            });

            this.name = name;
            this.localPlacement = localPlacement;
            this.compositionType = compositionType;
            this.elevation = elevation;
        }

        public void AddModelObject(IModelObject modelObject)
        {
            ContainmentRelation rel = new ContainmentRelation(Model,this,new List<ISchemaEntity>() {modelObject});
        }
    }
}

using System.Collections.Generic;
using Xbim.Ifc2x3.Kernel;
using IFCMapper.Geomterical_Entities;
using IFCMapper.Geomterical_Entities.ExtrudedCrossSections;
using IFCMapper.Material_Resources;
using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xbim.Ifc;
using Xbim.Ifc2x3.MeasureResource;
using Xbim.Ifc2x3.SharedBldgElements;
using Xbim.Ifc2x3.ProductExtension;
using Xbim.Common;
using IFCMapper.Model_info;

namespace IFCMapper.Relational_Entities
{
    class ContainmentRelation
    {
        List<ISchemaEntity> relatedElements;

        BuildingStorey relatingStructure;

        IfcStore Model { get; set; }

        public ContainmentRelation(IfcStore model, BuildingStorey relatingStructure, List<ISchemaEntity> relatedElements)
        {
            this.relatingStructure = relatingStructure;

            this.relatedElements = relatedElements;


            var resultingRelation = model.Instances.New<IfcRelContainedInSpatialStructure>(rel =>
             {
                 foreach (ISchemaEntity entity in relatedElements)
                 {
                     rel.RelatedElements.Add(entity.IfcProduct);
                 }
                 rel.RelatingStructure = relatingStructure.IfcBuildingStorey;

             });
        }







    }
}

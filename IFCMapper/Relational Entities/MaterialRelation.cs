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
    class MaterialRelation
    {
        List<ISchemaEntity> relatedElements;

        Material material;

        IfcStore Model { get; set; }

        public MaterialRelation(IfcStore model, Material material, List<ISchemaEntity> relatedElements)
        {
            this.material = material;

            this.relatedElements = relatedElements;


            var resultingRelation = model.Instances.New<IfcRelAssociatesMaterial>(rel =>
            {
                foreach (ISchemaEntity entity in relatedElements)
                {
                    rel.RelatedObjects.Add(entity.IfcProduct);
                }
                rel.RelatingMaterial = material.IfcMaterial;
            });
        }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xbim.Ifc;
using Xbim.Ifc2x3.GeometryResource;
using Xbim.Ifc2x3.MeasureResource;
using Xbim.Ifc2x3.ProfileResource;
using Xbim.Ifc2x3.RepresentationResource;

namespace IFCMapper.Geomterical_Entities.ExtrudedCrossSections
{
    class ProductionDefinitionShape
    {
        private List<ShapeRepresentation> representations;
        private IfcProductDefinitionShape ifcProductionDefShape;
        public List<ShapeRepresentation> Representations => representations;

        public IfcStore Model { get; set; }
        public IfcProductDefinitionShape IfcProductionDefShape => ifcProductionDefShape;

        public ProductionDefinitionShape(IfcStore model,List<ShapeRepresentation> representations)
        {
            this.representations = representations;
            ifcProductionDefShape = model.Instances.New<IfcProductDefinitionShape>(s=>
            {
                foreach(var rep in representations)
                {
                    s.Representations.Add(rep.IfcShapeRepresenation);
                }
            }
            );
        }

    }
}

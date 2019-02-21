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
    class ShapeRepresentation
    {
        private GeometricRepresentationSubContext contextOfItems;
        private List<ExtrudedAreaSolid> extrudedSolidList;
        private string representationIdentifier;
        private string representationType;
        private IfcShapeRepresentation ifcShapeRepresenation;



        public IfcStore Model { get; set; }
        public GeometricRepresentationSubContext ContextOfItems => contextOfItems;
        public List<ExtrudedAreaSolid> ExtrudedSolidList => extrudedSolidList;
        public string RepresentationIdentifier => representationIdentifier;
        public string RepresentationType => representationType;
        public IfcShapeRepresentation IfcShapeRepresenation => ifcShapeRepresenation;


        public ShapeRepresentation(IfcStore model, GeometricRepresentationSubContext contextOfItems, List<ExtrudedAreaSolid> extrudedSolids, string representationIdentifier = "Body", string representationType = "SweptSolid")
        {
            this.Model = model;
            this.contextOfItems = contextOfItems;
            this.representationType = representationType;
            this.representationIdentifier = representationIdentifier;
            this.extrudedSolidList = extrudedSolids;

            ifcShapeRepresenation = model.Instances.New<IfcShapeRepresentation>(r =>
             {
                 foreach (var shape in extrudedSolids)
                 {
                     r.Items.Add(shape.IfcExtrusionSolid);
                 }
                 r.ContextOfItems = contextOfItems.IfcRepresentationSubContext;
                 r.RepresentationType = representationType;
                 r.RepresentationIdentifier = RepresentationIdentifier;
             });



        }












    }
}

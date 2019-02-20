using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xbim.Ifc;
using Xbim.Ifc2x3.GeometryResource;
using Xbim.Ifc2x3.MeasureResource;
using Xbim.Ifc2x3.RepresentationResource;

namespace IFCMapper.Geomterical_Entities.ExtrudedCrossSections
{
    class GeometricRepresentationSubContext
    {
        private GeometricRepresentationContext parentContext;
        private string contextIdentifier;
        private string contextType;
        private IfcGeometricProjectionEnum targetView;
        private IfcGeometricRepresentationSubContext ifcRepresentationSubContext;


        public GeometricRepresentationContext ParentContext => parentContext;
        public string ContextIdentifier => contextIdentifier;
        public string ContextType => contextType;
        public IfcGeometricProjectionEnum TargetView => targetView;
        public IfcGeometricRepresentationSubContext IfcRepresentationSubContext => ifcRepresentationSubContext;
        public IfcStore Model { get; set; }



        public GeometricRepresentationSubContext(IfcStore model, string contextIdentifier, string contextType, IfcGeometricProjectionEnum targetView, GeometricRepresentationContext parentContext)
        {

            this.contextIdentifier = contextIdentifier;
            this.contextType = contextType;
            this.targetView = targetView;
            this.parentContext = parentContext;


            ifcRepresentationSubContext = model.Instances.New<IfcGeometricRepresentationSubContext>(sc =>
             {
                 sc.ContextIdentifier = contextIdentifier;
                 sc.ContextType = contextType;
                 sc.TargetView = targetView;
                 sc.ParentContext = parentContext.IfcRepresenationContext;
             });
        }
    }
}

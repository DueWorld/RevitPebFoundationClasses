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
    class GeometricRepresentationContext
    {
        private string contextType;
        private IfcDimensionCount coordinateSpaceDimension;
        private double precision;
        private PlacementAxis3D worldCoordinateSystem;
        private IfcGeometricRepresentationContext ifcRepresenationContext;

        public string ContextType => contextType;
        public long CoordinateSpaceDimension => coordinateSpaceDimension;
        public double Precision => precision;
        public PlacementAxis3D WorldCoordinateSystem => worldCoordinateSystem;
        public IfcGeometricRepresentationContext IfcRepresenationContext => ifcRepresenationContext;
        public IfcStore Model { get; set; }

        public GeometricRepresentationContext(IfcStore model, string contextType, long coordinateSpaceDim, double precision, PlacementAxis3D worldCoordinateSystem)
        {
            this.contextType = contextType;
            this.coordinateSpaceDimension = coordinateSpaceDim;
            this.precision = precision;
            this.worldCoordinateSystem = worldCoordinateSystem;
            this.Model = model;
            ifcRepresenationContext = Model.Instances.New<IfcGeometricRepresentationContext>(r =>
            {
                r.ContextType = contextType;
                r.CoordinateSpaceDimension = coordinateSpaceDim;
                r.Precision = precision;
                r.WorldCoordinateSystem = worldCoordinateSystem.IfcAxis2Placement3D;
            });
        }







    }
}

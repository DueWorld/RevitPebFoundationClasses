using IFCMapper.Geomterical_Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xbim.Ifc;
using Xbim.Ifc2x3.GeometricConstraintResource;
using Xbim.Ifc2x3.GeometricModelResource;
using Xbim.Ifc2x3.GeometryResource;
using Xbim.Ifc2x3.ProductExtension;
using Xbim.Ifc2x3.ProfileResource;
using Xbim.Ifc2x3.RepresentationResource;
using Xbim.Ifc2x3.SharedBldgElements;

namespace IFCMapper.RevitRetreiver
{
    class RevitColumn
    {
        private Point origin;
        private Vector axis;
        private Vector reffDirection;
        private double overallWidth;
        private double overallDepth;
        private double flangeThickness;
        private double webThickness;
        private double height;
        private List<RevitPlate> components;

        public Point Origin => origin;
        public Vector Axis => axis;
        public Vector ReffDirection => reffDirection;
        public double OverallWidth => overallWidth;
        public double OverallDepth => overallDepth;
        public double FlangeThickness => flangeThickness;
        public double WebThickness => webThickness;
        public List<RevitPlate> Components => components;
        public double Height => height;

        public RevitColumn(IfcColumn column)
        {
            //initializing
            origin = new Point();
            axis = new Vector();
            reffDirection = new Vector();
            components = new List<RevitPlate>();

            //location and orientationData
            var localPlacement = ((IfcLocalPlacement)column.ObjectPlacement).RelativePlacement;
            var axisPlacement3D = ((IfcAxis2Placement3D)localPlacement);
            origin.X = axisPlacement3D.Location.X;
            origin.Y = axisPlacement3D.Location.Y;
            origin.Z = axisPlacement3D.Location.Z;



            if (axisPlacement3D.Axis == null)
            {
                Axis.X = 0;
                Axis.Y = 0;
                Axis.Z = 1;
                reffDirection.X = 1;
                reffDirection.Y = 0;
                reffDirection.Z = 0;
            }
            else
            {

                axis.X = axisPlacement3D.Axis.X;
                axis.Y = axisPlacement3D.Axis.Y;
                axis.Z = axisPlacement3D.Axis.Z;

                reffDirection.X = axisPlacement3D.RefDirection.X;
                reffDirection.Y = axisPlacement3D.RefDirection.Y;
                reffDirection.Z = axisPlacement3D.RefDirection.Z;
            }

            //GeometrixData
            overallWidth = ((IfcIShapeProfileDef)column.Representation.Representations.OfType<IfcShapeRepresentation>().FirstOrDefault().Items.OfType<IfcMappedItem>().FirstOrDefault().MappingSource.MappedRepresentation.Items.OfType<IfcExtrudedAreaSolid>().FirstOrDefault().SweptArea).OverallWidth;
            overallDepth = ((IfcIShapeProfileDef)column.Representation.Representations.OfType<IfcShapeRepresentation>().FirstOrDefault().Items.OfType<IfcMappedItem>().FirstOrDefault().MappingSource.MappedRepresentation.Items.OfType<IfcExtrudedAreaSolid>().FirstOrDefault().SweptArea).OverallDepth;
            webThickness = ((IfcIShapeProfileDef)column.Representation.Representations.OfType<IfcShapeRepresentation>().FirstOrDefault().Items.OfType<IfcMappedItem>().FirstOrDefault().MappingSource.MappedRepresentation.Items.OfType<IfcExtrudedAreaSolid>().FirstOrDefault().SweptArea).WebThickness;
            flangeThickness = ((IfcIShapeProfileDef)column.Representation.Representations.OfType<IfcShapeRepresentation>().FirstOrDefault().Items.OfType<IfcMappedItem>().FirstOrDefault().MappingSource.MappedRepresentation.Items.OfType<IfcExtrudedAreaSolid>().FirstOrDefault().SweptArea).FlangeThickness;
            height = (column.Representation.Representations.OfType<IfcShapeRepresentation>().FirstOrDefault().Items.OfType<IfcMappedItem>().FirstOrDefault().MappingSource.MappedRepresentation.Items.OfType<IfcExtrudedAreaSolid>().FirstOrDefault()).Depth;


            GetComponants();
        }
        private void GetComponants()
        {
            RevitPlate web = new RevitPlate(origin, axis, reffDirection, webThickness, (overallDepth - 2 * flangeThickness),height);



            Vector flangeReffDirection = GetPrePendDirection(reffDirection, axis);
            var scalarQ = (overallDepth / 2) - (flangeThickness / 2);
            Point flangeOneOrigin = new Point((origin.X + scalarQ * flangeReffDirection.X), (origin.Y + scalarQ * flangeReffDirection.Y), origin.Z);
            Point flangeTwoOrigin = new Point((origin.X + scalarQ * flangeReffDirection.X * -1), (origin.Y + scalarQ * flangeReffDirection.Y * -1), origin.Z);


            RevitPlate flangeOne = new RevitPlate(flangeOneOrigin, axis, flangeReffDirection, FlangeThickness, overallWidth,height);
            RevitPlate flangeTwo = new RevitPlate(flangeTwoOrigin, axis, flangeReffDirection, FlangeThickness, overallWidth,height);

            components.Add(web);
            components.Add(flangeOne);
            components.Add(flangeTwo);
        }
        private Vector GetPrePendDirection(Vector direction, Vector sharedDirection)
        {
            double x = sharedDirection.Y * direction.Z - direction.Y * sharedDirection.Z;
            double y = (sharedDirection.X * direction.Z - direction.X * sharedDirection.Z) * -1;
            double z = sharedDirection.X * direction.Y - direction.X * sharedDirection.Y;
            return new Vector(x, y, z);
        }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xbim.Ifc;
using Xbim.Ifc2x3.GeometryResource;

namespace IFCMapper.RevitRetreiver
{
    class RevitPlate
    {
        private Point origin;
        private Vector axis;
        private Vector reffDirection;
        private double overallWidth;
        private double overallDepth;
        private double height;

        public Point Origin => origin;
        public Vector Axis => axis;
        public Vector ReffDirection => reffDirection;
        public double OverallWidth => overallWidth;
        public double OverallDepth => overallDepth;
        public double Height => height;

        public RevitPlate(Point origin, Vector axis, Vector reffDirection, double width, double depth,double height)
        {
            this.origin = origin;
            this.axis = axis;
            this.reffDirection = reffDirection;
            this.overallWidth = width;
            this.overallDepth = depth;
            this.height = height;
        }
    }
}

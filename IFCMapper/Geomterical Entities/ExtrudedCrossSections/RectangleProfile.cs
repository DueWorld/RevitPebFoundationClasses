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
    class RectangleProfile : IProfile
    {
        private double xDim;
        private double yDim;
        private IfcProfileTypeEnum profileType;
        private string profileName;
        private IfcProfileDef ifcRectangleProfile;
        private PlacementAxis2D position;

        public double XDim => XDim;
        public double YDim => yDim;
        public string ProfileName => profileName;
        public IfcProfileTypeEnum ProfileType => profileType;
        public PlacementAxis2D Position => position;


        public IfcStore Model { get; set; }
        public IfcProfileDef ProfileDef => ifcRectangleProfile;

        public RectangleProfile(IfcStore model, double x, double y, IfcProfileTypeEnum profileType,PlacementAxis2D position)
        {
            this.Model = model;
            this.xDim = x;
            this.yDim = y;
            this.profileName = $"PL{x}*{y}";
            this.profileType = profileType;
            this.position = position;


            IfcProfileDef result = model.Instances.OfType<IfcRectangleProfileDef>().Where(rect =>
            {
                if (rect.ProfileName == profileName && rect.ProfileType == profileType && rect.XDim == x && rect.YDim == y && rect.ProfileType == profileType && rect.Position.Equals(position.IfcAxis2Placement2D))
                    return true;
                else
                    return false;
            }).FirstOrDefault();

            if (result == null)
            {
                ifcRectangleProfile = model.Instances.New<IfcRectangleProfileDef>(rect =>
                 {
                     rect.ProfileName = profileName;
                     rect.ProfileType = profileType;
                     rect.XDim = x;
                     rect.YDim = y;
                     rect.Position = position.IfcAxis2Placement2D;
                 }
                );
            }

            else
                ifcRectangleProfile = result;
        }
    }
}

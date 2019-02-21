using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xbim.Ifc;
using Xbim.Ifc2x3.GeometricConstraintResource;
using Xbim.Ifc2x3.GeometricModelResource;
using Xbim.Ifc2x3.GeometryResource;
using Xbim.Ifc2x3.Kernel;
using Xbim.Ifc2x3.ProfileResource;
using Xbim.Ifc2x3.RepresentationResource;
using Xbim.Ifc2x3.SharedBldgElements;

namespace IFCMapper.RevitRetreiver
{
    class RevitSeeker
    {
        private List<RevitColumn> revitColumns;
        private List<IfcColumn> ifcColumns;

        public List<RevitColumn> RevitColumns => revitColumns;


        private  List<IfcColumn> RetrieveColumns(IfcStore model)
        {
            var retreivedColumn = model.Instances.OfType<IfcColumn>();
            var columnList = retreivedColumn.ToList();
            return columnList;
        }
        public RevitSeeker(IfcStore model)
        {
            ifcColumns = RetrieveColumns(model);
            revitColumns = new List<RevitColumn>();
            foreach (var column in ifcColumns)
            {
                revitColumns.Add(new RevitColumn(column));
            }
        }
    }
}

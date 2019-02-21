using IFCMapper.Geomterical_Entities.ExtrudedCrossSections;
using IFCMapper.Relational_Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xbim.Ifc;
using Xbim.Ifc2x3.ActorResource;
using Xbim.Ifc2x3.Kernel;

namespace IFCMapper.Model_info
{
    class Project
    {
        private IfcProject ifcProject;
        private string name;
        private GeometricRepresentationContext context;
        private UnitAssignmentRelation units;


        public IfcProject IfcProject => ifcProject;
        public string Name => name;
        public GeometricRepresentationContext Context => context;
        public UnitAssignmentRelation Units => units;

    
        public Project(IfcStore model, string name, GeometricRepresentationContext context, UnitAssignmentRelation units)
        {
            ifcProject = model.Instances.New<IfcProject>(p =>
            {
                p.Name = name;
                p.RepresentationContexts.Add(context.IfcRepresenationContext);
                p.UnitsInContext = units.IfcUnitAssignment;
            });
            this.name = name;
            this.context = context;
            this.units = units;
        }
    }
}

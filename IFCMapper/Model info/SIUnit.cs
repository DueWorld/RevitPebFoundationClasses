using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xbim.Ifc;
using Xbim.Ifc2x3.MeasureResource;

namespace IFCMapper.Model_info
{
    class SIUnit
    {
        private IfcSIUnit ifcSiUnit;
        IfcUnitEnum unitType;
        IfcSIPrefix prefix;
        IfcSIUnitName name;


        public IfcSIUnit IfcSIUnit => ifcSiUnit;
        public IfcUnitEnum UnitType => unitType;
        public IfcSIPrefix Prefix => prefix;
        public IfcSIUnitName Name => name;


        public SIUnit(IfcStore model,IfcSIUnitName name,IfcSIPrefix prefix,IfcUnitEnum unitType )
        {
            ifcSiUnit = model.Instances.New<IfcSIUnit>(p =>
            {
                p.Name = name;
                p.Prefix = prefix;
                p.UnitType = unitType;
            });

            this.name = name;
            this.prefix = prefix;
            this.unitType = unitType;
        }
        public SIUnit(IfcStore model, IfcSIUnitName name, IfcUnitEnum unitType)
        {
            ifcSiUnit = model.Instances.New<IfcSIUnit>(p =>
            {
                p.Name = name;
                p.UnitType = unitType;
            });

            this.name = name;
            this.unitType = unitType;
        }
    }
}

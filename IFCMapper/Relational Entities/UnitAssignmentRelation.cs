using IFCMapper.Model_info;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xbim.Ifc;
using Xbim.Ifc2x3;
using Xbim.Ifc2x3.MeasureResource;

namespace IFCMapper.Relational_Entities
{
    class UnitAssignmentRelation
    {
        private IfcUnitAssignment ifcUnitAssignment;
        private List<SIUnit> units;


        public IfcUnitAssignment IfcUnitAssignment => ifcUnitAssignment;
        public List<SIUnit> Units => units;


        public UnitAssignmentRelation(IfcStore model,List<SIUnit> units)
        {
            ifcUnitAssignment = model.Instances.New<IfcUnitAssignment>(p =>
            {
                foreach (var item in units)
                {
                    p.Units.Add(item.IfcSIUnit);
                }
                
            });

            this.units = units;
           
        }
        public UnitAssignmentRelation(IfcStore model)
        {
            ifcUnitAssignment = model.Instances.New<IfcUnitAssignment>(p =>
            {
                units = new List<SIUnit>()
                {
                    new SIUnit(model,IfcSIUnitName.METRE,IfcSIPrefix.MILLI,IfcUnitEnum.LENGTHUNIT),
                    new SIUnit(model,IfcSIUnitName.SQUARE_METRE,IfcUnitEnum.AREAUNIT),
                    new SIUnit(model,IfcSIUnitName.CUBIC_METRE,IfcUnitEnum.VOLUMEUNIT),
                    new SIUnit(model,IfcSIUnitName.GRAM,IfcSIPrefix.KILO,IfcUnitEnum.MASSUNIT),
                    new SIUnit(model,IfcSIUnitName.SECOND,IfcUnitEnum.TIMEUNIT),
                    new SIUnit(model,IfcSIUnitName.RADIAN,IfcUnitEnum.PLANEANGLEUNIT),
                    new SIUnit(model,IfcSIUnitName.STERADIAN,IfcUnitEnum.SOLIDANGLEUNIT),
                    new SIUnit(model,IfcSIUnitName.DEGREE_CELSIUS,IfcUnitEnum.THERMODYNAMICTEMPERATUREUNIT),
                    new SIUnit(model,IfcSIUnitName.LUMEN,IfcUnitEnum.LUMINOUSFLUXUNIT)
                };

                foreach (var item in units)
                {
                    p.Units.Add(item.IfcSIUnit);
                }

            });
        }
    }
}

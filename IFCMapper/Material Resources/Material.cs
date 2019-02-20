using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xbim.Ifc;
using Xbim.Ifc2x3.Interfaces;
using Xbim.Ifc2x3.MeasureResource;
using Xbim.Ifc2x3.PropertyResource;
using Xbim.Common.Step21;
using Xbim.Ifc2x3.GeometryResource;
using Xbim.Ifc2x3.MaterialResource;

namespace IFCMapper.Material_Resources
{
    class Material
    {
        private string name;


        private IfcMaterial ifcMaterial;


        public IfcMaterial IfcMaterial => ifcMaterial;


        public string Name => name;


        public IfcStore Model { get; set; }


        public Material(IfcStore Model, string name)
        {
            this.name = name;
            IfcMaterial result = Model.Instances.OfType<IfcMaterial>().Where(m => m.Name == name).FirstOrDefault();

            if (result == null)
                ifcMaterial = Model.Instances.New<IfcMaterial>(m => m.Name = name);
            else
                ifcMaterial = result;
        }


    }
}

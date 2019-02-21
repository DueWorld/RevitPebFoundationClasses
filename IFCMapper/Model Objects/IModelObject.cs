using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xbim.Ifc;

namespace IFCMapper.Model_Objects
{
    interface IModelObject:ISchemaEntity
    {
        string Name { get; }
        string Description { get; }
        string ObjectType { get; }

    }
}

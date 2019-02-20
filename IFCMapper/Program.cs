using System.Linq;
using Xbim.Ifc;
using Xbim.Ifc2x3.Interfaces;
using Xbim.Ifc2x3.MeasureResource;
using Xbim.Ifc2x3.PropertyResource;
using Xbim.Common.Step21;
using IFCMapper.Geomterical_Entities;
using IFCMapper.Model_info;
using IFCMapper.Relational_Entities;

namespace IFCMapper
{
    class Program
    {
        static void Main(string[] args)
        {
            //CHANGE PATH HERE.
            string path = @"C:\Users\Scorias\Desktop\IFC trails";

            var editor = new XbimEditorCredentials
            {
                ApplicationDevelopersName = "xBIM Team",
                ApplicationFullName = "xBIM Toolkit",
                ApplicationIdentifier = "xBIM",
                ApplicationVersion = "4.0",
                EditorsFamilyName = "sane",
                EditorsGivenName = "Johann blast",
                EditorsOrganisationName = "Independent Architecture"
            };

            using (IfcStore model = IfcStore.Create(editor, IfcSchemaVersion.Ifc2X3, XbimStoreType.InMemoryModel))
            {
                using (var txn = model.BeginTransaction("Initialise Model"))
                {
                    //ALL CODE HERE.
                    Person Hbeed = new Person(model, "Hbood", "Hababeed");
                    Organization Habdland = new Organization(model, "Habdland");
                    Application app = new Application(model, Habdland, "1.00", "REEEE", "50");
                    PersonOrgRelation POR = new PersonOrgRelation(model, Habdland, Hbeed);
                    OwnerHistory ownerHistory = new OwnerHistory(model, POR, app, Xbim.Ifc2x3.UtilityResource.IfcChangeActionEnum.NOCHANGE, 11122001);
;                        
                    txn.Commit();
                }






                model.SaveAs($"{path}\\REEEEEEEEEEEEEEEE.ifcxml");
            }

        }
    }
}

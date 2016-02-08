using BootSharp.Data;
using BootSharp.Data.NHibernate;

namespace BootSharp.Tests.Data.NHibernate.Mappings
{
    public class AMapper : NhDataMapper<A>
    {
        protected override void MapProperties()
        {
            Map(a => a.Name)
                .Length(512)
                .Not.Nullable();

            base.MapProperties();
        }

        protected override void MapForeignKeys()
        {
            // A has many B and B has one A
            // NH: HasMany(a => a.BCollection).Table("B").KeyColumn("A_Id");
            // BS: ManyToOne(a => a.BCollection, b => b.A, new DataMapBase("B", "A_Id"));
            ManyToOne(a => a.BCollection, b => b.A, false, new DataMapBase("B", "A_Id"));

            // A has many C and C has many A, using join table AC
            // NH: HasManyToMany(a => a.CCollection).Table("AC").ParentKeyColumn("A_Id").ChildKeyColumn("C_Id");
            // BS: ManyToMany(a => a.CCollection, c => c.ACollection, new DataMapBase("AC", "A_Id", "C_Id"));
            ManyToMany(a => a.CCollection, c => c.ACollection, new DataMapBase("AC", "A_Id", "C_Id"));

            base.MapForeignKeys();
        }
    }
}

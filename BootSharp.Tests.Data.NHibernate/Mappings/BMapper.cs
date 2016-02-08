using BootSharp.Data;
using BootSharp.Data.NHibernate;

namespace BootSharp.Tests.Data.NHibernate.Mappings
{
    public class BMapper : NhDataMapper<B>
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
            // B has one A and A has many B
            // NH: References(b => b.A).Column("A_Id").Not.Nullable();
            // BS: OneToMany(b => b.A, a => a.BCollection, false, new DataMapBase(null, "A_Id"));
            OneToMany(b => b.A, a => a.BCollection, false, new DataMapBase(null, "A_Id"));

            base.MapForeignKeys();
        }
    }
}

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
            HasMany(a => a.BCollection)
                .LazyLoad();

            // A has many C and C has many A, using join table AC
            HasManyToMany(a => a.CCollection)
                .LazyLoad()
                .Cascade.All()
                .Table("AC");

            base.MapForeignKeys();
        }
    }
}

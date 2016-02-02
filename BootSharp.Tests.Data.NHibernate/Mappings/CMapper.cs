using BootSharp.Data.NHibernate;

namespace BootSharp.Tests.Data.NHibernate.Mappings
{
    public class CMapper : NhDataMapper<C>
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
            // C has many A and A has many C, using join table AC
            HasManyToMany(c => c.ACollection)
                .Cascade.All()
                .Table("AC");

            base.MapForeignKeys();
        }
    }
}

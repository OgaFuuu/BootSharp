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
            References(b => b.A)
                .Column("A_Id")
                .Not.Nullable();

            base.MapForeignKeys();
        }
    }
}

using BootSharp.Data.EntityFramework;

namespace BootSharp.Tests.Data.EntityFramework.Mappings
{
    public class BMapper : EfDataMapper<B>
    {
        protected override void MapProperties()
        {
            Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(512);

            base.MapProperties();
        }

        protected override void MapForeignKeys()
        {
            // B has one A and A has many B.
            HasRequired(b => b.A).WithMany(a => a.BCollection);
            
            base.MapForeignKeys();
        }
    }
}

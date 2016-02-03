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
            // EF: HasRequired(b => b.A).WithMany(a => a.BCollection);
            // BS: OneToMany(b => b.A, a => a.BCollection);
            OneToMany(b => b.A, a => a.BCollection);

            base.MapForeignKeys();
        }
    }
}

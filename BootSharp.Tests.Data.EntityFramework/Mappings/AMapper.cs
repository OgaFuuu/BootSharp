using BootSharp.Data.EntityFramework;

namespace BootSharp.Tests.Data.EntityFramework.Mappings
{
    public class AMapper : EfDataMapper<A>
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
            // A has many B and B has one A
            HasMany(a => a.BCollection)
                .WithRequired(b => b.A);

            // A has many C and C has many A, using join table AC
            HasMany(a => a.CCollection)
                .WithMany(c => c.ACollection)
                .Map(m =>
                {
                    m.ToTable("AC");
                });

            base.MapForeignKeys();
        }
    }
}

using BootSharp.Data.EntityFramework;

namespace BootSharp.Tests.Data.EntityFramework.Mappings
{
    public class CMapper : EfDataMapper<C>
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
            // C has many A and A has many C, using join table AC
            HasMany(c => c.ACollection)
                .WithMany(a => a.CCollection)
                .Map(m =>
                {
                    m.ToTable("AC");
                });
            
            base.MapForeignKeys();
        }
    }
}

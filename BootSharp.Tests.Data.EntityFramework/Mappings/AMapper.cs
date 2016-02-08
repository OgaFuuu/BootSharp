using BootSharp.Data;
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
            // EF: HasMany(a => a.BCollection).WithRequired(b => b.A);
            // BS: ManyToOne(a => a.BCollection, b => b.A);
            ManyToOne(a => a.BCollection, b => b.A);

            // A has many C and C has many A, using join table AC
            // EF: HasMany(a => a.CCollection).WithMany(c => c.ACollection).Map(m => { m.ToTable("AC"); });
            // BS: ManyToMany(a => a.CCollection, c => c.ACollection, new DataMapBase("AC"));
            ManyToMany(a => a.CCollection, c => c.ACollection, new DataMapBase("AC"));

            base.MapForeignKeys();
        }
    }
}

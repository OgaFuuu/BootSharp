﻿using BootSharp.Data;
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
            // NH: HasManyToMany(c => c.ACollection).Table("AC").ParentKeyColumn("C_Id").ChildKeyColumn("A_Id");
            // BS: ManyToMany(c => c.ACollection, a => a.CCollection, new DataMapBase("AC", "C_Id", "A_Id"));
            ManyToMany(c => c.ACollection, a => a.CCollection, new DataMapBase("AC", "C_Id", "A_Id"));

            base.MapForeignKeys();
        }
    }
}

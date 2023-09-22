using FluentNHibernate.Mapping;

namespace QuanLySinhVien.Data
{
    public class SinhVienMapping
    {
        class SinhVienMappping : ClassMap<SinhVien>
        {

            public SinhVienMappping()
            {
                Id(x => x.Guid).GeneratedBy.Guid().Column("Guid");
                Map(x => x.Name).Column("Name");
                Map(x => x.Birth).Column("Birth");
                Map(x => x.Class).Column("Class");
                Map(x => x.Score).Column("Score");
                
                Table("SinhVien");
            }
        }
    }
}

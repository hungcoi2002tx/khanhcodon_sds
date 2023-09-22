using FluentNHibernate.Conventions.Inspections;
using FluentNHibernate.Mapping;
using GrpcServer.Data;

namespace ServerGrpc.Data
{
    public class LopHocMapping
    {
        public class LopHocMappping : ClassMap<LopHoc>
        {

            public LopHocMappping()
            {
                Id(x => x.ClassId).Column("ClassId").GeneratedBy.Assigned();
                Map(x => x.ClassName).Column("ClassName");
                Map(x => x.ObjectName).Column("ObjectName");
                Map(x => x.Status).Column("Status");

                Table("LopHoc");
            }
        }
    }
}

using FluentNHibernate.Cfg.Db;
using FluentNHibernate.Cfg;
using NHibernate;
using NHibernate.Tool.hbm2ddl;
using ISession = NHibernate.ISession;
using NHibernate.Cfg.MappingSchema;
using NHibernate.Mapping.ByCode;
using NHibernate.Cfg;
using NHibernate.Driver;
using NHibernate.Dialect;

namespace GrpcServer.Data
{
    public class FluentNHibernateHelper
    {
        private static ISessionFactory _sessionFactory;

        private static ISessionFactory SessionFactory
        {
            get
            {
                if (_sessionFactory == null)
                {
                    string connectionString = "Data Source = DESKTOP-FHOHN79\\SQLEXPRESS; Initial Catalog = QuanLySinhVien; User Id=khanhdq; Password=123abc;";
                    _sessionFactory = Fluently.Configure()
                        .Database(MsSqlConfiguration.MsSql2012.ConnectionString(connectionString))
                        .Mappings(m => m.FluentMappings.AddFromAssemblyOf<LopHoc>())
                        .BuildSessionFactory();
                }
                return _sessionFactory;
            }

        }

        public static ISession GetSession()
        {
            return SessionFactory.OpenSession();
        }
    }
}

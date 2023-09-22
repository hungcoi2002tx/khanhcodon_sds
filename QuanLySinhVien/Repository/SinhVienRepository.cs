using System;
using Microsoft.AspNetCore.Http;
using Microsoft.JSInterop;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using QuanLySinhVien.Data;
using ISession = NHibernate.ISession;

namespace QuanLySinhVien.Repository
{
    public class SinhVienRepository : ISinhVienRepository
    {
        public async Task<List<SinhVien>> GetListSinhVien()
        {
            try
            {
                List<SinhVien> sv;
                using (ISession session = FluentNHibernateHelper.GetSession())
                {
                    sv = session.Query<SinhVien>().ToList();

                }
                return sv;

            }
            catch (Exception e)
            {

                throw e;
            }

        }

        public async Task<bool> UpdateSinhVien(SinhVien sv)
        {

            using (var session = FluentNHibernateHelper.GetSession())
            {
                using (var tran = session.BeginTransaction())
                {
                    try
                    {

                        session.Update(sv);

                        tran.Commit();
                        return true;
                    }
                    catch (Exception exx)
                    {
                        tran.Rollback();
                        throw exx;
                        return false;
                    }
                }
            }
        }

        public async Task<SinhVien> GetSinhVienByID(Guid? id)
        {
            SinhVien sv;
            using (ISession session = FluentNHibernateHelper.GetSession())
            {
                using (var tran = session.BeginTransaction())
                    try
                    {
                        sv = session.Get<SinhVien>(id);
                        return sv;
                    }
                    catch (Exception exx)
                    {
                        tran.Rollback();
                        throw exx;
                        return sv;
                    }
            }


        }

        public async Task<bool> AddSinhVien(SinhVien sv)
        {

            using (var session = FluentNHibernateHelper.GetSession())
            {
                using (var tran = session.BeginTransaction())
                {
                    try
                    {

                        session.Save(sv);

                        tran.Commit();
                        return true;
                    }
                    catch (Exception exx)
                    {
                        tran.Rollback();
                        throw exx;
                        return false;
                    }
                }
            }
        }

        public async Task<List<SinhVien>> TaskListSearchSV(string name)
        {
            try
            {
                List<SinhVien> sv;
                using (ISession session = FluentNHibernateHelper.GetSession())
                {
                    sv = session.Query<SinhVien>().Where(c => c.Name == name).ToList();

                }
                return sv;

            }
            catch (Exception e)
            {

                throw e;
            }

        }

        public void DeleteSinhVien(SinhVien sinhVien)
        {
            using (var session = FluentNHibernateHelper.GetSession())
            {
                using (var tran = session.BeginTransaction())
                {
                    session.Delete(sinhVien);
                    tran.Commit();
                }
            }
        }

    }
}

using QuanLySinhVien.Data;
using System.Runtime.Serialization;
using Microsoft.JSInterop;

namespace QuanLySinhVien.Repository
{
    public interface ISinhVienRepository
    {
        Task<List<SinhVien>> GetListSinhVien();
        Task<bool> UpdateSinhVien(SinhVien sv);
        Task<SinhVien> GetSinhVienByID(Guid? id);
        Task<bool> AddSinhVien(SinhVien sv);
        Task<List<SinhVien>> TaskListSearchSV(string name);
        void DeleteSinhVien(SinhVien sinhVien);
    }
}

using QuanLySinhVien.Data;

namespace QuanLySinhVien.Service
{
    public interface ISinhVienService
    {
        Task<List<SinhVien>> GetListSinhVien();
        Task<bool> UpdateSinhVien(SinhVien sv);
        Task<SinhVien> GetSinhVienByID(Guid? id);
        Task<bool> AddSinhVien(SinhVien sv);
        Task<List<SinhVien>> TaskListSearchSV(string name);
        void DeleteSinhVien(SinhVien sinhVien);
    }
}

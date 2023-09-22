using QuanLySinhVien.Data;
using Share;

namespace QuanLySinhVien.Repository
{
    public interface ILopHocRepository
    {
        Task<List<LopHoc>> GetListLopHoc();
        Task<bool> UpdateLopHoc(LopHoc lh);
        Task<LopHoc> GetLopHocByID(int id);
        Task<bool> AddLopHoc(LopHoc lh);
        void DeleteLopHoc(LopHoc lophoc);

    }
}

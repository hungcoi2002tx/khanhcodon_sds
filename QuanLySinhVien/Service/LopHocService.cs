using QuanLySinhVien.Data;
using QuanLySinhVien.Repository;
using Share;

namespace QuanLySinhVien.Service
{
    public class LopHocService : ILopHocService
    {
        ILopHocRepository _lopHocRepository;
        public LopHocService(ILopHocRepository lopHocRepository)
        {
            _lopHocRepository = lopHocRepository;
        }
        public async Task<List<LopHoc>> GetListLopHoc()
        {
            return await _lopHocRepository.GetListLopHoc();
        }
        public async Task<bool> UpdateLopHoc(LopHoc lh)
        {
            return await _lopHocRepository.UpdateLopHoc(lh);
        }
        public async Task<LopHoc> GetLopHocByID(int id)
        {
            return await _lopHocRepository.GetLopHocByID(id);
        }
        public async Task<bool> AddLopHoc(LopHoc sv)
        {
            return await _lopHocRepository.AddLopHoc(sv);
        }
        public async void DeleteLopHoc(LopHoc lophoc) 
        {
            _lopHocRepository.DeleteLopHoc(lophoc);
        }

    }
}

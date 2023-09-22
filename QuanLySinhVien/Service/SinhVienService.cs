using QuanLySinhVien.Data;
using QuanLySinhVien.Repository;

namespace QuanLySinhVien.Service
{
    public class SinhVienService : ISinhVienService
    {
        ISinhVienRepository _sinhVienRepository;
        public SinhVienService(ISinhVienRepository sinhVienRepository)
        {
            _sinhVienRepository = sinhVienRepository;
        }
        public async Task<List<SinhVien>> GetListSinhVien()
        {
            return await _sinhVienRepository.GetListSinhVien();
        }
        public async Task<bool> UpdateSinhVien(SinhVien sv)
        {
            return await _sinhVienRepository.UpdateSinhVien(sv);
        }
        public async Task<SinhVien> GetSinhVienByID(Guid? id)
        {
            return await _sinhVienRepository.GetSinhVienByID(id);
        }
        public async Task<bool> AddSinhVien(SinhVien sv)
        {
            return await _sinhVienRepository.AddSinhVien(sv);
        }
        public async Task<List<SinhVien>> TaskListSearchSV(string name)
        {
            return await _sinhVienRepository.TaskListSearchSV(name);
        }
        public void DeleteSinhVien(SinhVien sinhVien) 
        {
            _sinhVienRepository.DeleteSinhVien(sinhVien);
        }
        
    }
}

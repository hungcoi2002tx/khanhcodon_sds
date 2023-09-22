using System.ComponentModel.DataAnnotations;

namespace QuanLySinhVien.Data
{
    public class SinhVienViewModel
    {
        [Display(Name = "STT")]
        public int STT { get; set; }

        [Display(Name = "Mã sinh viên")]
        public Guid? Guid { get; set; }

        [Display(Name = "Tên sinh viên")]
        public string Name { get; set; }

        [Display(Name = "Ngày sinh")]
        public DateTime? Birth { get; set; }

        [Display(Name = "Mã lớp học")]
        public string Class { get; set; }
        [Display(Name = "Điểm tích lũy")]
        public float Score { get; set; }

    }
}

using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace QuanLySinhVien.Data
{
    public class LopHocViewModel
    {
        [Display(Name = "STT")]
        public int STT { get; set; }
        [Display(Name = "Mã lớp học")]
        public int ClassId { get; set; }

        [Display(Name = "Tên lớp học")]
        public string ClassName { get; set; }

        [Display(Name = "Tên môn học")]
        public string ObjectName { get; set; }

        [Display(Name = "Trạng thái")]
        public bool Status { get; set; }
    }
}

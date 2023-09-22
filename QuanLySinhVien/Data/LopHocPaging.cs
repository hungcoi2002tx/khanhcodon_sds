using System.Runtime.Serialization;

namespace QuanLySinhVien.Data
{
    public class LopHocPaging
    {
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public int Total { get; set; }
    }
}

using System.Runtime.Serialization;

namespace QuanLySinhVien.Data
{
    public class LopHoc
    {
        public virtual int ClassId { get; set; }
        public virtual string ClassName { get; set; }
        public virtual string ObjectName { get; set; }
        public virtual bool Status { get; set; }
    }
}

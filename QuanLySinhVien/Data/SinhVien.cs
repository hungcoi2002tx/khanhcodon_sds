namespace QuanLySinhVien.Data
{
    public class SinhVien
    {
        public virtual Guid? Guid { get; set; }
        public virtual string Name { get; set; }
        public virtual DateTime? Birth { get; set; }
        public virtual string Class { get; set; }
        public virtual float Score { get; set; }
    }
}

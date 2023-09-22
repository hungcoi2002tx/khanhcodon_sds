using System.Security.Claims;
using Share;

namespace QuanLySinhVien.Data
{
    public class LopHocMapper
    {
        public LopHocGrpc ClassToClassGrpc(LopHoc _lopHoc)
        {
            LopHocGrpc lopHocGrpc = new LopHocGrpc();
            lopHocGrpc.ClassId = _lopHoc.ClassId.ToString();
            lopHocGrpc.ClassName = _lopHoc.ClassName;
            lopHocGrpc.ObjectName = _lopHoc.ObjectName;
            lopHocGrpc.Status = _lopHoc.Status;
            return lopHocGrpc;
        }
        public LopHoc ClassGrpcToClass(LopHocGrpc lopHocGrpc)
        {
            LopHoc _lopHoc = new LopHoc();
            _lopHoc.ClassId = int.Parse(lopHocGrpc.ClassId);
            _lopHoc.ClassName = lopHocGrpc.ClassName;
            _lopHoc.ObjectName = lopHocGrpc.ObjectName;
            _lopHoc.Status = lopHocGrpc.Status;
            return _lopHoc;
        }
    }
}

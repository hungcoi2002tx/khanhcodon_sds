using GrpcServer.Data;
using ProtoBuf.Grpc;
using  Share;

namespace GrpcServer.Repository
{
    public interface ILopHocRepository
    {
        List<LopHoc> GetListLopHoc(Empty request);
        Boolean UpdateLopHoc(LopHoc lh);
        LopHoc GetLopHocByID(int id);
        Boolean AddLopHoc(LopHoc lh);
        void DeleteLopHoc(LopHoc lophoc);
    }
}

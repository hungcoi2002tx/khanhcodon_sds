using GrpcServer.Data;
using GrpcServer.Repository;
using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Impl;
using ProtoBuf.Grpc;
using Share;
using System.Security.Claims;

namespace GrpcServer.Services
{
    public class LopHocService : ILopHocServiceGrpc
    {

        private readonly ILogger<LopHocService> logger;
        private readonly ILopHocRepository _lopHocRepository;
        LopHocMapper lopHocMapper = new LopHocMapper();

        public LopHocService(ILogger<LopHocService> logger, ILopHocRepository classRepository)
        {
            this.logger = logger;
            _lopHocRepository = classRepository;
        }
        public  ListLH GetListLopHoc(Empty request, CallContext context = default)
        {
            ListLH listLH = new ListLH();
            List<LopHoc> lh =  _lopHocRepository.GetListLopHoc(request);
            foreach (var item in lh)
            {
                LopHocGrpc lopHocGrpc = lopHocMapper.ClassToClassGrpc(item);
                listLH.List.Add(lopHocGrpc);
            }
            return listLH;
        }

        public LopHocGrpc GetLopHocById(string request , CallContext context = default)
        {
            LopHocGrpc lopHocGrpc = new LopHocGrpc();
            LopHoc lh =  _lopHocRepository.GetLopHocByID(int.Parse(request));

            lopHocGrpc = lopHocMapper.ClassToClassGrpc(lh);
            return lopHocGrpc;
        }

        public Empty AddLopHoc(LopHocGrpc request, CallContext context = default)
        {
            Empty empty = new Empty();
            LopHoc lophoc = lopHocMapper.ClassGrpcToClass(request);
            _lopHocRepository.AddLopHoc(lophoc);

            return empty;

        }

        public Empty UpdateLopHoc(LopHocGrpc request, CallContext context = default)
        {
            Empty empty = new Empty();
            LopHoc lophoc = lopHocMapper.ClassGrpcToClass(request);
             _lopHocRepository.UpdateLopHoc(lophoc);

            return empty;

        }

        public void DeleteLopHoc(LopHocGrpc request, CallContext context = default)
        {
            LopHoc lophoc = lopHocMapper.ClassGrpcToClass(request);
            _lopHocRepository.DeleteLopHoc(lophoc);
        }
    }
}

using Grpc.Net.Client;
using ProtoBuf.Grpc.Client;
using QuanLySinhVien.Service;
using Share;
using Azure.Core;
using QuanLySinhVien.Data;
using System.Security.Claims;
using System.ServiceModel;

namespace QuanLySinhVien.Repository
{
    public class LopHocRepository : ILopHocRepository
    {
        LopHocMapper lopHocMapper = new LopHocMapper();
        public ILopHocServiceGrpc getService()
        {

            //var handler = new Grpc.Net.Client.Web.GrpcWebHandler(Grpc.Net.Client.Web.GrpcWebMode.GrpcWeb, new HttpClientHandler());
            //var channel = Grpc.Net.Client.GrpcChannel.ForAddress("https://localhost:7029", new Grpc.Net.Client.GrpcChannelOptions() { HttpClient = new HttpClient(handler) });
            var httpHandler = new HttpClientHandler();
            httpHandler.ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator;
            var channel = GrpcChannel.ForAddress($"https://localhost:7029", new GrpcChannelOptions { HttpHandler = httpHandler });
            return channel.CreateGrpcService<ILopHocServiceGrpc>();
        }

        public async Task<List<LopHoc>> GetListLopHoc()
        {
            try
            {
                List<LopHoc> lh = new List<LopHoc>();
                Empty empty = new Empty();
               
                var client = getService();
                var list =  client.GetListLopHoc(empty);
                foreach (var item in list.List)
                {
                    LopHoc _lopHoc = lopHocMapper.ClassGrpcToClass(item);
                    lh.Add(_lopHoc);
                }
                return lh;

            }
            catch (Exception e)
            {

                throw e;
            }

        }

        public async Task<LopHoc> GetLopHocByID(int id)
        {
            try
            {
                LopHoc lh = new LopHoc();

                var client = getService();
                var lh1 =  client.GetLopHocById(id.ToString());
                lh = lopHocMapper.ClassGrpcToClass(lh1);
                return lh;

            }
            catch (Exception e)
            {

                throw e;
            }
        }

        
        public async Task<bool> AddLopHoc(LopHoc lophoc)
        {
            try
            {
                LopHocGrpc lophocGrpc = lopHocMapper.ClassToClassGrpc(lophoc);
                var client = getService();
                client.AddLopHoc(lophocGrpc);

                return true;
            }
            catch (Exception )
            {
                return false;
            }
           
        }

        public async Task<bool> UpdateLopHoc(LopHoc lophoc)
        {
            try
            {
                LopHocGrpc lophocGrpc = lopHocMapper.ClassToClassGrpc(lophoc);
                var client = getService();
                client.UpdateLopHoc(lophocGrpc);

                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }
        public void DeleteLopHoc(LopHoc lophoc)
        {
            LopHocGrpc lopHocGrpc = lopHocMapper.ClassToClassGrpc(lophoc);
            var client = getService();
            client.DeleteLopHoc(lopHocGrpc);
        }

    }
}

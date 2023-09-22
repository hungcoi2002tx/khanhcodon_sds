using ProtoBuf.Grpc;
using System.Runtime.Serialization;
using System.ServiceModel;

namespace Share
{
    [DataContract]
    public class Empty
    {

    }

    [DataContract]
    public class LopHocGrpc
    {
        [DataMember(Order = 1)]
        public string ClassId { get; set; }
        [DataMember(Order = 2)]
        public string ClassName { get; set; }
        [DataMember(Order = 3)]
        public string ObjectName { get; set; }
        [DataMember(Order = 4)]
        public virtual bool Status { get; set; }
    }
    [DataContract]
    public class ListLH
    {
        [DataMember(Order = 1)]
        public List<LopHocGrpc> List = new List<LopHocGrpc>();
    }

    [ServiceContract]
    public interface ILopHocServiceGrpc
    {

        [OperationContract]
        ListLH GetListLopHoc(Empty request, CallContext context = default);
        [OperationContract]
        Empty UpdateLopHoc(LopHocGrpc lh, CallContext context = default);
        [OperationContract]
        LopHocGrpc GetLopHocById(string id, CallContext context = default);
        [OperationContract]
        Empty AddLopHoc(LopHocGrpc lh, CallContext context = default);
        [OperationContract]
        void DeleteLopHoc(LopHocGrpc lophoc, CallContext context = default);
    }
}
using AntDesign;
using Microsoft.AspNetCore.Components;
using QuanLySinhVien.Data;
using QuanLySinhVien.Service;
using System.Security.Cryptography;

namespace QuanLySinhVien.Pages
{
    public partial class EditLopHoc : ComponentBase
    {
        [Inject] ILopHocService lopHocService { get; set; }
        [Inject] NotificationService _notice { get; set; }
        [Parameter] public EventCallback Cancel { get; set; }
        [Parameter] public EventCallback<LopHoc> ValueChange { get; set; }
        LopHocEditModel EditModel { get; set; } = new LopHocEditModel();

        protected override void OnInitialized() { }

        public async Task UpdateLopHoc()
        {
            LopHoc lophoc = new LopHoc();

            lophoc.ClassId = EditModel.ClassId;
            lophoc.ClassName = EditModel.ClassName;
            lophoc.ObjectName = EditModel.ObjectName;
            lophoc.Status = EditModel.Status;

            if ((EditModel.ClassId == null || EditModel.ClassId == 0) & EditModel.ClassName != null & EditModel.ObjectName != null)
            {
                Random rnd = new Random();
                lophoc.ClassId = rnd.Next(500);
                lophoc.ClassName = EditModel.ClassName;
                lophoc.ObjectName = EditModel.ObjectName;
                lophoc.Status = EditModel.Status;
                lopHocService.AddLopHoc(lophoc);
                Navigation.NavigateTo("lophoclist");
                await ValueChange.InvokeAsync(lophoc);
                await NoticeWithIcon(NotificationType.Success);

            }
            else if (EditModel.ClassId != null & EditModel.ClassId != 0 & EditModel.ClassName != null & EditModel.ObjectName != null)
            {

                lopHocService.UpdateLopHoc(lophoc);
                Navigation.NavigateTo("lophoclist");
                await ValueChange.InvokeAsync(lophoc);
                await NoticeWithIcon(NotificationType.Success);

            }
            else if (EditModel.ClassId == null & (EditModel.ClassName == null || EditModel.ObjectName == null))
            {
                await NoticeWithIcon1(NotificationType.Error);
            }
            else
            {

                LoadData(lophoc);
                await NoticeWithIcon1(NotificationType.Error);
            }


        }

        public void LoadData(LopHoc lopHoc)
        {
           
            try
            {
                EditModel.ClassId = lopHoc.ClassId;
                EditModel.ClassName = lopHoc.ClassName;
                EditModel.ObjectName = lopHoc.ObjectName;
                EditModel.Status = lopHoc.Status;

                StateHasChanged();
            }
            catch (Exception)
            {

                throw;
            }

        }

        public async Task NoticeWithIcon(NotificationType type)
        {


            try
            {
                await _notice.Open(new NotificationConfig()
                {
                    Message = "Thành công",
                    Placement = NotificationPlacement.TopRight,
                    NotificationType = type
                });
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task NoticeWithIcon1(NotificationType type)
        {
            try
            {
                await _notice.Error(new NotificationConfig()
                {
                    Message = "Thất bại",
                    Placement = NotificationPlacement.TopRight,
                });
            }
            catch (Exception)
            {

                throw;
            }

        }
    }
}

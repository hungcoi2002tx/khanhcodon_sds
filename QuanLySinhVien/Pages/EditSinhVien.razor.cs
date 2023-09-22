
using QuanLySinhVien.Data;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using AntDesign.Core.Extensions;
using System.Configuration;
using FluentNHibernate.Conventions.Inspections;
using System.Security.Cryptography;
using Microsoft.VisualBasic;
using AntDesign;
using Microsoft.JSInterop;
using System;
using QuanLySinhVien.Service;

namespace QuanLySinhVien.Pages
{

    public partial class EditSinhVien : ComponentBase
    {
        [Inject] ISinhVienService sinhVienService { get; set; }
        [Inject] NotificationService _notice { get; set; }
        [Parameter] public EventCallback Cancel { get; set; }
        [Parameter] public EventCallback<SinhVien> ValueChange { get; set; }
        SinhVienEditModel EditModel { get; set; } = new SinhVienEditModel();

        protected override void OnInitialized() { }
    
        public async Task UpdateSinhVien()
        {
            SinhVien sinhvien = new SinhVien();

            sinhvien.Guid = EditModel.Guid;
            sinhvien.Name = EditModel.Name;
            sinhvien.Birth = EditModel.Birth;
            sinhvien.Class = EditModel.Class;
            sinhvien.Score = EditModel.Score;

            if (EditModel.Guid == null & EditModel.Birth != null & EditModel.Name != null & EditModel.Score != 0 & EditModel.Score <= 4)
            {
               
                    sinhvien.Guid = Guid.NewGuid();
                    sinhvien.Name = EditModel.Name;
                    sinhvien.Birth = EditModel.Birth;
                    sinhvien.Class = EditModel.Class;
                    sinhvien.Score = EditModel.Score;
                    await  sinhVienService.AddSinhVien(sinhvien);
                Navigation.NavigateTo("sinhvienlist");
                await ValueChange.InvokeAsync(sinhvien);
                await NoticeWithIcon(NotificationType.Success);

            }
            else if(EditModel.Guid != null & EditModel.Birth != null & EditModel.Name != null & EditModel.Score != 0 & EditModel.Score <= 4)
            {
                    
                await   sinhVienService.UpdateSinhVien(sinhvien);
                Navigation.NavigateTo("sinhvienlist");
                await ValueChange.InvokeAsync(sinhvien);
                await NoticeWithIcon(NotificationType.Success);

            }
            else if(EditModel.Guid == null & (EditModel.Birth == null || EditModel.Name == null || EditModel.Score == 0 || EditModel.Score <= 4))
            {
                await NoticeWithIcon1(NotificationType.Error);
            }
            else
            {
                
                LoadData(sinhvien);
                  await NoticeWithIcon1(NotificationType.Error);
            }
            
       
        }

        public void LoadData(SinhVien sinhvien)
        {
            try
            {
                EditModel.Guid = sinhvien.Guid;
                EditModel.Name = sinhvien.Name;
                EditModel.Birth = sinhvien.Birth;
                EditModel.Class = sinhvien.Class;
                EditModel.Score = sinhvien.Score;

                StateHasChanged();
            }
            catch (Exception ex)
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

        public async Task  NoticeWithIcon1(NotificationType type)
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


using AntDesign;
using IronXL;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using OfficeOpenXml.Style;
using OfficeOpenXml;
using QuanLySinhVien.Data;
using QuanLySinhVien.Service;
using System.Data;

namespace QuanLySinhVien.Pages
{
    public partial class LopHocList
    {
        [Inject] ILopHocService lopHocService { get; set; }

        [Inject] IJSRuntime JSRuntime { get; set; }

        List<LopHoc> LopHocs { get; set; }
        bool visible = false;
        List<LopHocViewModel> LopHocViewModels { get; set; } = new List<LopHocViewModel>();

        // Create a DataTable
        private DataTable displayDataTable = new DataTable();
        LopHocPaging lopHocPaging { get; set; }
        EditLopHoc editLopHoc;

        protected override async Task OnInitializedAsync()
        {
            lopHocPaging = new LopHocPaging();
            LopHocViewModels = new List<LopHocViewModel>();
            lopHocPaging.PageIndex = 1;
            lopHocPaging.PageSize = 3;
            LopHocs = new();

            await LoadAsync();
        }

        public async Task LoadAsync()
        {
            try
            {
                LopHocViewModels.Clear();
                LopHocs = await lopHocService.GetListLopHoc();
                LopHocViewModels = GetViewModels(LopHocs);
                StateHasChanged();
            }
            catch (Exception ex)
            {

                throw;
            }
            
        }


        List<LopHocViewModel> GetViewModels(List<LopHoc> datas)
        {
            var models = new List<LopHocViewModel>();
            LopHocViewModel model;
            int stt = 1;
            datas.ForEach(c =>
            {
                model = new LopHocViewModel();
                model.STT = stt;
                model.ClassId = c.ClassId;
                model.ClassName = c.ClassName;
                model.ObjectName = c.ObjectName;
                model.Status = c.Status;

                models.Add(model);
                stt++;
            });
            return models;
        }

        void Add()
        {
            var lopHocData = new LopHoc();
            ShowDetail(lopHocData);
        }
        void ShowDetail(LopHoc data)
        {
            editLopHoc.LoadData(data);
            visible = true;
        }
        void Edit(LopHocViewModel model)
        {
            var lopHocData = LopHocs.FirstOrDefault(c => c.ClassId == model.ClassId);
            ShowDetail(lopHocData);
        }

        async Task Save(LopHoc data)
        {
            if (data.ClassId == null)
            {
                var resultAdd = await lopHocService.AddLopHoc(data);
            }
            else
            {

            }
            await LoadAsync();
            visible = false;
        }

        async Task DeleteLopHocAsync(int id)
        {
            try
            {
                LopHoc lh = await lopHocService.GetLopHocByID(id);
                lopHocService.DeleteLopHoc(lh);
                await LoadAsync();
            }
            catch (Exception e)
            {
                throw e;
            }


        }

        async Task Update(LopHoc data)
        {
            if (data.ClassId == null)
            {
                var resultAdd = await lopHocService.UpdateLopHoc(data);

            }
            else
            {

            }
            await LoadAsync();
        }



        async Task PageIndexChangeAsync(PaginationEventArgs e)
        {
            try
            {
                lopHocPaging.PageIndex = e.Page;
                await LoadAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }
        async Task PageSizeChangeAsync(PaginationEventArgs e)
        {
            try
            {
                lopHocPaging.PageSize = e.PageSize;
                await LoadAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }


        void Close()
        {
            try
            {
                visible = false;
                StateHasChanged();
            }
            catch (Exception exx)
            {

                throw;
            }
        }
    }
}

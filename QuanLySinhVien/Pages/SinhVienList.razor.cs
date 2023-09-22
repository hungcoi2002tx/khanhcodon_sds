using AntDesign;
using AntDesign.TableModels;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using OfficeOpenXml.Style;
using OfficeOpenXml;
using QuanLySinhVien.Data;
using AntDesign.Charts;
using IronXL;
using Microsoft.AspNetCore.Components.Forms;
using System.Data;
using QuanLySinhVien.Service;

namespace QuanLySinhVien.Pages
{
    public partial class SinhVienList : ComponentBase
    {
        [Inject] ISinhVienService sinhVienService { get; set; }

        [Inject] IJSRuntime JSRuntime { get; set; }

        List<SinhVien> SinhViens { get; set; }
        bool visible = false;
        List<SinhVienViewModel> SinhVienViewModels { get; set; } = new List<SinhVienViewModel>();

        // Create a DataTable
        private DataTable displayDataTable = new DataTable();
        SinhVienPaging sinhVienPaging { get; set; }
        TaskSearchSinhVien TaskSearchSinhViens = new TaskSearchSinhVien();
        EditSinhVien editSinhVien;

        protected override async Task OnInitializedAsync()
        {
            sinhVienPaging = new SinhVienPaging();
            SinhVienViewModels = new List<SinhVienViewModel>();
            sinhVienPaging.PageIndex = 1;
            sinhVienPaging.PageSize = 3;
            SinhViens = new();

            await LoadAsync();
        }

        public async Task LoadAsync()
        {
            SinhVienViewModels.Clear();
            SinhViens = await sinhVienService.GetListSinhVien();
            SinhVienViewModels = GetViewModels(SinhViens);
            StateHasChanged();
        }

        public async Task Search()
        {
            var name = TaskSearchSinhViens.Name;
            SinhVienViewModels.Clear();
            SinhViens = await sinhVienService.TaskListSearchSV(name);
            SinhVienViewModels = GetViewModels(SinhViens);
            StateHasChanged();
        }

        List<SinhVienViewModel> GetViewModels(List<SinhVien> datas)
        {
            var models = new List<SinhVienViewModel>();
            SinhVienViewModel model;
            int stt = 1;
            datas.ForEach(c =>
            {
                model = new SinhVienViewModel();
                model.STT = stt;
                model.Guid = c.Guid;
                model.Name = c.Name;
                model.Birth = c.Birth;
                model.Class = c.Class;
                model.Score = c.Score;
                models.Add(model);
                stt++;
            });
            return models;
        }


        //public void GenerateExcel()
        //{
        //    sinhVienService.GenerateExcel(iJSRuntime);
        //}

        private async Task ExportExcel()
        {

            ExcelPackage package = new ExcelPackage();
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            SinhViens = await sinhVienService.GetListSinhVien();
            SinhVienViewModels = GetViewModels(SinhViens);
            // Thêm một trang tính mới vào bảng tính
            ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("Danh sách sinh viên");



            // Thêm dữ liệu vào trang tính
            #region Header Row
            worksheet.Cells[1, 1].Value = "STT";
            worksheet.Cells[1, 1].Style.Font.Size = 12;
            worksheet.Cells[1, 1].Style.Font.Bold = true;
            worksheet.Cells[1, 1].Style.Border.Top.Style = ExcelBorderStyle.Hair;

            worksheet.Cells[1, 2].Value = "Họ tên";
            worksheet.Cells[1, 2].Style.Font.Size = 12;
            worksheet.Cells[1, 2].Style.Font.Bold = true;
            worksheet.Cells[1, 2].Style.Border.Top.Style = ExcelBorderStyle.Hair;

            worksheet.Cells[1, 3].Value = "Ngày sinh";
            worksheet.Cells[1, 3].Style.Font.Size = 12;
            worksheet.Cells[1, 3].Style.Font.Bold = true;
            worksheet.Cells[1, 3].Style.Border.Top.Style = ExcelBorderStyle.Hair;

            worksheet.Cells[1, 4].Value = "Lớp học";
            worksheet.Cells[1, 4].Style.Font.Size = 12;
            worksheet.Cells[1, 4].Style.Font.Bold = true;
            worksheet.Cells[1, 4].Style.Border.Top.Style = ExcelBorderStyle.Hair;

            worksheet.Cells[1, 5].Value = "Điểm tích lũy";
            worksheet.Cells[1, 5].Style.Font.Size = 12;
            worksheet.Cells[1, 5].Style.Font.Bold = true;
            worksheet.Cells[1, 5].Style.Border.Top.Style = ExcelBorderStyle.Hair;

            #endregion

            #region Value Row

            foreach (var item in SinhVienViewModels)
            {
                worksheet.Cells[item.STT + 1, 1].Value = item.STT;
                worksheet.Cells[item.STT + 1, 1].Style.Font.Size = 12;
                worksheet.Cells[item.STT + 1, 1].Style.Font.Bold = true;
                worksheet.Cells[item.STT + 1, 1].Style.Border.Top.Style = ExcelBorderStyle.Hair;

                worksheet.Cells[item.STT + 1, 2].Value = item.Name;
                worksheet.Cells[item.STT + 1, 2].Style.Font.Size = 12;
                worksheet.Cells[item.STT + 1, 2].Style.Font.Bold = true;
                worksheet.Cells[item.STT + 1, 2].Style.Border.Top.Style = ExcelBorderStyle.Hair;

                worksheet.Cells[item.STT + 1, 3].Value = item.Birth.ToString();
                worksheet.Cells[item.STT + 1, 3].Style.Font.Size = 12;
                worksheet.Cells[item.STT + 1, 3].Style.Font.Bold = true;
                worksheet.Cells[item.STT + 1, 3].Style.Border.Top.Style = ExcelBorderStyle.Hair;

                worksheet.Cells[item.STT + 1, 4].Value = item.Class;
                worksheet.Cells[item.STT + 1, 4].Style.Font.Size = 12;
                worksheet.Cells[item.STT + 1, 4].Style.Font.Bold = true;
                worksheet.Cells[item.STT + 1, 4].Style.Border.Top.Style = ExcelBorderStyle.Hair;

                worksheet.Cells[item.STT + 1, 5].Value = item.Score;
                worksheet.Cells[item.STT + 1, 5].Style.Font.Size = 12;
                worksheet.Cells[item.STT + 1, 5].Style.Font.Bold = true;
                worksheet.Cells[item.STT + 1, 5].Style.Border.Top.Style = ExcelBorderStyle.Hair;

            }

            #endregion


            // Tạo ra file Excel từ bảng tính

            var fileStream = await package.GetAsByteArrayAsync();
            var fileName = "SinhVien.xlsx";
            var base64 = Convert.ToBase64String(fileStream);

            await JSRuntime.InvokeVoidAsync("saveAsFile", fileName, base64);
        }

        
        public async Task OpenExcelFileFromDisk(InputFileChangeEventArgs e)
        {
            IronXL.License.LicenseKey = "PASTE TRIAL OR LICENSE KEY";
            // Open the File to a MemoryStream object
            MemoryStream ms = new MemoryStream();
            await e.File.OpenReadStream().CopyToAsync(ms);
            ms.Position = 0;
            // Define variables for IronXL
            WorkBook loadedWorkBook = WorkBook.FromStream(ms);
            WorkSheet loadedWorkSheet = loadedWorkBook.DefaultWorkSheet; // Or use .GetWorkSheet()
                                                                         // Add header Columns to the DataTable
            RangeRow headerRow = loadedWorkSheet.GetRow(0);

            for (int col = 0; col < loadedWorkSheet.ColumnCount; col++)
            {
                displayDataTable.Columns.Add(headerRow.ElementAt(col).ToString());
            }
            // Populate the DataTable
            for (int row = 1; row < loadedWorkSheet.RowCount; row++)
            {
                IEnumerable<string> excelRow = loadedWorkSheet.GetRow(row).ToArray().Select(c => c.ToString());
                displayDataTable.Rows.Add(excelRow.ToArray());
            }
            List<SinhVien> sinhvien = await sinhVienService.GetListSinhVien();

            List<SinhVien> sinhvienList = new List<SinhVien>();
            //sinhvienList.Clear();
            
                sinhvienList = (from DataRow displayDataTable in displayDataTable.Rows
                                select new SinhVien()
                                {
                                    Guid = Guid.NewGuid(),
                                    Name = displayDataTable["Họ Tên"].ToString(),
                                    Birth = Convert.ToDateTime(displayDataTable["Ngày sinh"]),
                                    Class = displayDataTable["Lớp học"].ToString(),
                                    Score = (float)Convert.ToDouble(displayDataTable["Điểm tích lũy"])
                                }).ToList();
           
            foreach (var svl in sinhvienList)
            {
                foreach (var sv in sinhvien)
                {
                    if (svl.Name != sv.Name && svl.Birth != sv.Birth && svl.Name != null && svl.Birth != null  )
                    {
                        sv.Guid = Guid.NewGuid();
                        sv.Name = svl.Name;
                        sv.Birth = svl.Birth;
                        sv.Class = svl.Class;
                        sv.Score = svl.Score;
                        await sinhVienService.AddSinhVien(sv);
                    }
                        
                    
                }
            }

            await LoadAsync();

        }

        async Task PageIndexChangeAsync(PaginationEventArgs e)
        {
            try
            {
                sinhVienPaging.PageIndex = e.Page;
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
                sinhVienPaging.PageSize = e.PageSize;
                await LoadAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        void Add()
        {
            var sinhVienData = new SinhVien();
            ShowDetail(sinhVienData);
        }
        void ShowDetail(SinhVien data)
        {
            editSinhVien.LoadData(data);
            visible = true;
        }
        void Edit(SinhVienViewModel model)
        {
            var sinhVienData = SinhViens.FirstOrDefault(c => c.Guid == model.Guid);
            ShowDetail(sinhVienData);
        }

        async Task Save(SinhVien data)
        {
            if (data.Guid == null)
            {
                var resultAdd = await sinhVienService.AddSinhVien(data);
            }
            else
            {

            }
            await LoadAsync();
            visible = false;
        }

        async Task DeleteSinhVienAsync(Guid? id)
        {
            try
            {
                SinhVien sv = await sinhVienService.GetSinhVienByID(id);
                sinhVienService.DeleteSinhVien(sv);
                await LoadAsync();
            }
            catch (Exception e)
            {
                throw e;
            }


        }

        async Task Update(SinhVien data)
        {
            if (data.Guid == null)
            {
                var resultAdd = await sinhVienService.UpdateSinhVien(data);

            }
            else
            {

            }
            await LoadAsync();
        }

        async Task Search(SinhVien data)
        {
            if (data.Guid == null)
            {
                var resultAdd = await sinhVienService.AddSinhVien(data);
            }
            else
            {

            }
            await LoadAsync();

        }

        public class TaskSearchSinhVien
        {
            public string Name { get; set; }
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

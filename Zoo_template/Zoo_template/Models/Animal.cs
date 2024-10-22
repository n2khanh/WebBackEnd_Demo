namespace Zoo_template.Models
{
    public class Animal
    {
        public int MaThu { get; set; }            // Mã thú (Khóa chính)
        public string TenGoi { get; set; }         // Tên gọi
        public string Loai { get; set; }           // Loài
        public string SachDo { get; set; }         // Sách đỏ
        public string TenKhoaHoc { get; set; }     // Tên khoa học
        public string TenTV { get; set; }          // Tên TV
        public string KieuSinh { get; set; }       // Kiểu sinh
        public string GioiTinh { get; set; }       // Giới tính
        public DateTime NgayVao { get; set; }      // Ngày vào
        public string NguonGoc { get; set; }       // Nguồn gốc
        public string DacDiem { get; set; }        // Đặc điểm
        public DateTime? NgaySinh { get; set; }    // Ngày sinh (nullable)
        public DateTime? NgayMat { get; set; }     // Ngày mất (nullable)
        public string HinhAnh { get; set; }        // Hình ảnh
        public int? TuoiTho { get; set; }
    }
}

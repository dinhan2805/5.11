using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using static WindowsFormsApp2.Program;

namespace WindowsFormsApp2
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
        public class KhachHang
        {
            public int MaKhachHang { get; set; }
            public string TenKhachHang { get; set; }
            public string SoDienThoai { get; set; }
            public string DiaChi { get; set; }
        }

        public class DichVu
        {
            public int MaDichVu { get; set; }
            public string TenDichVu { get; set; }
            public decimal GiaTien { get; set; }
        }

        public class HoaDon
        {
            public int MaHoaDon { get; set; }
            public KhachHang KhachHang { get; set; }
            public List<DichVu> DichVus { get; set; }
            public decimal TongTien => DichVus.Sum(dv => dv.GiaTien);

            public HoaDon()
            {
                DichVus = new List<DichVu>();
            }
        }
    }
}

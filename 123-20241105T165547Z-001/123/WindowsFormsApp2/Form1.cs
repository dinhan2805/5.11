using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static WindowsFormsApp2.Program;

namespace WindowsFormsApp2
{
    public partial class Form1 : Form
    {
        private List<KhachHang> danhSachKhachHang = new List<KhachHang>();
        private List<DichVu> danhSachDichVu = new List<DichVu>();
        private List<HoaDon> danhSachHoaDon = new List<HoaDon>();
        public Form1()
        {
            InitializeComponent();
            LoadDichVu();
            LoadKhachHang();
        }
        // Tải danh sách khách hàng vào DataGridView
        private void LoadDichVu()
        {
            // Giả sử chúng ta thêm một vài dịch vụ mẫu
            danhSachDichVu.Add(new DichVu { MaDichVu = 1, TenDichVu = "Dịch vụ 1", GiaTien = 100000 });
            danhSachDichVu.Add(new DichVu { MaDichVu = 2, TenDichVu = "Dịch vụ 2", GiaTien = 200000 });
            danhSachDichVu.Add(new DichVu { MaDichVu = 3, TenDichVu = "Dịch vụ 3", GiaTien = 300000 });

            comboBox1.DataSource = danhSachDichVu;
            comboBox1.DisplayMember = "TenDichVu";
            comboBox1.ValueMember = "MaDichVu";
        }

        // Tải danh sách dịch vụ vào ListBox
        private void LoadKhachHang()
        {
            danhSachKhachHang.Add(new KhachHang { MaKhachHang = 1, TenKhachHang = "Nguyễn Văn A", SoDienThoai = "0123456789", DiaChi = "Hà Nội" });
            danhSachKhachHang.Add(new KhachHang { MaKhachHang = 2, TenKhachHang = "Trần Thị B", SoDienThoai = "0987654321", DiaChi = "Hồ Chí Minh" });

            dataGridView1.DataSource = new BindingList<KhachHang>(danhSachKhachHang);
        }
        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            var kh = new KhachHang
            {
                MaKhachHang = danhSachKhachHang.Count + 1,
                TenKhachHang = textBox1.Text,
                SoDienThoai = textBox2.Text,
                DiaChi = textBox3.Text
            };
            danhSachKhachHang.Add(kh);
            dataGridView1.DataSource = new BindingList<KhachHang>(danhSachKhachHang);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var selectedKhachHang = (KhachHang)dataGridView1.CurrentRow?.DataBoundItem;
            if (selectedKhachHang != null)
            {
                // Điền thông tin khách hàng vào các TextBox để chỉnh sửa
                textBox1.Text = selectedKhachHang.TenKhachHang;
                textBox2.Text = selectedKhachHang.SoDienThoai;
                textBox3.Text = selectedKhachHang.DiaChi;

                // Khi người dùng nhấn lưu, sẽ cập nhật thông tin khách hàng
                button2.Enabled = true;  // Hiển thị nút lưu thay đổi
                button2.Click += (s, ev) =>
                {
                    selectedKhachHang.TenKhachHang = textBox1.Text;
                    selectedKhachHang.SoDienThoai = textBox2.Text;
                    selectedKhachHang.DiaChi = textBox3.Text;

                    // Cập nhật lại DataGridView
                    dataGridView1.DataSource = null;
                    dataGridView1.DataSource = new BindingList<KhachHang>(danhSachKhachHang);

                    // Tắt nút lưu sau khi cập nhật
                    button2.Enabled = false;
                };
            }
            else
            {
                MessageBox.Show("Vui lòng chọn khách hàng cần chỉnh sửa.");
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            var selectedKhachHang = (KhachHang)dataGridView1.CurrentRow?.DataBoundItem;
            if (selectedKhachHang != null)
            {
                // Xóa khách hàng khỏi danh sách
                danhSachKhachHang.Remove(selectedKhachHang);

                // Cập nhật lại DataGridView
                dataGridView1.DataSource = null;
                dataGridView1.DataSource = new BindingList<KhachHang>(danhSachKhachHang);
            }
            else
            {
                MessageBox.Show("Vui lòng chọn khách hàng cần xóa.");
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            var selectedKhachHang = (KhachHang)dataGridView1.CurrentRow?.DataBoundItem;
            if (selectedKhachHang != null)
            {
                var hoaDon = new HoaDon
                {
                    MaHoaDon = danhSachHoaDon.Count + 1,
                    KhachHang = selectedKhachHang
                };

                var dichVuChon = (DichVu)comboBox1.SelectedItem;
                hoaDon.DichVus.Add(dichVuChon);

                danhSachHoaDon.Add(hoaDon);

                // Hiển thị hóa đơn trong ListBox
                listBox1.Items.Clear();
                listBox1.Items.Add($"Mã hóa đơn: {hoaDon.MaHoaDon}");
                listBox1.Items.Add($"Khách hàng: {hoaDon.KhachHang.TenKhachHang}");
                foreach (var dv in hoaDon.DichVus)
                {
                    listBox1.Items.Add($"- {dv.TenDichVu}: {dv.GiaTien:C}");
                }
                listBox1.Items.Add($"Tổng tiền: {hoaDon.TongTien:C}");
            }

        }
    }
    }

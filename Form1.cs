using LAB05_BUS;
using LAB05_DAL.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LAB05_GUI
{
    public partial class form1 : Form
    {
        private readonly StudentService studentService = new StudentService();
        private readonly FacultyService facultyService = new FacultyService();

        public form1()
        {
            InitializeComponent();
        }

        private string selectedImagePath = string.Empty;

        private void btnChonAnh_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp;*.gif";
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    selectedImagePath = openFileDialog.FileName;

                    if (pictureBox.Image != null)
                    {
                        pictureBox.Image.Dispose();
                        pictureBox.Image = null;
                    }

                    using (var fs = new FileStream(selectedImagePath, FileMode.Open, FileAccess.Read))
                    {
                        pictureBox.Image = Image.FromStream(fs);
                    }
                }
            }
        }


        public void LoadAvatar(string MaSV)
        {
            string folderPath = Path.Combine(Application.StartupPath, "Images");
            var sv = studentService.GetStudentById(MaSV);

            if (sv == null || string.IsNullOrEmpty(sv.Avt))
            {
                pictureBox.Image = null;
                return;
            }

            string avatarPath = Path.Combine(folderPath, sv.Avt);
            if (File.Exists(avatarPath))
            {
                if (pictureBox.Image != null)
                {
                    pictureBox.Image.Dispose();
                }
                pictureBox.Image = Image.FromFile(avatarPath);
            }
            else
            {
                pictureBox.Image = null;
            }
        }

        public string SaveAvatar(string sourceFilePath, string MaSV)
        {
            try
            {
                if (!File.Exists(sourceFilePath))
                {
                    throw new FileNotFoundException("Tệp nguồn không tồn tại.");
                }

                string folderPath = Path.Combine(Application.StartupPath, "Images");
                if (!Directory.Exists(folderPath))
                {
                    Directory.CreateDirectory(folderPath);
                }

                string fileExtension = Path.GetExtension(sourceFilePath).ToLower();

                string fileName = $"{MaSV}{fileExtension}";
                string targetFilePath = Path.Combine(folderPath, fileName);

                File.Copy(sourceFilePath, targetFilePath, true);

                return fileName;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi lưu ảnh đại diện: {ex.Message}");
                return null;
            }
        }


        private void btnThem_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtMaSV.Text) || string.IsNullOrWhiteSpace(txtHoTen.Text) ||
                    string.IsNullOrWhiteSpace(txtDTB.Text) || cboKhoa.SelectedItem == null)
                {
                    MessageBox.Show("Vui lòng điền đầy đủ thông tin sinh viên.");
                    return;
                }

                if (!double.TryParse(txtDTB.Text, out double dtb) || dtb < 0 || dtb > 10)
                {
                    MessageBox.Show("Điểm trung bình phải là số từ 0 đến 10.");
                    return;
                }

                var newStudent = new SinhVien
                {
                    MaSV = txtMaSV.Text.Trim(),
                    TenSV = txtHoTen.Text.Trim(),
                    MaKhoa = ((Khoa)cboKhoa.SelectedItem).MaKhoa,
                    DTB = dtb
                };

                if (!string.IsNullOrEmpty(selectedImagePath))
                {
                    string savedFileName = SaveAvatar(selectedImagePath, newStudent.MaSV);
                    newStudent.Avt = savedFileName;
                }
                bool isAdded = studentService.AddStudent(newStudent);
                if (isAdded)
                {
                    MessageBox.Show("Thêm sinh viên thành công!");
                    LoadData(sender, e);
                    ClearForm();
                }
                else
                {
                    MessageBox.Show("Mã sinh viên đã tồn tại. Vui lòng nhập mã khác.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi thêm sinh viên: {ex.Message}");
            }
        }


        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                var listFaculties = facultyService.GetAll();
                var listStudents = studentService.GetAll();


                FillFacultyComboBox(listFaculties);
                LoadData(sender, e);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải dữ liệu: {ex.Message}");
            }
        }

        private void FillFacultyComboBox(List<Khoa> faculties)
        {
            if (faculties != null)
            {
                faculties.Insert(0, new Khoa());
                this.cboKhoa.DataSource = faculties;
                this.cboKhoa.DisplayMember = "TenKhoa";
                this.cboKhoa.ValueMember = "MaKhoa";
            }
            else
            {
                MessageBox.Show("Danh sách khoa rỗng!");
            }
        }

        private void LoadData(object sender, EventArgs e)
        {
            try
            {
                var listStudents = studentService.GetAll();
                var listFaculties = facultyService.GetAll();

                if (!listStudents.Any())
                {
                    MessageBox.Show("Không có sinh viên nào trong cơ sở dữ liệu!");
                    return;
                }

                var query = from sv in listStudents
                            join khoa in listFaculties on sv.MaKhoa equals khoa.MaKhoa
                            select new
                            {
                                MaSV = sv.MaSV,
                                HoTen = sv.TenSV,
                                TenKhoa = khoa.TenKhoa,
                                DTB = sv.DTB,
                                ChuyenNganh = sv.ChuyenNganh != null ? sv.ChuyenNganh.TenChuyenNganh : ""
                            };

                dgvSinhVien.Columns.Clear();
                dgvSinhVien.AutoGenerateColumns = true;
                dgvSinhVien.DataSource = query.ToList();
                dgvSinhVien.Columns["MaSV"].Width = 50;
                dgvSinhVien.Columns["HoTen"].Width = 150;
                dgvSinhVien.Columns["TenKhoa"].Width = 150;
                dgvSinhVien.Columns["DTB"].Width = 50;
                dgvSinhVien.Columns["ChuyenNganh"].Width = 180;

            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi trong LoadData: {ex.Message}");
            }
        }
        private void ClearForm()
        {
            txtMaSV.Clear();
            txtHoTen.Clear();
            txtDTB.Clear();
            cboKhoa.SelectedIndex = 0;
            pictureBox.Image = null;
            selectedImagePath = string.Empty;
            txtMaSV.Focus();
        }

        private void dgvSinhVien_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                var selectedRow = dgvSinhVien.Rows[e.RowIndex];
                string maSV = selectedRow.Cells["MaSV"].Value.ToString();
                var sv = studentService.GetStudentById(maSV);
                if (sv != null)
                {
                    txtMaSV.Text = sv.MaSV;
                    txtHoTen.Text = sv.TenSV;
                    txtDTB.Text = sv.DTB.ToString();
                    cboKhoa.SelectedValue = sv.MaKhoa;
                    LoadAvatar(sv.MaSV);
                }
            }


        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtMaSV.Text) || string.IsNullOrWhiteSpace(txtHoTen.Text) ||
                    string.IsNullOrWhiteSpace(txtDTB.Text) || cboKhoa.SelectedItem == null)
                {
                    MessageBox.Show("Vui lòng điền đầy đủ thông tin sinh viên.");
                    return;
                }

                if (!double.TryParse(txtDTB.Text, out double dtb) || dtb < 0 || dtb > 10)
                {
                    MessageBox.Show("Điểm trung bình phải là số từ 0 đến 10.");
                    return;
                }
                var existingStudent = studentService.GetStudentById(txtMaSV.Text.Trim());
                if (existingStudent == null)
                {
                    MessageBox.Show("Sinh viên không tồn tại. Vui lòng kiểm tra lại mã sinh viên.");
                    return;
                }

                existingStudent.TenSV = txtHoTen.Text.Trim();
                existingStudent.MaKhoa = ((Khoa)cboKhoa.SelectedItem).MaKhoa;
                existingStudent.DTB = dtb;

                if (!string.IsNullOrEmpty(selectedImagePath))
                {
                    if (pictureBox.Image != null)
                    {
                        pictureBox.Image.Dispose();
                        pictureBox.Image = null;
                    }

                    string imagesFolder = Path.Combine(Application.StartupPath, "Images");
                    if (!string.IsNullOrEmpty(existingStudent.Avt))
                    {
                        string oldFilePath = Path.Combine(imagesFolder, existingStudent.Avt);
                        if (File.Exists(oldFilePath))
                        {
                            File.Delete(oldFilePath);
                        }
                    }

                    string savedFileName = SaveAvatar(selectedImagePath, existingStudent.MaSV);
                    existingStudent.Avt = savedFileName;
                }

                bool isUpdated = studentService.UpdateStudent(existingStudent);
                if (isUpdated)
                {
                    MessageBox.Show("Cập nhật sinh viên thành công!");
                    LoadData(sender, e);
                    ClearForm();
                }
                else
                {
                    MessageBox.Show("Cập nhật sinh viên thất bại. Vui lòng thử lại.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi cập nhật sinh viên: {ex.Message}");
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            try
            {
                string maSV = string.Empty;

                if (dgvSinhVien.CurrentRow != null)
                {
                    maSV = dgvSinhVien.CurrentRow.Cells["MaSV"].Value.ToString();
                }
                else if (!string.IsNullOrWhiteSpace(txtMaSV.Text))
                {
                    maSV = txtMaSV.Text.Trim();
                }
                else
                {
                    MessageBox.Show("Vui lòng chọn sinh viên hoặc nhập mã sinh viên cần xóa.");
                    return;
                }

                DialogResult result = MessageBox.Show(
                    $"Bạn có chắc chắn muốn xóa sinh viên có mã {maSV} không?",
                    "Xác nhận xóa",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning);

                if (result == DialogResult.No)
                {
                    return;
                }

                var sv = studentService.GetStudentById(maSV);
                if (sv == null)
                {
                    MessageBox.Show("Không tìm thấy sinh viên trong cơ sở dữ liệu.");
                    return;
                }

                if (!string.IsNullOrEmpty(sv.Avt))
                {
                    string imagePath = Path.Combine(Application.StartupPath, "Images", sv.Avt);
                    if (File.Exists(imagePath))
                    {
                        try
                        {
                            File.Delete(imagePath);
                        }
                        catch (Exception)
                        {
                        }
                    }
                }

                bool isDeleted = studentService.DeleteStudent(maSV);

                if (isDeleted)
                {
                    MessageBox.Show("Xóa sinh viên thành công!");
                    LoadData(sender, e);
                    ClearForm();
                }
                else
                {
                    MessageBox.Show("Xóa sinh viên thất bại. Vui lòng thử lại.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi xóa sinh viên: {ex.Message}");
            }
        }

        private void cbDangKi_CheckedChanged(object sender, EventArgs e)
        {
            if (cbDangKi.Checked == true)
            {
                var listStudents = studentService.GetAllWithoutMajor();
                var listFaculties = facultyService.GetAll();
                if (!listStudents.Any())
                {
                    MessageBox.Show("Không có sinh viên nào trong cơ sở dữ liệu!");
                    return;
                }
                var query = from sv in listStudents
                            join khoa in listFaculties on sv.MaKhoa equals khoa.MaKhoa
                            select new
                            {
                                MaSV = sv.MaSV,
                                HoTen = sv.TenSV,
                                TenKhoa = khoa.TenKhoa,
                                DTB = sv.DTB,
                                ChuyenNganh = sv.ChuyenNganh != null ? sv.ChuyenNganh.TenChuyenNganh : ""
                            };
                dgvSinhVien.Columns.Clear();
                dgvSinhVien.AutoGenerateColumns = true;
                dgvSinhVien.DataSource = query.ToList();
                dgvSinhVien.Columns["MaSV"].Width = 50;
                dgvSinhVien.Columns["HoTen"].Width = 150;
                dgvSinhVien.Columns["TenKhoa"].Width = 150;
                dgvSinhVien.Columns["DTB"].Width = 50;
                dgvSinhVien.Columns["ChuyenNganh"].Width = 180;
            }
            else
            {
                LoadData(sender, e);

            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            frmDangKi frmDangKi = new frmDangKi();
            frmDangKi.ShowDialog();


        }
    }
}
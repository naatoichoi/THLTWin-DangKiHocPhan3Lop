using LAB05_BUS;
using LAB05_DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace LAB05_GUI
{
    public partial class frmDangKi : Form
    {
        private readonly FacultyService facultyService = new FacultyService();
        private readonly StudentService studentService = new StudentService();
        private readonly MajorService majorService = new MajorService();

        public frmDangKi()
        {
            InitializeComponent();
        }

        private void frmDangKi_Load(object sender, EventArgs e)
        {
            var faculties = facultyService.GetAll();
            FillFacultyComboBox(faculties);
        }

        private void FillFacultyComboBox(List<Khoa> faculties)
        {
            if (faculties == null || faculties.Count == 0)
            {
                MessageBox.Show("Danh sách khoa rỗng!");
                return;
            }

            cboKhoa.DataSource = faculties;
            cboKhoa.DisplayMember = "TenKhoa";
            cboKhoa.ValueMember = "MaKhoa";
        }

        private void FillMajorComboBox(List<ChuyenNganh> majors)
        {
            if (majors == null)
            {
                cboChuyenNganh.DataSource = null;
                return;
            }

            cboChuyenNganh.DataSource = majors;
            cboChuyenNganh.DisplayMember = "TenChuyenNganh"; 
            cboChuyenNganh.ValueMember = "MaChuyenNganh";   
        }

        private void cboKhoa_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboKhoa.SelectedValue == null)
                return;

            string maKhoa = cboKhoa.SelectedValue.ToString();

            var majors = majorService.GetByFacultyId(maKhoa);
            FillMajorComboBox(majors);

            var listStudents = studentService.GetStudentsWithoutMajorByFaculty(maKhoa);

            dgvDangKi.Columns.Clear();
            dgvDangKi.AutoGenerateColumns = false;
            dgvDangKi.AllowUserToAddRows = false;

            DataGridViewCheckBoxColumn chk = new DataGridViewCheckBoxColumn
            {
                HeaderText = "Chọn",
                Name = "Chon",
                Width = 50
            };
            dgvDangKi.Columns.Add(chk);

            dgvDangKi.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "MaSV",
                HeaderText = "Mã SV",
                Name = "MaSV",
                Width = 80
            });

            dgvDangKi.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "TenSV",
                HeaderText = "Họ tên",
                Name = "TenSV",
                Width = 150
            });

            dgvDangKi.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "DTB",
                HeaderText = "Điểm TB",
                Name = "DTB",
                Width = 80
            });

            dgvDangKi.DataSource = listStudents;
            if (dgvDangKi.Columns["Chon"] != null)
                dgvDangKi.Columns["Chon"].Width = 100;

            if (dgvDangKi.Columns["MaSV"] != null)
                dgvDangKi.Columns["MaSV"].Width = 200;

            if (dgvDangKi.Columns["TenSV"] != null)
                dgvDangKi.Columns["TenSV"].Width = 300;

            if (dgvDangKi.Columns["DTB"] != null)
                dgvDangKi.Columns["DTB"].Width = 100;
        }

        private void btnDangKi_Click(object sender, EventArgs e)
        {
            if (cboChuyenNganh.SelectedValue == null)
            {
                MessageBox.Show("Vui lòng chọn chuyên ngành!");
                return;
            }

            string maChuyenNganh = cboChuyenNganh.SelectedValue.ToString(); 
            List<string> danhSachChon = new List<string>();

            foreach (DataGridViewRow row in dgvDangKi.Rows)
            {
                bool isChecked = Convert.ToBoolean(row.Cells["Chon"].Value ?? false);
                if (isChecked)
                {
                    string maSV = row.Cells["MaSV"].Value.ToString();
                    danhSachChon.Add(maSV);
                }
            }

            if (danhSachChon.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn ít nhất một sinh viên để đăng ký!");
                return;
            }

            foreach (string maSV in danhSachChon)
            {
                var sv = studentService.GetStudentById(maSV);  
                if (sv != null)
                {
                    sv.MaChuyenNganh = int.Parse(maChuyenNganh);  
                    studentService.UpdateStudent(sv);  
                }
            }

            MessageBox.Show("Đăng ký chuyên ngành thành công!");
            cboKhoa_SelectedIndexChanged(sender, e);
        }
    }
}

using LAB05_DAL.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Data.Entity;

namespace LAB05_BUS
{
    public class StudentService
    {
        public List<SinhVien> GetAll()
        {
            using (var context = new StudentModel())
            {
                return context.SinhVien.Include(sv => sv.ChuyenNganh).ToList();  
            }
        }

        public List<SinhVien> GetStudentsWithoutMajorByFaculty(string maKhoa)
        {
            using (var context = new StudentModel())
            {
                return context.SinhVien
                    .Where(sv => sv.MaChuyenNganh == null && sv.MaKhoa.ToString() == maKhoa)
                    .ToList();
            }
        }

        public SinhVien GetStudentById(string maSV)
        {
            using (var context = new StudentModel())
            {
                return context.SinhVien.Include(sv => sv.ChuyenNganh).FirstOrDefault(sv => sv.MaSV == maSV);
            }
        }

        public bool UpdateStudent(SinhVien sv)
        {
            try
            {
                using (var context = new StudentModel())
                {
                    var existing = context.SinhVien.Include(x => x.ChuyenNganh).FirstOrDefault(x => x.MaSV == sv.MaSV);
                    if (existing == null)
                        return false;
                    existing.TenSV = sv.TenSV;
                    existing.DTB = sv.DTB;
                    existing.MaKhoa = sv.MaKhoa;
                    existing.MaChuyenNganh = sv.MaChuyenNganh;  
                    existing.Avt = sv.Avt;
                    context.SaveChanges();
                    return true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi cập nhật sinh viên: " + ex.Message);
                return false;
            }
        }

        public bool AddStudent(SinhVien sv)
        {
            try
            {
                using (var context = new StudentModel())
                {
                    var existing = context.SinhVien.FirstOrDefault(x => x.MaSV == sv.MaSV);
                    if (existing != null)
                        return false; 

                    context.SinhVien.Add(sv);
                    context.SaveChanges();
                    return true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi thêm sinh viên: " + ex.Message);
                return false;
            }
        }

        public bool DeleteStudent(string maSV)
        {
            try
            {
                using (var context = new StudentModel())
                {
                    var sv = context.SinhVien.FirstOrDefault(s => s.MaSV == maSV);
                    if (sv == null)
                        return false;

                    context.SinhVien.Remove(sv);
                    context.SaveChanges();
                    return true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi xóa sinh viên: " + ex.Message);
                return false;
            }
        }

        public List<SinhVien> GetAllWithoutMajor()
        {
            using (var context = new StudentModel())
            {
                return context.SinhVien
                    .Where(sv => sv.MaChuyenNganh == null)
                    .ToList();
            }
        }
    }
}

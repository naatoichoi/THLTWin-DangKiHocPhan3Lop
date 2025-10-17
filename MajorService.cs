using LAB05_DAL.Entities;
using System.Collections.Generic;
using System.Linq;

namespace LAB05_BUS
{
    public class MajorService
    {
        public List<ChuyenNganh> GetByFacultyId(string maKhoa)
        {
            using (StudentModel context = new StudentModel())
            {
                return context.ChuyenNganh.Where(m => m.MaKhoa.ToString() == maKhoa).ToList();

            }
        }
    }
}

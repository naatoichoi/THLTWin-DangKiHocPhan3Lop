using LAB05_DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LAB05_BUS
{
    public class FacultyService
    {
        public List<Khoa> GetAll()
        {
            StudentModel context = new StudentModel();
            return context.Khoa.ToList();
        }
    }
}

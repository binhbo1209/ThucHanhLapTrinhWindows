using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lap01_03
{
    public class Teacher : Person
    {
        public string ChuyenMon { get; set; }

        public Teacher(string maSo, string hoTen, string khoa, string chuyenMon)
            : base(maSo, hoTen, khoa)
        {
            ChuyenMon = chuyenMon;
        }

        public override string ToString()
        {
            return $"[Giang vien] Ma: {MaSo} | Ho va Ten: {HoTen} | Khoa: {Khoa} | Chuyen mon: {ChuyenMon}";
        }
    }
}

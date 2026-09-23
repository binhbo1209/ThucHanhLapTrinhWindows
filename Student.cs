using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lap01_03
{
    public class Student : Person
    {
        public double DiemTB { get; set; }
        public string? SoDienThoai { get; set; } 

        public Student(string maSo, string hoTen, string khoa, double diemTB, string? soDienThoai)
            : base(maSo, hoTen, khoa)
        {
            DiemTB = diemTB;
            SoDienThoai = soDienThoai;
        }

        public override string ToString()
        {
            string sdt = SoDienThoai ?? "Chua cap nhat";
            return $"[Sinh Vien] Ma: {MaSo} | Ho va Ten: {HoTen} | Khoa: {Khoa} | Diem TB: {DiemTB:0.0} | SDT: {sdt}";
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lap01_02
{
    public class Student
    {
        public string MaSo { get; set; }
        public string HoTen { get; set; }
        public string Khoa { get; set; }
        public double DiemTB { get; set; }
        public string? SoDienThoai { get; set; }
        public Student(string maSo, string hoTen, string khoa, double diemTB,
            string? soDienThoai)
        {
            MaSo = maSo;
            HoTen = hoTen;
            Khoa = khoa;
            DiemTB = diemTB;
            SoDienThoai = soDienThoai;
        }
        public override string ToString()
        {
            return $"{MaSo} - {HoTen} - {Khoa} - DiemTB: {DiemTB:0.0} - SoDienThoai";
        }
    }
}

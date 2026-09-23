using lap01_03;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Lab01_03
{
    class Program
    {
        static List<Person> danhSach = new List<Person>();

        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            bool tiepTuc = true;

            while (tiepTuc)
            {
                Console.Clear();
                Console.WriteLine("===== QUAN LY GIANG VIEN VA SINH VIEN =====");
                Console.WriteLine("1. Them sinh vien");
                Console.WriteLine("2. Thêm giang vien");
                Console.WriteLine("3. Xuat danh sach sinh vien");
                Console.WriteLine("4. Xuat danh sach giang vien");
                Console.WriteLine("5. So luong tung danh sach");
                Console.WriteLine("6. Xuat sinh vien khoa 'CNTT'");
                Console.WriteLine("7. Xuat giang vien co chuyen mon 'Lap trinh'");
                Console.WriteLine("8. Xuat sinh vien co diem cao nhat khoa 'CNTT'");
                Console.WriteLine("9. Thong ke so luong theo tung xep loai hoc luc");
                Console.WriteLine("0. Thoat");
                Console.Write("Chon chuc nang: ");

                string? luaChon = Console.ReadLine();
                switch (luaChon)
                {
                    case "1": ThemSinhVien(); break;
                    case "2": ThemGiaoVien(); break;
                    case "3": XuatDanhSach(danhSach.OfType<Student>()); break;
                    case "4": XuatDanhSach(danhSach.OfType<Teacher>()); break;
                    case "5": ThongKeSoLuong(); break;
                    case "6": XuatSVCntt(); break;
                    case "7": XuatGvLapTrinh(); break;
                    case "8": XuatSvGioiNhatCntt(); break;
                    case "9": ThongKeXepLoai(); break;
                    case "0": tiepTuc = false; break;
                    default: Console.WriteLine("Lua chon khong hop le!"); break;
                }

                if (tiepTuc)
                {
                    Console.WriteLine("\nNhap phim bat ky de tiep tuc...");
                    Console.ReadKey();
                }
            }
        }

        static void ThemSinhVien()
        {
            Console.WriteLine("\n--- THEM SINH VIEN ---");
            string maSo = NhapMaSo();
            if (maSo == null) return; // Đã trùng mã

            Console.Write("Ho ten: "); string hoTen = Console.ReadLine() ?? "";
            Console.Write("Khoa: "); string khoa = Console.ReadLine() ?? "";

            double diemTB = NhapDiemTB();

            Console.Write("So dien thoai (co the bo trong): ");
            string? sdtInput = Console.ReadLine();
            string? sdt = string.IsNullOrWhiteSpace(sdtInput) ? null : sdtInput;

            danhSach.Add(new Student(maSo, hoTen, khoa, diemTB, sdt));
            Console.WriteLine("=> Them thanh cong!");
        }

        static void ThemGiaoVien()
        {
            Console.WriteLine("\n--- THEM GIAO VIEN ---");
            string maSo = NhapMaSo();
            if (maSo == null) return;

            Console.Write("Ho ten: "); string hoTen = Console.ReadLine() ?? "";
            Console.Write("Khoa: "); string khoa = Console.ReadLine() ?? "";
            Console.Write("Chuyen mon: "); string chuyenMon = Console.ReadLine() ?? "";

            danhSach.Add(new Teacher(maSo, hoTen, khoa, chuyenMon));
            Console.WriteLine("=> Them thanh cong!");
        }

        static string NhapMaSo()
        {
            Console.Write("Ma so: ");
            string maSo = Console.ReadLine() ?? "";
            if (danhSach.Any(p => p.MaSo.Equals(maSo, StringComparison.OrdinalIgnoreCase)))
            {
                Console.WriteLine("Loi: Ma so da co trong he thong!");
                return null;
            }
            return maSo;
        }

        static double NhapDiemTB()
        {
            double diem;
            while (true)
            {
                Console.Write("Diem trung binh (0-10): ");
                if (double.TryParse(Console.ReadLine(), out diem) && diem >= 0 && diem <= 10)
                    return diem;
                Console.WriteLine("Loi! Diem nhap tu 0-10, vui long nhap lai!");
            }
        }

        static void XuatDanhSach<T>(IEnumerable<T> ds)
        {
            if (!ds.Any())
            {
                Console.WriteLine("!!!KHOGN CO DU LIEU!!!");
                return;
            }
            foreach (var item in ds) Console.WriteLine(item.ToString());
        }

        static void ThongKeSoLuong()
        {
            int svCount = danhSach.OfType<Student>().Count();
            int gvCount = danhSach.OfType<Teacher>().Count();
            Console.WriteLine($"Tong so Sinh Vien: {svCount}");
            Console.WriteLine($"Tong so Giang Vien: {gvCount}");
        }

        static void XuatSVCntt()
        {
            var result = danhSach.OfType<Student>().Where(s => s.Khoa.Equals("CNTT", StringComparison.OrdinalIgnoreCase));
            XuatDanhSach(result);
        }

        static void XuatGvLapTrinh()
        {
            var result = danhSach.OfType<Teacher>().Where(t => t.ChuyenMon.Contains("Lap trinh", StringComparison.OrdinalIgnoreCase) || t.ChuyenMon.Contains("Lập trình", StringComparison.OrdinalIgnoreCase));
            XuatDanhSach(result);
        }

        static void XuatSvGioiNhatCntt()
        {
            var bestStudent = danhSach.OfType<Student>()
                                      .Where(s => s.Khoa.Equals("CNTT", StringComparison.OrdinalIgnoreCase))
                                      .OrderByDescending(s => s.DiemTB)
                                      .FirstOrDefault();

            if (bestStudent != null)
                Console.WriteLine($"Sinh vien xuat sac nhat khoa CNTT:\n{bestStudent}");
            else
                Console.WriteLine("Khogn tim thay sinh vien thuoc khoa CNTT.");
        }

        static void ThongKeXepLoai()
        {
            var result = danhSach.OfType<Student>()
                                 .GroupBy(s => XepLoai(s.DiemTB))
                                 .Select(g => new { XepLoai = g.Key, SoLuong = g.Count() });

            if (!result.Any())
            {
                Console.WriteLine("Chua co du lieu sinh vien.");
                return;
            }

            foreach (var group in result)
                Console.WriteLine($"Xep loai {group.XepLoai}: {group.SoLuong} sinh vien");
        }

        static string XepLoai(double diem)
        {
            if (diem >= 9) return "Xuat Sac";
            if (diem >= 8) return "Gioi";
            if (diem >= 6.5) return "Kha";
            if (diem >= 5) return "Trung Binh";
            return "Yeu";
        }
    }
}
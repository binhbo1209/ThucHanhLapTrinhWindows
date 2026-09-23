using lap01_02;

Console.OutputEncoding =  System.Text.Encoding.UTF8;
List<Student> danhsach = new List<Student>();
bool tiepTuc = true;
while (tiepTuc)
{
    Console.WriteLine("=== QUAN LY SINH VIEN ===");
    Console.WriteLine("1. Them sinh vien");
    Console.WriteLine("2. Xuat danh sach sinh vien");
    Console.WriteLine("3. Tim kiem theo ho ten (gan dung)");
    Console.WriteLine("4. Loc theo khoa");
    Console.WriteLine("5. Sap xep theo diem trung binh (giam dan)");
    Console.WriteLine("6. Thong ke theo khoa");
    Console.WriteLine("0. Thoat");
    Console.Write("chon chuc nang: ");
    string? luachon = Console.ReadLine();
    switch (luachon)
    {
        case "1": ThemSinhVien(); break;
        case "2": XuatDanhSach(danhsach); break;
        case "3": TimKiemHoTen(); break;
        case "4": LocTheoKhoa(); break;
        case "5": SapXepTheoDiemTB(); break;
        case "6": ThongKeTheoKhoa(); break;
        case "0": tiepTuc = false; break;
        default: Console.WriteLine("Lua chon khong hop le! Xin thu lai!"); break;
    }
    if (tiepTuc)
    {
        Console.WriteLine("\nNhan phim bat ky de tiep tuc...");
        Console.ReadKey();
    }
}
void ThemSinhVien()
{
    Console.WriteLine("\n--- THEM SINH VIEN ---");
    Console.Write("Ma so: ");
    string maSo = Console.ReadLine();
    Console.Write("Ho ten: ");
    string hoTen = Console.ReadLine();
    Console.Write("Khoa: ");
    string khoa = Console.ReadLine();

    double diemTB = NhapSoThuc("Diem trung binh (0-10): ", 0, 10);

    Console.Write("So dien thoai (co the bo trong): ");
    string sdtInput = Console.ReadLine();
    string? soDienThoai = string.IsNullOrWhiteSpace(sdtInput) ? null : sdtInput;

    danhsach.Add(new Student(maSo, hoTen, khoa, diemTB, soDienThoai));
    Console.WriteLine("=>Them thanh cong!");
}
void XuatDanhSach(IEnumerable<Student> ds)
{
    Console.WriteLine("\n--- DANH SACH SINH VIEN ---");
    if (!ds.Any())
    {
        Console.WriteLine("!!!Khong co du lieu!!!");
        return;
    }
    foreach (var sv in ds)
        Console.WriteLine(sv);
}
void TimKiemHoTen()
{
    Console.Write("\nNhapten can tim: ");
    string tuKhoa = Console.ReadLine();

    var ketQua = danhsach.Where(x => x.HoTen.Contains(tuKhoa, StringComparison.OrdinalIgnoreCase));
    XuatDanhSach(ketQua);
}
void LocTheoKhoa()
{
    Console.Write("\nNhap khoa can loc: ");
    string khoa = Console.ReadLine();
    var ketQua = danhsach.Where(x => x.Khoa.Equals(khoa, StringComparison.OrdinalIgnoreCase));
    XuatDanhSach(ketQua);
}
void SapXepTheoDiemTB()
{
    var ketQua = danhsach.OrderByDescending(x => x.DiemTB);
    XuatDanhSach(ketQua);
}
void ThongKeTheoKhoa()
{
    Console.WriteLine("\n--- THONG KE THEO KHOA ---");
    if (!danhsach.Any())
    {
        Console.WriteLine("!!!Khong co du lieu!!!");
        return;
    }
    var thongKe = danhsach
        .GroupBy(x => x.Khoa)
        .Select(g => new
        {
            Khoa = g.Key,
            SoLuong = g.Count(),
            DiemTrungBinh = g.Average(x => x.DiemTB)
        })
        .OrderByDescending(x => x.SoLuong);
    foreach ( var nhom in thongKe)
    {
        Console.WriteLine("Khoa: {0,-15} | So luong: {1,3} | Diem TB: {2:0.00}",
            nhom.Khoa, nhom.SoLuong, nhom.DiemTrungBinh);
    }
}
double NhapSoThuc(string thongBao, double min, double max)
{
    double gtri;
    while (true)
    {
        Console.Write(thongBao);
        if (double.TryParse(Console.ReadLine(), out gtri) && gtri >= min && gtri <= max)
            return gtri;
        Console.WriteLine("=> Gia tri khong hop le, nhap lai (tu " + min + " den " + max + ").");
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyThuVien.Data;


public static class NhatKy
{
    private static readonly string FileLog = Path.Combine(AppContext.BaseDirectory, "error.log");

    public static void Ghi(Exception? ex)
    {
        if (ex == null) return;
        try
        {
            File.AppendAllText(FileLog,
                $"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] {ex}{Environment.NewLine}{Environment.NewLine}");
        }
        catch { /* không để việc ghi log làm sập app */ }
    }
}

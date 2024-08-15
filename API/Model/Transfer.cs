using System;

public class Transfer
{
    public Guid Id { get; set; }
    public string? ToNumber { get; set; }
    public string? ToItem { get; set; }
    public string? Material { get; set; }
    public string? StorageLocation { get; set; }
    public string? Warehouse {get; set;}
    public string? StorageBin { get; set; }
    public decimal QtyPick { get; set; }
    public decimal QtyConfirm { get; set; }
    public bool Pick { get; set; }
    public bool Confirm { get; set; }
    public DateTime CreatedDate { get; set; }
    public DateTime? LastModifiedDate { get; set; }
}


// jika ingin tetap ingin menggunaka character di model maka gunakan aray, agar character dapat terbaca secara keseluruhan
// apabila tanpa menggunakan array maka data akan mengambul 1 karakter

// contoh penggunaan aray "char[]"

// lalu juga ubah pada DB_MANAGER pada baris untuk convert biar bisa di baca dengan

// reader["......."].ToString().ToCharArray(),

// dibagian confirm juga sama. disini apabila kita menggunakan tipe data bolean maka, dan isi fieldnya null maka pada DB Manager
// harus di buat falidasi agar semua data dapat muncul saat ingin mengambil semua data dengan cara
// reader["confirm"] != DBNull.Value ? Convert.ToBoolean(reader["confirm"]) : false
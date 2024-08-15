# dotnet-Aplikasi-Mobile

Repository ini merupakan backend API yang menggunakan Microsoft SQL Server.

_arofan_

# Langkah-langkah Penggunaan

## 1. Membuat Database

Buat database dengan tabel `TransferOrder` menggunakan query SQL berikut (untuk Microsoft SQL Server):

```sql
CREATE TABLE TransferOrder (
    id UNIQUEIDENTIFIER DEFAULT NEWID() PRIMARY KEY,
    toNumber VARCHAR(10) NOT NULL,
    toItem VARCHAR(4) NOT NULL,
    material NCHAR(18) NOT NULL,
    storageLocation NCHAR(5),
    warehouse NCHAR(3),
    storageBin NCHAR(8),
    qtyPick DECIMAL(10,2) DEFAULT 0,
    qtyConfirm DECIMAL(10,2) DEFAULT 0,
    pick BIT DEFAULT 0,
    confirm BIT DEFAULT 0,
    CreatedDate DATETIME DEFAULT GETDATE(),
    LastModifiedDate DATETIME
);
```

pada kasus ini saya menggunakan database dari Microsoft (MICROSOFT SQL) jadi untuk anda yang menggunakan MYSQL atau yang lain anda dapat menyesuaikan.
untuk id saya menggunakan " UUID " sebagai pengganti AUTO INCREMENT 


## 2. Buka pada folder ` API ` lalu edit file ` appsettings.json `.
   
   pada bagian ini anda dapat menyesuaikan dengan koneksi pada database yang anda miliki.
   sebagai contoh, saya menggunakan MICROSOFT SQL.
   
   `DefaultConnection": "Server=localhost\\SQLEXPRESS04;Database=pertama;Trusted_Connection=True;" `

   lalu saya akan membagikan contoh jika anda menggunakan MYSQL.
   
   ```"DefaultConnection": "server=localhost;port=3306;user=root;password=;database=pertama;"```


## 3. Selanjutnya edit juga pada file ` launchSettings.json ` yang terletak pada folder ` Properties `.
   
   pada line ke 17 anda dapat melihat `` ( "applicationUrl": "http://192.168.54.176:5000;http://localhost:6969", ) ``

   pada bagian : `http://192.168.54.176:5000;http://localhost:6969` .

   anda dapat merubahnya, menggunakan IP yang anda miliki.

   untuk melihat IP yang anda miliki anda cukup membuka CMD (Command Prompt).
   
   Lalu ketikan. 
   ```sh
ipconfig 
   ```
   
   Setelah data muncul maka pilih pada bagian:
   ```
   Wireless LAN adapter Wi-Fi:

   Connection-specific DNS Suffix  . :
   Link-local IPv6 Address . . . . . : fe89::ec70:c8ee:k92a:7a37%6
   IPv4 Address. . . . . . . . . . . : 192.168.0.175
   Subnet Mask . . . . . . . . . . . : 255.255.252.0
   Default Gateway . . . . . . . . . : 192.168.52.1
```

   maka kamu dapat melihat IP yang kamu miliki saat ini, yaitu:
   
   ``IPv4 Address. . . . . . . . . . . : 192.168.0.175``

   Selanjutnya anda dapat mengubahnya menjadi:
   ``` http://192.168.0.175:5000;http://localhost:6969 ``` .

   anda juga dapat mengubah pada bagian :
   ` http://localhost:6969 `
   menjadi port yang anda inginkan.
   
   Port itu angka yang berada setelah titik dua ( : ).

  > Note: kenapa harus menggunakan IP ? . Karena saat ini kita menggunakan server lokal, jadi anda harus menggunakan 1 jaringan yang sama. pada 1 wifi yang sama contohnya 

## 4. Setelah anda melakukan itu, anda dapat melakukan perintah pada terminal
untuk memperbaiki/melihat apakah ada code yang error dan warning maka anda dapat mengetikan.
   ```sh 
   dotnet build
   ``` 
   
   lalu setelah semua berjalan tanpa error maka anda dapat melanjutkan perintah dengan mengetikan.
   
   ```sh 
   dotnet run
```
pada terminal. Fungsinya untuk menjalankan program (API)

## 5. Dengan begini API berjalan pada jaringan yang sama / localhost

## 6. Apabila terjadi ERROR anda bisa melakukan langkah-langkah berikut

### pastikan lagi apakah anda sudah memiliki dotnet (. NET)
Buka terminal dan jalankan perintah berikut.
```sh
dotnet --list-sdks
```

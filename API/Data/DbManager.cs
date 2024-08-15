using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;

public class DbManager
{
    // Menghubungkan ke database
    private readonly string _connectionString;
    private readonly SqlConnection _connection;

    public DbManager(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("DefaultConnection");
        _connection = new SqlConnection(_connectionString);
    }

    public List<Transfer> GetAllTransfers()
    {
        List<Transfer> TransferList = new List<Transfer>();
        try
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = "select * from TransferOrder;";
                SqlCommand command = new SqlCommand(query, connection);
                connection.Open();
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Transfer Transfer = new Transfer
                        {
                            Id = reader.GetGuid(reader.GetOrdinal("id")),
                            ToNumber = reader["toNumber"].ToString(),
                            ToItem = reader["toItem"].ToString(),
                            Material = reader["material"].ToString(),
                            StorageLocation = reader["storageLocation"] as string,
                            Warehouse = reader["warehouse"] as string,
                            StorageBin = reader["storageBin"] as string,
                            QtyPick = reader.GetDecimal(reader.GetOrdinal("qtyPick")),
                            QtyConfirm = reader.GetDecimal(reader.GetOrdinal("qtyConfirm")),
                            Pick = reader.GetBoolean(reader.GetOrdinal("pick")),
                            Confirm = reader.GetBoolean(reader.GetOrdinal("confirm")),
                            CreatedDate = reader.GetDateTime(reader.GetOrdinal("CreatedDate")),
                            LastModifiedDate = reader["LastModifiedDate"] != DBNull.Value ? reader.GetDateTime(reader.GetOrdinal("LastModifiedDate")) : (DateTime?)null
                        };
                        TransferList.Add(Transfer);
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
        return TransferList;
    }

public bool CreateTransfer(Transfer transfer)
{
    try
    {
        using (SqlConnection connection = new SqlConnection(_connectionString))
        {
            string query = @"INSERT INTO TransferOrder 
                            (toNumber, toItem, material, storageLocation, warehouse, storageBin, qtyPick, qtyConfirm, pick, confirm, CreatedDate, LastModifiedDate)
                            VALUES 
                            (@ToNumber, @ToItem, @Material, @StorageLocation, @Warehouse, @StorageBin, @QtyPick, @QtyConfirm, @Pick, @Confirm, @CreatedDate, @LastModifiedDate)";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@ToNumber", transfer.ToNumber);
            command.Parameters.AddWithValue("@ToItem", transfer.ToItem);
            command.Parameters.AddWithValue("@Material", transfer.Material);
            command.Parameters.AddWithValue("@StorageLocation", transfer.StorageLocation ?? (object)DBNull.Value);
            command.Parameters.AddWithValue("@Warehouse", transfer.Warehouse ?? (object)DBNull.Value);
            command.Parameters.AddWithValue("@StorageBin", transfer.StorageBin ?? (object)DBNull.Value);
            command.Parameters.AddWithValue("@QtyPick", transfer.QtyPick);
            command.Parameters.AddWithValue("@QtyConfirm", transfer.QtyConfirm);
            command.Parameters.AddWithValue("@Pick", transfer.Pick);
            command.Parameters.AddWithValue("@Confirm", transfer.Confirm);
            command.Parameters.AddWithValue("@CreatedDate", transfer.CreatedDate != DateTime.MinValue ? (object)transfer.CreatedDate : DBNull.Value);
            command.Parameters.AddWithValue("@LastModifiedDate", transfer.LastModifiedDate.HasValue ? (object)transfer.LastModifiedDate.Value : DBNull.Value);

            connection.Open();
            int result = command.ExecuteNonQuery();

            return result > 0;
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine(ex.Message);
        return false;
    }
}

public bool EditTransfer(Transfer transfer)
{
    try
    {
        using (SqlConnection connection = new SqlConnection(_connectionString))
        {
            string query = @"UPDATE TransferOrder SET 
                                toNumber = @ToNumber,
                                toItem = @ToItem,
                                material = @Material,
                                storageLocation = @StorageLocation,
                                warehouse = @Warehouse,
                                storageBin = @StorageBin,
                                qtyPick = @QtyPick,
                                qtyConfirm = @QtyConfirm,
                                pick = @Pick,
                                confirm = @Confirm,
                                LastModifiedDate = @LastModifiedDate
                                WHERE id = @Id";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@Id", transfer.Id);
            command.Parameters.AddWithValue("@ToNumber", transfer.ToNumber);
            command.Parameters.AddWithValue("@ToItem", transfer.ToItem);
            command.Parameters.AddWithValue("@Material", transfer.Material);
            command.Parameters.AddWithValue("@StorageLocation", transfer.StorageLocation ?? (object)DBNull.Value);
            command.Parameters.AddWithValue("@Warehouse", transfer.Warehouse ?? (object)DBNull.Value);
            command.Parameters.AddWithValue("@StorageBin", transfer.StorageBin ?? (object)DBNull.Value);
            command.Parameters.AddWithValue("@QtyPick", transfer.QtyPick);
            command.Parameters.AddWithValue("@QtyConfirm", transfer.QtyConfirm);
            command.Parameters.AddWithValue("@Pick", transfer.Pick);
            command.Parameters.AddWithValue("@Confirm", transfer.Confirm);
            command.Parameters.AddWithValue("@LastModifiedDate", transfer.LastModifiedDate.HasValue ? (object)transfer.LastModifiedDate.Value : DBNull.Value);

            connection.Open();
            int result = command.ExecuteNonQuery();

            return result > 0;
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine(ex.Message);
        return false;
    }
}

public bool DeleteTransfer(Guid id)
{
    try
    {
        using (SqlConnection connection = new SqlConnection(_connectionString))
        {
            string query = "DELETE FROM TransferOrder WHERE id = @Id";
            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@Id", id);

            connection.Open();
            int result = command.ExecuteNonQuery();

            return result > 0;
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine(ex.Message);
        return false;
    }
}

public Transfer? GetTransferByToNumber(string toNumber)
    {
        Transfer? transfer = null;
        try
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = "SELECT * FROM TransferOrder WHERE toNumber = @ToNumber";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@ToNumber", toNumber);
                connection.Open();
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        transfer = new Transfer
                        {
                            Id = reader.GetGuid(reader.GetOrdinal("id")),
                            ToNumber = reader["toNumber"].ToString(),
                            ToItem = reader["toItem"].ToString(),
                            Material = reader["material"].ToString(),
                            StorageLocation = reader["storageLocation"] as string,
                            Warehouse = reader["warehouse"] as string,
                            StorageBin = reader["storageBin"] as string,
                            QtyPick = reader.GetDecimal(reader.GetOrdinal("qtyPick")),
                            QtyConfirm = reader.GetDecimal(reader.GetOrdinal("qtyConfirm")),
                            Pick = reader.GetBoolean(reader.GetOrdinal("pick")),
                            Confirm = reader.GetBoolean(reader.GetOrdinal("confirm")),
                            CreatedDate = reader.GetDateTime(reader.GetOrdinal("CreatedDate")),
                            LastModifiedDate = reader["LastModifiedDate"] != DBNull.Value ? reader.GetDateTime(reader.GetOrdinal("LastModifiedDate")) : (DateTime?)null
                        };
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
        return transfer;
    }

public bool UpdateQtyPickAndPick(string warehouse, string toNumber, string toItem, decimal qtyPick)
{
    try
    {
        using (SqlConnection connection = new SqlConnection(_connectionString))
        {
            // Query to update qtyPick and pick based on warehouse, toNumber, and toItem
            string query = @"UPDATE TransferOrder 
                             SET qtyPick = @QtyPick, 
                                 pick = @Pick, 
                                 LastModifiedDate = @LastModifiedDate 
                             WHERE warehouse = @Warehouse AND toNumber = @ToNumber AND toItem = @ToItem";

            SqlCommand command = new SqlCommand(query, connection);

            // Set parameters for the query
            command.Parameters.AddWithValue("@QtyPick", qtyPick);
            command.Parameters.AddWithValue("@Pick", true);  // Assuming you want to set Pick to true
            command.Parameters.AddWithValue("@LastModifiedDate", DateTime.Now);
            command.Parameters.AddWithValue("@Warehouse", warehouse);
            command.Parameters.AddWithValue("@ToNumber", toNumber);
            command.Parameters.AddWithValue("@ToItem", toItem);

            connection.Open();
            int result = command.ExecuteNonQuery();
            connection.Close();

            // Check if the update was successful
            return result > 0;
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine(ex.Message);
        return false;
    }
}


public bool UpdateQtyConfirmAndConfirm(string warehouse, string toNumber, string toItem, decimal qtyConfirm)
{
    try
    {
        using (SqlConnection connection = new SqlConnection(_connectionString))
        {
            // Query to update qtyConfirm and confirm based on warehouse, toNumber, and toItem
            string query = @"UPDATE TransferOrder 
                             SET qtyConfirm = @QtyConfirm, 
                                 confirm = @Confirm, 
                                 LastModifiedDate = @LastModifiedDate 
                             WHERE warehouse = @Warehouse AND toNumber = @ToNumber AND toItem = @ToItem";

            SqlCommand command = new SqlCommand(query, connection);

            // Set parameters for the query
            command.Parameters.AddWithValue("@QtyConfirm", qtyConfirm);
            command.Parameters.AddWithValue("@Confirm", true);  // Assuming you want to set Confirm to true
            command.Parameters.AddWithValue("@LastModifiedDate", DateTime.Now);
            command.Parameters.AddWithValue("@Warehouse", warehouse);
            command.Parameters.AddWithValue("@ToNumber", toNumber);
            command.Parameters.AddWithValue("@ToItem", toItem);

            connection.Open();
            int result = command.ExecuteNonQuery();
            connection.Close();

            // Check if the update was successful
            return result > 0;
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine(ex.Message);
        return false;
    }
}


public Transfer? GetTransferByFields(string warehouse, string toNumber, string toItem)
{
    Transfer? transfer = null;
    try
    {
        using (SqlConnection connection = new SqlConnection(_connectionString))
        {
            string query = "SELECT * FROM TransferOrder WHERE warehouse = @Warehouse AND toNumber = @ToNumber AND toItem = @ToItem";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@Warehouse", warehouse);
            command.Parameters.AddWithValue("@ToNumber", toNumber);
            command.Parameters.AddWithValue("@ToItem", toItem);
            connection.Open();
            using (SqlDataReader reader = command.ExecuteReader())
            {
                if (reader.Read())
                {
                    transfer = new Transfer
                    {
                        Id = reader.GetGuid(reader.GetOrdinal("id")),
                        ToNumber = reader["toNumber"].ToString(),
                        ToItem = reader["toItem"].ToString(),
                        Material = reader["material"].ToString(),
                        StorageLocation = reader["storageLocation"] as string,
                        Warehouse = reader["warehouse"] as string,
                        StorageBin = reader["storageBin"] as string,
                        QtyPick = reader.GetDecimal(reader.GetOrdinal("qtyPick")),
                        QtyConfirm = reader.GetDecimal(reader.GetOrdinal("qtyConfirm")),
                        Pick = reader.GetBoolean(reader.GetOrdinal("pick")),
                        Confirm = reader.GetBoolean(reader.GetOrdinal("confirm")),
                        CreatedDate = reader.GetDateTime(reader.GetOrdinal("CreatedDate")),
                        LastModifiedDate = reader["LastModifiedDate"] != DBNull.Value ? reader.GetDateTime(reader.GetOrdinal("LastModifiedDate")) : (DateTime?)null
                    };
                }
            }
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine(ex.Message);
    }
    return transfer;
}


}

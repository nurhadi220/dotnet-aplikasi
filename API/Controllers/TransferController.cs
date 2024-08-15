using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

[ApiController]
[Route("warehouse-api/api/v1")]
public class TransferController : ControllerBase
{
    private readonly DbManager _dbarofan;

    Response response = new Response();

    public TransferController(IConfiguration configuration)
    {
        _dbarofan = new DbManager(configuration);
    }

    [HttpGet("get-all-transfers")]
    public IActionResult GetAllTransfers()
    {
        try
        {
            var transferData = _dbarofan.GetAllTransfers();
            var responseTransferList = transferData.Select(t => new ResponseTransfer
            {
                Id = t.Id,
                ToNumber = t.ToNumber,
                ToItem = t.ToItem,
                Material = t.Material,
                SLoc = t.StorageLocation,
                Warehouse = t.Warehouse,
                SBin = t.StorageBin,
                QtyPick = t.QtyPick,
                QtyConfirm = t.QtyConfirm,
                Pick = t.Pick,
                Confirm = t.Confirm,
                CreatedDate = t.CreatedDate,
                LastModifiedDate = t.LastModifiedDate
            }).ToList();

            response.status = 200;
            response.pesan = "Success";
            response.data = responseTransferList;
        }
        catch (Exception ex)
        {
            response.status = 500;
            response.pesan = ex.Message;
        }
        return Ok(response);
    }

    [HttpPost("create-transfer")]
public IActionResult CreateTransfer([FromBody] Transfer transfer)
{
    try
    {
        if (transfer == null)
        {
            response.status = 400;
            response.pesan = "Invalid data.";
            return BadRequest(response);
        }

        // Panggil metode CreateTransfer dari DbManager
        bool isCreated = _dbarofan.CreateTransfer(transfer);

        if (isCreated)
        {
            response.status = 201;
            response.pesan = "Transfer created successfully.";
            response.data = transfer; // Mengembalikan objek transfer yang baru saja dibuat
            return CreatedAtAction(nameof(CreateTransfer), new { id = transfer.Id }, response);
        }
        else
        {
            response.status = 500;
            response.pesan = "Failed to create transfer.";
            return StatusCode(500, response);
        }
    }
    catch (Exception ex)
    {
        response.status = 500;
        response.pesan = ex.Message;
        return StatusCode(500, response);
    }
}
[HttpPut("edit-transfer/{id}")]
public IActionResult EditTransfer(Guid id, [FromBody] Transfer transfer)
{
    try
    {
        if (transfer == null || id != transfer.Id)
        {
            response.status = 400;
            response.pesan = "Invalid transfer data or ID mismatch.";
            return BadRequest(response);
        }

        // Panggil metode EditTransfer dari DbManager
        bool isUpdated = _dbarofan.EditTransfer(transfer);

        if (isUpdated)
        {
            response.status = 200;
            response.pesan = "DATA BERHASIL DI UPDATE";
            response.data = transfer;
            return Ok(response);
        }
        else
        {
            response.status = 404;
            response.pesan = "DATA TIDAK DITEMUKAN, GAGAL DI EDIT";
            return NotFound(response);
        }
    }
    catch (Exception ex)
    {
        response.status = 500;
        response.pesan = ex.Message;
        return StatusCode(500, response);
    }
}

[HttpDelete("delete-transfer/{id}")]
public IActionResult DeleteTransfer(Guid id)
{
    try
    {
        // Panggil metode DeleteTransfer dari DbManager
        bool isDeleted = _dbarofan.DeleteTransfer(id);

        if (isDeleted)
        {
            response.status = 200;
            response.pesan = "DATA BERHASIL DI DELETE";
            return Ok(response);
        }
        else
        {
            response.status = 404;
            response.pesan = "DATA TIDAK DITEMUKAN, GAGAL DI HAPUS";
            return NotFound(response);
        }
    }
    catch (Exception ex)
    {
        response.status = 500;
        response.pesan = ex.Message;
        return StatusCode(500, response);
    }
}


}

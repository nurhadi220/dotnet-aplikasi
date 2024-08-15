using Microsoft.AspNetCore.Mvc;
using System;

[ApiController]
[Route("warehouse-api/api/v1")]
public class ConfirmController : ControllerBase
{
    private readonly DbManager _dbarofan;
    private Response response = new Response();

    public ConfirmController(IConfiguration configuration)
    {
        _dbarofan = new DbManager(configuration);
    }

    [HttpPut("update-confirm")]
    public IActionResult UpdateConfirm([FromQuery] string warehouse, [FromQuery] string toNumber, [FromQuery] string toItem, [FromBody] decimal qtyConfirm)
    {
        try
        {
            // Ambil detail transfer berdasarkan parameter
            var transfer = _dbarofan.GetTransferByFields(warehouse, toNumber, toItem);

            if (transfer != null)
            {
                // Update qtyConfirm dan confirm
                transfer.QtyConfirm = qtyConfirm;
                transfer.Confirm = true;
                transfer.LastModifiedDate = DateTime.Now;

                // Proses update pada database
                bool isUpdated = _dbarofan.UpdateQtyConfirmAndConfirm(warehouse, toNumber, toItem, qtyConfirm);

                if (isUpdated)
                {
                    response.status = 200;
                    response.pesan = "QtyConfirm dan Confirm berhasil diperbarui.";
                    response.data = transfer; // Menggunakan Transfer langsung
                    return Ok(response);
                }
                else
                {
                    response.status = 500;
                    response.pesan = "Gagal memperbarui QtyConfirm dan Confirm.";
                    return StatusCode(500, response);
                }
            }
            else
            {
                response.status = 404;
                response.pesan = "Data tidak ditemukan.";
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

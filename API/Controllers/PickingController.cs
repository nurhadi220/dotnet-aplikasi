using Microsoft.AspNetCore.Mvc;
using System;

[ApiController]
[Route("warehouse-api/api/v1")]
public class PickingController : ControllerBase
{
    private readonly DbManager _dbarofan;
    private Response response = new Response();

    public PickingController(IConfiguration configuration)
    {
        _dbarofan = new DbManager(configuration);
    }

    [HttpGet("get-transfer-by-fields")]
    public IActionResult GetTransferByFields([FromQuery] string warehouse, [FromQuery] string toNumber, [FromQuery] string toItem)
    {
        try
        {
            // Cari data Transfer berdasarkan warehouse, toNumber, dan toItem
            var transferData = _dbarofan.GetTransferByFields(warehouse, toNumber, toItem);

            if (transferData == null)
            {
                response.status = 404;
                response.pesan = "Transfer Order tidak ditemukan.";
                return NotFound(response);
            }

            response.status = 200;
            response.data = transferData;
            return Ok(response);
        }
        catch (Exception ex)
        {
            response.status = 500;
            response.pesan = ex.Message;
            return StatusCode(500, response);
        }
    }

    [HttpPut("update-pick")]
    public IActionResult UpdatePick([FromQuery] string warehouse, [FromQuery] string toNumber, [FromQuery] string toItem, [FromBody] decimal qtyPick)
    {
        try
        {
            // Ambil detail transfer berdasarkan parameter
            var transfer = _dbarofan.GetTransferByFields(warehouse, toNumber, toItem);

            if (transfer != null)
            {
                // Update qtyPick dan pick
                transfer.QtyPick = qtyPick;
                transfer.Pick = true;
                transfer.LastModifiedDate = DateTime.Now;

                // Proses update pada database
                bool isUpdated = _dbarofan.UpdateQtyPickAndPick(warehouse, toNumber, toItem, qtyPick);

                if (isUpdated)
                {
                    response.status = 200;
                    response.pesan = "QtyPick dan Pick berhasil diperbarui.";
                    response.data = transfer;
                    return Ok(response);
                }
                else
                {
                    response.status = 500;
                    response.pesan = "Gagal memperbarui QtyPick dan Pick.";
                    return StatusCode(500, response);
                }
            }
            else
            {
                response.status = 404;
                response.pesan = "Transfer Order tidak ditemukan.";
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

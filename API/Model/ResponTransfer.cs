public class ResponseTransfer
{
    public Guid Id { get; set; }
    public string? ToNumber { get; set; }
    public string? ToItem { get; set; }
    public string? Material { get; set; }
    public string? SLoc{ get; set; }
    public string? Warehouse {get; set;}
    public string? SBin { get; set; }
    public decimal QtyPick { get; set; }
    public decimal QtyConfirm { get; set; }
    public bool Pick { get; set; }
    public bool Confirm { get; set; }
    public DateTime CreatedDate { get; set; }
    public DateTime? LastModifiedDate { get; set; }
}

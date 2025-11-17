namespace PE_PRN232_FA25_TranNguyenVietQuang_BE_api.DTOs;

public class BearProfileDto
{
    public string BearProfileId { get; set; }
    public string? BearTypeId { get; set; }
    public string? BearName { get; set; }
    public int? BearWeight { get; set; }
    public string? Characteristics { get; set; }
    public string? CareNeeds { get; set; }
    public DateTime? ModifiedDate { get; set; }
}
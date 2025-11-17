namespace PE_PRN232_FA25_LeHoangTrong_BE_api.DTOs;

public class BearProfileDto
{
    public int BearProfileId { get; set; }
    public int? BearTypeId { get; set; }
    public string? BearName { get; set; }
    public decimal? BearWeight { get; set; }
    public string? Characteristics { get; set; }
    public string? CareNeeds { get; set; }
    public string? ModifiedDate { get; set; }
}
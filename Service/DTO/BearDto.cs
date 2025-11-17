using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.DTO;
public class CreateRequest
{
    [Required]
    public int BearTypeId { get; set; }

    [Required]
    public string BearName { get; set; }

    [Required]
    [Range(200, double.MaxValue, ErrorMessage = "Weignt must be greater than 200.")]
    public decimal BearWeight { get; set; }

    [Required]
    public string Characteristics { get; set; }

    [Required]
    public string CareNeeds { get; set; }
}

public class UpdateRequest
{
    [Required]
    public int Id { get; set; }
    public int BearTypeId { get; set; }

    public string BearName { get; set; }

    public decimal BearWeight { get; set; }

    public string Characteristics { get; set; }

    public string CareNeeds { get; set; }
}

public class GetResponse
{
    public int BearProfileId { get; set; }

    public int BearTypeId { get; set; }

    public string BearTypeName { get; set; }

    public string BearName { get; set; }

    public decimal BearWeight { get; set; }

    public string Characteristics { get; set; }

    public string CareNeeds { get; set; }

}




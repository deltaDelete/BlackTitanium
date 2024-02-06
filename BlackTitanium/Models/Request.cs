using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using JsonSubTypes;
using Newtonsoft.Json;

namespace BlackTitanium.Models;

[JsonConverter(typeof(JsonSubtypes), "type")]
[JsonSubtypes.KnownSubType(typeof(RegisterRequest), "register")]
[JsonSubtypes.KnownSubType(typeof(LoginRequest), "login")]
public class Request {
}

public class RegisterRequest : Request {
    [MaxLength(50)]
    public string LastName { get; set; } = string.Empty;

    [MaxLength(50)]
    public string FirstName { get; set; } = string.Empty;

    [MaxLength(50)]
    public string MiddleName { get; set; } = string.Empty;


    public string FullName => $"{LastName} {FirstName} {MiddleName}";

    [ForeignKey(nameof(Gender))]
    public int GenderId { get; set; }

    public ulong Phone { get; set; }

    public DateOnly Birthday { get; set; }
    public DateOnly PassportDate { get; set; }

    [Range(0000, 9999)]
    public int PassportSeries { get; set; }

    [Range(000000, 999999)]
    public int PassportNumber { get; set; }

    [MaxLength(255)]
    public string Login { get; set; } = string.Empty;

    [MinLength(8)]
    public string Password { get; set; } = string.Empty;
}

public class LoginRequest {
    [MaxLength(255)]
    public string Login { get; set; } = string.Empty;
    [MinLength(8)]
    public string Password { get; set; } = string.Empty;
}
﻿using odaurehonbe.Data;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

public class TicketClerk
{
    [Key]
    public int AccountID { get; set; }
    public string Name { get; set; }
    public string Gender { get; set; }
    public string PhoneNumber { get; set; }
    public DateOnly HireDate { get; set; }
    [JsonIgnore]  

    public Account Account { get; set; }
}
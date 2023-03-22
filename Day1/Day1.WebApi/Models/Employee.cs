﻿using System;
using System.ComponentModel.DataAnnotations;

public class Employee
{
    [Required]
    public Guid Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateTime Birthday { get; set; } 
};
﻿using System.ComponentModel.DataAnnotations;
using InventoryMgmt.Model;

namespace InventoryMgmt;

public class EmailLogs
{
    [Key]
    public int Id { get; set; }
    public int ItemId { get; set; }
    public ItemModel item { get; set; }
    public string Type { get; set; }
    public bool IsSent { get; set; }
    public string User { get; set; }

    public DateTime dateTime {get; set; } =DateTime.Now;
}

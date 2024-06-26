﻿namespace InventoryMgmt
{
    public class UserDefinedErrorCode
    {
        public enum ErrorCode
        {
            InsertItemError = 1000,
            UpdateItemError = 1001,
            GetItemError = 1002
        }
    }
    public static class SessionVariableEnum
    {
        public static readonly string CurrentUser = "currentUser";
    }
    public static class NotificationVariableEnum
    {
        public static readonly string LowStock = "LowInventory";
        public static readonly string MileStoneSales = "MileStoneSales";
        public static readonly string PurchaseItemInventory= "PurchaseItemInventory";
    }

    public static class EmailLogAlertTypeEnum
    {
        public static readonly string MileStoneSales = "MileStoneSalesAlert";
        public static readonly string QuantityLowStock = "StockLowAlert";
    }

    public static class EmailSubjectEnum
    {
        public static readonly string MileStoneSales= "Milestone Sales Alert";
        public static readonly string QuantityLowStock= "Low Stock Alert";
        public static readonly string AddingItemToInventory= "Adding Item to inventory";
    }
}

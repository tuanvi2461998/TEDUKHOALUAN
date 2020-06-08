using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace TeduCoreApp.Data.Enums
{
    public enum PaymentMethod
    {
        [Description("Thanh toán tại nhà")]
        CashOnDelivery,
        [Description("Chuyển khoảns")]
        OnlinBanking
    }
}

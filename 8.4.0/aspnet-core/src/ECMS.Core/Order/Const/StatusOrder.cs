using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECMS.Order.Const
{
    public enum StatusOrder
    {
        // Cho khách hàng thanh toán qua VNPay
        Pending ,
        //Thanh toán thành công
        Success,
        //Thất bại
        Cancel
    }
}

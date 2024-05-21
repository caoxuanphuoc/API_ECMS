using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using ECMS.Order;
using System.ComponentModel.DataAnnotations;

namespace ECMS.OrderDomain.OrderHistories.Dto
{
    [AutoMapTo(typeof(OrderHistory))]
    public class UpdateOrderHistoryDto : EntityDto<long>
    {
        [Required]
        public string TransactionNo { get; set; }
        [Required]
        public long ResponseCode { get; set; }
        [Required]
        public string Message { get; set; }
        [Required]
        public string OrderCode { get; set; }

        /*   vnp_Amount
           vnp_BankCode
           vnp_BankTranNo
           vnp_CardType
           vnp_OrderInfo
           vnp_PayDate
           vnp_ResponseCode
           vnp_TmnCode
           vnp_TransactionNo
           vnp_TransactionStatus
           vnp_TxnRef*/
    }
}

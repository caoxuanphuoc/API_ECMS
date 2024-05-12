using ECMS.Payment.Dto;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECMS.Payment
{
    public interface IPaymentAppService
    {
        string GetPaymentRequest( HttpContext httpContext,VnPaymentRequestDto input);
        VnPaymentResponseDto PaymentExcute(IQueryCollection colection);
    }
}

using ePizza.Entities.Concrete;
using Razorpay.Api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ePizza.Services.Interfaces
{
    public interface IPaymentService
    {
        string CreateOrder(decimal amount, string currency, string receipt);
        Payment  GetPaymentDetails(string paymentId);
        string CapturePayment(string paymentId, string orderId);
        bool VerifySignature(string signature, string orderId, string paymentId);
        Task<int> SavePaymentDetails(PaymentDetails model);
    }
}

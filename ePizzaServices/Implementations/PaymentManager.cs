using ePizza.Entities.Concrete;
using ePizza.Repositories.Interfaces;
using ePizza.Services.Interfaces;
using ePizza.Services.Models;
using Microsoft.Extensions.Options;
using Razorpay.Api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ePizza.Services.Implementations
{
    public class PaymentManager : IPaymentService
    {
        
        private readonly IOptions<RazorPayConfig> _razorPayConfing;
        private readonly RazorpayClient _client;
        private readonly IRepository<PaymentDetails> _repository;
        private readonly ICartRepository _cartRepository;

        public PaymentManager(IOptions<RazorPayConfig> razorPayConfing, RazorpayClient client, IRepository<PaymentDetails> repository, ICartRepository cartRepository)
        {
            _razorPayConfing = razorPayConfing;
            _client = client;
            _repository = repository;
            _cartRepository = cartRepository;
        }

        public string CapturePayment(string paymentId, string orderId)
        {
            if (string.IsNullOrWhiteSpace(paymentId))
            {
                try
                {
                    Payment payment = _client.Payment.Fetch(paymentId);
                    Dictionary<string, object> options = new Dictionary<string, object>();
                    options.Add("amount", payment.Attributes["amount"]);
                    options.Add("currency", payment.Attributes["currency"]);
                    Payment paymentCaptured = payment.Capture(options);
                    return paymentCaptured.Attributes["status"];
                }
                catch (Exception ex)
                {

                    return ex.Message;
                }
            }
            return null;
        }

        public string CreateOrder(decimal amount, string currency, string receipt)
        {
            try
            {
                Dictionary<string, object> options = new Dictionary<string, object>()
            {
                {"amount", amount },
                {"currency", currency },
                {"receipt", receipt },
            };
                Razorpay.Api.Order orderResponse = _client.Order.Create(options);
                return orderResponse["id"].ToString();
            }
            catch (Exception ex)
            {

                return ex.Message;
            }
        }

        public Payment GetPaymentDetails(string paymentId)
        {
            if (!string.IsNullOrWhiteSpace(paymentId))
            {
                return _client.Payment.Fetch(paymentId);
            }
            return null;
        }

        public async Task<int> SavePaymentDetails(PaymentDetails model)
        {
            await _repository.AddAsync(model);
            var cart = _cartRepository.FindAsync(model.CartId);
            cart.Result.IsActive = false;
            return await _repository.SaveAsync();
        }

        public bool VerifySignature(string signature, string orderId, string paymentId)
        {
            string payload = $"{orderId}{paymentId}";
            string secret = RazorpayClient.Secret;
            string actualSignature = GetActualSignature(payload, secret);
            return actualSignature.Equals(signature);
        }

        private string GetActualSignature(string payload, string secret)
        {
            byte[] secretbytes = StringEncode(secret);
            HMACSHA256 hashHmac = new HMACSHA256(secretbytes);
            var bytes = StringEncode(payload);
            return HashCode(hashHmac.ComputeHash(bytes));
        }

        private string HashCode(byte[] bytes)
        {
            return BitConverter.ToString(bytes).Replace("_", "").ToLower();
        }

        private byte[] StringEncode(string secret)
        {
            var encoding = new ASCIIEncoding();
            return encoding.GetBytes(secret);
        }
    }
}

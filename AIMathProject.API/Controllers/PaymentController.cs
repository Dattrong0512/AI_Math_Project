using AIMathProject.Infrastructure.PaymentServices.VnPay.Model;
using AIMathProject.Infrastructure.PaymentServices.VnPay.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AIMathProject.API.Controllers
{
    [Route("payment")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly ILogger<PaymentController> _logger;


        private readonly IVnPayService _vnPay;
        public PaymentController(ILogger<PaymentController> logger, IVnPayService vnPay)
        {
            _logger = logger;
            _vnPay = vnPay;
        }

        [HttpPost]
        public IActionResult CreatePaymentUrlVnPay([FromBody] PaymentInfomationModel model)
        {
            var url = _vnPay.CreatePaymentUrl(model, HttpContext);
            _logger.LogInformation(url);
            return Redirect(url);
        }

        [HttpGet("vnpay/callback")]
        public IActionResult PaymentCallBackVnPay()
        {
            var response = _vnPay.PaymentExecute(Request.Query);
            if (response.Success)
            {
                _logger.LogInformation("Payment successful. OrderId: {OrderId}, TransactionId: {TransactionId}", response.OrderId, response.TransactionId);
                return Ok(new { success = true, message = "Thanh toán thành công", data = response });
            }
            else
            {
                _logger.LogWarning("Payment failed. ResponseCode: {ResponseCode}", response.VnPayResponseCode);
                return BadRequest(new { success = false, message = "Thanh toán thất bại", data = response });
            }
        }
    }
}

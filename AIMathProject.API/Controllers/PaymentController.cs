using AIMathProject.Application.Abstracts;
using AIMathProject.Application.Command.Payment;
using AIMathProject.Application.Dto.Pagination;
using AIMathProject.Application.Dto.Payment.PaymentDto;
using AIMathProject.Application.Queries.Payment;
using AIMathProject.Infrastructure.PaymentServices.VnPay.Model;
using AIMathProject.Infrastructure.PaymentServices.VnPay.Services;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Drawing.Printing;
using System.Web;

namespace AIMathProject.API.Controllers
{
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly ILogger<PaymentController> _logger;

        private readonly ITemplateReader _templateReader;
        private readonly IVnPayService _vnPay;
        private readonly IMediator _mediator;

        public PaymentController(ILogger<PaymentController> logger, IVnPayService vnPay, ITemplateReader templateReader, IMediator mediator)
        {
            _logger = logger;
            _vnPay = vnPay;
            _templateReader = templateReader;
            _mediator = mediator;
        }
        #region CreateUrlVnPayForPlan
        /// <summary>
        /// Generates a VnPay payment URL for purchasing a study plan.
        /// </summary>
        /// <remarks>
        /// *Only logged in users can use this api*
        /// This API creates a VnPay payment URL for a specific study plan purchase by a user.
        ///
        /// **Request:**
        /// Send a request with the following route parameters:
        /// - **idPlan**: The ID of the study plan to be purchased.
        /// - **idUser**: The ID of the user making the payment.
        ///
        /// **Example Request:**
        /// ```http
        /// POST /payment/plan/2/user/1
        /// ```
        ///
        /// **Response:**
        /// ```json
        /// {
        ///     "paymentUrl": "https://sandbox.vnpayment.vn/payment/link-to-pay"
        /// }
        /// ```
        /// </remarks>
        /// <param name="idPlan">The ID of the study plan.</param>
        /// <param name="idUser">The ID of the user making the payment.</param>
        /// <returns>Returns a VnPay payment URL to redirect the user for payment.</returns>
        [Authorize(Policy = "UserOrAdmin")]
        [HttpPost("api/payment/plan/{idPlan:int}/user/{idUser:int}")]
        public async Task<IActionResult> CreatePaymentPlansUrlVnPay([FromRoute] int idPlan, [FromRoute] int idUser)
        {
            string url = await _vnPay.CreatePayment(idPlan, idUser, HttpContext);
            _logger.LogInformation(url);
            return Ok(new { paymentUrl = url });
        }
        #endregion

        #region VnPayCallback
        /// <summary>
        /// Handles the VnPay payment callback and displays the payment result.
        /// Used only by back-end, front-end doesn't care     
        /// </summary>
        [HttpGet("payment/vnpay/callback")]
        public async Task<IActionResult> PaymentCallBackVnPay()
        {
            var response = await _vnPay.PaymentExecute(Request.Query);

            _logger.LogInformation("Response from PaymentExecute: Success={Success}, ErrorMessage={ErrorMessage}, ResponseCode={ResponseCode}",
                response.Success, response.ErrorMessage, response.VnPayResponseCode);

            // Ghi log kết quả
            if (response.Success)
            {
                _logger.LogInformation("Payment successful. OrderId: {OrderId}, TransactionId: {TransactionId}",
                    response.OrderId, response.TransactionId);
            }
            else
            {
                _logger.LogWarning("Payment failed. ResponseCode: {ResponseCode}, ErrorMessage: {ErrorMessage}",
                    response.VnPayResponseCode, response.ErrorMessage);
            }

            // Đọc nội dung file HTML từ TemplateReader
            string htmlContent = await _templateReader.GetTemplate("Template/PaymentNotification.html");

            // Lấy tất cả tham số từ Request.Query
            var query = Request.Query;
            var className = response.Success ? "success" : "failure";
            var message = response.Success ? "Thanh toán thành công" : "Thanh toán thất bại";
            var errorMessage = !string.IsNullOrEmpty(response.ErrorMessage) && !response.Success
                ? $"<div class=\"payment-result failure\" style=\"margin-top: 10px;\">{response.ErrorMessage}</div>"
                : "";
            var orderId = response.OrderId != null
                ? $"<p><span class=\"label\">Mã đơn hàng:</span><span class=\"value\">{response.OrderId}</span></p>"
                : "";
            var transactionId = response.TransactionId != null
                ? $"<p><span class=\"label\">Mã giao dịch:</span><span class=\"value\">{response.TransactionId}</span></p>"
                : "";
            var vnp_Amount = query.ContainsKey("vnp_Amount")
                ? long.TryParse(query["vnp_Amount"].ToString(), out long amount)
                    ? $"<p><span class=\"label\">Số tiền:</span><span class=\"value\">{amount / 100} VND</span></p>"
                    : "<p><span class=\"label\">Số tiền:</span><span class=\"value\">Không hợp lệ</span></p>"
                : "";
            var vnp_BankCode = query.ContainsKey("vnp_BankCode")
                ? $"<p><span class=\"label\">Ngân hàng:</span><span class=\"value\">{query["vnp_BankCode"]}</span></p>"
                : "";
            var vnp_BankTranNo = query.ContainsKey("vnp_BankTranNo")
                ? $"<p><span class=\"label\">Mã giao dịch ngân hàng:</span><span class=\"value\">{query["vnp_BankTranNo"]}</span></p>"
                : "";
            var vnp_CardType = query.ContainsKey("vnp_CardType")
                ? $"<p><span class=\"label\">Loại thẻ:</span><span class=\"value\">{query["vnp_CardType"]}</span></p>"
                : "";
            var vnp_OrderInfo = query.ContainsKey("vnp_OrderInfo")
                ? $"<p><span class=\"label\">Thông tin đơn hàng:</span><span class=\"value\">{HttpUtility.UrlDecode(query["vnp_OrderInfo"])}</span></p>"
                : "";
            var vnp_PayDate = query.ContainsKey("vnp_PayDate")
                ? DateTime.TryParseExact(query["vnp_PayDate"].ToString(), "yyyyMMddHHmmss", null, System.Globalization.DateTimeStyles.None, out var payDate)
                    ? $"<p><span class=\"label\">Thời gian thanh toán:</span><span class=\"value\">{payDate:dd/MM/yyyy HH:mm:ss}</span></p>"
                    : "<p><span class=\"label\">Thời gian thanh toán:</span><span class=\"value\">Không hợp lệ</span></p>"
                : "";
            var vnp_TransactionStatus = query.ContainsKey("vnp_TransactionStatus")
                ? $"<p><span class=\"label\">Trạng thái giao dịch:</span><span class=\"value\">{query["vnp_TransactionStatus"]}</span></p>"
                : "";
            var vnp_TxnRef = query.ContainsKey("vnp_TxnRef")
                ? $"<p><span class=\"label\">Mã tham chiếu:</span><span class=\"value\">{query["vnp_TxnRef"]}</span></p>"
                : "";

            // Thay thế placeholder trong HTML
            htmlContent = htmlContent.Replace("{0}", className)
                                    .Replace("{1}", message)
                                    .Replace("{2}", orderId)
                                    .Replace("{3}", transactionId)
                                    .Replace("{4}", vnp_Amount)
                                    .Replace("{5}", vnp_BankCode)
                                    .Replace("{6}", vnp_BankTranNo)
                                    .Replace("{7}", vnp_CardType)
                                    .Replace("{8}", vnp_OrderInfo)
                                    .Replace("{9}", vnp_PayDate)
                                    .Replace("{10}", vnp_TransactionStatus)
                                    .Replace("{11}", vnp_TxnRef)
                                    .Replace("{12}", errorMessage);

            return Content(htmlContent, "text/html");
        }
        #endregion


        /// <summary>
        /// Creates a payment for a token package purchase.
        /// </summary>
        /// <remarks>
        /// *Only logged in users can use this API*
        /// This API initiates a payment process for a specific token package purchase by a user.
        ///
        /// **Request:**
        /// Send a request with the following route parameters:
        /// - **tokenPackageId**: The ID of the token package to be purchased.
        /// - **userId**: The ID of the user making the payment.
        /// 
        /// /// **Example Request:**
        /// ```http
        /// POST /payment/token/1/user/1
        /// ```
        /// 
        /// </remarks>
        /// <param name="tokenPackageId">The ID of the token package.</param>
        /// <param name="userId">The ID of the user making the payment.</param>
        /// <returns>Returns string to know success or fail</returns>
        [Authorize(Policy = "UserOrAdmin")]
        [HttpPost("api/payment/token/{tokenPackageId:int}/user/{userId:int}")]
        public async Task<ActionResult<PaymentDto>> CreatePaymentTokenPackage([FromRoute] int tokenPackageId, [FromRoute] int userId)
        {
            var payment = await _mediator.Send(new CreatePaymentTokenPackage(userId, tokenPackageId));
            if (payment == null)
            {
                return NotFound("No payment information found for the given user ID.");
            }
            return Ok(payment);
        }

        /// <summary>
        /// Retrieve the latest payment information for a specific user.
        /// </summary>
        /// <remarks>
        /// *Only logged in users can use this api*  
        /// This API fetches the latest payment details associated with a given user ID, including payment method, plan, and token package information (if available).  
        /// The response includes data such as payment ID, method ID, user ID, plan details, and transaction information.  
        ///
        /// **Request:**  
        /// Send a request with the following route parameter:  
        /// - **userId**: The ID of the user whose payment information is to be retrieved.  
        ///
        /// **Example Request:**  
        /// ```http
        /// GET /api/payment/user/1
        /// ``` 
        /// </remarks>
        /// <param name="userId">The ID of the user whose payment information is to be retrieved.</param>
        /// <returns>Returns the payment details for the user or an error if no data is found.</returns>
        /// <response code="200">Returns the payment information successfully.</response>
        /// <response code="404">Indicates that no payment information was found for the given user ID.</response>
        [Authorize(Policy = "UserOrAdmin")]
        [HttpGet("api/payment/user/{userId:int}")]
        public async Task<ActionResult<PaymentDto>> GetLatestInforUserPaymentById([FromRoute] int userId)
        {
            return Ok(await _mediator.Send(new GetLatestInfoPayment(userId)));

        }

        /// <summary>
        /// Retrieves all payment information for a specific user.
        /// </summary>
        /// <remarks>
        /// *Only logged in users can use this api*  
        /// This API fetches the latest payment details associated with a given user ID, including payment method, plan, and token package information (if available).  
        /// The response includes data such as payment ID, method ID, user ID, plan details, and transaction information.  
        ///
        /// **Request:**  
        /// Send a request with the following route parameter:  
        /// - **userId**: The ID of the user whose payment information is to be retrieved.  
        ///
        /// **Example Request:**  
        /// ```http
        /// GET /api/payment/user/1/all
        /// ``` 
        /// </remarks>
        /// <param name="userId">The ID of the user whose payment information is to be retrieved.</param>
        /// <returns>Returns the payment details for the user or an error if no data is found.</returns>
        /// <response code="200">Returns the payment information successfully.</response>
        /// <response code="404">Indicates that no payment information was found for the given user ID.</response>
        [Authorize(Policy = "UserOrAdmin")]
        [HttpGet("api/payment/user/{userId:int}/all")]
        public async Task<ActionResult<PaymentDto>> GetAllInforUserPaymentById([FromRoute] int userId)
        {
            return Ok(await _mediator.Send(new GetAllPaymentInfoQuery(userId)));
        }

        /// <summary>
        /// Get all system payments with pagination
        /// </summary>
        /// <remarks>
        /// *Only admin users can access this API*
        /// 
        /// This API retrieves all payment records in the system with pagination support.
        /// Results are ordered by date in descending order (most recent first).
        /// 
        /// **Path Parameters:**
        /// - **pageIndex**: Zero-based page index (0 for first page)
        /// - **pageSize**: Number of items per page (recommended: 10-50)
        /// 
        /// **Example Requests:**
        /// - First page with 10 items: `/api/payments/pageindex/0/pagesize/10`
        /// - Second page with 20 items: `/api/payments/pageindex/1/pagesize/20`
        /// </remarks>
        /// <param name="pageIndex">Zero-based page index (0 for first page)</param>
        /// <param name="pageSize">Number of items per page</param>
        /// <returns>A list of all payments matching the filter criteria</returns>

        [Authorize(Policy = "Admin")]
        [HttpGet("api/payments/pageindex/{pageIndex:int}/pagesize/{pageSize:int}")]
        public async Task<ActionResult<Pagination<PaymentDto>>> GetAllPaymentsPaginated(
            [FromRoute] int pageIndex,
            [FromRoute] int pageSize)
        {
            if (pageIndex < 0)
            {
                return BadRequest("Page index must be 0 or greater.");
            }

            if (pageSize <= 0 || pageSize > 100)
            {
                return BadRequest("Page size must be between 1 and 100.");
            }

            var payments = await _mediator.Send(new GetAllPaymentsPaginatedQuery(pageIndex, pageSize));
            return Ok(payments);
        }


        /// <summary>
        /// Retrieves paginated payment information for a specific user.
        /// </summary>
        /// <remarks>
        /// *Only logged in users can use this API*  
        /// This API fetches paginated payment details for a specific user, including payment method, plan, and token package information.
        /// Results are ordered by date in descending order (most recent first).
        /// 
        /// **Path Parameters:**
        /// - **userId**: The ID of the user whose payment information is to be retrieved
        /// - **pageIndex**: Zero-based page index (0 for first page)
        /// - **pageSize**: Number of items per page (recommended: 10-50)
        ///
        /// **Example Request:**  
        /// ```http
        /// GET /api/payment/user/1/pageindex/0/pagesize/10
        /// ``` 
        /// </remarks>
        /// <param name="userId">The ID of the user whose payment information is to be retrieved</param>
        /// <param name="pageIndex">Zero-based page index (0 for first page)</param>
        /// <param name="pageSize">Number of items per page</param>
        /// <returns>Returns paginated payment details for the user</returns>
        /// <response code="200">Returns the payment information successfully.</response>
        /// <response code="400">Indicates invalid pagination parameters.</response>
        [Authorize(Policy = "UserOrAdmin")]
        [HttpGet("api/payment/user/{userId:int}/pageindex/{pageIndex:int}/pagesize/{pageSize:int}")]
        public async Task<ActionResult<Pagination<PaymentDto>>> GetAllPaymentsByUserPaginated(
            [FromRoute] int userId,
            [FromRoute] int pageIndex,
            [FromRoute] int pageSize)
        {
            if (pageIndex < 0)
            {
                return BadRequest("Page index must be 0 or greater.");
            }

            if (pageSize <= 0 || pageSize > 100)
            {
                return BadRequest("Page size must be between 1 and 100.");
            }

            var payments = await _mediator.Send(new GetAllPaymentsByUserPaginatedQuery(userId, pageIndex, pageSize));
            return Ok(payments);
        }
    }
}

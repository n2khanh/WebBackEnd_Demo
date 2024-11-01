using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using Zoo_template.Models;

namespace Zoo_template.Controllers
{
    public class HomeCustomer : Controller
    {
        private readonly ZooContext _db;

        public HomeCustomer(ZooContext db)
        {
            _db = db; // Use dependency injection for the DbContext
        }

        public IActionResult GetTicketPrice(int? TicketID)
        {
            if (TicketID == null)
            {
                return Json(new { success = false, message = "Lỗi" });
            }

            var ticket = _db.TTickets.Find(TicketID);

            if (ticket == null)
            {
                return Json(new { success = false, message = "Không tìm thấy vé" });
            }

            return Json(new { success = true, price = ticket.Price });
        }

        public IActionResult Index()
        {
            return View("CustomerIndex");
        }

        private IEnumerable<SelectListItem> GetTicketOptions()
        {
            return _db.TTickets.Select(t => new SelectListItem
            {
                Value = t.TicketId.ToString(),
                Text = t.TicketName
            }).ToList();
        }

        private IEnumerable<SelectListItem> GetPaymentMethodOptions()
        {
            return _db.TPayMethods.Select(pm => new SelectListItem
            {
                Value = pm.PayMethodId.ToString(),
                Text = pm.MethodName
            }).ToList();
        }

        [HttpGet]
        public IActionResult Order()
        {
            // Populate dropdowns for the initial GET request
            ViewBag.Ticket = GetTicketOptions();
            ViewBag.PayMethod = GetPaymentMethodOptions();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Order(TGuest model)
        {
            // Check if the model state is valid
            if (!ModelState.IsValid)
            {
                // Repopulate dropdowns on validation failure
                ViewBag.Ticket = GetTicketOptions();
                ViewBag.PayMethod = GetPaymentMethodOptions();
                return View(model); // Return the view with model errors
            }

            // Retrieve the selected payment method
            var selectedPaymentMethod = await _db.TPayMethods
                .FirstOrDefaultAsync(pm => pm.PayMethodId == model.PayMethodID);

            if (selectedPaymentMethod != null) // If the payment method exists
            {
                _db.TGuests.Add(model);
                await _db.SaveChangesAsync();
                if (selectedPaymentMethod.MethodName == "Tiền Mặt")
                {
                    return RedirectToAction("Success");
                }
                else if (selectedPaymentMethod.MethodName == "Chuyển Khoản")
                {
                    return RedirectToAction("QRcode");
                }
               
            }

            // If we reach here, the payment method is invalid
            ModelState.AddModelError("PayMethodID", "Phương thức thanh toán không hợp lệ.");
            ViewBag.Ticket = GetTicketOptions(); // Repopulate dropdowns
            ViewBag.PayMethod = GetPaymentMethodOptions();
            return View(model);
        }

        [HttpGet]
        public IActionResult Success()
        {
            ViewBag.Message = "Đặt vé thành công!";
            return View();
        }

        [HttpGet]
        public IActionResult QRcode()
        {
            return View(); // Return the online payment view
        }
    }
}
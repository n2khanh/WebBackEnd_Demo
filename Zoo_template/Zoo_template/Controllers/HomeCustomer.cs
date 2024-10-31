using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using Zoo_template.Models;

namespace Zoo_template.Controllers
{
    public class HomeCustomer : Controller
    {
        public IActionResult GetTicketPrice(int? TicketID)
        {
            if (TicketID == null)
            {
                return Json(new { success = false, message = "Lỗi" });
            }

            using (var db = new ZooContext())
            {
                var ticket = db.TTickets.Find(TicketID); 

                if (ticket == null)
                {
                    return Json(new { success = false, message = "Không tìm thấy vé" });
                }

                return Json(new { success = true, price = ticket.Price }); // Trả về giá vé dưới dạng JSON
            }
        }

        public IActionResult Index()
        {
            return View("CustomerIndex");
        }

        public async Task<IActionResult> Order()
        {
            using (var db = new ZooContext())
            {
                var paymentMethods = db.TPayMethods.ToList(); // Giả sử bạn có bảng PaymentMethods trong database
                ViewBag.PayMethod = new SelectList(paymentMethods, "PayMethodId", "MethodName"); // Thay "Id" và "Name" bằng tên trường thực tế của bạn
                var tickets = await db.TTickets.ToListAsync();
                ViewBag.TTicket = new SelectList(tickets, "TicketId", "TicketName");

                // Lưu giá vé vào ViewBag
                var ticketPrices = tickets.ToDictionary(ticket => ticket.TicketId, ticket => ticket.Price);

                ViewBag.TicketPrices = ticketPrices;
            }

            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Order(TGuest model)
        {
            using (var db = new ZooContext())
            {

                // Lưu thông tin đặt vé vào cơ sở dữ liệu (nếu cần thiết)
                db.TGuests.Add(model); // Lưu thông tin khách hàng
                await db.SaveChangesAsync(); // Lưu thay đổi

                // Lấy danh sách phương thức thanh toán từ cơ sở dữ liệu
                var paymentMethods = await db.TPayMethods.ToListAsync();

                // Chuyển hướng tùy theo phương thức thanh toán
                var selectedPaymentMethod = paymentMethods.FirstOrDefault(pm => pm.PayMethodId == model.PayMethodID);

                if (selectedPaymentMethod != null) // Nếu phương thức thanh toán tồn tại
                {
                    if (selectedPaymentMethod.MethodName == "Thanh Toán Trực Tiếp")
                    {
                        return RedirectToAction("Success");
                    }
                    else if (selectedPaymentMethod.MethodName == "Thanh Toán Online")
                    {
                        return RedirectToAction("PaymentPage");
                    }
                }
                ModelState.AddModelError("PayMethodID", "Phương thức thanh toán không hợp lệ.");
                return View(model);
            }
        }

        public IActionResult Success()
        {
            return View(); // Trả về view thông báo thành công
        }

        public IActionResult PaymentPage()
        {
            return View(); // Trả về view thanh toán online
        }


    }
}

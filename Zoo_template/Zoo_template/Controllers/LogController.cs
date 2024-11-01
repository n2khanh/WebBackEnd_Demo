using Microsoft.AspNetCore.Mvc;
using Zoo_template.Models;

namespace Zoo_template.Controllers
{
    public class LogController : Controller
    {
        //DangNhap
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(string UserName, string password)
        {
            using (var db = new ZooContext())
            {
                var user = db.TLogin.SingleOrDefault(m => m.UserName.ToLower() == UserName.ToLower());

                if (user == null)
                {
                    ViewBag.error = "Tài khoản không tồn tại";
                    ViewBag.UserName = UserName;
                    return View();
                }

                if (user.Password != password)
                {
                    ViewBag.UserName = UserName;
                    ViewBag.error = "Mật khẩu không chính xác";
                    return View();
                }
                HttpContext.Session.SetString("user", user.UserName);
               if(user.UserName == "Admin") return RedirectToAction("Index", "Home");
               else return RedirectToAction("Index", "HomeCustomer");


            }


        }
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
      
        public IActionResult Register(TLogin model)
        {
            if (ModelState.IsValid)
            {
                using (var db = new ZooContext())
                {
                    // Kiểm tra xem tên người dùng đã tồn tại chưa
                    var existingUser = db.TLogin.SingleOrDefault(u => u.UserName.ToLower() == model.UserName.ToLower());
                    if (existingUser != null)
                    {
                        ViewBag.error = "Tên người dùng đã tồn tại.";
                        return View(model);
                    }

                    // Tạo đối tượng User mới
                    var newUser = new TLogin
                    {
                        UserName = model.UserName,
                        Password = model.Password, // Mã hóa mật khẩu trước khi lưu
                        Name = model.Name,
                        Age = model.Age,
                        Gender = model.Gender,
                        Email = model.Email
                    };

                    // Lưu thông tin người dùng vào cơ sở dữ liệu
                    db.TLogin.Add(newUser); // Sử dụng bảng TLogin để lưu thông tin người dùng
                    db.SaveChanges();

                    // Chuyển hướng đến trang đăng nhập
                    return RedirectToAction("Login");
                }
            }

            // Nếu model không hợp lệ, trả về view với model hiện tại
            return View(model);
        }
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();

            return RedirectToAction("Login", "Log");
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;
using TestWebSIte.Data;
using TestWebSIte.Models;
using TestWebSIte.ViewModel;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TestWebSIte.Controllers
{
    public class AccountController : Controller
    {
        private readonly ILogger<AccountController> _logger;
        private readonly IConfiguration _configuration;

        public AccountController(IConfiguration configuration, ILogger<AccountController> logger)
        {
            _configuration = configuration;
            _logger = logger;
        }
        //public IConfiguration _configuration { get; }
        //public ILogger _logger { get; }

        // 관리자or사용자or아무나 : 로그인 View 보기
        // GET: /<controller>/
        public IActionResult Login()
        {
            // 로그인 되어있을때 로그인화면으로 가게된다면 세션 리무브(로그아웃) 시키고 로그인 화면 보여주기
            if (HttpContext.Session.GetInt32("USER_LOGIN_KEY") != null)
            {
                HttpContext.Session.Remove("USER_LOGIN_KEY");
            }


            return View();
        }

        // 관리자or사용자or아무나 : 로그인 기능 실행
        [HttpPost]
        public IActionResult Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                using (var db = new BoardDbContext())
                {
                    var user = db.Users.FirstOrDefault(u => u.UserId.Equals(model.UserId) && u.UserPw.Equals(model.UserPw));

                    if (user != null)
                    {
                        HttpContext.Session.SetInt32("USER_LOGIN_KEY", user.UserNo);
                        return RedirectToAction("LoginSuccess", "Home");
                    }
                }
                ModelState.AddModelError(string.Empty, "사용자 ID 혹은 비밀번호가 올바르지 않습니다.");
            }
            return View(model);
        }

        // 관리자or사용자 : 로그아웃 ( 세션에서 제외 )
        public IActionResult Logout()
        {
            HttpContext.Session.Remove("USER_LOGIN_KEY");
            return RedirectToAction("Index", "Home");
        }

        // 관리자or사용자or아무나 : 아이디 or 비밀번호 찾기 View
        public IActionResult FindAccount()
        {
            return View();
        }

        // 관리자or사용자or아무나 : 아이디 찾기 View
        public IActionResult FindId()
        {
            return View();
        }

        // 관리자or사용자or아무나 : 아이디 찾기 기능 실행
        [HttpPost]
        public IActionResult FindId(FindIdViewModel model)
        {
            if (ModelState.IsValid)
            {
                using (var db = new BoardDbContext())
                {
                    var user = db.Users.FirstOrDefault(u => u.UserEmail.Equals(model.UserEmail));

                    if (user != null)
                    {
                        var id = user.UserId.ToString();
                        ViewBag.id = id;
                        return View();
                    }
                }
                ModelState.AddModelError(string.Empty, "입력하신 Email에 일치하는 ID가 없습니다.");
            }
            return View(model);
        }

        // 관리자or사용자or아무나 : 비밀번호 찾기 View
        public IActionResult FIndPw()
        {
            return View();
        }

        // 관리자or사용자or아무나 : 비밀번호 찾기 기능 실행
        [HttpPost]
        public IActionResult FindPw(FindPwViewModel model)
        {
            if (ModelState.IsValid)
            {
                using (var db = new BoardDbContext())
                {
                    var user = db.Users.FirstOrDefault(u => u.UserId.Equals(model.UserId) && u.UserEmail.Equals(model.UserEmail));

                    if (user != null)
                    {
                        var pw = user.UserPw.ToString();
                        ViewBag.pw = pw;
                        return View();
                    }
                }
                ModelState.AddModelError(string.Empty, "입력하신 ID 또는 Email에 일치하는 비밀번호가 없습니다.");
            }
            return View(model);
        }

        // 사용자 본인 or 관리자 : 정보 수정 View
        public IActionResult ChangeInfo()
        {
            if (HttpContext.Session.GetInt32("USER_LOGIN_KEY") == null)
            {
                return RedirectToAction("Login", "Account");
            }
            // 현재 로그인된 User의 UserNo 가져오기
            var id = int.Parse(HttpContext.Session.GetInt32("USER_LOGIN_KEY").ToString());
            if (id == 0)
            {
                return NotFound();
            }

            using (var db = new BoardDbContext())
            {
                // 현재 로그인된 User의 UserNo로 Db에 해당하는 유저객체 가져오기
                var user = db.Users.Find(id);
                if (user != null)
                {
                    return View(user);
                }
            }
            return RedirectToAction("Index", "Home");
        }

        // 사용자 본인 or 관리자 : 정보 수정 기능 실행
        [HttpPost]
        public IActionResult ChangeInfo(User user)
        {
            if (HttpContext.Session.GetInt32("USER_LOGIN_KEY") == null)
            {
                return RedirectToAction("Login", "Account");
            }
            var id = int.Parse(HttpContext.Session.GetInt32("USER_LOGIN_KEY").ToString());
            if (id != user.UserNo)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {

                using (var db = new BoardDbContext())
                {
                    db.Update(user);
                    db.SaveChanges();
                }
                return RedirectToAction("ChangeMyInforSuccess", "Home");
            }
            return View(user);
        }

        // 사용자 본인 or 관리자 : 정보수정시 비밀번호 체크 View
        public IActionResult ChkPw()
        {
            if (HttpContext.Session.GetInt32("USER_LOGIN_KEY") == null)
            {
                return RedirectToAction("Login", "Account");
            }
            return View();
        }

        // 사용자 본인 or 관리자 : 정보수정시 비밀번호 체크 기능 실행
        [HttpPost]
        public IActionResult ChkPw(ChkPwViewModel model)
        {
            if (HttpContext.Session.GetInt32("USER_LOGIN_KEY") == null)
            {
                return RedirectToAction("Login", "Account");
            }
            var id = int.Parse(HttpContext.Session.GetInt32("USER_LOGIN_KEY").ToString());
            if (id == 0)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                using (var db = new BoardDbContext())
                {
                    var user = db.Users.Find(id);
                    var pw = user.UserPw;
                    if (pw.Equals(model.UserPw))
                    {
                        return RedirectToAction("ChangeInfo", "Account");
                    }
                }
                ModelState.AddModelError(string.Empty, "비밀번호를 잘못 입력하셨습니다.");
            }
            return View();
        }

        // 아무나 : 회원가입 VIew
        [AllowAnonymous]
        public IActionResult SignUp()
        {
            ViewData["ReCaptchaKey"] = _configuration.GetSection("GoogleReCaptcha:key").Value;
            return View();
        }

        // 아무나 : 회원가입 기능 실행
        [HttpPost]
        public IActionResult SignUp(User model)
        {
            ViewData["ReCaptchaKey"] = _configuration.GetSection("GoogleReCaptcha:key").Value;

            model.SignUpYear = DateTime.Now.ToString("yyyy");
            model.SignUpMonth = DateTime.Now.ToString("MM");
            model.SignUpDay = DateTime.Now.ToString("MM'/'dd'/'yyyy");

            if (ModelState.IsValid)
            {
                if (!ReCaptchaPassed(
                    Request.Form["g-recaptcha-response"], // that's how you get it from the Request object
                    _configuration.GetSection("GoogleReCaptcha:secret").Value,
                    _logger
                    ))
                {
                    ModelState.AddModelError(string.Empty, "넌 틀렸어. 이 멍청한 로봇아! Go play some 1x1 on SFs instead.");
                    return View(model);
                }
                using (var db = new BoardDbContext())
                {
                    db.Users.Add(model);
                    db.SaveChanges();
                }
                return RedirectToAction("Index", "Home");
            }

            return View(model);
        }

        // @@@ 아이디 중복체크 - 구현중 @@@
        public int IdChkForm()
        {
            var userId = Request.Query["UserId"];
            using (var db = new BoardDbContext())
            {
                var user = db.Users.ToList();
                var user_id = from u in user
                              select u;
                user_id = user_id.Where(u => u.UserId == userId);
                if (userId.Equals(""))
                {
                    return 2;
                }
                else if (user_id.Count() != 0)
                {
                    return 1;
                }
            }
            return 0;
        }


        /// <summary>
        /// 본인 사용자 : 내 정보 보기 View
        /// </summary>
        /// <param name="userNo"></param>
        /// <returns></returns>
        public IActionResult Detail()
        {
            if (HttpContext.Session.GetInt32("USER_LOGIN_KEY") == null)
            {
                return RedirectToAction("Login", "Account");
            }

            var userNo = int.Parse(HttpContext.Session.GetInt32("USER_LOGIN_KEY").ToString());

            using (var db = new BoardDbContext())
            {
                var user = db.Users.FirstOrDefault(u => u.UserNo.Equals(userNo));
                return View(user);
            }
        }

        /////////////////////////////////////////////// -- 2020 - 05 - 13 추가 사항
        /// <summary>
        /// 관리자 - 회원정보보기
        /// </summary>
        /// <returns></returns>
        public IActionResult ViewMember()
        {
            return View();
        }
        //test1111111

        ///////////////////////////

        public static bool ReCaptchaPassed(string gRecaptchaResponse, string secret, ILogger logger)
        {
            HttpClient httpClient = new HttpClient();
            var res = httpClient.GetAsync($"https://www.google.com/recaptcha/api/siteverify?secret={secret}&response={gRecaptchaResponse}").Result;
            if (res.StatusCode != HttpStatusCode.OK)
            {
                logger.LogError("Error while sending request to ReCaptcha");
                return false;
            }

            string JSONres = res.Content.ReadAsStringAsync().Result;
            dynamic JSONdata = JObject.Parse(JSONres);
            if (JSONdata.success != "true")
            {
                return false;
            }

            return true;
        }
    }
}

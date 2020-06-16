using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
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
            var pw = Request.Form["UserPw"].ToString(); // 사용자가 입력한 비밀번호
            var db2 = new BoardDbContext(); // db 연결
            var IdUser = db2.Users.FirstOrDefault(u => u.UserId.Equals(model.UserId)); // 사용자가 입력한 ID랑 일치하는 ID를 DB에서 유저 객체로 가져와서
            if (IdUser != null) // 일치하는 유저가 존재할때
            {
                // 해쉬 생성
                string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: pw, // 사용자가 입력한 비밀번호
                salt: IdUser.UserSalt, // DB에서 가져온 유저의 Salt값
                prf: KeyDerivationPrf.HMACSHA1, // 해쉬 : SHA1
                iterationCount: 10000, // 10000번 반복
                numBytesRequested: 256 / 8 // 길이 32
                ));

                model.UserPw = hashed; // 사용자가 입력한 비밀번호 해시 값

                if (ModelState.IsValid) // DB에 Null 포함 값이 다 존재 한다면
                {
                    using (var db = new BoardDbContext())
                    {
                        // 사용자가 입력한 ID && 사용자가 입력한 비밀번호 해시값과 DB에 담긴 ID와 해시값이 각각 같은 유저 객체 하나 가져오기
                        var user = db.Users.FirstOrDefault(u => u.UserId.Equals(model.UserId) && u.UserPw.Equals(model.UserPw));

                        if (user != null) // 유저가 존재하면
                        {
                            HttpContext.Session.SetInt32("USER_LOGIN_KEY", user.UserNo); // 세션에 유저 로그인키로 가져온 유저의 No 저장
                            return RedirectToAction("LoginSuccess", "Home"); // 로그인 성공 화면 출력
                        }
                    }
                }
            }
            ModelState.AddModelError(string.Empty, "사용자 ID 혹은 비밀번호가 올바르지 않습니다."); // ID 또는 비밀번호가 일치하지 않을때
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
                    var user = db.Users.FirstOrDefault(u => u.UserEmail.Equals(model.UserEmail)); // 사용자가 입력한 Email과 DB에 해당하는 유저중 Email이 같은 유저객체 하나 가져오기

                    if (user != null)
                    {
                        // DB에서 가져온 유저객체의 ID를 가져와서 
                        var id = user.UserId.ToString();
                        ViewBag.id = id; // ViewBag에다가 id값을 넣는다
                        return View();
                    }
                }
                ModelState.AddModelError(string.Empty, "입력하신 Email에 일치하는 ID가 없습니다."); // 사용자가 입력한 Email에 해당하는 유저가 없을시 Error 문구 노출
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
                    // 사용자가 입력한 ID && 사용자가 입력한 비밀번호 해시값과 DB에 담긴 ID와 해시값이 각각 같은 유저 객체 하나 가져오기
                    var user = db.Users.FirstOrDefault(u => u.UserId.Equals(model.UserId) && u.UserEmail.Equals(model.UserEmail));
                    if (user != null)
                    {
                        // 해당하는 유저의 비밀번호 가져오기
                        var pw = user.UserPw.ToString();
                        ViewBag.pw = pw; // ViewBag에 가져온 비밀번호 담기
                        return View();
                    }
                }
                ModelState.AddModelError(string.Empty, "입력하신 ID 또는 Email에 일치하는 비밀번호가 없습니다."); // 사용자가 입력한 ID 또는 Email에 해당하는 유저가 없을시 Error 문구 노출
            }
            return View(model);
        }

        // 사용자 본인 or 관리자 : 정보 수정 View
        public IActionResult ChangeInfo()
        {
            // 로그인 안되어있을시
            if (HttpContext.Session.GetInt32("USER_LOGIN_KEY") == null)
            {
                // 로그인 페이지로 이동
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
            // 현재 로그인 상태가 아니면
            if (HttpContext.Session.GetInt32("USER_LOGIN_KEY") == null)
            {
                // 로그인 페이지로 이동
                return RedirectToAction("Login", "Account");
            }
            // 현재 접속중인 유저의 No를 가져와서
            var id = int.Parse(HttpContext.Session.GetInt32("USER_LOGIN_KEY").ToString());

            // 가져온 유저의 No와 일치하는 유저가 없으면
            if (id != user.UserNo)
            {
                return NotFound();
            }

            // 사용자가 입력한 패스워드 가져오기
            var pw = Request.Form["UserPw"].ToString();

            // 16바이트 랜덤 바이트 값 가져오기 ( 솔트 )
            byte[] salt = new byte[128 / 8];
            using (var rng = RandomNumberGenerator.Create())
            {
                // 랜덤함수 돌리고
                rng.GetBytes(salt);
            }

            // 해시값 생성 ( 사용자가 입력한 패스워드 , 16바이트 랜덤 바이트 (솔트 값) , 암호화 형식 (SHA1) , 해시할 횟수 , 생성될 바이트 길이 ( 32 바이트 )
            string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: pw,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA1,
                iterationCount: 10000,
                numBytesRequested: 256 / 8
                ));

            user.UserPw = hashed; // 생성된 해시값을 DB에 넣을 유저 패스워드에 값을 넣고
            user.UserSalt = salt; // 새로 생성된 솔트값을 DB에 넣을 유저 솔트에 값을 넣고

            if (ModelState.IsValid)
            {
                using (var db = new BoardDbContext())
                {
                    // DB 업데이트
                    db.Update(user);
                    // DB 저장 and 체인지
                    db.SaveChanges();
                }
                // 성공하면 정보 변경 성공 화면으로 이동
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
            // 현재 로그인 중인 유저의 No값 가져오기
            var id = int.Parse(HttpContext.Session.GetInt32("USER_LOGIN_KEY").ToString());
            if (id == 0)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                using (var db = new BoardDbContext())
                {
                    // 가져온 유저의 No값으로 DB에서 해당하는 유저 객체를 가져온다
                    var user = db.Users.Find(id);
                    var pw = user.UserPw; // DB에 저장되어있는 Password

                    string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                        password: model.UserPw, // 사용자가 입력한 Password
                        salt: user.UserSalt,
                        prf: KeyDerivationPrf.HMACSHA1,
                        iterationCount: 10000,
                        numBytesRequested: 256 / 8
                        ));

                    // 생성된 해시값이랑 DB에 저장되어있는 해시값이 같으면  
                    if (pw.Equals(hashed))
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
            // View단에 사용될 리캡챠 키값
            ViewData["ReCaptchaKey"] = _configuration.GetSection("GoogleReCaptcha:key").Value;
            // 사용자가 입력한 패스워드값(해시 전)
            var pw = Request.Form["UserPw"].ToString();

            // DB에 저장될 회원가입 당시의 날짜 , 월 , 년
            model.SignUpYear = DateTime.Now.ToString("yyyy");
            model.SignUpMonth = DateTime.Now.ToString("MM");
            model.SignUpDay = DateTime.Now.ToString("MM'/'dd'/'yyyy");

            byte[] salt = new byte[128 / 8];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(salt);
            }

            string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: pw,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA1,
                iterationCount: 10000,
                numBytesRequested: 256 / 8
                ));

            model.UserPw = hashed;
            model.UserSalt = salt;

            if (ModelState.IsValid)
            {
                // 리캡챠 패스가 안됫을 경우 -> 리캡챠 통과 안됫을 경우
                if (!ReCaptchaPassed(
                    Request.Form["g-recaptcha-response"], // that's how you get it from the Request object
                    _configuration.GetSection("GoogleReCaptcha:secret").Value,
                    _logger
                    ))
                {
                    ModelState.AddModelError(string.Empty, "넌 틀렸어. 이 멍청한 로봇아! Go play some 1x1 on SFs instead."); // 에러 문구 노출
                    return View(model);
                }
                // 리캡챠 패스됫을 경우
                using (var db = new BoardDbContext())
                {
                    db.Users.Add(model); // DB에 생성한 유저 객체 Add
                    db.SaveChanges(); // DB 저장 and 체인지
                }
                return RedirectToAction("Index", "Home"); // 회원가입 성공시 Home/Index/
            }

            return View(model);
        }

        // @@@ 아이디 중복체크 - 구현완료 - ajax로 구현함! @@@
        public int IdChkForm()
        {
            // ajax로 get방식의 url에 있는 userId 값 가져오기
            var userId = Request.Query["UserId"];
            using (var db = new BoardDbContext())
            {
                var user = db.Users.ToList(); // DB에 있는 모든 유저객체 가져오기
                // 가져온 유저객체들 에서 Linq쿼리 실행 
                var user_id = from u in user
                              select u;
                user_id = user_id.Where(u => u.UserId == userId); // 실행한 Linq쿼리에서 where 조건절 추가해서 ajax로 가져온 userId값과 가져온 유저객체들 중 ID가 일치하는 유저 객체 가져오기
                if (userId.Equals(""))
                {
                    return 2; // input text id창에 사용자가 입력한 값이 공백이면 return 2
                }
                else if (user_id.Count() != 0)
                {
                    return 1; // where 조건절 실행후 일치하는 유저가 1명 이상이면 return 1 -> 즉, ID중복이면.
                }
            }
            return 0; // 이외에 모든 경우엔 return 0 -> 아이디 중복이 아닌경우
        }


        /// <summary>
        /// 본인 사용자 : 내 정보 보기 View
        /// </summary>
        /// <param name="userNo"></param>
        /// <returns></returns>
        public IActionResult Detail()
        {
            // 로그인 안되어있으면 Login 페이지로 이동
            if (HttpContext.Session.GetInt32("USER_LOGIN_KEY") == null)
            {
                return RedirectToAction("Login", "Account");
            }
            // 현재 로그인 되어있는 유저의 No를 세션에서 가져와서
            var userNo = int.Parse(HttpContext.Session.GetInt32("USER_LOGIN_KEY").ToString());

            using (var db = new BoardDbContext())
            {
                // 세션에서 가져온 userNo와 DB에 있는 유저객체들의 userNo를 비교하여 일치하는 유저객체 하나를 가져온다.
                var user = db.Users.FirstOrDefault(u => u.UserNo.Equals(userNo));
                return View(user);
            }
        }

        ////////////////////////////////////////// -- ReCaptcha API
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

        ///////////// -- TEST 01
    }
}

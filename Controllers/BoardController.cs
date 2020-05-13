﻿using System;
using X.PagedList;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TestWebSIte.Data;
using TestWebSIte.Models;
using System.Reflection.Metadata;
using System.Collections.Generic;
using TestWebSIte.ViewModel;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Threading.Tasks;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TestWebSIte.Controllers
{
    public class BoardController : Controller
    {

        /// <summary>
        /// 게시물 보기 - 아무나
        /// </summary>
        /// <returns></returns>
        // GET: /<controller>/
        public IActionResult Index(int? Page)
        {
            using (var db = new BoardDbContext())
            {
                var list = db.Boards.ToList();
                var PageNo = Page ?? 1;
                var PageSize = 5;

                return View(list.ToPagedList(PageNo, PageSize));
            }
        }

        // 관리자 : 상품 추가 VIew
        public IActionResult Add()
        {
            if (HttpContext.Session.GetInt32("USER_LOGIN_KEY") == null)
            {
                return RedirectToAction("Login", "Account");
            }
            return View();
        }

        /// <summary>
        /// 관리자 : 상품 추가 기능 실행
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Add(Board model)
        {
            if (HttpContext.Session.GetInt32("USER_LOGIN_KEY") == null)
            {
                return RedirectToAction("Login", "Account");
            }
            model.UserNo = int.Parse(HttpContext.Session.GetInt32("USER_LOGIN_KEY").ToString());

            if (ModelState.IsValid)
            {
                model.Regdate = DateTime.Now.ToString("yyyy-MM-dd");
                model.Modidate = DateTime.Now.ToString("yyyy-MM-dd");
                using (var db = new BoardDbContext())
                {
                    db.Boards.Add(model);
                    if (db.SaveChanges() > 0)
                    {
                        return Redirect("Index");
                    }
                }
            }
            ModelState.AddModelError(string.Empty, "게시물을 저장할 수 없습니다.");
            return View();
        }

        /// <summary>
        /// 관리자 : 상품 정보 상세보기 VIew
        /// </summary>
        /// <param name="boardNo"></param>
        /// <returns></returns>
        public IActionResult Detail(int boardNo)
        {

            using (var db = new BoardDbContext())
            {
                var board = db.Boards.FirstOrDefault(b => b.BoardNo.Equals(boardNo));
                return View(board);
            }
        }

        // 관리자 : 상품 정보 수정 View
        public IActionResult Edit(int boardNo)
        {
            if (HttpContext.Session.GetInt32("USER_LOGIN_KEY") == null)
            {
                return RedirectToAction("Login", "Account");
            }

            using (var db = new BoardDbContext())
            {
                var board = db.Boards.FirstOrDefault(b => b.BoardNo.Equals(boardNo));
                return View(board);
            }
        }

        // 관리자 : 상품 정보 수정 기능 실행
        [HttpPost]
        public IActionResult Edit(Board model)
        {
            if (HttpContext.Session.GetInt32("USER_LOGIN_KEY") == null)
            {
                return RedirectToAction("Login", "Account");
            }
            //if (model.UserNo != int.Parse(HttpContext.Session.GetInt32("USER_LOGIN_KEY").ToString()))
            //{
            //    return RedirectToAction("Index" , "Home");
            //}
            if (ModelState.IsValid)
            {
                model.Modidate = DateTime.Now.ToString("yyyy-MM-dd");

                using (var db = new BoardDbContext())
                {
                    var board = db.Boards.Update(model);
                    db.SaveChanges();

                    return Redirect("Index");
                }
            }
            ModelState.AddModelError(string.Empty, "수정이 불가합니다.");
            return View();
        }

        /// <summary>
        /// 게시물 삭제 - 관리자가 쓰는 Delete ( 데이터에서 완전 삭제되는것 ) ... 본인도 써야할듯..Index에서 Data에 있는거 다가져옴
        /// </summary>
        /// <param name="boardNo"></param>
        /// <returns></returns>
        public IActionResult Delete(int boardNo)
        {
            if (HttpContext.Session.GetInt32("USER_LOGIN_KEY") == null)
            {
                return RedirectToAction("Login", "Account");
            }

            using (var db = new BoardDbContext())
            {
                var board = db.Boards.FirstOrDefault(b => b.BoardNo.Equals(boardNo));
                db.Boards.Remove(board);
                db.SaveChanges();
            }
            return Redirect("Index");
        }

        // 사용자 : 상품 리스트 View
        public IActionResult IndexCus(int? Page)
        {
            using (var db = new BoardDbContext())
            {
                var list = db.Boards.ToList();
                var PageNo = Page ?? 1;
                var PageSize = 5;

                return View(list.ToPagedList(PageNo, PageSize));
            }
        }

        // 사용자 : 주문하기 View
        public IActionResult Order(int boardNo)
        {
            if (HttpContext.Session.GetInt32("USER_LOGIN_KEY") == null)
            {
                return RedirectToAction("Login", "Account");
            }
            using (var db = new BoardDbContext())
            {
                var board = db.Boards.FirstOrDefault(b => b.BoardNo.Equals(boardNo));
                return View(board);
            }
        }

        // 사용자 : 주문하기 기능 실행
        [HttpPost]
        public IActionResult Order(Order model, int boardNo)
        {
            var userNo = int.Parse(HttpContext.Session.GetInt32("USER_LOGIN_KEY").ToString());
            model.OrderDay = DateTime.Now.ToString("MM'/'dd'/'yyyy");
            model.OrderMonth = DateTime.Now.ToString("MM");
            model.OrderYear = DateTime.Now.ToString("yyyy");
            if (ModelState.IsValid)
            {
                using (var db = new BoardDbContext())
                {
                    var list = db.Boards.FirstOrDefault(b => b.BoardNo.Equals(boardNo));
                    model.BoardContent = list.BoardContent;
                    model.UserNo = userNo;
                    db.Orders.Add(model);
                    db.SaveChanges();
                }
                return RedirectToAction("Index", "Home");
            }

            return View(model);
        }

        // 관리자 : 미승인 주문내역 View
        public IActionResult OrderList(int? Page)
        {
            using (var db = new BoardDbContext())
            {
                var list = db.Orders.ToList();
                var PageNo = Page ?? 1;
                var PageSize = 5;

                return View(list.ToPagedList(PageNo, PageSize));
            }
        }

        // 관리자 : 미승인 주문내역에서 승인처리 기능 실행
        [HttpPost]
        public IActionResult OrderList(int orderNo, ApprovedOrder model)
        {
            if (ModelState.IsValid)
            {
                using (var db = new BoardDbContext())
                {
                    var order = db.Orders.Find(orderNo);
                    model.OrderNo = order.OrderNo;
                    model.BoardNo = order.BoardNo;
                    model.UserNo = order.UserNo;
                    model.BoardTitle = order.BoardTitle;
                    model.BoardContent = order.BoardContent;


                    db.ApprovedOrders.Add(model);
                    db.Orders.Remove(order);
                    db.SaveChanges();
                }

                return RedirectToAction("Index", "Home");
            }
            return Redirect("Index");
        }

        // 사용자 : 상품 리스트 View
        public IActionResult ProductDetail(int boardNo)
        {

            using (var db = new BoardDbContext())
            {
                var board = db.Boards.FirstOrDefault(b => b.BoardNo.Equals(boardNo));
                return View(board);
            }
        }

        // 관리자 : 승인된 리스트 View
        public IActionResult ApprovalOrder(int? Page)
        {
            using (var db = new BoardDbContext())
            {
                var list = db.ApprovedOrders.ToList();
                var PageNo = Page ?? 1;
                var PageSize = 5;

                return View(list.ToPagedList(PageNo, PageSize));
            }
        }

        /// <summary>
        /// 사용자 - 상품주문 SELECT로 바로하기 view
        /// </summary>
        /// <param name="productChoice"></param>
        /// <returns></returns>
        public async Task<IActionResult> Delivery(string productChoice)
        {
            using (var db = new BoardDbContext())
            {
                IQueryable<string> titleQuery = from p in db.Boards
                                                orderby p.BoardTitle
                                                select p.BoardTitle;

                IQueryable<string> contentQuery = from c in db.Boards
                                                  orderby c.BoardContent
                                                  select c.BoardContent;

                var boards = from b in db.Boards
                             select b;

                if (!string.IsNullOrEmpty(productChoice))
                {
                    boards = boards.Where(p => p.BoardTitle == productChoice);
                }

                var boardTItleVM = new ProductViewModel
                {
                    //Titles = new List<SelectListItem> { new SelectListItem { } }
                    Titles = new SelectList(await titleQuery.Distinct().ToListAsync()),
                    Products = await boards.ToListAsync(),
                    Contents = await contentQuery.Distinct().ToListAsync()
                };
                return View(boardTItleVM);
            }
        }

        /// <summary>
        /// 사용자 - 바로 상품주문에서 SELECT 하면 폼 전송
        /// </summary>
        /// <param name="productChoice"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Delivery(string productChoice, ProductViewModel model)
        {
            //var title = model.Boards.Equals(productChoice);
            //var content = model.Contents;

            using (var db = new BoardDbContext())
            {
                IQueryable<string> titleQuery = from p in db.Boards
                                                orderby p.BoardTitle
                                                select p.BoardTitle;

                IQueryable<string> contentQuery = from c in db.Boards
                                                  orderby c.BoardContent
                                                  select c.BoardContent;

                var boards = from b in db.Boards
                             select b;

                if (!string.IsNullOrEmpty(productChoice))
                {
                    boards = boards.Where(p => p.BoardTitle == productChoice);
                }

                var boardTItleVM = new ProductViewModel
                {
                    //Titles = new List<SelectListItem> { new SelectListItem { } }
                    Titles = new SelectList(await titleQuery.Distinct().ToListAsync()),
                    Products = await boards.ToListAsync(),
                    Contents = await contentQuery.Distinct().ToListAsync()
                };



                var list = db.Boards.FirstOrDefault(b => b.BoardTitle.Equals(productChoice));

                if (list == null)
                {
                    ViewBag.content = "상품명을 선택해 주십시오.";
                    return View();
                }

                var content = list.BoardContent;
                var boardNo = list.BoardNo;
                ViewBag.content = content;
                ViewBag.boardNo = boardNo;

                return View(boardTItleVM);
            }
        }

        /// <summary>
        /// 사용자 - 상품주문 SELECT로 바로하기 기능 실행
        /// </summary>
        /// <param name="model"></param>
        /// <param name="boardNo"></param>
        /// <param name="productChoice"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult DOrder(Order model, int boardNo, string productChoice)
        {
            var userNo = int.Parse(HttpContext.Session.GetInt32("USER_LOGIN_KEY").ToString());
            //model.BoardContent = Request.Form["Contents"].ToString();
            model.OrderDay = DateTime.Now.ToString("MM'/'dd'/'yyyy");
            model.OrderMonth = DateTime.Now.ToString("MM");
            model.OrderYear = DateTime.Now.ToString("yyyy");
            if (ModelState.IsValid)
            {
                using (var db = new BoardDbContext())
                {
                    var list = db.Boards.FirstOrDefault(b => b.BoardNo.Equals(boardNo));

                    model.BoardContent = list.BoardContent;
                    model.BoardNo = list.BoardNo;
                    model.UserNo = userNo;
                    model.BoardTitle = productChoice;
                    db.Orders.Add(model);
                    db.SaveChanges();
                }
                return RedirectToAction("Index", "Home");
            }

            return View(model);
        }

        // 사용자 본인 : 주문 내역 확인
        public IActionResult ChkOrderList(int? Page)
        {
            if (HttpContext.Session.GetInt32("USER_LOGIN_KEY") == null)
            {
                return RedirectToAction("Login", "Account");
            }

            var userNo = int.Parse(HttpContext.Session.GetInt32("USER_LOGIN_KEY").ToString());
            using (var db = new BoardDbContext())
            {
                // var user = db.Users.FirstOrDefault(u => u.UserNo.Equals(userNo));
                var order = db.ApprovedOrders.ToList();
                //List<ApprovedOrder> apo = new List<ApprovedOrder>();
                var order2 = from _app in order
                             where _app.UserNo == userNo
                             select _app;

                var PageNo = Page ?? 1;
                var PageSize = 5;
                // var list = 
                return View(order2.ToPagedList(PageNo, PageSize));
            }
        }

        /// <summary>
        /// 관리자 : 고객전달사항 view
        /// </summary>
        /// <returns></returns>
        public IActionResult CustomerDelivery()
        {
            return View();
        }

        /// <summary>
        /// 사용자 - 공지사항 view
        /// </summary>
        /// <param name="Page"></param>
        /// <returns></returns>
        public IActionResult NoticeList(int? Page)
        {
            using (var db = new BoardDbContext())
            {
                var list = db.Notices.ToList();
                var PageNo = Page ?? 1;
                var PageSize = 5;

                return View(list.ToPagedList(PageNo, PageSize));
            }
        }

        /// <summary>
        /// 관리자 - 공지사항 등록 view
        /// </summary>
        /// <returns></returns>
        public IActionResult Notice()
        {
            if (HttpContext.Session.GetInt32("USER_LOGIN_KEY") == null)
            {
                return RedirectToAction("Login", "Account");
            }
            return View();
        }

        /// <summary>
        /// 관리자 - 공지사항 등록 기능 실행
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Notice(Notice model)
        {
            if (HttpContext.Session.GetInt32("USER_LOGIN_KEY") == null)
            {
                return RedirectToAction("Login", "Account");
            }
            model.UserNo = int.Parse(HttpContext.Session.GetInt32("USER_LOGIN_KEY").ToString());

            if (ModelState.IsValid)
            {
                model.Regdate = DateTime.Now.ToString("yyyy-MM-dd");
                using (var db = new BoardDbContext())
                {
                    db.Notices.Add(model);
                    if (db.SaveChanges() > 0)
                    {
                        return Redirect("Index");
                    }
                }
            }
            ModelState.AddModelError(string.Empty, "공지사항을 등록할 수 없습니다.");
            return View();
        }

        /// <summary>
        /// 사용자 - 고객센터 view
        /// </summary>
        /// <returns></returns>
        public IActionResult ServiceCenter()
        {
            return View();
        }

        /// <summary>
        /// 사용자 - 공지사항 디테일 view
        /// </summary>
        /// <param name="noticeNo"></param>
        /// <returns></returns>
        public IActionResult NoticeDetail(int noticeNo)
        {

            using (var db = new BoardDbContext())
            {
                var notice = db.Notices.FirstOrDefault(b => b.NoticeNo.Equals(noticeNo));
                return View(notice);
            }
        }

        /// <summary>
        /// 관리자 - FAQ 등록 view
        /// </summary>
        /// <returns></returns>
        public IActionResult FaqAdd()
        {
            return View();
        }

        /// <summary>
        ///  관리자 - FAQ 등록 기능 실행
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult FaqAdd(Faq model)
        {
            if (HttpContext.Session.GetInt32("USER_LOGIN_KEY") == null)
            {
                return RedirectToAction("Login", "Account");
            }
            model.UserNo = int.Parse(HttpContext.Session.GetInt32("USER_LOGIN_KEY").ToString());
            // model.Regdate = DateTime.Now; -> 이거 타입 datetime일때 사용하셈 시간초 구분 다되고 더 정확하게 나옴
            model.Regdate = DateTime.Now.ToString("yyyy-MM-dd");
            if (ModelState.IsValid)
            {
                using (var db = new BoardDbContext())
                {
                    db.Faqs.Add(model);
                    if (db.SaveChanges() > 0)
                    {
                        return Redirect("Index");
                    }
                }
            }
            ModelState.AddModelError(string.Empty, "게시물을 저장할 수 없습니다.");
            return View();
        }

        /// <summary>
        /// 사용자 - FAQ 리스트 view (수정 필요)
        /// </summary>
        /// <param name="CategoryChoice"></param>
        /// <returns></returns>
        public async Task<IActionResult> FaqList(string CategoryChoice)
        {
            using (var db = new BoardDbContext())
            {
                IQueryable<string> categoryQuery = from p in db.Faqs
                                                   orderby p.CategoryChoice
                                                   select p.CategoryChoice;

                IQueryable<string> titleQuery = from c in db.Faqs
                                                orderby c.FaqTItle
                                                select c.FaqTItle;

                var faq = from f in db.Faqs
                          select f;

                if (!string.IsNullOrEmpty(CategoryChoice))
                {
                    faq = faq.Where(p => p.CategoryChoice == CategoryChoice);
                }

                var faqCategoryVM = new FaqViewModel
                {
                    Categories = new SelectList(await categoryQuery.Distinct().ToListAsync()),
                    Titles = await titleQuery.Distinct().ToListAsync(),
                    Questions = await faq.ToListAsync()
                };
                // var count = faqCategoryVM.Titles.Count;


                // model.Categories = new SelectList(await categoryQuery.Distinct().ToListAsync());
                // model.Titles = await titleQuery.Distinct().ToListAsync();


                // ViewBag.Category = faqCategoryVM.Categories;
                // ViewBag.count = count;

                // var list = faqCategoryVM.Titles.ToList();
                // var PageNo = Page ?? 1;
                // var PageSize = 5;

                //return View(list.ToPagedList(PageNo,PageSize));
                return View(faqCategoryVM);
            }
        }

        // 안쓰는중
        //[HttpPost]
        //public async Task<IActionResult> FaqList(int? Page, FaqViewModel model, string CategoryChoice, int d)
        //{
        //    using (var db = new BoardDbContext())
        //    {
        //        IQueryable<string> categoryQuery = from p in db.Faqs
        //                                           orderby p.CategoryChoice
        //                                           select p.CategoryChoice;
        //
        //        IQueryable<string> titleQuery = from c in db.Faqs
        //                                        orderby c.FaqTItle
        //                                        select c.FaqTItle;
        //        
        //
        //        var faq = from b in db.Faqs
        //                  select b;
        //
        //        if (!string.IsNullOrEmpty(CategoryChoice))
        //        {
        //            faq = faq.Where(p => p.CategoryChoice == CategoryChoice);
        //        }
        //
        //        model.Questions = await faq.ToListAsync();
        //        model.Categories = new SelectList(await categoryQuery.Distinct().ToListAsync());
        //        model.Titles = new List<string>(await titleQuery.Distinct().ToListAsync());
        //        ViewBag.Category = model.Categories;
        //        ViewBag.Titles = model.Titles;
        //
        //        var list = db.Faqs.ToList();
        //        var PageNo = Page ?? 1;
        //        var PageSize = 5;
        //
        //        return View(list.ToPagedList(PageNo, PageSize));
        //    }
        //}

        public IActionResult FaqDetail(int faqNo)
        {
            using (var db = new BoardDbContext())
            {
                var faq = db.Faqs.FirstOrDefault(b => b.FaqNo.Equals(faqNo));
                return View(faq);
            }
        }

        public IActionResult Statistics()
        {
            return View();
        }

        //public IActionResult StatisticsOrderDaily()
        //{
        //return View();
        //}

        // 일별 주문
        // UserNo값 안들어오네;; 흠...
        // [HttpPost]
        public async Task<IActionResult> StatisticsOrderDaily(int? Page)
        {
            using (var db = new BoardDbContext())
            {
                var list = await db.Orders.ToListAsync();
                var PageNo = Page ?? 1;
                var PageSize = 5;
                ViewBag.count = list.Count();
                return View(list.ToPagedList(PageNo, PageSize));
            }
        }

        /// <summary>
        /// 관리자 - 일별 주문 검색 기능 실행
        /// </summary>
        /// <param name="Page"></param>
        /// <param name="zz"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> StatisticsOrderDaily(int? Page, int zz)
        {
            var OrderDay = Request.Form["OrderDay"].ToString();

            using (var db = new BoardDbContext())
            {
                IQueryable<int> userQuery = from p in db.Orders
                                            orderby p.UserNo
                                            select p.UserNo;

                var order = from f in db.Orders
                            select f;

                if (!string.IsNullOrEmpty(OrderDay))
                {
                    order = order.Where(p => p.OrderDay == OrderDay);

                }
                ViewBag.count = order.Count();
                var list = await order.ToListAsync();
                var PageNo = Page ?? 1;
                var PageSize = 5;
                return View(list.ToPagedList(PageNo, PageSize));
            }
        }

        /// <summary>
        /// 관리자 - 월별 주문 view
        /// </summary>
        /// <param name="Page"></param>
        /// <returns></returns>
        public async Task<IActionResult> StatisticsOrderMonthly(int? Page)
        {
            //var OrderYear = Request.Form["OrderYear"].ToString();
            //var OrderMonth = Request.Form["OrderMonth"].ToString();
            var OrderYear = Request.Query["OrderYear"].ToString();
            var OrderMonth = Request.Query["OrderMonth"].ToString();
            using (var db = new BoardDbContext())
            {
                IQueryable<int> userQuery = from p in db.Orders
                                            orderby p.UserNo
                                            select p.UserNo;

                var order = from f in db.Orders
                            select f;

                if (!string.IsNullOrEmpty(OrderYear) && !string.IsNullOrEmpty(OrderMonth))
                {
                    order = order.Where(p => p.OrderYear == OrderYear);
                    order = order.Where(p => p.OrderMonth == OrderMonth);
                }
                ViewBag.count = order.Count();
                ViewBag.year = OrderYear;
                ViewBag.month = OrderMonth;
                var list = await order.ToListAsync();
                var PageNo = Page ?? 1;
                var PageSize = 5;
                return View(list.ToPagedList(PageNo, PageSize));
            }
        }

        // 월별 주문
        [HttpPost]
        public async Task<IActionResult> StatisticsOrderMonthly(int? Page, int zz)
        {
            var OrderYear = Request.Form["OrderYear"].ToString();
            var OrderMonth = Request.Form["OrderMonth"].ToString();

            using (var db = new BoardDbContext())
            {
                IQueryable<int> userQuery = from p in db.Orders
                                            orderby p.UserNo
                                            select p.UserNo;

                var order = from f in db.Orders
                            select f;

                if (!string.IsNullOrEmpty(OrderYear) && !string.IsNullOrEmpty(OrderMonth))
                {
                    order = order.Where(p => p.OrderYear == OrderYear);
                    order = order.Where(p => p.OrderMonth == OrderMonth);
                }
                ViewBag.count = order.Count();
                ViewBag.year = OrderYear;
                ViewBag.month = OrderMonth;
                var list = await order.ToListAsync();
                var PageNo = Page ?? 1;
                var PageSize = 5;
                return View(list.ToPagedList(PageNo, PageSize));
            }
        }

        // 일별 회원가입
        public async Task<IActionResult> StatisticsSignUpDaily(int? Page)
        {

            using (var db = new BoardDbContext())
            {
                var list = await db.Users.ToListAsync();
                var PageNo = Page ?? 1;
                var PageSize = 5;
                ViewBag.count = list.Count();
                return View(list.ToPagedList(PageNo, PageSize));
            }
        }

        // 일별 회원가입
        [HttpPost]
        public async Task<IActionResult> StatisticsSignUpDaily(int? Page, int zz)
        {
            var SignUpDay = Request.Form["SignUpDay"].ToString();

            using (var db = new BoardDbContext())
            {
                IQueryable<int> userQuery = from p in db.Users
                                            orderby p.UserNo
                                            select p.UserNo;

                var user = from f in db.Users
                           select f;

                if (!string.IsNullOrEmpty(SignUpDay))
                {
                    user = user.Where(p => p.SignUpDay == SignUpDay);
                }
                ViewBag.count = user.Count();
                var list = await user.ToListAsync();
                var PageNo = Page ?? 1;
                var PageSize = 5;
                return View(list.ToPagedList(PageNo, PageSize));
            }
        }

        /// <summary>
        /// 관리자 - 월별 회원가입 수 view
        /// </summary>
        /// <param name="Page"></param>
        /// <returns></returns>
        public async Task<IActionResult> StatisticsSignUpMonthly(int? Page)
        {
            var SignUpYear = Request.Query["SignUpYear"].ToString();
            var SignUpMonth = Request.Query["SignUpMonth"].ToString();

            using (var db = new BoardDbContext())
            {
                IQueryable<int> userQuery = from p in db.Users
                                            orderby p.UserNo
                                            select p.UserNo;

                var user = from f in db.Users
                           select f;

                if (!string.IsNullOrEmpty(SignUpYear) && !string.IsNullOrEmpty(SignUpMonth))
                {
                    user = user.Where(p => p.SignUpYear == SignUpYear);
                    user = user.Where(p => p.SignUpMonth == SignUpMonth);
                }
                ViewBag.count = user.Count();
                ViewBag.year = SignUpYear;
                ViewBag.month = SignUpMonth;
                var list = await user.ToListAsync();
                var PageNo = Page ?? 1;
                var PageSize = 5;
                return View(list.ToPagedList(PageNo, PageSize));
            }
        }

        // 월별 회원가입
        [HttpPost]
        public async Task<IActionResult> StatisticsSignUpMonthly(int? Page, int zz)
        {
            var SignUpYear = Request.Form["SignUpYear"].ToString();
            var SignUpMonth = Request.Form["SignUpMonth"].ToString();

            using (var db = new BoardDbContext())
            {
                IQueryable<int> userQuery = from p in db.Users
                                            orderby p.UserNo
                                            select p.UserNo;

                var user = from f in db.Users
                           select f;

                if (!string.IsNullOrEmpty(SignUpYear) && !string.IsNullOrEmpty(SignUpMonth))
                {
                    user = user.Where(p => p.SignUpYear == SignUpYear);
                    user = user.Where(p => p.SignUpMonth == SignUpMonth);
                }
                ViewBag.count = user.Count();
                ViewBag.year = SignUpYear;
                ViewBag.month = SignUpMonth;
                var list = await user.ToListAsync();
                var PageNo = Page ?? 1;
                var PageSize = 5;
                return View(list.ToPagedList(PageNo, PageSize));
            }
        }

        public IActionResult Questions()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Questions(Inquire model)
        {
            if (HttpContext.Session.GetInt32("USER_LOGIN_KEY") == null)
            {
                return RedirectToAction("Login", "Account");
            }
            model.UserNo = int.Parse(HttpContext.Session.GetInt32("USER_LOGIN_KEY").ToString());
            model.QuestionYear = DateTime.Now.ToString("yyyy");
            model.QuestionMonth = DateTime.Now.ToString("MM");
            model.QuestionDay = DateTime.Now.ToString("MM'/'dd'/'yyyy");
            model.QuestionTime = DateTime.Now.ToString("HH:mm");
            // model.Regdate = DateTime.Now; -> 이거 타입 datetime일때 사용하셈 시간초 구분 다되고 더 정확하게 나옴
            // model.Regdate = DateTime.Now.ToString("yyyy-MM-dd");
            if (ModelState.IsValid)
            {
                using (var db = new BoardDbContext())
                {
                    db.Inquires.Add(model);
                    if (db.SaveChanges() > 0)
                    {
                        return Redirect("IndexCus");
                    }
                }
            }
            ModelState.AddModelError(string.Empty, "게시물을 저장할 수 없습니다.");
            return View();
        }

        public async Task<IActionResult> AnswerList(int? Page, int zz)
        {
            using (var db = new BoardDbContext())
            {
                IQueryable<int> userQuery = from p in db.Inquires
                                            orderby p.UserNo
                                            select p.UserNo;
                IQueryable<string> categoryQuery = from p in db.Inquires
                                                   orderby p.CategoryChoice
                                                   select p.CategoryChoice;


                var inquires = from f in db.Inquires
                               select f;

                var Categories = new SelectList(await categoryQuery.Distinct().ToListAsync());
                ViewBag.Categories = Categories;

                var list = await inquires.ToListAsync();
                ViewBag.Count = list.Count;
                var PageNo = Page ?? 1;
                var PageSize = 5;


                //var order2 = await order.ToListAsync();
                return View(list.ToPagedList(PageNo, PageSize));
            }
        }

        [HttpPost]
        public async Task<IActionResult> AnswerList(int? Page)
        {
            var QuestionDay = Request.Form["QuestionDay"].ToString();
            var ChoiceCategory = Request.Form["ChoiceCategory"].ToString();
            if (ChoiceCategory == "카테고리")
            {
                ChoiceCategory = "";
            }
            using (var db = new BoardDbContext())
            {
                IQueryable<int> userQuery = from p in db.Inquires
                                            orderby p.UserNo
                                            select p.UserNo;
                IQueryable<string> categoryQuery = from p in db.Inquires
                                                   orderby p.CategoryChoice
                                                   select p.CategoryChoice;

                var inquires = from f in db.Inquires
                               select f;

                var Categories = new SelectList(await categoryQuery.Distinct().ToListAsync());
                ViewBag.Categories = Categories;

                if (!string.IsNullOrEmpty(QuestionDay))
                {
                    inquires = inquires.Where(p => p.QuestionDay == QuestionDay);
                    ViewBag.count = inquires.Count();
                    ViewBag.day = QuestionDay;
                }
                if (!string.IsNullOrEmpty(ChoiceCategory))
                {
                    inquires = inquires.Where(p => p.CategoryChoice == ChoiceCategory);

                    ViewBag.day = QuestionDay;
                }
                ViewBag.count = inquires.Count();
                var list = await inquires.ToListAsync();
                var PageNo = Page ?? 1;
                var PageSize = 5;


                //var order2 = await order.ToListAsync();
                return View(list.ToPagedList(PageNo, PageSize));
            }
        }

        public IActionResult AnswerDetail(int inquireNo)
        {
            using (var db = new BoardDbContext())
            {
                var inquire = db.Inquires.FirstOrDefault(b => b.InquireNo.Equals(inquireNo));
                return View(inquire);
            }

        }

        // 답변한 내역 상세보기 view - 관리자
        public IActionResult OkAnswerDetail(int answerNo)
        {
            using (var db = new BoardDbContext())
            {
                var answer = db.Answers.FirstOrDefault(b => b.AnswerNo.Equals(answerNo));
                return View(answer);
            }

        }

        public IActionResult Answer(int inquireNo)
        {
            using (var db = new BoardDbContext())
            {
                var inquire = db.Inquires.FirstOrDefault(b => b.InquireNo.Equals(inquireNo));
                return View(inquire);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Answer(int InquireNo, Answer model)
        {
            //var InquireNo = Request.Form["InquireNo"].ToString();
            var AnswerContent = Request.Form["AnswerContent"].ToString();
            if (ModelState.IsValid)
            {
                using (var db = new BoardDbContext())
                {
                    var inquire = await db.Inquires.FindAsync(InquireNo);
                    model.InquireNo = inquire.InquireNo;
                    model.Title = inquire.Title;
                    model.Questions = inquire.Questions;
                    model.CategoryChoice = inquire.CategoryChoice;
                    model.UserNo = inquire.UserNo;
                    model.QuestionYear = inquire.QuestionYear;
                    model.QuestionMonth = inquire.QuestionMonth;
                    model.QuestionDay = inquire.QuestionDay;
                    model.QuestionTime = inquire.QuestionTime;
                    model.AnswerContent = AnswerContent;
                    model.AnswerDay = DateTime.Now.ToString("MM'/'dd'/'yyyy");
                    db.Answers.Add(model);
                    db.Inquires.Remove(inquire);
                    db.SaveChanges();
                    return Redirect("Index");
                }
            }
            ModelState.AddModelError(string.Empty, "답변이 불가합니다.ㅋㅋ");
            return View();
        }

        // 답변한 내역 VIew - 관리자
        public async Task<IActionResult> OkAnswerList(int? Page, int zz)
        {
            using (var db = new BoardDbContext())
            {
                IQueryable<int> userQuery = from p in db.Answers
                                            orderby p.UserNo
                                            select p.UserNo;
                IQueryable<string> categoryQuery = from p in db.Answers
                                                   orderby p.CategoryChoice
                                                   select p.CategoryChoice;


                var answers = from f in db.Answers
                              select f;

                var Categories = new SelectList(await categoryQuery.Distinct().ToListAsync());
                ViewBag.Categories = Categories;

                var list = await answers.ToListAsync();
                ViewBag.Count = list.Count;
                var PageNo = Page ?? 1;
                var PageSize = 5;


                //var order2 = await order.ToListAsync();
                return View(list.ToPagedList(PageNo, PageSize));
            }
        }

        // 답변한 내역 기능 실행 - 관리자
        [HttpPost]
        public async Task<IActionResult> OkAnswerList(int? Page)
        {
            var QuestionDay = Request.Form["QuestionDay"].ToString();
            var ChoiceCategory = Request.Form["ChoiceCategory"].ToString();
            if (ChoiceCategory == "카테고리")
            {
                ChoiceCategory = "";
            }

            using (var db = new BoardDbContext())
            {
                IQueryable<int> userQuery = from p in db.Answers
                                            orderby p.UserNo
                                            select p.UserNo;
                IQueryable<string> categoryQuery = from p in db.Answers
                                                   orderby p.CategoryChoice
                                                   select p.CategoryChoice;

                var answers = from f in db.Answers
                              select f;

                var Categories = new SelectList(await categoryQuery.Distinct().ToListAsync());
                ViewBag.Categories = Categories;

                if (!string.IsNullOrEmpty(QuestionDay))
                {
                    answers = answers.Where(p => p.QuestionDay == QuestionDay);
                    ViewBag.count = answers.Count();
                    ViewBag.day = QuestionDay;
                }
                if (!string.IsNullOrEmpty(ChoiceCategory))
                {
                    answers = answers.Where(p => p.CategoryChoice == ChoiceCategory);
                    ViewBag.count = answers.Count();
                    ViewBag.day = QuestionDay;
                }
                ViewBag.count = answers.Count();
                var list = await answers.ToListAsync();
                var PageNo = Page ?? 1;
                var PageSize = 5;


                //var order2 = await order.ToListAsync();
                return View(list.ToPagedList(PageNo, PageSize));
            }
        }

        // 사용자 - 1:1 문의 내역
        public async Task<IActionResult> ChkQuestions(int? Page)
        {
            if (HttpContext.Session.GetInt32("USER_LOGIN_KEY") == null)
            {
                return RedirectToAction("Login", "Account");
            }

            var userNo = int.Parse(HttpContext.Session.GetInt32("USER_LOGIN_KEY").ToString());
            using (var db = new BoardDbContext())
            {
                var answers = from a in db.Answers
                              where a.UserNo == userNo
                              select a;

                var list = await answers.ToListAsync();
                var PageNo = Page ?? 1;
                var PageSize = 5;

                return View(list.ToPagedList(PageNo, PageSize));
            }
        }

        // 사용자 - 1:1 문의 상세 내역
        public IActionResult QuestionDetail(int answerNo)
        {
            using (var db = new BoardDbContext())
            {
                var answer = db.Answers.FirstOrDefault(b => b.AnswerNo.Equals(answerNo));
                return View(answer);
            }
        }

        ////////////////////// -- 2020 - 04 - 27 추가 사항

        // 관리자 - 미승인 주문 상세 view
        public IActionResult OrderDetail(int orderNo)
        {
            using (var db = new BoardDbContext())
            {
                var order = db.Orders.FirstOrDefault(o => o.OrderNo.Equals(orderNo));
                return View(order);
            }
        }

        // 관리자 - 승인 주문 상세 view
        public IActionResult ApprovalDetail(int approvalNo)
        {
            using (var db = new BoardDbContext())
            {
                var approvedOrder = db.ApprovedOrders.FirstOrDefault(o => o.ApprovalNo.Equals(approvalNo));
                return View(approvedOrder);
            }
        }

        ///////////////////////// -- 2020 - 05 - 06 추가 사항
        public IActionResult ChkOrder()
        {
            return View();
        }

        public IActionResult NoChkOrderList(int? Page)
        {
            if (HttpContext.Session.GetInt32("USER_LOGIN_KEY") == null)
            {
                return RedirectToAction("Login", "Account");
            }

            var userNo = int.Parse(HttpContext.Session.GetInt32("USER_LOGIN_KEY").ToString());
            using (var db = new BoardDbContext())
            {
                // var user = db.Users.FirstOrDefault(u => u.UserNo.Equals(userNo));
                var order = db.Orders.ToList();
                //List<ApprovedOrder> apo = new List<ApprovedOrder>();
                var order2 = from _app in order
                             where _app.UserNo == userNo
                             select _app;

                var PageNo = Page ?? 1;
                var PageSize = 5;
                // var list = 
                return View(order2.ToPagedList(PageNo, PageSize));
            }
        }

        // TEST 7할게 제발 에러좀 나지말자
        // TEST 8이다!!!
        
        
    }
}

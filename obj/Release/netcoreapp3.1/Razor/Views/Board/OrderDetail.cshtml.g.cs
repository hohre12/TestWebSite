#pragma checksum "C:\Users\JaeWon\source\repos\TestWebSIte\Views\Board\OrderDetail.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "081c3d058e147631783e469553685dce415129fa"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Board_OrderDetail), @"mvc.1.0.view", @"/Views/Board/OrderDetail.cshtml")]
namespace AspNetCore
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
#nullable restore
#line 1 "C:\Users\JaeWon\source\repos\TestWebSIte\Views\_ViewImports.cshtml"
using TestWebSIte;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\Users\JaeWon\source\repos\TestWebSIte\Views\_ViewImports.cshtml"
using TestWebSIte.Models;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"081c3d058e147631783e469553685dce415129fa", @"/Views/Board/OrderDetail.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"b63f779b38372bf5d0bfbb79ed68c7ac14c73cfa", @"/Views/_ViewImports.cshtml")]
    public class Views_Board_OrderDetail : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("btn btn-secondary"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-controller", "Board", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_2 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "OrderList", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        #line hidden
        #pragma warning disable 0649
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperExecutionContext __tagHelperExecutionContext;
        #pragma warning restore 0649
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner __tagHelperRunner = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner();
        #pragma warning disable 0169
        private string __tagHelperStringValueBuffer;
        #pragma warning restore 0169
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __backed__tagHelperScopeManager = null;
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __tagHelperScopeManager
        {
            get
            {
                if (__backed__tagHelperScopeManager == null)
                {
                    __backed__tagHelperScopeManager = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager(StartTagHelperWritingScope, EndTagHelperWritingScope);
                }
                return __backed__tagHelperScopeManager;
            }
        }
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("<div class=\"row\">\r\n    <div class=\"col-xl-12 col-md-12\">\r\n        <h2 class=\"text-center\">주문번호 : ");
#nullable restore
#line 3 "C:\Users\JaeWon\source\repos\TestWebSIte\Views\Board\OrderDetail.cshtml"
                                  Write(Model.OrderNo);

#line default
#line hidden
#nullable disable
            WriteLiteral(" 번 주문의 상세 내역입니다.</h2>\r\n        <div class=\"table table-bordered\">\r\n            <table width=\"100%\" style=\"table-layout:fixed\">\r\n                <tr>\r\n                    <th class=\"table-active\" width=\"10%\">주문 번호</th>\r\n                    <td>");
#nullable restore
#line 8 "C:\Users\JaeWon\source\repos\TestWebSIte\Views\Board\OrderDetail.cshtml"
                   Write(Model.OrderNo);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                    <th class=\"table-active\" width=\"10%\">주문 날짜</th>\r\n                    <td>");
#nullable restore
#line 10 "C:\Users\JaeWon\source\repos\TestWebSIte\Views\Board\OrderDetail.cshtml"
                   Write(Model.OrderDay);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                </tr>\r\n\r\n                <tr>\r\n                    <th class=\"table-active\" width=\"10%\">상품 번호</th>\r\n                    <td>");
#nullable restore
#line 15 "C:\Users\JaeWon\source\repos\TestWebSIte\Views\Board\OrderDetail.cshtml"
                   Write(Model.BoardNo);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                    <th class=\"table-active\" width=\"10%\">주문자</th>\r\n                    <td>");
#nullable restore
#line 17 "C:\Users\JaeWon\source\repos\TestWebSIte\Views\Board\OrderDetail.cshtml"
                   Write(Model.UserNo);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                </tr>\r\n\r\n\r\n                <tr>\r\n                    <th class=\"table-active\" width=\"10%\">제목</th>\r\n                    <td colspan=\"3\">");
#nullable restore
#line 23 "C:\Users\JaeWon\source\repos\TestWebSIte\Views\Board\OrderDetail.cshtml"
                               Write(Model.BoardTitle);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                </tr>\r\n\r\n                <tr>\r\n                    <th class=\"table-active\" width=\"10%\">문의 내용</th>\r\n                    <td colspan=\"3\">");
#nullable restore
#line 28 "C:\Users\JaeWon\source\repos\TestWebSIte\Views\Board\OrderDetail.cshtml"
                               Write(Html.Raw(Model.BoardContent));

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                </tr>\r\n               \r\n            </table>\r\n        </div>\r\n    </div>\r\n</div>\r\n\r\n<div class=\"form-group text-center\">\r\n    ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "081c3d058e147631783e469553685dce415129fa6922", async() => {
                WriteLiteral("뒤로가기");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_0);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Controller = (string)__tagHelperAttribute_1.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_1);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_2.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_2);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n    <!--a class=\"btn btn-primary\" asp-controller=\"Board\" asp-action=\"OrderList\">승인</a-->\r\n</div>\r\n\r\n");
        }
        #pragma warning restore 1998
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.IModelExpressionProvider ModelExpressionProvider { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IUrlHelper Url { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IViewComponentHelper Component { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IJsonHelper Json { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<dynamic> Html { get; private set; }
    }
}
#pragma warning restore 1591
#pragma checksum "D:\PROJECT-TTS-D6\BlogManagement\Web\Views\Cart\ShoppingCart.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "739d4faa5686c5297899c2f620dc5416983442f9"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Cart_ShoppingCart), @"mvc.1.0.view", @"/Views/Cart/ShoppingCart.cshtml")]
namespace AspNetCore
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
#nullable restore
#line 1 "D:\PROJECT-TTS-D6\BlogManagement\Web\Views\_ViewImports.cshtml"
using Web;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "D:\PROJECT-TTS-D6\BlogManagement\Web\Views\_ViewImports.cshtml"
using Web.Models;

#line default
#line hidden
#nullable disable
#nullable restore
#line 3 "D:\PROJECT-TTS-D6\BlogManagement\Web\Views\_ViewImports.cshtml"
using System.Linq;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"739d4faa5686c5297899c2f620dc5416983442f9", @"/Views/Cart/ShoppingCart.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"2f8e7aed5fd78fc40b8d36ab50a84b7ff7853a5a", @"/Views/_ViewImports.cshtml")]
    public class Views_Cart_ShoppingCart : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<Domain.Shop.Dto.ShoppingCart.ShoppingCartViewModel>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("style", new global::Microsoft.AspNetCore.Html.HtmlString("width: 200px; height: 100px; "), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("alt", new global::Microsoft.AspNetCore.Html.HtmlString("Spicy jalapeno bacon"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_2 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("title", new global::Microsoft.AspNetCore.Html.HtmlString("Spicy jalapeno bacon"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_3 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("img-thumbnail"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_4 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "index", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_5 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-controller", "Home", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_6 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-area", "", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_7 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("btn btn-default"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_8 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "HomeOrder", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_9 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-controller", "Order", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_10 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("btn btn-primary"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
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
        private global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper;
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#nullable restore
#line 2 "D:\PROJECT-TTS-D6\BlogManagement\Web\Views\Cart\ShoppingCart.cshtml"
  
    ViewData["Title"] = "Giỏ hàng";
    ViewData["Name"] = "Giỏ hàng";
    ViewData["Controller"] = "Trang chủ";
    ViewData["Action"] = "Giỏ hàng";

#line default
#line hidden
#nullable disable
            WriteLiteral(@"
<div id=""wrapper"" class=""wrapper-fluid banner-effect-3"">
    <div class=""cart-page"">
        <div class=""container"">
            <div id=""checkout-cart"" class=""container"">
                <div class=""row"">
                    <div id=""content"" class=""col-sm-12"">
                        <h1 class=""title"">
                            Use Gift Certificate
                            &nbsp;(14.50kg)
                        </h1>
                        <div class=""table-responsive"">
                            <table class=""table table-bordered"">
                                <thead>
                                    <tr>
                                        <td class=""text-center"">Image</td>
                                        <td class=""text-left"">Product Name</td>
                                        <td class=""text-left"">Model</td>
                                        <td class=""text-left"">Quantity</td>
                                        <td class=""text-right"">Unit Price</td>
          ");
            WriteLiteral("                              <td class=\"text-right\">Total</td>\n                                    </tr>\n                                </thead>\n                                <tbody>\n");
#nullable restore
#line 32 "D:\PROJECT-TTS-D6\BlogManagement\Web\Views\Cart\ShoppingCart.cshtml"
                                     foreach (var item in Model.ShoppingCart.cartProducts)
                                    {

#line default
#line hidden
#nullable disable
            WriteLiteral("                                            <tr>\n                                                <td class=\"text-center\">\n");
#nullable restore
#line 36 "D:\PROJECT-TTS-D6\BlogManagement\Web\Views\Cart\ShoppingCart.cshtml"
                                                     if (@item.Product.DisplayImages.Any())
                                                    {

#line default
#line hidden
#nullable disable
            WriteLiteral("                                                        <a>");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("img", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagOnly, "739d4faa5686c5297899c2f620dc5416983442f99429", async() => {
            }
            );
            __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper);
            BeginAddHtmlAttributeValues(__tagHelperExecutionContext, "src", 2, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            AddHtmlAttributeValue("", 1893, "~/imageUpload/", 1893, 14, true);
#nullable restore
#line 38 "D:\PROJECT-TTS-D6\BlogManagement\Web\Views\Cart\ShoppingCart.cshtml"
AddHtmlAttributeValue("", 1907, item.Product.DisplayImages[0], 1907, 30, false);

#line default
#line hidden
#nullable disable
            EndAddHtmlAttributeValues(__tagHelperExecutionContext);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_0);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_1);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_2);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_3);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("</a>\n");
#nullable restore
#line 39 "D:\PROJECT-TTS-D6\BlogManagement\Web\Views\Cart\ShoppingCart.cshtml"
                                                    }
                                                    else
                                                    {

#line default
#line hidden
#nullable disable
            WriteLiteral("                                                        <a><img src =\"#\" style = \"width: 200px; height: 100px; \" alt = \"Spicy jalapeno bacon\" title = \"Spicy jalapeno bacon\" class=\"img-thumbnail\"></a>\n");
#nullable restore
#line 43 "D:\PROJECT-TTS-D6\BlogManagement\Web\Views\Cart\ShoppingCart.cshtml"
                                                    }

#line default
#line hidden
#nullable disable
            WriteLiteral("                                                </td>\n                                                <td class=\"text-left\">\n                                                    <a>");
#nullable restore
#line 46 "D:\PROJECT-TTS-D6\BlogManagement\Web\Views\Cart\ShoppingCart.cshtml"
                                                  Write(item.Product.ProductName);

#line default
#line hidden
#nullable disable
            WriteLiteral("</a><br>\n                                                </td>\n                                                <td class=\"text-left\">");
#nullable restore
#line 48 "D:\PROJECT-TTS-D6\BlogManagement\Web\Views\Cart\ShoppingCart.cshtml"
                                                                 Write(item.Product.ProductTypeName);

#line default
#line hidden
#nullable disable
            WriteLiteral(@"</td>
                                                <td class=""text-left"">
                                                    <div class=""input-group btn-block"" style=""max-width: 200px;"">
                                                        <input type=""text"" name=""quantity[121]""");
            BeginWriteAttribute("value", " value=\"", 3132, "\"", 3154, 1);
#nullable restore
#line 51 "D:\PROJECT-TTS-D6\BlogManagement\Web\Views\Cart\ShoppingCart.cshtml"
WriteAttributeValue("", 3140, item.Quantity, 3140, 14, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(" size=\"1\" class=\"form-control\"");
            BeginWriteAttribute("id", " id=\"", 3185, "\"", 3205, 1);
#nullable restore
#line 51 "D:\PROJECT-TTS-D6\BlogManagement\Web\Views\Cart\ShoppingCart.cshtml"
WriteAttributeValue("", 3190, item.ProductId, 3190, 15, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(">\n                                                        <span class=\"input-group-btn\">\n                                                            <button type=\"button\" data-toggle=\"tooltip\"");
            BeginWriteAttribute("onclick", "  onclick=\"", 3398, "\"", 3434, 3);
            WriteAttributeValue("", 3409, "UpDate(\'", 3409, 8, true);
#nullable restore
#line 53 "D:\PROJECT-TTS-D6\BlogManagement\Web\Views\Cart\ShoppingCart.cshtml"
WriteAttributeValue("", 3417, item.ProductId, 3417, 15, false);

#line default
#line hidden
#nullable disable
            WriteAttributeValue("", 3432, "\')", 3432, 2, true);
            EndWriteAttribute();
            BeginWriteAttribute("title", " title=\"", 3435, "\"", 3443, 0);
            EndWriteAttribute();
            WriteLiteral(" class=\"btn btn-primary\" data-original-title=\"Update\"><i class=\"fa fa-refresh\"></i></button>\n                                                            <button type=\"button\" data-toggle=\"tooltip\"");
            BeginWriteAttribute("onclick", " onclick=\"", 3640, "\"", 3675, 3);
            WriteAttributeValue("", 3650, "Delete(\'", 3650, 8, true);
#nullable restore
#line 54 "D:\PROJECT-TTS-D6\BlogManagement\Web\Views\Cart\ShoppingCart.cshtml"
WriteAttributeValue("", 3658, item.ProductId, 3658, 15, false);

#line default
#line hidden
#nullable disable
            WriteAttributeValue("", 3673, "\')", 3673, 2, true);
            EndWriteAttribute();
            WriteLiteral(@" class=""btn btn-danger"" data-original-title=""Remove""><i class=""fa fa-times-circle""></i></button>
                                                        </span>
                                                    </div>
                                                </td>
                                                <td class=""text-right""> ");
#nullable restore
#line 58 "D:\PROJECT-TTS-D6\BlogManagement\Web\Views\Cart\ShoppingCart.cshtml"
                                                                   Write(item.Product.Price);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\n                                                <td class=\"text-right\">");
#nullable restore
#line 59 "D:\PROJECT-TTS-D6\BlogManagement\Web\Views\Cart\ShoppingCart.cshtml"
                                                                   Write((item.Quantity * item.Product.Price));

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\n                                            </tr>  \n");
#nullable restore
#line 61 "D:\PROJECT-TTS-D6\BlogManagement\Web\Views\Cart\ShoppingCart.cshtml"
                                    }

#line default
#line hidden
#nullable disable
            WriteLiteral("                                </tbody>\n\n                            </table>\n                        </div>\n\n\n                        <div class=\"buttons clearfix\">\n                            <div class=\"pull-left\">\n                                ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "739d4faa5686c5297899c2f620dc5416983442f917009", async() => {
                WriteLiteral("Continue Shopping");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_4.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_4);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Controller = (string)__tagHelperAttribute_5.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_5);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Area = (string)__tagHelperAttribute_6.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_6);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_7);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\n                            </div>\n                            <div class=\"pull-right\">\n                                ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "739d4faa5686c5297899c2f620dc5416983442f918787", async() => {
                WriteLiteral("Checkout");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_8.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_8);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Controller = (string)__tagHelperAttribute_9.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_9);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Area = (string)__tagHelperAttribute_6.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_6);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_10);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral(@"
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>


    <div class=""back-to-top"">
        <i class=""fa fa-angle-up"" aria-hidden=""true""></i>
    </div>
</div>
");
            DefineSection("Scripts", async() => {
                WriteLiteral("\n\n    <!-- page script -->\n    <script>\n\n    function Delete(id){\n\t\tvar r = confirm(\"Bạn có chắc chắn muốn xóa sản phẩm này?\");\n        if (r == true) {\n            $.ajax(\n            {\n                type: \"POST\",\n                url: \'");
#nullable restore
#line 99 "D:\PROJECT-TTS-D6\BlogManagement\Web\Views\Cart\ShoppingCart.cshtml"
                 Write(Url.ActionLink("Delete", "Cart"));

#line default
#line hidden
#nullable disable
                WriteLiteral(@"',
                data: {
                    id: id
                },
                error: function (result) {
                    alert(""error"");
                },
                success: function (result) {
                    if (result == true) {
                        window.location.reload();
                    }
                    else {
						alert(""Có lỗi xảy ra, vui lòng thử lại sau!"");
                    }
                }
            });
        }
        }
        function UpDate(id){
            
            $.ajax(
            {
                type: ""POST"",
                url: '");
#nullable restore
#line 122 "D:\PROJECT-TTS-D6\BlogManagement\Web\Views\Cart\ShoppingCart.cshtml"
                 Write(Url.ActionLink("Update", "Cart"));

#line default
#line hidden
#nullable disable
                WriteLiteral(@"',
                data: {
                    id: id,
                    quantity: $(""#"" + id).val()
                   
                },
                error: function (result) {
                    alert(""error"");
                },
                success: function (result) {
                    if (result == true) {
                        window.location.reload();
                    }
                    else {
						alert(""Có lỗi xảy ra, vui lòng thử lại sau!"");
                    }
                }
            });
        
        }

    </script>
");
            }
            );
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<Domain.Shop.Dto.ShoppingCart.ShoppingCartViewModel> Html { get; private set; }
    }
}
#pragma warning restore 1591

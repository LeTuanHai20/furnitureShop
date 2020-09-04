#pragma checksum "D:\PROJECT-TTS-D6\BlogManagement\Web\Areas\Administrator\Views\Product\Review.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "e8d2244d80a05fdc5a58eb6702a1c2a8649f2ca7"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Areas_Administrator_Views_Product_Review), @"mvc.1.0.view", @"/Areas/Administrator/Views/Product/Review.cshtml")]
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
#line 1 "D:\PROJECT-TTS-D6\BlogManagement\Web\Areas\Administrator\Views\_ViewImports.cshtml"
using Web;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "D:\PROJECT-TTS-D6\BlogManagement\Web\Areas\Administrator\Views\_ViewImports.cshtml"
using Web.Models;

#line default
#line hidden
#nullable disable
#nullable restore
#line 3 "D:\PROJECT-TTS-D6\BlogManagement\Web\Areas\Administrator\Views\_ViewImports.cshtml"
using System.Linq;

#line default
#line hidden
#nullable disable
#nullable restore
#line 4 "D:\PROJECT-TTS-D6\BlogManagement\Web\Areas\Administrator\Views\_ViewImports.cshtml"
using Infrastructure.Common.Enums;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"e8d2244d80a05fdc5a58eb6702a1c2a8649f2ca7", @"/Areas/Administrator/Views/Product/Review.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"daee3a04158b1f2edb8411932491d235e3bcd21a", @"/Areas/Administrator/Views/_ViewImports.cshtml")]
    public class Areas_Administrator_Views_Product_Review : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<IEnumerable<Domain.Shop.Dto.ProductReview.ProductReviewViewModel>>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral(@"<!-- Main content -->
<section class=""content"">
    <div class=""row"">
        <div class=""col-md-12"">
            <div class=""card"">
                <div class=""card-header"">
                    <h5 class=""card-title"">Danh sách đánh giá</h5>
                    
                </div>
                <!-- /.card-header -->
                <div class=""card-body"" style=""overflow-x: auto;"">
                    <table id=""table-product"" class=""table table-bordered table-striped"">
                        <thead>
                            <tr>
                                <th>Tên sản phẩm</th>
                                <th>Tên người đánh giá</th>
                                <th>Nội dung</th>
                                <th>Số sao</th>
                                <th style=""width: 80px;""></th>     
                            </tr>
                        </thead>
                        <tbody>
");
#nullable restore
#line 24 "D:\PROJECT-TTS-D6\BlogManagement\Web\Areas\Administrator\Views\Product\Review.cshtml"
                             foreach (var item in Model)
                            {


#line default
#line hidden
#nullable disable
            WriteLiteral("                            <tr>\n                                <td>");
#nullable restore
#line 28 "D:\PROJECT-TTS-D6\BlogManagement\Web\Areas\Administrator\Views\Product\Review.cshtml"
                               Write(item.Product.ProductName);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\n                                <td>");
#nullable restore
#line 29 "D:\PROJECT-TTS-D6\BlogManagement\Web\Areas\Administrator\Views\Product\Review.cshtml"
                               Write(item.Customer.FirstName);

#line default
#line hidden
#nullable disable
            WriteLiteral("  ");
#nullable restore
#line 29 "D:\PROJECT-TTS-D6\BlogManagement\Web\Areas\Administrator\Views\Product\Review.cshtml"
                                                         Write(item.Customer.LastName);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\n                                <td>");
#nullable restore
#line 30 "D:\PROJECT-TTS-D6\BlogManagement\Web\Areas\Administrator\Views\Product\Review.cshtml"
                               Write(item.Review);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\n                                <td>");
#nullable restore
#line 31 "D:\PROJECT-TTS-D6\BlogManagement\Web\Areas\Administrator\Views\Product\Review.cshtml"
                               Write(item.Star);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\n                                <td>\n                                    <div class=\"btn-group\">\n                                        <a role=\"button\" class=\"btn btn-danger\" href=\"#\"");
            BeginWriteAttribute("onclick", " onclick=\"", 1568, "\"", 1596, 3);
            WriteAttributeValue("", 1578, "Delete(\'", 1578, 8, true);
#nullable restore
#line 34 "D:\PROJECT-TTS-D6\BlogManagement\Web\Areas\Administrator\Views\Product\Review.cshtml"
WriteAttributeValue("", 1586, item.Id, 1586, 8, false);

#line default
#line hidden
#nullable disable
            WriteAttributeValue("", 1594, "\')", 1594, 2, true);
            EndWriteAttribute();
            WriteLiteral(@">
                                            <i class=""fas fa-trash""></i>
                                        </a>
                                    </div>
                                </td>
                               
                            </tr>");
#nullable restore
#line 40 "D:\PROJECT-TTS-D6\BlogManagement\Web\Areas\Administrator\Views\Product\Review.cshtml"
                                 }

#line default
#line hidden
#nullable disable
            WriteLiteral("                        </tbody>\n                    </table>\n                    <!-- /.row -->\n                </div>\n            </div>\n            <!-- /.card -->\n        </div>\n        <!-- /.col -->\n    </div>\n</section>\n\n");
            DefineSection("Scripts", async() => {
                WriteLiteral("\n    <!-- page script -->\n    <script>\n  \n    function Delete(id){\n\t\tvar r = confirm(\"Bạn có chắc chắn muốn xóa bình luận này?\");\n        if (r == true) {\n            $.ajax(\n            {\n                type: \"POST\",\n                url: \'");
#nullable restore
#line 63 "D:\PROJECT-TTS-D6\BlogManagement\Web\Areas\Administrator\Views\Product\Review.cshtml"
                 Write(Url.ActionLink("DeleteReview", "Product"));

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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<IEnumerable<Domain.Shop.Dto.ProductReview.ProductReviewViewModel>> Html { get; private set; }
    }
}
#pragma warning restore 1591

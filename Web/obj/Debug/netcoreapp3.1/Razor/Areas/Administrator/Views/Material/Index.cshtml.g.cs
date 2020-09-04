#pragma checksum "D:\PROJECT-TTS-D6\BlogManagement\Web\Areas\Administrator\Views\Material\Index.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "1381a3c33686e93b89b52d88b6efae43e8b97e90"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Areas_Administrator_Views_Material_Index), @"mvc.1.0.view", @"/Areas/Administrator/Views/Material/Index.cshtml")]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"1381a3c33686e93b89b52d88b6efae43e8b97e90", @"/Areas/Administrator/Views/Material/Index.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"daee3a04158b1f2edb8411932491d235e3bcd21a", @"/Areas/Administrator/Views/_ViewImports.cshtml")]
    public class Areas_Administrator_Views_Material_Index : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<IEnumerable<Domain.Shop.Dto.Materials.MaterialViewModel>>
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
                    <h5 class=""card-title"">Danh sách chất liệu</h5>
                    <div class=""card-tools"">

                        <a");
            BeginWriteAttribute("href", " href=\"", 379, "\"", 411, 1);
#nullable restore
#line 11 "D:\PROJECT-TTS-D6\BlogManagement\Web\Areas\Administrator\Views\Material\Index.cshtml"
WriteAttributeValue("", 386, Url.ActionLink("Create"), 386, 25, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(@" role=""button"" class=""btn bg-gradient-success btn-sm"">
                            Thêm mới
                        </a>
                    </div>
                </div>
                <!-- /.card-header -->
                <div class=""card-body"" style=""overflow-x: auto;"">
                    <table id=""table-roles"" class=""table table-bordered table-striped"">
                        <thead>
                            <tr>
                                <th>Material Name</th>
                                <th>Note</th>
                                <th style=""width: 80px;""></th>
                            </tr>
                        </thead>
                        <tbody>
");
#nullable restore
#line 27 "D:\PROJECT-TTS-D6\BlogManagement\Web\Areas\Administrator\Views\Material\Index.cshtml"
                             foreach (var item in Model)
                            {


#line default
#line hidden
#nullable disable
            WriteLiteral("                <tr>\n                    <td>");
#nullable restore
#line 31 "D:\PROJECT-TTS-D6\BlogManagement\Web\Areas\Administrator\Views\Material\Index.cshtml"
                   Write(Html.DisplayFor(modelItem => item.MaterialName));

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\n                    <td>");
#nullable restore
#line 32 "D:\PROJECT-TTS-D6\BlogManagement\Web\Areas\Administrator\Views\Material\Index.cshtml"
                   Write(Html.DisplayFor(modelItem => item.Note));

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\n                    <td>\n                        <div class=\"btn-group\">\n                            <a role=\"button\" class=\"btn btn-info\"");
            BeginWriteAttribute("href", " href=\"", 1499, "\"", 1565, 1);
#nullable restore
#line 35 "D:\PROJECT-TTS-D6\BlogManagement\Web\Areas\Administrator\Views\Material\Index.cshtml"
WriteAttributeValue("", 1506, Url.ActionLink("Update", "Material", new { id = item.Id }), 1506, 59, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(">\n                                <i class=\"fas fa-edit\"></i>\n                            </a>\n                            <a role=\"button\" class=\"btn btn-danger\" href=\"#\"");
            BeginWriteAttribute("onclick", " onclick=\"", 1737, "\"", 1765, 3);
            WriteAttributeValue("", 1747, "Delete(\'", 1747, 8, true);
#nullable restore
#line 38 "D:\PROJECT-TTS-D6\BlogManagement\Web\Areas\Administrator\Views\Material\Index.cshtml"
WriteAttributeValue("", 1755, item.Id, 1755, 8, false);

#line default
#line hidden
#nullable disable
            WriteAttributeValue("", 1763, "\')", 1763, 2, true);
            EndWriteAttribute();
            WriteLiteral(">\n                                <i class=\"fas fa-trash\"></i>\n                            </a>\n                        </div>\n                    </td>\n                </tr>");
#nullable restore
#line 43 "D:\PROJECT-TTS-D6\BlogManagement\Web\Areas\Administrator\Views\Material\Index.cshtml"
                     }

#line default
#line hidden
#nullable disable
            WriteLiteral("                        </tbody>\n                    </table>\n                    <!-- /.row -->\n                </div>\n            </div>\n            <!-- /.card -->\n        </div>\n        <!-- /.col -->\n    </div>\n</section>\n\n");
            DefineSection("Scripts", async() => {
                WriteLiteral(@"

    <!-- page script -->
    <script>
    $(function () {
		$('#table-roles').DataTable({
			""columnDefs"": [
				{ ""orderable"": false, ""targets"": 2 }
			]
		});
    });
    function Delete(id){
		var r = confirm(""Bạn có chắc chắn muốn xóa Vật liệu này?"");
        if (r == true) {
            $.ajax(
            {
                type: ""POST"",
                url: '");
#nullable restore
#line 73 "D:\PROJECT-TTS-D6\BlogManagement\Web\Areas\Administrator\Views\Material\Index.cshtml"
                 Write(Url.ActionLink("Delete", "Material"));

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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<IEnumerable<Domain.Shop.Dto.Materials.MaterialViewModel>> Html { get; private set; }
    }
}
#pragma warning restore 1591

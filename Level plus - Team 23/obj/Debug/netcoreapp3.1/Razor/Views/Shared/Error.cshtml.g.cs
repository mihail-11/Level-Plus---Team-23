#pragma checksum "D:\FINKI\osmi semestar\Upravuvanje na IKT proekti\Proekt\Level plus - Team 23\Views\Shared\Error.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "3df42a0ff232a8c400b887d2e60ddde1b623442a"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Shared_Error), @"mvc.1.0.view", @"/Views/Shared/Error.cshtml")]
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
#line 1 "D:\FINKI\osmi semestar\Upravuvanje na IKT proekti\Proekt\Level plus - Team 23\Views\_ViewImports.cshtml"
using Level_plus___Team_23;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "D:\FINKI\osmi semestar\Upravuvanje na IKT proekti\Proekt\Level plus - Team 23\Views\_ViewImports.cshtml"
using Level_plus___Team_23.Models;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"3df42a0ff232a8c400b887d2e60ddde1b623442a", @"/Views/Shared/Error.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"8f5ee425857b12f80dc32758892a1e5b4e049d0b", @"/Views/_ViewImports.cshtml")]
    #nullable restore
    public class Views_Shared_Error : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<ErrorViewModel>
    #nullable disable
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#nullable restore
#line 2 "D:\FINKI\osmi semestar\Upravuvanje na IKT proekti\Proekt\Level plus - Team 23\Views\Shared\Error.cshtml"
  
    ViewData["Title"] = "Error";

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n<h1 class=\"text-danger\">Error.</h1>\r\n<h2 class=\"text-danger\">An error occurred while processing your request.</h2>\r\n\r\n");
#nullable restore
#line 9 "D:\FINKI\osmi semestar\Upravuvanje na IKT proekti\Proekt\Level plus - Team 23\Views\Shared\Error.cshtml"
 if (Model.ShowRequestId)
{

#line default
#line hidden
#nullable disable
            WriteLiteral("<p>\r\n    <strong>Request ID:</strong> <code>");
#nullable restore
#line 12 "D:\FINKI\osmi semestar\Upravuvanje na IKT proekti\Proekt\Level plus - Team 23\Views\Shared\Error.cshtml"
                                  Write(Model.RequestId);

#line default
#line hidden
#nullable disable
            WriteLiteral("</code>\r\n</p>\r\n");
#nullable restore
#line 14 "D:\FINKI\osmi semestar\Upravuvanje na IKT proekti\Proekt\Level plus - Team 23\Views\Shared\Error.cshtml"
}

#line default
#line hidden
#nullable disable
            WriteLiteral(@"
<div style=""background-color: #A52A2A; color: White; height: 10px;"">
</div>
<div style=""background-color: #F5F5DC; color: White; height: 170px;"">
    <div style="" padding:20px;"">
        <h3>
            Application Error:
        </h3>
        <h4>
            Sorry, an error occurred while processing your request.
        </h4>
        <h6>");
#nullable restore
#line 26 "D:\FINKI\osmi semestar\Upravuvanje na IKT proekti\Proekt\Level plus - Team 23\Views\Shared\Error.cshtml"
       Write(Html.ActionLink("Go Back To Home Page", "Index", "Home"));

#line default
#line hidden
#nullable disable
            WriteLiteral("</h6>\r\n        <br />\r\n        <br />\r\n    </div>\r\n</div>\r\n<div style=\"background-color: #A52A2A; color: White; height: 20px;\">\r\n</div> ");
        }
        #pragma warning restore 1998
        #nullable restore
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.IModelExpressionProvider ModelExpressionProvider { get; private set; } = default!;
        #nullable disable
        #nullable restore
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IUrlHelper Url { get; private set; } = default!;
        #nullable disable
        #nullable restore
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IViewComponentHelper Component { get; private set; } = default!;
        #nullable disable
        #nullable restore
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IJsonHelper Json { get; private set; } = default!;
        #nullable disable
        #nullable restore
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<ErrorViewModel> Html { get; private set; } = default!;
        #nullable disable
    }
}
#pragma warning restore 1591

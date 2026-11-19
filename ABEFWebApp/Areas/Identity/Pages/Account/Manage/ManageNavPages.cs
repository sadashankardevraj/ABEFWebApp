using Microsoft.AspNetCore.Mvc.Rendering;

namespace ABEFWebApp.Areas.Identity.Pages.Account.Manage;

public static class ManageNavPages
{
    public const string Index = "Index";
    public const string ChangePassword = "ChangePassword";

    public static string? IndexNavClass(ViewContext viewContext) => PageNavClass(viewContext, Index);

    public static string? ChangePasswordNavClass(ViewContext viewContext) => PageNavClass(viewContext, ChangePassword);

    public static string? PageNavClass(ViewContext viewContext, string page)
    {
        var activePage = viewContext.ViewData["ActivePage"] as string
            ?? Path.GetFileNameWithoutExtension(viewContext.ActionDescriptor.DisplayName);
        return string.Equals(activePage, page, StringComparison.OrdinalIgnoreCase) ? "active" : null;
    }
}

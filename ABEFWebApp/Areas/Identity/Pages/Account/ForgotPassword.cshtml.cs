#nullable disable

using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Text.Encodings.Web;
using ABEFWebApp.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;

namespace ABEFWebApp.Areas.Identity.Pages.Account;

public class ForgotPasswordModel : PageModel
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IEmailSender _emailSender;

    public ForgotPasswordModel(UserManager<ApplicationUser> userManager, IEmailSender emailSender)
    {
        _userManager = userManager;
        _emailSender = emailSender;
    }

    [BindProperty]
    public InputModel Input { get; set; }

    public class InputModel
    {
        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Enter a valid email address.")]
        public string Email { get; set; }
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        var user = await _userManager.FindByEmailAsync(Input.Email);
        if (user is null)
        {
            return RedirectToPage("./ForgotPasswordConfirmation");
        }

        if (_userManager.Options.SignIn.RequireConfirmedAccount
            && !(await _userManager.IsEmailConfirmedAsync(user)))
        {
            return RedirectToPage("./ForgotPasswordConfirmation");
        }

        var code = await _userManager.GeneratePasswordResetTokenAsync(user);
        code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
        var callbackUrl = Url.Page(
            "/Account/ResetPassword",
            pageHandler: null,
            values: new { area = "Identity", code },
            protocol: Request.Scheme);

        var storedEmail = await _userManager.GetEmailAsync(user);
        if (string.IsNullOrEmpty(storedEmail))
        {
            return RedirectToPage("./ForgotPasswordConfirmation");
        }

        await _emailSender.SendEmailAsync(
            storedEmail,
            "Reset your BEA password",
            $"""
            <p>Hello {HtmlEncoder.Default.Encode(user.FirstName)},</p>
            <p>Please reset your password by <a href="{HtmlEncoder.Default.Encode(callbackUrl)}">clicking here</a>.</p>
            <p>If you did not request this, you can ignore this email.</p>
            """);

        return RedirectToPage("./ForgotPasswordConfirmation");
    }
}

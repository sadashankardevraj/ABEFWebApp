#nullable disable

using System.ComponentModel.DataAnnotations;
using ABEFWebApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ABEFWebApp.Areas.Identity.Pages.Account.Manage;

[Authorize]
public class IndexModel : PageModel
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly SignInManager<ApplicationUser> _signInManager;

    public IndexModel(
        UserManager<ApplicationUser> userManager,
        SignInManager<ApplicationUser> signInManager)
    {
        _userManager = userManager;
        _signInManager = signInManager;
    }

    public string Username { get; set; }

    [TempData]
    public string StatusMessage { get; set; }

    [BindProperty]
    public InputModel Input { get; set; }

    public class InputModel
    {
        [Required(ErrorMessage = "First name is required.")]
        [StringLength(50, ErrorMessage = "First name cannot exceed 50 characters.")]
        [RegularExpression(@"^[A-Za-z ]+$", ErrorMessage = "First name may only contain letters and spaces.")]
        [Display(Name = "First name")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last name is required.")]
        [StringLength(50, ErrorMessage = "Last name cannot exceed 50 characters.")]
        [RegularExpression(@"^[A-Za-z ]+$", ErrorMessage = "Last name may only contain letters and spaces.")]
        [Display(Name = "Last name")]
        public string LastName { get; set; }

        [StringLength(100, ErrorMessage = "Organisation cannot exceed 100 characters.")]
        [Display(Name = "Organisation")]
        public string Organisation { get; set; }

        [Phone(ErrorMessage = "Enter a valid phone number.")]
        [Display(Name = "Phone")]
        public string PhoneNumber { get; set; }

        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }

    private async Task LoadAsync(ApplicationUser user)
    {
        Username = await _userManager.GetUserNameAsync(user);
        Input = new InputModel
        {
            FirstName = user.FirstName,
            LastName = user.LastName,
            Organisation = user.Organisation,
            PhoneNumber = await _userManager.GetPhoneNumberAsync(user),
            Email = await _userManager.GetEmailAsync(user)
        };
    }

    public async Task<IActionResult> OnGetAsync()
    {
        var user = await _userManager.GetUserAsync(User);
        if (user == null)
        {
            return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
        }

        await LoadAsync(user);
        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        var user = await _userManager.GetUserAsync(User);
        if (user == null)
        {
            return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
        }

        if (!ModelState.IsValid)
        {
            Input.Email = await _userManager.GetEmailAsync(user);
            return Page();
        }

        user.FirstName = Input.FirstName.Trim();
        user.LastName = Input.LastName.Trim();
        user.Organisation = string.IsNullOrWhiteSpace(Input.Organisation) ? null : Input.Organisation.Trim();

        var phoneNumber = await _userManager.GetPhoneNumberAsync(user);
        var newPhone = string.IsNullOrWhiteSpace(Input.PhoneNumber) ? null : Input.PhoneNumber.Trim();
        if (newPhone != phoneNumber)
        {
            var setPhoneResult = await _userManager.SetPhoneNumberAsync(user, newPhone);
            if (!setPhoneResult.Succeeded)
            {
                ModelState.AddModelError("Input.PhoneNumber", "Enter a valid phone number.");
                await LoadAsync(user);
                return Page();
            }
        }

        var updateResult = await _userManager.UpdateAsync(user);
        if (!updateResult.Succeeded)
        {
            foreach (var error in updateResult.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }

            await LoadAsync(user);
            return Page();
        }

        await _signInManager.RefreshSignInAsync(user);
        StatusMessage = "Profile updated";
        return RedirectToPage();
    }
}

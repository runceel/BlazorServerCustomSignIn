using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;

namespace BlazorApp2.Areas.MyLogin.Pages;

[IgnoreAntiforgeryToken]
public class IndexModel : PageModel
{
    [BindProperty]
    public User? SignInUser { get; set; }

    public async Task<IActionResult> OnPostAsync()
    {
        if (ModelState.IsValid is false)
        {
            return Page();
        }

        // 認証して、Blazorのほうにリダイレクト
        var principal = new ClaimsPrincipal(new ClaimsIdentity(new[]
        {
            new Claim(ClaimTypes.Name, SignInUser!.Name!),
            new Claim(ClaimTypes.Role, "Administrator"),
        }, CookieAuthenticationDefaults.AuthenticationScheme));
        await HttpContext.SignInAsync(
            CookieAuthenticationDefaults.AuthenticationScheme,
            principal);

        return Redirect("~/");
    }
    public void OnGet()
    {
    }
}

public class User
{
    [Required]
    public string? Name { get; set; }
}
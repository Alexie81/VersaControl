using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using VersaControl.Server.Models;

namespace VersaControl.Server.Controllers
{
    [Route("Account/[action]")]
    public partial class AccountController : Controller
    {
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly RoleManager<ApplicationRole> roleManager;
        private readonly IWebHostEnvironment env;
        private readonly IConfiguration configuration;

        public AccountController(IWebHostEnvironment env, SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager,
            RoleManager<ApplicationRole> roleManager, IConfiguration configuration)
        {
            this.signInManager = signInManager;
            this.userManager = userManager;
            this.roleManager = roleManager;
            this.env = env;
            this.configuration = configuration;
        }

        private IActionResult RedirectWithError(string error, string redirectUrl = null)
        {
             if (!string.IsNullOrEmpty(redirectUrl))
             {
                 return Redirect($"~/Login?error={error}&redirectUrl={Uri.EscapeDataString(redirectUrl.Replace("~", ""))}");
             }
             else
             {
                 return Redirect($"~/Login?error={error}");
             }
        }

        [HttpGet]
        public async Task<IActionResult> Login(string returnUrl)
        {
            if (returnUrl != "/" && !string.IsNullOrEmpty(returnUrl))
            {
                return Redirect($"~/Login?redirectUrl={Uri.EscapeDataString(returnUrl)}");
            }

            return Redirect("~/Login");
        }

        [HttpPost]
        public async Task<IActionResult> Login(string userName, string password, string redirectUrl)
        {
            redirectUrl = string.IsNullOrEmpty(redirectUrl) ? "~/" : redirectUrl.StartsWith("/") ? redirectUrl : $"~/{redirectUrl}";

            if (env.EnvironmentName == "Development" && userName == "admin" && password == "admin")
            {
                var claims = new List<Claim>()
                {
                    new Claim(ClaimTypes.Name, "admin"),
                    new Claim(ClaimTypes.Email, "admin")
                };

                roleManager.Roles.ToList().ForEach(r => claims.Add(new Claim(ClaimTypes.Role, r.Name)));
                await signInManager.SignInWithClaimsAsync(new ApplicationUser { UserName = userName, Email = userName }, isPersistent: false, claims);

                return Redirect(redirectUrl);
            }

            if (!string.IsNullOrEmpty(userName) && !string.IsNullOrEmpty(password))
            {

                var user = await userManager.FindByNameAsync(userName);

                if (user == null)
                {
                    return RedirectWithError("Invalid user or password", redirectUrl);
                }

                if (!user.EmailConfirmed)
                {
                    return RedirectWithError("User email not confirmed", redirectUrl);
                }
                var result = await signInManager.PasswordSignInAsync(userName, password, false, false);

                if (result.Succeeded)
                {
                    return Redirect(redirectUrl);
                }
            }

            return RedirectWithError("Invalid user or password", redirectUrl);
        }
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> ChangePassword(string oldPassword, string newPassword)
        {
            if (string.IsNullOrEmpty(oldPassword) || string.IsNullOrEmpty(newPassword))
            {
                return BadRequest("Invalid password");
            }

            var id = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var user = await userManager.FindByIdAsync(id);

            var result = await userManager.ChangePasswordAsync(user, oldPassword, newPassword);

            if (result.Succeeded)
            {
                return Ok();
            }

            var message = string.Join(", ", result.Errors.Select(error => error.Description));

            return BadRequest(message);
        }

        [HttpPost]
        public ApplicationAuthenticationState CurrentUser()
        {
            return new ApplicationAuthenticationState
            {
                IsAuthenticated = User.Identity.IsAuthenticated,
                Name = User.Identity.Name,
                Claims = User.Claims.Select(c => new ApplicationClaim { Type = c.Type, Value = c.Value })
            };
        }

        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();

            return Redirect("~/");
        }

        [HttpPost]
        public async Task<IActionResult> Register(string userName, string password)
        {
            if (string.IsNullOrEmpty(userName) || string.IsNullOrEmpty(password))
            {
                return BadRequest("Invalid user name or password.");
            }

            var user = new ApplicationUser { UserName = userName, Email = userName };
            var result = await userManager.CreateAsync(user, password);

            if (result.Succeeded)
            {
                try
                {
                    var code = await userManager.GenerateEmailConfirmationTokenAsync(user);

                    var callbackUrl = Url.Action("ConfirmEmail", "Account", new { userId = user.Id, code }, protocol: Request.Scheme);

                    var text = $@"Hi, <br /> <br />
We received your registration request for VersaControl. <br /> <br />
To confirm your registration please click the following link: <a href=""{callbackUrl}"">confirm your registration</a> <br /> <br />
If you didn't request this registration, you can safely ignore this email. Someone else might have typed your email address by mistake.";                    

                    await SendEmailAsync(user.Email, "Confirm your registration", text);


                    return Ok();
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }

            var message = string.Join(", ", result.Errors.Select(error => error.Description));

            return BadRequest(message);
        }

        public async Task<IActionResult> ConfirmEmail(string userId, string code)
        {
            var user = await userManager.FindByIdAsync(userId);

            var result = await userManager.ConfirmEmailAsync(user, code);

            if (result.Succeeded)
            {
                return Redirect("~/Login?info=Your registration has been confirmed");
            }

            return RedirectWithError("Invalid user or confirmation code");
        }

        private async Task SendEmailAsync(string to, string subject, string body)
        {

            var mailMessage = new System.Net.Mail.MailMessage();
            mailMessage.From = new System.Net.Mail.MailAddress(configuration.GetValue<string>("Smtp:User"));
            mailMessage.Body = body;
            mailMessage.Subject = subject;
            mailMessage.BodyEncoding = System.Text.Encoding.UTF8;
            mailMessage.SubjectEncoding = System.Text.Encoding.UTF8;
            mailMessage.IsBodyHtml = true;
            mailMessage.To.Add(to);

            var client = new System.Net.Mail.SmtpClient(configuration.GetValue<string>("Smtp:Host"))
            {
                UseDefaultCredentials = false,
                EnableSsl = configuration.GetValue<bool>("Smtp:Ssl"),
                Port = configuration.GetValue<int>("Smtp:Port"),
                Credentials = new System.Net.NetworkCredential(configuration.GetValue<string>("Smtp:User"), configuration.GetValue<string>("Smtp:Password"))
            };

            await client.SendMailAsync(mailMessage);
        }
    }
}

using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Text;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Globalization;
using OneStopApp_Api.ViewModel;
using OneStopApp.Interface;
using OneStopApp_Api.EntityFramework.Data;
using OneStopApp_Api.Interface;
using OneStopApp_Api.EntityFramework.Model;
using Dms.Services.ViewModel.Security;
using OneStopApp_Api.Enums;
using SendGrid;
using SendGrid.Helpers.Mail;
using onestopapp_api.Interface;

namespace OneStopApp.Service
{
    public class UserService : IUserService
    {
        private static TextInfo _casingHelper = new CultureInfo("en-US", false).TextInfo;
        private readonly OsaDbContext _context;
        private readonly IEmailDomainService _emailDomainService;
        private readonly ISaltPasswordService _saltPasswordService;
        private readonly ISharedService _sharedService;

        public UserService(OsaDbContext context, IEmailDomainService emailDomainService, ISaltPasswordService SaltedPasswordService, ISharedService sharedService)
        {
            _context = context;
            _emailDomainService = emailDomainService;
            _saltPasswordService = SaltedPasswordService;
            _sharedService = sharedService;
        }

        public async Task<bool> CheckUserExists(string userName)
        {
            bool userExists = false;
            userExists = await _context.Users.AnyAsync(usr => usr.UserName == userName);
            return userExists;
        }
        public async Task<bool> CheckEmailAddress(string email)
        {
            bool emailExists = false;
            emailExists = await _context.Users.AnyAsync(usr => usr.EmailAddress == email);
            return emailExists;
        }
        public RegisterUserViewModel CreateUser(RegisterUserViewModel Model)
        {
            if (Model == null) return null;

            var user = new User
            {
                UserName = Model.UserName,
                Password = _saltPasswordService.SaltPassword(Model.Password),
                EmailAddress = Model.Email,
                StatusId = (int)UserStatusEnum.Active,
                CreatedDate = DateTime.UtcNow,
            };
            _context.Users.Add(user);
            _context.SaveChanges();
            var User = _context.Users.Where(u => u.EmailAddress == Model.Email).First();
            var Profile = new Profile
            {
                UserId = User.Id,
                FirstName = Model.FirstName,
                LastName = Model.LastName,
                CountryId = Model.CountryId,

            };

            var UserRole = new UserRole
            {
                UserId = User.Id,
                RoleId = 1,
            };

            _context.Profiles.Add(Profile);
            _context.UserRoles.Add(UserRole);
            _context.SaveChanges();
            _sharedService.SendEmail("test email", User.EmailAddress);
            return Model;
        }

        public async Task<ResponseMessageViewModel> ForgotUserName(string email)
        {
            var response = new ResponseMessageViewModel();

            if (!string.IsNullOrEmpty(email))
            {
                var externalUser = await _context.Users
                .Where(a => a.EmailAddress == email)
                .Select(a => new { a.UserName, a.Id, a.EmailAddress }).FirstOrDefaultAsync();

                if (externalUser != null)
                {
                    var profile = await _context.Profiles
                    .Where(a => a.UserId == externalUser.Id)
                    .Select(a => new
                    {
                        FullName = string.Format("{0}, {1} {2}", a.LastName, a.FirstName, a.MiddleName)
                    }).FirstOrDefaultAsync();
                    string subject = "FAA DMS Log in Username";
                    StringBuilder sbrbody = new StringBuilder();
                    sbrbody.Append(string.Format("Dear {0}", profile.FullName));
                    sbrbody.AppendLine().AppendLine().Append("Please find your username for log in as following");
                    sbrbody.AppendLine().Append(string.Format("User name : {0}", externalUser.UserName));
                    sbrbody.AppendLine().AppendLine().Append("Designee Management System (DMS),").AppendLine().Append("Administration.");
                    string body = sbrbody.ToString();

                    // try
                    // {
                    _emailDomainService.SendEmail(new List<string>() { externalUser.EmailAddress }, new List<string>(), subject, body, null);
                    // }
                    // catch (Exception)
                    // {

                    // }

                    response.success = true;
                    response.Message = string.Format("An email has been sent to {0} with the User Name. Please click ok to return to the home page.", externalUser.EmailAddress);
                }
                else
                {
                    response.success = false;
                    response.Message = "The email address is either invalid or cannot be found.";
                }
            }

            return response;
        }

        public async Task<ResponseMessageViewModel> ChangePassword(ForgotUserNameChangePasswordViewModel model)
        {
            var respone = new ResponseMessageViewModel();
            var user = await _context.Users
                .Where(a => a.UserName == model.UserName)
                .Select(a => a).FirstOrDefaultAsync();

            if (user != null)
            {
                user.Password = _saltPasswordService.SaltPassword(model.Password);
                _context.SaveChanges();
                respone.success = true;
                respone.Message = "The password has been changed successfully.";
            }
            else
            {
                respone.success = false;
                respone.Message = "Failed to update password.";
            }
            return respone;
        }

        public List<CountryViewModel> GetCountries()
        {
            var Countries = _context.Countries.Select(c => new CountryViewModel
            {
                Id = c.Id,
                Name = c.NiceName,
                IsActive = c.IsActive
            }).ToList();
            return Countries;
        }
    }
}
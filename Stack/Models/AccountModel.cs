using Stack.DBModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Data.Entity.Migrations;
using System.Globalization;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations.Schema;
using Stack.Common;
using Exception = System.Exception;
using Stack.Entities;
using System.Net.Mail;
using System.Net;

namespace Stack.Models
{
    public class AccountModel
    {
        #region "Properties"
        private StackEntities db = new StackEntities();
        public int UserId { get; set; }

        [DisplayName("Email:")]
        [Required(ErrorMessage = "Please enter the email address.")]
        public string Email { get; set; }
        [DisplayName("Password:")]
        [Required(ErrorMessage = "Please enter the password.")]
        public string Password { get; set; }

        [DisplayName("Confirm Password:")]
        [Required(ErrorMessage = "Please enter the confirm password.")]
        public string ConfirmPassword { get; set; }
        public string ContactNumber { get; set; }

        [DisplayName("First Name:")]
        [Required(ErrorMessage = "Please enter the first name.")]
        public string FirstName { get; set; }

        [DisplayName("Last Name:")]
        [Required(ErrorMessage = "Please enter the last name.")]
        public string LastName { get; set; }
        [DisplayName("Gender:")]
        //[Required(ErrorMessage = "Please enter gender.")]
        public bool Gender { get; set; }
        public int GenderId { get; set; }
        public string Address { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public string PinCode { get; set; }
        public string ProfilePicUrl { get; set; }
        public string CreatedDate { get; set; }
        public string CreatedById { get; set; }
        public string CreatedBy { get; set; }
        public string LastEditedById { get; set; }
        public string LastEditedDate { get; set; }
        public string DOB { get; set; }
        public string Description { get; set; }
        //[Required(ErrorMessage = "Please enter the first name.")]
        public bool TermsOfConditions { get; set; }
        //[Required(ErrorMessage = "Please enter the first name.")]
        public bool SpecialOffers { get; set; }
        public int RoleId { get; set; }
        public string RoleName { get; set; }
        #endregion

        #region "Methods"
        public void Save()
        {
            using (var db = new StackContext())
            {
                var model = new Users
                {
                    UserId = UserId,
                    Email = Email,
                    FirstName = FirstName,
                    LastName = LastName,
                    Password = Password,
                    Gender = true,
                    CreatedDate = DateTime.Now,
                    LastEditedDate = DateTime.Now,
                    ProfilePicUrl = ProfilePicUrl,
                    Description = Description,
                    Address = Address,
                    State = State,
                    Country = Country,
                    //Da = DOB,
                    ContactNumber = ContactNumber
                };
                model.UserId = GetUserId(Email);
                db.Entry(model).State = EntityState.Modified;
                //db.Users.Remove(Password);
                db.Entry(model).Property(x => x.Password).IsModified = false;
                db.Entry(model).Property(x => x.CreatedDate).IsModified = false;
                db.Entry(model).Property(x => x.CreatedById).IsModified = false;
                db.Users.AddOrUpdate(model);
                db.SaveChanges();
            }
        }
        public void PasswordChangeLink()
        {
            string smtpAddress = "smtp.gmail.com";
            int portNumber = 587;
            bool enableSSL = true;
            string emailFromAddress = "trsmca35@gmail.com"; //Sender Email Address  
            string password = "Epam@123$"; //Sender Password  
            string emailToAddress = "receiver@gmail.com"; //Receiver Email Address  
            string subject = "Hello";
            string body = "Hello, This is Email sending test using gmail.";

            using (MailMessage mail = new MailMessage())
            {
                mail.From = new MailAddress(emailFromAddress);
                mail.To.Add(emailToAddress);
                mail.Subject = subject;
                mail.Body = body;
                mail.IsBodyHtml = true;
                //mail.Attachments.Add(new Attachment("D:\\TestFile.txt"));//--Uncomment this to send any attachment  
                using (SmtpClient smtp = new SmtpClient(smtpAddress, portNumber))
                {
                    smtp.Credentials = new NetworkCredential(emailFromAddress, password);
                    smtp.EnableSsl = enableSSL;
                    smtp.Send(mail);
                }
            }
        }
        public void UpdateProfile()
        {
            using (var db = new StackContext())
            {
                var model = new Users
                {
                    UserId = UserId,
                    Email = Email,
                    FirstName = FirstName,
                    LastName = LastName,
                    Password = Password,
                    Gender = true,
                    CreatedDate = DateTime.Now,
                    LastEditedDate = DateTime.Now,
                    ProfilePicUrl = ProfilePicUrl,
                    Description = Description
                };
                if (UserId == null)
                    model.UserId = GetUserId(Email);
                db.Entry(model).State = EntityState.Modified;
                //db.Users.Remove(Password);
                db.Entry(model).Property(x => x.Password).IsModified = false;
                //db.Entry(model).Property(x => x.UserId).IsModified = false;
                db.Entry(model).Property(x => x.Email).IsModified = false;
                db.Entry(model).Property(x => x.FirstName).IsModified = false;
                db.Entry(model).Property(x => x.LastName).IsModified = false;
                db.Entry(model).Property(x => x.Gender).IsModified = false;
                db.Entry(model).Property(x => x.CreatedDate).IsModified = false;
                db.Entry(model).Property(x => x.LastEditedDate).IsModified = false;
                db.Entry(model).Property(x => x.CreatedById).IsModified = false;
                db.Entry(model).Property(x => x.LastEditedById).IsModified = false;
                db.Users.AddOrUpdate(model);
                db.SaveChanges();
            }
        }
        public void Edit()
        {
            using (var db = new StackContext())
            {
                using (var ctx = new StackContext())
                {
                    var user = new Users();
                    if (!string.IsNullOrEmpty(Password))
                        user = ctx.Users.ToList().Find(x => x.Email == Email && x.Password == Password);
                    else
                        user = ctx.Users.ToList().Find(x => x.Email == Email);
                    if (user != null)
                    {
                        UserId = user.UserId;
                        FirstName = user.FirstName;
                        LastName = user.LastName;
                        Email = user.Email;
                        Address = user.Address;
                        ContactNumber = user.ContactNumber;
                        CreatedDate = user.CreatedDate.ToString(CultureInfo.InvariantCulture);
                        ProfilePicUrl = user.ProfilePicUrl;
                        Description = user.Description;
                        RoleId = user.RoleId;
                    }
                }
            }
        }
        public bool UserAlreadyExists(string eMail)
        {
            using (var db = new StackContext())
            {
                using (var ctx = new StackContext())
                {
                    var user = ctx.Users.ToList().Find(x => x.Email == eMail);
                    if (user != null)
                        return true;
                    else
                        return false;
                }
            }
        }
        public void Edit(int _userID)
        {
            using (var db = new StackContext())
            {
                using (var ctx = new StackContext())
                {
                    var user = ctx.Users.ToList().Find(x => x.UserId == _userID);
                    UserId = user.UserId;
                    Email = user.Email;
                    Password = user.Password;
                    ContactNumber = user.ContactNumber;
                    FirstName = user.FirstName;
                    LastName = user.LastName;
                    Gender = user.Gender;
                    Address = user.Address;
                    State = user.State;
                    Country = user.Country;
                    PinCode = user.PinCode;
                    ProfilePicUrl = user.ProfilePicUrl;
                    Description = user.Description;
                    CreatedDate = user.CreatedDate.ToString(CultureInfo.InvariantCulture);
                    //CreatedById = user.CreatedById;
                    LastEditedDate = user.LastEditedDate.ToString(CultureInfo.InvariantCulture);
                    //LastEditedById = user.LastEditedById;
                }
            }
        }
        public AccountModel GetUsers(int userId)
        {
            //try
            //{
            var model = new AccountModel();
            var user = db.Users.ToList().Find(x => x.UserId == UserId);
            if (user != null)
            {
                model.UserId = user.UserId;
                model.FirstName = user.FirstName;
                model.LastName = user.LastName;
                model.Email = user.Email;
                model.Address = user.Address;
                model.ContactNumber = user.ContactNumber;
                model.CreatedDate = user.CreatedDate.ToString();
                model.ProfilePicUrl = user.ProfilePicUrl;
                model.CreatedBy = user.FirstName + " " + user.LastName;
                //using (var db = new StackContext())
                //{
                //    using (var ctx = new StackContext())
                //    {
                //        var user = ctx.Users.ToList().Find(x => x.UserId == UserId);
                //        model.UserId = user.UserId;
                //        model.FirstName = user.FirstName;
                //        model.LastName = user.LastName;
                //        model.Email = user.Email;
                //        model.Address = user.Address;
                //        model.ContactNumber = user.ContactNumber;
                //        model.CreatedDate = user.CreatedDate.ToString(CultureInfo.InvariantCulture);
                //        model.ProfilePicUrl = user.ProfilePicUrl;
                //        model.CreatedBy = user.FirstName + " " + user.LastName;
                //    }
                //}
            }
            return model;
            //}
            //catch (Exception ex)
            //{

            //}

            return null;
        }
        public static int GetUserId(string Email)
        {
            using (var db = new StackContext())
            {
                using (var ctx = new StackContext())
                {
                    var user = ctx.Users.ToList().Find(x => x.Email == Email);
                    if (user != null)
                        return user.UserId;
                }
            }
            return 0;
        }

        #endregion
    }
}
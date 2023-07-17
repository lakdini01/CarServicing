//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace UserWebService.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class User
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "First name is required !")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Last name is required !")]
        public string LastName { get; set; }
        [Required(ErrorMessage = "Age is required !")]
        [Range(18, 70, ErrorMessage = "You are not eligible !")]
        public Nullable<int> Age { get; set; }
        [Required(ErrorMessage = "Gender is required !")]

        public string Gender { get; set; }
        [Required(ErrorMessage = "Mobile number is required !")]
        [DataType(DataType.PhoneNumber,ErrorMessage ="Mobile shouldn't contain non numeric values")]
        public Nullable<int> ContactNumber { get; set; }
        [Required(ErrorMessage = "Email is required !")]
        [DataType(DataType.EmailAddress, ErrorMessage = "Please put a valid email id !")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Password is required !")]
        [RegularExpression(@"^(?=.*[A-Za-z])(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{8,}$", ErrorMessage = "Password is not strong")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required(ErrorMessage = "Date is required")]
        [DataType(DataType.Date)]
        public Nullable<DateTime> DateOfJoin { get; set; }
        public Nullable<int> RoleId { get; set; }
    }
}
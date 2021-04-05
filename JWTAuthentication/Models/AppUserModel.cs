using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JWTAuthentication.Models
{
    public class AppUserModel
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string Mobile { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string PersonalCode { get; set; }
        public string UserAddress { get; set; }
        public bool? IsActive { get; set; }
        public bool? IsAdmin { get; set; }
        public string UserImage { get; set; }
        public string JobTitle { get; set; }
        public int? EducationDegreeId { get; set; }
        public int? GenderTypeId { get; set; }
        public int? CityId { get; set; }
    }
}

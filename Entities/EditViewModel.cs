using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TrainingSample.Entities
{
    public class EditViewModel
    {
        public EditViewModel()
        {
            CarDetails = new List<carDetailsUser>();
        }
        public int UserId { get; set; }
        public string FullName { get; set; }
        public string UserEmail { get; set; }
        public string PasswordHash { get; set; }
        public string CivilIdNumber { get; set; }
        public List<carDetailsUser> CarDetails { get; set; }
    }
    public class carDetailsUser
    {
        public int Id { get; set; }
        public string CarLicenseValue { get; set; }
    }
}
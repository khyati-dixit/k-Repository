using PagedList;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using TrainingSample.Entities;
using TrainingSample.EntityFramework;

namespace TrainingSample.Repository
{
    public class UserDetailsRepository : IUserDetails
    {
        
       
        public EditViewModel GetEditDetail(int id)
        {
            using (var en = new TraineeEntities())
            {
                var dtls = en.UserDetails.Where(x => x.UserId == id).FirstOrDefault();
                var dtls1 = en.CarDetails.Where(x => x.UserId == id).Select(y => new
                carDetailsUser
                {
                    CarLicenseValue = y.CarLicense,
                    Id = y.Id
                }).ToList();

                var userdetails = new EditViewModel
                {
                    UserId = dtls.UserId,
                    UserEmail = dtls.UserEmail,
                    FullName = dtls.FullName,
                    CivilIdNumber = dtls.CivilIdNumber
                };
                userdetails.CarDetails.AddRange(dtls1);
                return userdetails;
                //en.Entry(dtls).State = EntityState.Modified;
                //userdetails.CarDetails.AddRange(dtls1);
                //dtls1.CarLicense = insert.CarLicense;
                //en.Entry(dtls1).State = EntityState.Modified;
                ///en.SaveChanges();
        

            }
        }

        public void GetEditDetail1(EditViewModel insert)
        {
            using(var en = new TraineeEntities())
            {
                var dtls = en.UserDetails.Where(x => x.UserId == insert.UserId).FirstOrDefault();
                var dtls1 = en.CarDetails.Where(x => x.UserId == insert.UserId).ToList();

                //var dtls1 = en.CarDetails.Where(x => x.UserId == insert.UserId).FirstOrDefault();

                dtls.FullName = insert.FullName;
                dtls.UserEmail = insert.UserEmail;
                dtls.PasswordHash = insert.PasswordHash;
                dtls.CivilIdNumber = insert.CivilIdNumber;
                
                // dtls1.CarLicense = insert.CarDetails.AddRange();
                en.Entry(dtls).State = EntityState.Modified;

                foreach (var car in insert.CarDetails)
                {
                    var userCar = dtls1.Where(x => x.Id == car.Id).FirstOrDefault();

                    userCar.CarLicense = car.CarLicenseValue;

                    en.Entry(userCar).State = EntityState.Modified;
                }

                en.SaveChanges();
            }
            
        }
        public void GetInsertDetail(UserDetails insert)
        {
            using (var dbContext = new TraineeEntities())
            {
                var user = new UserDetail()
                {
                    FullName = insert.FullName,
                    UserEmail = insert.UserEmail,
                    PasswordHash = insert.PasswordHash,
                    CivilIdNumber = insert.CivilIdNumber,
                    DOB = insert.DOB,
                    MobileNo = insert.MobileNo,
                    Address = insert.Address,
                    RoleId = insert.RoleId,
                    ProfilePic = insert.ProfilePic,
                    //CreatedDate = insert.CreatedDate,
                    //ModifiedDate = insert.ModifiedDate,
                    //IsNotificationActive = insert.IsNotificationActive,
                    //IsActive = insert.IsActive,
                    //DeviceId = insert.DeviceId,
                    //DeviceType = insert.DeviceType,
                    //FcmToken = insert.FcmToken,
                    //verify = insert.verify,
                    //VerifiedBy = insert.VerifiedBy,
                    //Area = insert.Area,
                    //Block = insert.Block,
                    //Street = insert.Street,
                    //Housing = insert.Housing,
                    //Floor = insert.Floor,
                    //NewPass = insert.NewPass,
                    //ConPass = insert.ConPass,
                    //Jadda = insert.Jadda,
                    //Reason = insert.Reason,
                    //ActivatedBy = insert.ActivatedBy,
                    //ActivatedDate = insert.ActivatedDate
                };
                var car = new CarDetail()
                {
                    CarLicense = insert.CarLicense,
                    UserId = insert.UserId
                };
                dbContext.UserDetails.Add(user);
                dbContext.CarDetails.Add(car);
                dbContext.SaveChanges();
            }
        }

        public IEnumerable<UserDetails> GetUserDetails()
        {
            using (var dbContext = new TraineeEntities())
            {
                List<UserDetail> userDetails = dbContext.UserDetails.ToList();
                List<CarDetail> carDetails = dbContext.CarDetails.ToList();
                List<UserDetails> userViewModels = new List<UserDetails>();
                foreach (var user in userDetails)
                {

                    var data = new UserDetails
                    {

                        UserId = user.UserId,
                        FullName = user.FullName,
                        UserEmail = user.UserEmail,
                        CivilIdNumber = user.CivilIdNumber,


                    };

                    var cardetails = string.Join(",", carDetails.Where(x => x.UserId == user.UserId).Select(y => y.CarLicense).ToList());

                    data.CarLicense = cardetails;


                    userViewModels.Add(data);

                }

                return userViewModels;




            }



        }
        public void GetDeleteDetail(int? id)
        {
            using (var dbContext = new TraineeEntities())
            {
                var user = dbContext.UserDetails.Where(x => x.UserId == id).FirstOrDefault();
                var car = dbContext.CarDetails.Where(x => x.UserId == id).ToList();
                user.IsActive = false;
                dbContext.Entry(user).State = EntityState.Modified;
                dbContext.UserDetails.Remove(user);
                if (car.Count() > 0)
                {
                    dbContext.CarDetails.RemoveRange(car)
;
                }
                dbContext.SaveChanges();
            }


        }
    }
}
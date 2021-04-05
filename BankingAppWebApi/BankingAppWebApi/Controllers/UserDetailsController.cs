using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BankingAppWebApi.Models;
using System.Web.Http.Cors;


namespace BankingAppWebApi.Controllers
{
    
    
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class UserDetailsController : ApiController
    {
        //[HttpGet]
        //public HttpResponseMessage ShowUserDetails()
        //{
        //    BankingAppEntities db = new BankingAppEntities();
        //    var data = from user in db.UserDetails select new { DeptName = emp.Dept.Name, Location = emp.Dept.Location, EmpId = emp.Id, EmpName = emp.Name, Salary = emp.Salary };
        //    return Request.CreateResponse(HttpStatusCode.OK, data);
        //}


        //[HttpGet]
        [Route("api/UserDetails/GetUserDetails")]
        public HttpResponseMessage GetUserDetails()
        {
            using (BankingAppEntities db = new BankingAppEntities())
            {
                var data = db.UserDetails.ToList();
                return Request.CreateResponse(HttpStatusCode.OK, data);
            }
        }


        //[HttpGet]
       /* [Route("api/UserDetails/GetUserDetails/{id}")]
        public HttpResponseMessage GetUserDetails(int id)
        {
            using (BankingAppEntities db = new BankingAppEntities())
            {
                var data = db.UserDetails.Find(id);
                if (data != null)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, data);
                }
                else
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "User with id: " + id + " not found");
                }
            }
        }*/


        [Route("api/UserDetails/PostUserDetails")]
        public HttpResponseMessage PostUserDetails([FromBody] UserDetails userdetails)
        {
            try
            {
                using (BankingAppEntities db = new BankingAppEntities())
                {
                    db.UserDetails.Add(userdetails);
                    db.SaveChanges();
                    return Request.CreateResponse(HttpStatusCode.Created, userdetails);
                }
            }
            catch (Exception e)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, e);
            }
        }

        public HttpResponseMessage PutUserDetails(int id, [FromBody] UserDetails userdetails)
        {
            try
            {
                using (BankingAppEntities db = new BankingAppEntities())
                {
                    var data = db.UserDetails.Find(id);
                    if (data == null)
                    {
                        return Request.CreateResponse(HttpStatusCode.NotFound, "User with id: " + id + " not found");
                    }
                    else
                    {
                        data.Title = userdetails.Title;
                        data.FirstName = userdetails.FirstName;
                        data.MiddleName = userdetails.MiddleName;
                        data.LastName = userdetails.LastName;
                        data.FatherName = userdetails.FatherName;
                        data.MobileNumber = userdetails.MobileNumber;
                        data.Email = userdetails.Email;
                        data.AadharNumber = userdetails.AadharNumber;
                        data.DOB = userdetails.DOB;
                        data.ResidentialAddressLine1 = userdetails.ResidentialAddressLine1;
                        data.ResidentialAddressLine2 = userdetails.ResidentialAddressLine2;
                        data.ResidentialLandmark = userdetails.ResidentialLandmark;
                        data.ResidentState = userdetails.ResidentState;
                        data.ResidentialCity = userdetails.ResidentialCity;
                        data.ResidentialPinCode = userdetails.ResidentialPinCode;
                        data.PermanentAddressLine1 = userdetails.PermanentAddressLine1;
                        data.PermanentAddressLine2 = userdetails.PermanentAddressLine2;
                        data.PermanentLandmark = userdetails.PermanentLandmark;
                        data.PermanentState = userdetails.PermanentState;
                        data.PermanentCity = userdetails.PermanentCity;
                        data.PermanentPinCode = userdetails.PermanentPinCode;
                        data.OccupationalType = userdetails.OccupationalType;
                        data.SourceOfIncome = userdetails.SourceOfIncome;
                        data.GrossAnnualIncome = userdetails.GrossAnnualIncome;
                        data.IsNetBanking = userdetails.IsNetBanking;
                        data.DebitCard = userdetails.DebitCard;
                        db.SaveChanges();
                        return Request.CreateResponse(HttpStatusCode.OK, data);
                    }
                }
            }
            catch (Exception e)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, e.Message);
            }
        }


        [Route("api/UserDetails/DeleteUserDetails/{id}")]
        public HttpResponseMessage DeleteUserDetails(int id)
        {
            try
            {
                using (BankingAppEntities db = new BankingAppEntities())
                {
                    var data = db.UserDetails.Find(id);
                    if (data == null)
                    {
                        return Request.CreateResponse(HttpStatusCode.NotFound, "User with id: " + id + " not found");
                    }
                    else
                    {
                        db.UserDetails.Remove(data);
                        db.SaveChanges();
                        return Request.CreateResponse(HttpStatusCode.OK, data);
                    }
                }
            }
            catch (Exception e)
            {
               return Request.CreateErrorResponse(HttpStatusCode.BadRequest, e.Message);
            }
        }


        [Route("api/UserDetails/GetUserDetails/{id}")]
        public HttpResponseMessage GetUserDetails(int id)
        {
            using (BankingAppEntities db = new BankingAppEntities())
            {
                var data = db.UserDetails.Find(id);
                if (data != null)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, data);
                }
                else
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "User with id: " + id + " not found");
                }
            }
        }


        [Route("api/UserDetails/GetUserDetailsAdmin")]
        public HttpResponseMessage GetUserDetailsAdmin()
        {
            using (BankingAppEntities db = new BankingAppEntities())
            {
                var data = (from a in db.UserDetails select new AdminUserDetails { Id = a.Id, Name = a.FirstName + " " + a.MiddleName + " " + a.LastName, MobileNumber = a.MobileNumber }).ToList();
                return Request.CreateResponse(HttpStatusCode.OK, data);
            }
        }

    }
}
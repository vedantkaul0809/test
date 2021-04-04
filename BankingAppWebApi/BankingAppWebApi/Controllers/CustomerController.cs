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
    //change 1 in customer
    
    
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class CustomerController : ApiController
    {
        //[HttpGet]
        [Route("api/Customer/GetCustomer")]
        public HttpResponseMessage GetCustomer()
        {
            using (BankingAppEntities db = new BankingAppEntities())
            {
                var data = db.Customers.ToList();
                return Request.CreateResponse(HttpStatusCode.OK, data);
            }
        }


        //[HttpGet]
        [Route("api/Customer/GetCustomer/{AccountNumber}")]
        public HttpResponseMessage GetCustomer(long AccountNumber)
        {
            using (BankingAppEntities db = new BankingAppEntities())
            {
                var data = db.Customers.Find(AccountNumber);
                if (data != null)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, data);
                }
                else
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "User with Account number: " + AccountNumber + " not found");
                }
            }
        }


        [HttpGet]
        [Route("api/Customer/ShowCustomerForgotId/{AccountNumber}/{Otp}")]
        public HttpResponseMessage ShowCustomerForgotId(long AccountNumber, long Otp)
        {
            using (BankingAppEntities db = new BankingAppEntities())
            {
                //var data = db.Customers.Find(AccountNumber);
                //var val = data.Where(s => s.Otp == Otp).FirstOrDefault();
                ///var data = from s in db.Customers where s.AccountNumber == AccountNumber && s.Otp == Otp select s;
                var data = db.Customers.Where(s => s.AccountNumber == AccountNumber).Where(s => s.Otp == Otp).FirstOrDefault();
                if (data != null)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, data);
                }
                else
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Invalid Otp");
                }
            }
        }


        [HttpGet]
        [Route("api/Customer/ShowCustomerById/{CustomerId}")]
        public HttpResponseMessage ShowCustomerById(int CustomerId)
        {
            using (BankingAppEntities db = new BankingAppEntities())
            {
                var data = db.Customers.Where(s => s.CustomerId == CustomerId).FirstOrDefault();
                if (data != null)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, data);
                }
                else
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "User with Customer ID: " + CustomerId + " not found");
                }
            }
        }


        [HttpGet]
        [Route("api/Customer/ShowCustomerByIdOtp/{CustomerId}/{Otp}")]
        public HttpResponseMessage ShowCustomerByIdOtp(int CustomerId, long Otp)
        {
            using (BankingAppEntities db = new BankingAppEntities())
            {
                var data = db.Customers.Where(s => s.CustomerId == CustomerId).Where(s => s.Otp == Otp).FirstOrDefault();
                if (data != null)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, data);
                }
                else
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Invalid Otp ");
                }
            }
        }

        [Route("api/Customer/PostCustomerByIdOtp")]
        public HttpResponseMessage PostCustomerByIdOtp([FromBody] Customer customer)
        {
            try
            {
                using (BankingAppEntities db = new BankingAppEntities())
                {
                    var data = db.Customers.Where(c => c.CustomerId == customer.CustomerId && c.Otp == customer.Otp).FirstOrDefault();

                    if (data != null)

                        return Request.CreateResponse(HttpStatusCode.OK, "Valid customerid and otp");

                    else

                        return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Invalid credentials ");
                }
            }
            catch (Exception e)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, e);
            }
        }



        [Route("api/Customer/PutCustomerById/{CustomerId}")]
        public HttpResponseMessage PutCustomer(int CustomerId, [FromBody] Customer customer)
        {
            try
            {
                using (BankingAppEntities db = new BankingAppEntities())
                {
                    var data = db.Customers.Where(c => c.CustomerId == customer.CustomerId).FirstOrDefault();
                    if (data == null)
                    {
                        return Request.CreateResponse(HttpStatusCode.NotFound, "User with Customer Id " + CustomerId + " not found");
                    }
                    else
                    {
                        data.LoginPassword = customer.LoginPassword;
                        // data.TransactionPassword = customer.TransactionPassword;
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



        [Route("api/Customer/PostCustomer")]
        public HttpResponseMessage PostCustomer([FromBody] Customer customer)
        {
            try
            {
                using (BankingAppEntities db = new BankingAppEntities())
                {
                    db.Customers.Add(customer);
                    db.SaveChanges();
                    return Request.CreateResponse(HttpStatusCode.Created, customer);
                }
            }
            catch (Exception e)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, e);
            }
        }


        //public HttpResponseMessage PutCustomer(long AccountNumber, [FromBody] Customer customer)
        //{
        //    try
        //    {
        //        using (BankingAppEntities db = new BankingAppEntities())
        //        {
        //            var data = db.Customers.Find(AccountNumber);
        //            if (data == null)
        //            {
        //                return Request.CreateResponse(HttpStatusCode.NotFound, "User with Account Number: " + AccountNumber + " not found");
        //            }
        //            else
        //            {
        //                data.LoginPassword = customer.LoginPassword;
        //                data.TransactionPassword = customer.TransactionPassword;
        //                db.SaveChanges();
        //                return Request.CreateResponse(HttpStatusCode.OK, data);
        //            }
        //        }
        //    }
        //    catch (Exception e)
        //    {
        //        return Request.CreateErrorResponse(HttpStatusCode.BadRequest, e.Message);
        //    }
        //}


        [Route("api/Customer/DeleteCustomer/{AccountNumber}")]
        public HttpResponseMessage DeleteCustomer(long AccountNumber)
        {
            try
            {
                using (BankingAppEntities db = new BankingAppEntities())
                {
                    var data = db.Customers.Find(AccountNumber);
                    if (data == null)
                    {
                        return Request.CreateResponse(HttpStatusCode.NotFound, "User with Account number: " + AccountNumber + " not found");
                    }
                    else
                    {
                        db.Customers.Remove(data);
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


        [Route("api/Customer/PutCustomer/{AccountNumber}")]
        public HttpResponseMessage PutCustomer(long AccountNumber, [FromBody] Customer customer)
        {
            try
            {
                using (BankingAppEntities db = new BankingAppEntities())
                {
                    var data = db.Customers.Where(c => c.AccountNumber == customer.AccountNumber).FirstOrDefault();
                    if (data == null)
                    {
                        return Request.CreateResponse(HttpStatusCode.NotFound, "User with Account Number: " + AccountNumber + " not found");
                    }
                    else
                    {
                        data.LoginPassword = customer.LoginPassword;
                        data.TransactionPassword = customer.TransactionPassword;
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



        [Route("api/Customer/PutCustomerNetBanking")]
        public HttpResponseMessage PutCustomerNetBanking([FromBody] Customer customer)
        {
            try
            {
                using (BankingAppEntities db = new BankingAppEntities())
                {
                    var data = db.Customers.Find(customer.AccountNumber);

                    if (data.Otp == customer.Otp)
                    {
                        data.LoginPassword = customer.LoginPassword;
                        data.TransactionPassword = customer.TransactionPassword;
                        db.SaveChanges();
                        return Request.CreateResponse(HttpStatusCode.OK, "User with Account Number: " + customer.AccountNumber + " registered for Net Banking successfully!");
                    }
                    else
                    {

                        return Request.CreateResponse(HttpStatusCode.NotFound, "Invalid Account Number or Otp");
                    }
                }
            }
            catch (Exception e)
            {

                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, e.Message);
            }
        }


       /* [Route("api/Customer/PutCustomerNetBanking/{AccountNumber}")]
        public HttpResponseMessage PutCustomerNetBanking(long AccountNumber, [FromBody] Customer customer)
        {
            try
            {
                using (BankingAppEntities db = new BankingAppEntities())
                {
                    var data = db.Customers.Find(AccountNumber);

                    if (data.Otp == customer.Otp)
                    {
                        data.LoginPassword = customer.LoginPassword;
                        data.TransactionPassword = customer.TransactionPassword;
                        db.SaveChanges();
                        return Request.CreateResponse(HttpStatusCode.OK, "User with Account Number: " + AccountNumber + " registered for Net Banking successfully!");
                    }
                    else
                    {

                        return Request.CreateResponse(HttpStatusCode.NotFound, "Invalid Account Number or Otp");
                    }
                }
            }
            catch (Exception e)
            {

                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, e.Message);
            }
        }*/

       /* [Route("api/Customer/PostLogin")]
        public HttpResponseMessage PostLogin([FromBody] Customer customer)
        {
            try
            {
                using (BankingAppEntities db = new BankingAppEntities())
                {
                    //UserLogin_Result res = db.UserLogin(customer.CustomerId, customer.LoginPassword).FirstOrDefault();
                   // var data = db.Customers.Where(c => c.AccountNumber == customer.AccountNumber).FirstOrDefault();
                    var data = db.Customers.Where(c => c.CustomerId == customer.CustomerId).FirstOrDefault();
                    if (data.LoginPassword == customer.LoginPassword)
                        return Request.CreateResponse(HttpStatusCode.OK, "Logged in as Admin");
                    else
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Invalid Credentials");
                }
            }
            catch (Exception e)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, e);
            }
        }*/


        [Route("api/Customer/PostLogin")]
        public HttpResponseMessage PostLogin([FromBody] Customer customer)
        {
            try
            {
                using (BankingAppEntities db = new BankingAppEntities())
                {
                    UserLogin_Result data = db.UserLogin(customer.CustomerId, customer.LoginPassword).FirstOrDefault();
                    if (data != null)
                        return Request.CreateResponse(HttpStatusCode.OK, data);
                    else
                        return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Invalid");
                }
            }
            catch (Exception e)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, e);
            }
        }



        [Route("api/Customer/PutCustomerLogin/{CustomerId}")]
        public HttpResponseMessage PutCustomerLogin(int CustomerId, [FromBody] Customer customer)
        {
            try
            {
                using (BankingAppEntities db = new BankingAppEntities())
                {
                    var data = db.Customers.Where(s => s.CustomerId == CustomerId).FirstOrDefault();
                    if (data == null)
                    {
                        return Request.CreateResponse(HttpStatusCode.NotFound, "Customer with id not found");
                    }
                    else
                    {
                        //data.TotalCnt = customer.TotalCnt;
                        data.TotalCnt = data.TotalCnt + 1;
                        if (data.TotalCnt >= 3)
                        {
                            data.Status = 1;
                        }
                        else
                            data.Status = 0;
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


        [Route("api/Customer/PutCustomerChangePassword/{CustomerId}")]
        public HttpResponseMessage PutCustomerChangePassword(int CustomerId, [FromBody] Customer customer)
        {
            try
            {
                using (BankingAppEntities db = new BankingAppEntities())
                {
                    var data = db.Customers.Where(s => s.CustomerId == CustomerId).FirstOrDefault();
                    if (data == null)
                    {
                        return Request.CreateResponse(HttpStatusCode.NotFound, "Customer with id not found");
                    }
                    else
                    {
                        //data.TotalCnt = customer.TotalCnt;
                        //data.TotalCnt = data.TotalCnt + 1;
                        //if (data.TotalCnt >= 3)
                        //{
                        //    data.Status = 1;
                        //}
                        //else
                        data.TotalCnt = 0;
                        data.Status = 0;
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

    }
}
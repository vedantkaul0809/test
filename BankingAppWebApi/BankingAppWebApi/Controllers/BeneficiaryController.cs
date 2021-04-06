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
    
   //change 1 in beneficiary controller
  
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class BeneficiaryController : ApiController
    {
        [Route("api/Beneficiary/ShowBeneficiary/{AccNo}")]
        [HttpGet]
        public HttpResponseMessage ShowBeneficiary(long AccNo)
        {
            BankingAppEntities db = new BankingAppEntities();
            var data = from user in db.Beneficiaries where user.HolderAccountNumber == AccNo select new 
                { user.BeneficiaryAccountNumber, user.Nickname };
            return Request.CreateResponse(HttpStatusCode.OK, data);
        }


        [Route("api/Beneficiary/GetBeneficiary")]
        public HttpResponseMessage GetBeneficiary()
        {
            using (BankingAppEntities db = new BankingAppEntities())
            {
                var data = db.Beneficiaries.ToList();
                return Request.CreateResponse(HttpStatusCode.OK, data);
            }
        }


        //[HttpGet]
        //public HttpResponseMessage GetBeneficiary(int bid)
        //{
        //    using (BankingAppEntities db = new BankingAppEntities())
        //    {
        //        var data = db.Beneficiaries.Find(bid);
        //        if (data != null)
        //        {
        //            return Request.CreateResponse(HttpStatusCode.OK, data);
        //        }
        //        else
        //        {
        //            return Request.CreateErrorResponse(HttpStatusCode.NotFound, "User with Id: " + AccountNumber + " not found");
        //        }
        //    }
        //}


        [Route("api/Beneficiary/PostBeneficiary")]
        public HttpResponseMessage PostBeneficiary([FromBody] Beneficiary beneficiary)
        {
            try
            {
                using (BankingAppEntities db = new BankingAppEntities())
                {
                    db.Beneficiaries.Add(beneficiary);
                    db.SaveChanges();
                    return Request.CreateResponse(HttpStatusCode.Created, beneficiary);
                }
            }
            catch (Exception e)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, e);
            }
        }


        //public HttpResponseMessage PutBeneficiary(long b_acc, [FromBody] Beneficiary beneficiary)
        //{
        //    try
        //    {
        //        using (BankingAppEntities db = new BankingAppEntities())
        //        {
        //            var data = db.Beneficiaries.Where(b=>b.BeneficiaryAccountNumber==b_acc);
        //            if (data == null)
        //            {
        //                return Request.CreateResponse(HttpStatusCode.NotFound, "Beneficiary with Account Number: " + b_acc + " not found");
        //            }
        //            else
        //            {
        //                data.BeneficiaryId= beneficiary.BeneficiaryId;
        //                data.Nickname = beneficiary.Nickname;
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


        [Route("api/Beneficiary/DeleteBeneficiary/{b_ac}")]
        public HttpResponseMessage DeleteBeneficiary(long b_ac)
        {
            try
            {
                using (BankingAppEntities db = new BankingAppEntities())
                {
                    var data = db.Beneficiaries.Find(b_ac);
                    if (data == null)
                    {
                        return Request.CreateResponse(HttpStatusCode.NotFound, "Beneficiary with Account number: " + b_ac + " not found");
                    }
                    else
                    {
                        db.Beneficiaries.Remove(data);
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


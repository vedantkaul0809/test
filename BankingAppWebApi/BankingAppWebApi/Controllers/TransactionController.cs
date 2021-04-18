using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BankingAppWebApi.Models;
using System.Web.Http.Cors;



namespace BankingAppWebApi.Controllers
{//pullrequest change in transaction 
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class TransactionController : ApiController
    {

        [Route("api/Transaction/ShowTransaction/{AccNo}")]
        [HttpGet]
        public HttpResponseMessage ShowTransaction(long AccNo)
        {
            BankingAppEntities db = new BankingAppEntities();
            var data = from user in db.Transactions where user.FromAccountNumber == AccNo select new { user.TId, user.ToAccountNumber, user.Amount, user.TransactionType, user.MaturityInstruction, user.Remark, user.TransactionDate };
            return Request.CreateResponse(HttpStatusCode.OK, data);
        }


        //[Route("api/Transaction/ShowTransaction/{AccNo}")]
        //[HttpGet]
        //public HttpResponseMessage ShowTransactionBetweenDates(long AccNo)
        //{
        //    BankingAppEntities db = new BankingAppEntities();
        //    var data = from user in db.Transactions where user.FromAccountNumber == AccNo select new { user.TId, user.ToAccountNumber, user.Amount, user.TransactionType, user.MaturityInstruction, user.Remark, user.TransactionDate };
        //    return Request.CreateResponse(HttpStatusCode.OK, data);
        //}

        [Route("api/Transaction/ShowLast5Transaction/{AccNo}")]
        [HttpGet]
        public HttpResponseMessage ShowLast5Transaction(long AccNo)
        {
            BankingAppEntities db = new BankingAppEntities();
            var data = from user in db.Transactions where user.FromAccountNumber == AccNo orderby user.TransactionDate select new { user.TId, user.ToAccountNumber, user.Amount, user.TransactionType, user.MaturityInstruction, user.Remark, user.TransactionDate };
            return Request.CreateResponse(HttpStatusCode.OK, data.Take(5));
        }

        [Route("api/Transaction/GetTransaction")]
        public HttpResponseMessage GetTransaction()
        {
            using (BankingAppEntities db = new BankingAppEntities())
            {
                var data = db.Transactions.ToList();
                return Request.CreateResponse(HttpStatusCode.OK, data);
            }
        }


        [Route("api/Transaction/GetTransaction/{t_id}")]
        public HttpResponseMessage GetTransaction(int t_id)
        {
            using (BankingAppEntities db = new BankingAppEntities())
            {
                var data = db.Transactions.Find(t_id);
                if (data != null)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, data);
                }
                else
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Transaction with Id: " + t_id + " not found");
                }
            }
        }


        [Route("api/Transaction/PostTransaction")]
        public HttpResponseMessage PostTransaction([FromBody] Transaction transaction)
        {
            try
            {
                using (BankingAppEntities db = new BankingAppEntities())
                {
                    db.Transactions.Add(transaction);
                    db.SaveChanges();
                    return Request.CreateResponse(HttpStatusCode.Created, transaction);
                }
            }
            catch (Exception e)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, e);
            }
        }

        [HttpGet]
        public HttpResponseMessage ShowTransactionStatementDate(long AccNo, DateTime datestart, DateTime dateend)
        {
            BankingAppEntities db = new BankingAppEntities();

            var data = (from user in db.Transactions
                        where (user.FromAccountNumber == AccNo || user.ToAccountNumber == AccNo)
                        && (user.TransactionDate > datestart && user.TransactionDate < dateend)
                        orderby user.TransactionDate descending
                        select new { user.TId, user.ToAccountNumber, user.FromAccountNumber, user.Amount, user.TransactionType, 
                            user.MaturityInstruction, user.Remark, user.TransactionDate }).ToList();

            if (data != null)
            {
                return Request.CreateResponse(HttpStatusCode.OK, data);
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Invalid Account Number");
            }
        }
    }
}

using IMS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Http;

namespace IMS.Controllers
{
    [RoutePrefix("api/User")]

    public class UserController : ApiController
    {
        // GET: User
     
        TestEntities db = new TestEntities();



        [HttpGet]
        [Route("GetUserByID/{ID}")]
        public IHttpActionResult GetUserByID(int ID)
        {
            try
            {
                using (TestEntities db = new TestEntities())
                {


                    List<Users> result = (from i in db.tblUsers.AsNoTracking()
                                          where i.ID == ID
                                          select new Users
                                          {
                                              ID = i.ID,
                                              UserName = i.UserName,
                                              Email = i.Email,
                                              CreatedAt = i.CreatedAt,
                                          }).ToList();

                    return Ok(result);
                }
            }

            catch (Exception ex)
            {
                return InternalServerError();
            }
        }

    

        [HttpPost]
        [Route("AddUser")]
        public IHttpActionResult AddUser([FromBody] Users users)
        {
            try
            {
                using (TestEntities db = new TestEntities())
                {


                    tblUser tblUser = new tblUser();
                    tblUser.UserName = users.UserName;
                    tblUser.Email = users.Email;
                    tblUser.CreatedAt = users.CreatedAt;
                    db.tblUsers.Add(tblUser);
                    db.SaveChanges();
                    return Ok("User Added Successfully");
                }
            }
            catch (Exception ex)
            {
                return InternalServerError();
            }
        }

        [HttpPut]
        [Route("UpdateUserByID")]
        public IHttpActionResult UpdateUserByID([FromBody] Users users)
        {
            try
            {
                using (TestEntities db = new TestEntities())
                {


                    tblUser tblUser = new tblUser();
                    tblUser = db.tblUsers.Where(i => i.ID == users.ID).FirstOrDefault();
                    if (tblUser != null)
                    {
                        tblUser.UserName = users.UserName;
                        tblUser.Email = users.Email;
                        db.SaveChanges();
                        return Ok("User Updated Successfully");
                    }
                    else
                    {
                        return Ok(HttpStatusCode.NoContent);
                    }

                }
            }
            catch (Exception ex)
            {
                return InternalServerError();
            }
        }
    }
}
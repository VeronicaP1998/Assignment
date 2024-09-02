using IMS.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace IMS.Controllers
{
    [RoutePrefix("api/Blog")]

    public class BlogController : ApiController
    {
        [HttpPost]
        [Route("PostCreation")]
        public IHttpActionResult PostCreation([FromBody] Post inputObjPost)
        {
            try
            {
                using (TestEntities db = new TestEntities())
                {
                    
                    tblPost objtblPost = new tblPost();
                    objtblPost.Title = inputObjPost.Title;
                    objtblPost.Content = inputObjPost.Content;
                    objtblPost.Author_ID = inputObjPost.Author_ID;
                    objtblPost.Created_At = inputObjPost.Created_At;
                    db.tblPosts.Add(objtblPost);
                    db.SaveChanges();
                    return Ok("Post Created Successfully");
                }

            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [HttpPost]
        [Route("{ID}/CommentCreation")]//PostID
        public IHttpActionResult CommentCreation(int ID,[FromBody] Comments inputObjComment)
        {
            try
            {
                using (TestEntities db = new TestEntities())
                {
                    tblComment objtblComment = new tblComment();
                    objtblComment.Post_ID = ID;
                    objtblComment.Content = inputObjComment.Content;
                    objtblComment.Author_ID = inputObjComment.Author_ID;
                    objtblComment.Created_At = inputObjComment.Created_At;
                    db.tblComments.Add(objtblComment);
                    db.SaveChanges();
                    return Ok("Comment Created Successfully");
                }

            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [HttpGet]
        [Route("{id}/FetchAllComments")]//PostID
        public IHttpActionResult FetchAllComments(int ID)
        {
            try
            {
                using (TestEntities db = new TestEntities())
                {
                    PostDTO result = (from p in db.tblPosts
                                            where p.ID == ID
                                            select new PostDTO
                                            {
                                                ID = p.ID,
                                                Comments = (from cmnt in db.tblComments
                                                            where cmnt.Post_ID == p.ID
                                                            select new Comments
                                                            {
                                                                Author_ID = cmnt.Author_ID,
                                                                Content = cmnt.Content,
                                                                Post_ID = cmnt.Post_ID,
                                                                Created_At = cmnt.Created_At
                                                            }).ToList()

                                            }).FirstOrDefault();
                    



                    return Ok(result);
                }
            }

            catch (Exception ex)
            {
                return InternalServerError();
            }
        }

    }
}
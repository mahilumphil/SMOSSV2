using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Microsoft.AspNet.Identity;

namespace SmartShopWebApp.ApiControllers
{
    public class ApiActPostItemCommentController : ApiController
    {
        private Data.smartshopdbDataContext db = new Data.smartshopdbDataContext();

        [HttpPost, Route("api/add/postitemcomment")]
        public HttpResponseMessage addActPostItemComment(Entities.ActPostItemComment add)
        {
            try
            {
                Data.ActPostItemComment newActPostItemComment = new Data.ActPostItemComment();

                var userId = User.Identity.GetUserId();
                newActPostItemComment.PostId = add.PostId;
                newActPostItemComment.Comment = add.Comment;
                newActPostItemComment.CommentByUserId = userId;
                newActPostItemComment.CommentDate = DateTime.Today;
                newActPostItemComment.UpdatedDate = DateTime.Today;
                db.ActPostItemComments.InsertOnSubmit(newActPostItemComment);
                db.SubmitChanges();

                return Request.CreateResponse(HttpStatusCode.OK);

            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
        }


        [HttpGet, Route("api/list/postitemcomment/{postId}")]
        public List<Entities.ActPostItemComment> listActPostItemComment(String postId)
        {

            var postComment = from d in db.ActPostItemComments
                              where d.PostId == Convert.ToInt32(postId)
                              select new Entities.ActPostItemComment
                              {
                                  Id = d.Id,
                                  PostId = d.PostId,
                                  Comment = d.Comment,
                                  CommentByUserId = d.CommentByUserId,
                                  CommentByUser = d.AspNetUser.FullName,
                                  CommentDate = d.CommentDate.ToShortDateString(),
                                  UpdatedDate = d.UpdatedDate.ToShortDateString()
                              };
            return postComment.ToList();
        }



        [HttpDelete, Route("api/delete/postitemcomment/{id}/{commentByUserId}")]
        public HttpResponseMessage deleteActPostItemComment(String id, String commentByUserId)
        {
            try
            {
                var userId = User.Identity.GetUserId();
                var delActPostItemComment = from d in db.ActPostItemComments
                                            where d.Id == Convert.ToInt32(id)
                                            select d;

                if (delActPostItemComment.Any())
                {
                    if (delActPostItemComment.FirstOrDefault().CommentByUserId.Equals(userId))
                    {
                        db.ActPostItemComments.DeleteOnSubmit(delActPostItemComment.First());
                        db.SubmitChanges();

                        return Request.CreateResponse(HttpStatusCode.OK);
                    }
                    else {
                        return Request.CreateResponse(HttpStatusCode.BadRequest);
                    }

                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.NotFound);
                }
            }
            catch (Exception e)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }


        }


        [HttpPut, Route("api/update/postitemcomment/{id}")]
        public HttpResponseMessage updateActPostItemComment(String id, Entities.ActPostItemComment postitemcomment)
        {
            try
            {
                var actPostItemComment = from d in db.ActPostItemComments
                                         where d.Id == Convert.ToInt32(id)
                                         select d;

                if (actPostItemComment.Any())
                {
                    var updateActPostItemComment = actPostItemComment.FirstOrDefault();
                    updateActPostItemComment.Id = postitemcomment.Id;
                    updateActPostItemComment.PostId = postitemcomment.PostId;
                    updateActPostItemComment.Comment = postitemcomment.Comment;
                    updateActPostItemComment.CommentByUserId = postitemcomment.CommentByUserId;
                    updateActPostItemComment.CommentDate = DateTime.Today;
                    updateActPostItemComment.UpdatedDate = DateTime.Today;
                    db.SubmitChanges();

                    return Request.CreateResponse(HttpStatusCode.OK);
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.NotFound);
                }
            }
            catch (Exception e)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
        }
    }
}

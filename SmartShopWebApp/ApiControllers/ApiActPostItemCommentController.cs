using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

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

                newActPostItemComment.Id = add.Id;
                newActPostItemComment.PostId = add.PostId;
                newActPostItemComment.Comment = add.Comment;
                newActPostItemComment.CommentByUserId = add.CommentByUserId;
                newActPostItemComment.CommentDate = Convert.ToDateTime(add.CommentDate);
                newActPostItemComment.UpdatedDate = Convert.ToDateTime(add.UpdatedDate);



                return Request.CreateResponse(HttpStatusCode.OK);

            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
        }


        [HttpGet, Route("api/list/postitemcomment")]
        public List<Entities.ActPostItemComment> listActPostItemComment()
        {
            var postComment = from d in db.ActPostItemComments
                              select new Entities.ActPostItemComment
                              {
                                  Id = d.Id,
                                  PostId = d.PostId,
                                  Comment = d.Comment,
                                  CommentByUserId = d.CommentByUserId,
                                  CommentDate = d.CommentDate.ToShortDateString(),
                                  UpdatedDate = d.UpdatedDate.ToShortDateString()
                              };
            return postComment.ToList();
        }

        [HttpDelete, Route("api/delete/postitemcomment/{id}")]
        public HttpResponseMessage deleteActPostItemComment(String id)
        {
            try
            {
                var delActPostItemComment = from d in db.ActPostItemComments
                                            where d.Id == Convert.ToInt32(id)
                                            select d;

                if (delActPostItemComment.Any())
                {
                    db.ActPostItemComments.DeleteOnSubmit(delActPostItemComment.First());
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
                    updateActPostItemComment.CommentDate = Convert.ToDateTime(postitemcomment.CommentDate);
                    updateActPostItemComment.UpdatedDate = Convert.ToDateTime(postitemcomment.UpdatedDate);
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

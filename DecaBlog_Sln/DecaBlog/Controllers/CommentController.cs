using DecaBlog.Commons.Helpers;
using DecaBlog.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using DecaBlog.Models;
using DecaBlog.Models.DTO;
using Microsoft.AspNetCore.Identity;

namespace DecaBlog.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        private readonly ICommentService _commentService;

        public CommentController(ICommentService commentService)
        {
            _commentService = commentService;
        }

        [HttpGet("get-comment-by-id/{commentId}")]
      //  [Authorize(Roles = "Admin, Editor, Decadev")]
        public async Task<IActionResult> GetCommentById([FromRoute] string commentId)
        {
            var comment = await _commentService.GetCommentById(commentId);
            if (comment == null)
            {
                ModelState.AddModelError("Not found", "");
                return NotFound(ResponseHelper.BuildResponse<object>(false, "No comment found", ModelState, null));
            }
            return Ok(ResponseHelper.BuildResponse<object>(true, "Successfully fetched all comments", ResponseHelper.NoErrors, comment));
        }

        [HttpGet("get-all-comments")]
        [Authorize(Roles = "Admin, Editor, Decadev")]
        public async Task<IActionResult> GetAllComments([FromQuery] int pageNumber,  int perPage)
        {
            var comments = await _commentService.GetAllComments(pageNumber, perPage);
            if (comments == null)
            {
                ModelState.AddModelError("Not found", "");
                return NotFound(ResponseHelper.BuildResponse<object>(false, "No comment found", ModelState, null));
            }
            return Ok(ResponseHelper.BuildResponse<object>(true, "Successfully fetched all comments", ResponseHelper.NoErrors, comments));
        }

        [HttpGet("get-comment-by-commentorId/{authorId}")]
        public async Task<IActionResult> GetCommentByCommentId([FromRoute] string authorId, [FromQuery] int pageNumber, [FromQuery] int perPage)
        {
            var commentToResponseResponse = await _commentService.GetCommentByCommenterIdAsync(authorId, pageNumber, perPage);
            if (commentToResponseResponse.Data.Count() == 0)
            {
                return NotFound(ResponseHelper.BuildResponse<object>(false,
                    "comments not found for this author", null, null));
            }

            return Ok(ResponseHelper.BuildResponse<object>(true, "Comment successfully retrieved ",
                ResponseHelper.NoErrors, commentToResponseResponse));
        }

        [Authorize(Roles = "Admin, Editor, Decadev")]
        [HttpPost("add-comment/{topicId}")]
        public async Task<IActionResult> AddComment([FromBody] CommentToAddDto model, [FromRoute] string topicId)
        {
            if (!ModelState.IsValid)
                return BadRequest(ResponseHelper.BuildResponse<string>(false, "Comment failed to Add.", ModelState, null));
            var response = await _commentService.AddComment(model, topicId);
            if (response == null)
            {
                ModelState.AddModelError("Failed To Add Article", "Comment Addition Failed, Please Try again");
                return BadRequest(ResponseHelper.BuildResponse<string>(false, "Comment failed to Add.", ModelState, null));
            }
            return Ok(ResponseHelper.BuildResponse<object>(true, "Comment Added successful", ResponseHelper.NoErrors, response));
        }

        [HttpPost("vote-comment/{commentId}")]
        [Authorize(Roles = "Admin, Editor, Decadev")]
        public async Task<IActionResult> VoteComment([FromRoute] string commentId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ResponseHelper.BuildResponse<object>(false, "Bad input", ModelState, null));
            }
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var returnedResponse = await _commentService.VoteComment(commentId, userId);
            if (returnedResponse == null)
            {
                ModelState.AddModelError("Comment vote failed", "Failed to vote comment because you have already voted this comment or comment doesn't exist");
                return BadRequest(ResponseHelper.BuildResponse<object>(false, "Unable to vote", ModelState, null));
            }
            return Ok(ResponseHelper.BuildResponse<object>(true, "Vote succeeded", ResponseHelper.NoErrors, returnedResponse));
        }

        [HttpPost("unvote-comment/{commentId}")]
        [Authorize(Roles = "Admin, Editor, Decadev")]
        public async Task<IActionResult> UnVoteComment([FromRoute] string commentId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ResponseHelper.BuildResponse<object>(false, "Bad input", ModelState, null));
            }
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var returnedResponse = await _commentService.UnVoteComment(commentId, userId);
            if (returnedResponse == null)
            {
                ModelState.AddModelError("Comment unvote failed", "Failed to unvote comment because you have not voted this comment or comment doesn't exist");
                return BadRequest(ResponseHelper.BuildResponse<object>(false, "Unable to unvote", ModelState, null));
            }
            return Ok(ResponseHelper.BuildResponse<object>(true, "Unvote succeeded", ResponseHelper.NoErrors, returnedResponse));
        }

        [HttpDelete("delete-comment/{commentId}")]
        public async Task<IActionResult> DeleteComment([FromRoute] string commentId)
        {
            var deleteCommentResponse = await _commentService.DeleteCommentById(commentId);
            if (deleteCommentResponse == false)
            {
                return NotFound(ResponseHelper.BuildResponse<object>(false,
                    "comment not found or unable to delete comment", ModelState, null));
            }

            return Ok(ResponseHelper.BuildResponse<object>(true, "Comment successfully deleted",
                ResponseHelper.NoErrors, null));
        }

        [HttpGet("get-comments-by-topicId")]
        public async Task<IActionResult> GetCommentByTopicId([FromQuery] int pageNumber, int perPage, string topicId)
        {
            if (String.IsNullOrEmpty(topicId))
            {
                ModelState.AddModelError("failed to retrieve comments", "failed to retrieve comments");
                return BadRequest(ResponseHelper.BuildResponse<object>(false, "failed to retrieve comments", null, null));
            }
            var comments = await _commentService.GetCommentsByTopicId(topicId, pageNumber, perPage);
            if (comments == null)
            {
                ModelState.AddModelError("Not found", "");
                return NotFound(ResponseHelper.BuildResponse<object>(false, "No comment found", ModelState, null));
            }
            return Ok(ResponseHelper.BuildResponse<object>(true, "Successfully fetched all comments", ResponseHelper.NoErrors, comments));
        }
    }
}

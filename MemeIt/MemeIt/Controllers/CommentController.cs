using MemeIt.Core;
using MemeIt.Infrastructure;
using MemeIt.Models.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MemeIt.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        private readonly ICommentRepository _commentRepository;
        private readonly IMemeRepository _memeRepository;
        private readonly IUserRepository _userRepository;

        public CommentController(ICommentRepository commentRepository, IMemeRepository memeRepository, IUserRepository userRepository)
        {
            _commentRepository = commentRepository;
            _memeRepository = memeRepository;
            _userRepository = userRepository;
        }

        [HttpGet]
        [Authorize(Roles = "baseUser")]
        [Route("meme/{memeId:long}/all")]
        public async Task<ActionResult> GetMemesComments([FromRoute] long memeId)
        {
            var meme = await _memeRepository.GetMemeByIdAsync(memeId);
            if (meme == null) return BadRequest("Meme was not found");

            var memeComments = await _commentRepository.GetMemesCommentsAsync(memeId);
            if (memeComments == null) return BadRequest("No comments where found");

            var memeCommentDtos = memeComments.Select(x => x.ToMemeCommentDto()).ToList();

            return Ok(memeCommentDtos);
        }

        [HttpPost]
        [Authorize(Roles = "baseUser")]
        [Route("meme/{memeId:long}/add")]
        public async Task<ActionResult> AddCommentToMeme([FromBody] CommentOnMemeDto comment)
        {
            var meme = await _memeRepository.GetMemeByIdAsync(comment.MemeId);
            if (meme == null) return BadRequest("No meme was found");

            var user = await _userRepository.GetUserAsync(comment.UserId);
            if (user == null) return BadRequest("No user was found");

            await _commentRepository.AddCommentAsync(comment.ToComment(meme, user));

            return Ok();
        }


        [HttpPut]
        [Authorize(Roles = "baseUser")]
        [Route("{commentId:long}/edit")]
        public async Task<ActionResult> EditComment([FromRoute] long commentId, [FromBody]string newMessage)
        {
            var comment = await _commentRepository.GetCommentAsync(commentId);
            if (comment == null) return BadRequest("Comment wasn't found");

            await _commentRepository.EditCommentAsync(newMessage, commentId);

            return Ok();
        }

        [HttpDelete]
        [Authorize(Roles = "baseUser")]
        [Route("{commentId:long}/delete")]
        public async Task<ActionResult> DeleteComment([FromRoute] long commentId)
        {
            var comment = await _commentRepository.GetCommentAsync(commentId);
            if (comment == null) return BadRequest("Comment wasn't found");

            await _commentRepository.DeleteCommentAsync(commentId);

            return Ok();
        }
    }
}

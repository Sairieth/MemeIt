using Azure.Storage.Blobs;
using MemeIt.Core;
using MemeIt.Data.Services;
using MemeIt.Infrastructure;
using MemeIt.Models.DTOs;
using MemeIt.Models.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MemeIt.Controllers
{
    [Route("api/memes")]
    [ApiController]
    public class MemeController : ControllerBase
    {
        private readonly IBlobService _blobService;
        private readonly IMemeRepository _memeRepository;
        private readonly IUserRepository _userRepository;

        public MemeController(IBlobService blobService, IMemeRepository memeRepository, IUserRepository userRepository)
        {
            _blobService = blobService;
            _memeRepository = memeRepository;
            _userRepository = userRepository;
        }

        [HttpGet]
        [Route("{tag}")]
        public async Task<ActionResult<List<MemeDto>>> GetMemesByTag([FromRoute] string tag)
        {
            var memes = await _memeRepository.GetMemesByTagAsync(tag);

            var memeDtosByTag = memes?.Select(x => x.MemeToMemeDto()).ToList();

            if (memeDtosByTag == null) return BadRequest("No memes by tag");

            return Ok(memeDtosByTag);
        }

        [HttpGet]
        [Authorize(Roles = "baseUser")]
        [Route("user/{userId:long}")]
        public async Task<ActionResult<List<MemeDto>>> GetMemesByUserId([FromRoute] long userId)
        {
            var memes = await _memeRepository.GetUsersMemesAsync(userId);

            var memeDtosByTag = memes?.Select(x => x.MemeToMemeDto()).ToList();

            if (memeDtosByTag == null) return BadRequest("No memes by user");

            return Ok(memeDtosByTag);
        }

        [HttpGet]
        [Route("all")]
        public async Task<ActionResult<List<MemeDto>>> GetAllMemes()
        {
            var memes = await _memeRepository.GetAllAsync();

            var memeDtosByTag = memes?.Select(x => x.MemeToMemeDto()).ToList();

            if (memeDtosByTag == null) return BadRequest("No memes by tag");

            return Ok(memeDtosByTag);
        }

        [HttpPost]
        [Authorize(Roles = "baseUser")]
        [Route("post")]
        public async Task<ActionResult> PostMeme([FromForm] PostDataDto post)
        {
            if (!ModelState.IsValid) return BadRequest();
            if (post.File == null || post.File.Length > 1) return BadRequest();

            var fileName = Guid.NewGuid().ToString() + Path.GetExtension(post.File.FileName);

            var result = await _blobService.UploadBlobAsync(fileName, post.File, ConfigurationService.Configuration.AzureBlobStorageContainerName);
            var user = await _userRepository.GetUserAsync(post.UserId);

            if (result && user != null)
            {
                var imgUri = await _blobService.GetBlobUriAsync(fileName, ConfigurationService.Configuration.AzureBlobStorageContainerName);
                await _memeRepository.AddMemeAsync(post.PostDataDtoToMeme(imgUri, user));
                return Ok();
            }

            return BadRequest();
        }

        [HttpPut]
        [Authorize(Roles = "baseUser")]
        [Route("update/{memeId:long}")]
        public async Task<ActionResult> EditMemeTitle([FromRoute] long memeId, [FromBody] string newTitle)
        {
            var meme = await _memeRepository.GetMemeByIdAsync(memeId);

            if (meme == null) return BadRequest("No meme was found");

            await _memeRepository.EditMemeTitleAsync(newTitle, memeId);

            return Ok();
        }

        [HttpPut]
        [Authorize(Roles = "baseUser")]
        [Route("update/{memeId:long}")]
        public async Task<ActionResult> EditMemeTag([FromRoute] long memeId, [FromBody] string newTag)
        {
            var meme = await _memeRepository.GetMemeByIdAsync(memeId);

            if (meme == null) return BadRequest("No meme was found");

            await _memeRepository.EditMemeTagAsync(newTag, memeId);

            return Ok();
        }

        [HttpDelete]
        [Authorize(Roles = "baseUser")]
        [Route("remove/{memeId:long}")]
        public async Task<ActionResult> DeleteMeme([FromRoute] long memeId)
        {
            var meme = await _memeRepository.GetMemeByIdAsync(memeId);

            if (meme == null) return BadRequest("No meme was found");

            await _memeRepository.RemoveMemeAsync(memeId);

            return Ok("OK");
        }
    }
}
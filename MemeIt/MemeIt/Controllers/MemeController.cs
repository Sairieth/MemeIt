using Azure.Storage.Blobs;
using MemeIt.Core;
using MemeIt.Data.Services;
using MemeIt.Infrastructure;
using MemeIt.Models.DTOs;
using MemeIt.Models.Entities;
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
        private readonly IAuthService _authService;

        public MemeController(IBlobService blobService, IMemeRepository memeRepository, IUserRepository userRepository, IAuthService authService, IConfigurationRoot configurationRoot)
        {
            _blobService = blobService;
            _memeRepository = memeRepository;
            _userRepository = userRepository;
            _authService = authService;
        }

        [HttpPost]
        [Route("post")]
        public async Task<ActionResult> PostMeme([FromBody] PostDataDto post)
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

        [HttpGet]
        [Route("all")]
        public async Task<ActionResult<List<MemeDto>>> GetAllMemes()
        {
            var memes = await _memeRepository.GetAll();

            var memeDtosByTag = memes?.Select(x => x.MemeToMemeDto()).ToList();

            if (memeDtosByTag == null) return BadRequest("No memes by tag");

            return Ok(memeDtosByTag);
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
        [Route("user/{userId:long}")]
        public async Task<ActionResult<List<MemeDto>>> GetMemesByUserId([FromRoute] long userId)
        {
            var memes = await _memeRepository.GetUsersMemesAsync(userId);

            var memeDtosByTag = memes?.Select(x => x.MemeToMemeDto()).ToList();

            if (memeDtosByTag == null) return BadRequest("No memes by user");

            return Ok(memeDtosByTag);
        }

        [HttpPut]
        [Route("update/{memeId:long}")]
        public async Task<ActionResult> EditMemeTitle([FromRoute] long memeId, [FromBody] string newTitle)
        {
            var meme = await _memeRepository.GetMemeByIdAsync(memeId);

            if (meme == null) return BadRequest("No meme was found");

            await _memeRepository.EditMemeTitleAsync(newTitle, memeId);

            return Ok();
        }

        [HttpPut]
        [Route("update/{memeId:long}")]
        public async Task<ActionResult> EditMemeTag([FromRoute] long memeId, [FromBody] string newTag)
        {
            var meme = await _memeRepository.GetMemeByIdAsync(memeId);

            if (meme == null) return BadRequest("No meme was found");

            await _memeRepository.EditMemeTagAsync(newTag, memeId);

            return Ok();
        }

        [HttpDelete]
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
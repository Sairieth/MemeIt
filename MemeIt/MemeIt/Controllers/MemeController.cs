using Azure.Storage.Blobs;
using MemeIt.Core;
using MemeIt.Infrastructure;
using MemeIt.Models.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MemeIt.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MemeController : ControllerBase
    {
        private readonly IBlobService _blobService;
        private readonly IMemeRepository _memeRepository;
        private const string ImgContainerName = "images";

        public MemeController(IBlobService blobService, IMemeRepository memeRepository)
        {
            _blobService = blobService;
            _memeRepository = memeRepository;
        }

        [HttpPost]
        [Route("post")]
        public async Task<ActionResult> PostMeme(PostDataDto post)
        {
            if (!ModelState.IsValid) return BadRequest();
            if (post.File == null || post.File.Length > 1) return BadRequest();

            var fileName = Guid.NewGuid().ToString() + Path.GetExtension(post.File.FileName);

            var result = await _blobService.UploadBlobAsync(fileName, post.File, ImgContainerName);

            var imgUri = await _blobService.GetBlobUriAsync(fileName, ImgContainerName);

            if (result)
            {
                await _memeRepository.AddMemeAsync(post.ToMeme(imgUri));
                return Ok();
            }

            return BadRequest();
        }
    }
}
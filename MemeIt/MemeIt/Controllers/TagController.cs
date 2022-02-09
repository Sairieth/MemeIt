using MemeIt.Core;
using MemeIt.Infrastructure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MemeIt.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TagController : ControllerBase
    {
        public readonly ITagRepository _TagRepository;

        public TagController(ITagRepository tagRepository)
        {
            _TagRepository = tagRepository;
        }

        [HttpGet]
        [Route("get-all")]
        public async Task<ActionResult> GetTags()
        {
            var tags = await _TagRepository.GetAllTagsAsync();
            if (tags == null) return NotFound();

            var tagsList = tags.Select(x => x.Title).OrderBy(y=>y).ToList();

            return Ok(tagsList);
        }
    }
}

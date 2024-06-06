using Application.Dtos.Blog;
using Core.Entities;
using Core.UnitOfWork;
using Microsoft.AspNetCore.Mvc;

namespace AboutMe.Web.Controllers
{
    public class TagController : BaseController
    {
        private readonly IUnitOfWork _unitOfWork;
        public TagController(IUnitOfWork unitOfWork) 
        {
            _unitOfWork = unitOfWork;
        }

        [HttpPost]
        public async Task<IActionResult> CreateTag([FromBody]CreateTagDTO dto)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest("The field can only contain letters, spaces, and underscores.");
            }

            var tag = new Tag
            {
                Name = dto.Name
            };

            await _unitOfWork.TagRepository.AddAsync(tag);
            await _unitOfWork.SaveChangesAsync();

            return Ok();
        }
    }
}

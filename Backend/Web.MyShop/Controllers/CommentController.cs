using Infrastructure.MyShop.Interfaces;
using Infrastructure.MyShop.Models.DTO.CommentDTO;
using Microsoft.AspNetCore.Mvc;

namespace Web.MyShop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        private readonly ICommentService _commentService;

        public CommentController(ICommentService commentService)
        {
            _commentService = commentService;
        }




        [HttpGet]
        [Route("GetAll")]
        public async Task<IActionResult> GetAllAsync()
        {
            var res = await _commentService.GetAllAsync();
            return Ok(res);
        }
        [HttpGet]
        [Route("GetCommentsByProductId/{id}")]
        public async Task<IActionResult> GetCommentsByProductIdAsync(int id) //FindByIdVM model
        {
            var res = await _commentService.GetCommentsByProductIdAsync(id); //model.id
            return Ok(res);
        }
        [HttpPost]
        [Route("create")]
        public async Task<IActionResult> CreateCommentAsync([FromForm] CommentCreateDTO model)
        {
            var res = await _commentService.CreateCommentAsync(model);
            return Ok(res);
        }
        [HttpPut]
        [Route("edit")]
        public async Task<IActionResult> EditCommentAsync([FromForm] CommentEditDTO model)
        {
            var res = await _commentService.EditCommentAsync(model);
            return Ok(res);
        }
        [HttpDelete]
        [Route("delete/{id}")]
        public async Task<IActionResult> DeleteCommentAsync(int id) //FindByIdVM model
        {
            var res = await _commentService.DeleteCommentAsync(id); //model.id
            return Ok(res);
        }

    }
}

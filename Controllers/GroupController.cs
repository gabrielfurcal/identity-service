using identity_service.Models;
using identity_service.Services.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace identity_service.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GroupController : ControllerBase
    {
        private readonly IGroupService _groupService;
        private readonly ILogger<GroupController> _logger;

        public GroupController(IGroupService groupService, ILogger<GroupController> logger)
        {
            this._groupService = groupService;
            this._logger = logger;
        }

        [HttpGet("All")]
        public async Task<ActionResult<IEnumerable<GroupDTO>>> GetAll()
        {
            try
            {
                var groups = await _groupService.FindAll();
                return Ok(groups);
            }
            catch (Exception ex)
            {
                var errorMessage = "Error retrieving groups";

                _logger.LogError(ex, errorMessage);
                return StatusCode(500, errorMessage);
            }
        }

        [HttpGet]
        [Route("{Id}")]
        public async Task<ActionResult<GroupDTO>> GetById(int Id)
        {
            try
            {
                var group = await _groupService.FindById(Id);
                return Ok(group);
            }
            catch (Exception ex)
            {
                var errorMessage = $"Error retrieving group with ID: {Id}";

                _logger.LogError(ex, errorMessage);
                return StatusCode(500, errorMessage);
            }
        }

        [HttpPost]
        public async Task<ActionResult<GroupDTO>> Create([FromBody] GroupDTO Group)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var createdGroup = await _groupService.Save(Group, null);
                return CreatedAtAction(nameof(GetById), new { Id = createdGroup.Id }, createdGroup);
            }
            catch (Exception ex)
            {
                var errorMessage = $"Error creating group";

                _logger.LogError(ex, errorMessage);
                return StatusCode(500, errorMessage);                
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<GroupDTO>> Update(int Id, [FromBody] GroupDTO Group)
        {
            try
            {
                var updatedGroup = await _groupService.Save(Group, Id);
                return Ok(updatedGroup);
            }
            catch (Exception ex)
            {
                var errorMessage = $"Error updating group with ID: {Id}";

                _logger.LogError(ex, errorMessage);
                return StatusCode(500, errorMessage);                
            }            
        }

        [HttpDelete("{Id}")]
        public async Task<ActionResult<bool>> Delete(int Id)
        {
            try
            {
                var deleted = await _groupService.DeleteById(Id);
                return Ok(deleted);
            }
            catch (Exception ex)
            {
                var errorMessage = $"Error deletiing group, with ID: {Id}";

                _logger.LogError(ex, errorMessage);
                return StatusCode(500, errorMessage);                
            }                  
        }
    }
}
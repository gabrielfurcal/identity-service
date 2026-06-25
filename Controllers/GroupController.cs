using identity_service.DTOs;
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

        [HttpGet("{id}")]
        public async Task<ActionResult<GroupDTO>> GetById(int id)
        {
            try
            {
                var group = await _groupService.FindById(id);
                return Ok(group);
            }
            catch (Exception ex)
            {
                var errorMessage = $"Error retrieving group with ID: {id}";

                _logger.LogError(ex, errorMessage);
                return StatusCode(500, errorMessage);
            }
        }

        [HttpPost]
        public async Task<ActionResult<GroupDTO>> Create([FromBody] GroupDTO group)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var createdGroup = await _groupService.Save(group, null);
                return CreatedAtAction(nameof(GetById), new { createdGroup.Id }, createdGroup);
            }
            catch (Exception ex)
            {
                var errorMessage = $"Error creating group";

                _logger.LogError(ex, errorMessage);
                return StatusCode(500, errorMessage);                
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<GroupDTO>> Update(int id, [FromBody] GroupDTO group)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var groupToSend = new GroupDTO(id, group.Name, group.Description);
                var updatedGroup = await _groupService.Save(groupToSend, id);
                
                return Ok(updatedGroup);
            }
            catch (Exception ex)
            {
                var errorMessage = $"Error updating group with ID: {id}";

                _logger.LogError(ex, errorMessage);
                return StatusCode(500, errorMessage);                
            }            
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> Delete(int id)
        {
            try
            {
                var deleted = await _groupService.DeleteById(id);
                return Ok(deleted);
            }
            catch (Exception ex)
            {
                var errorMessage = $"Error deletiing group, with ID: {id}";

                _logger.LogError(ex, errorMessage);
                return StatusCode(500, errorMessage);                
            }                  
        }
    }
}
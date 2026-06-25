using identity_service.DTOs;
using identity_service.Services.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace identity_service.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RoleController : ControllerBase
    {
        private readonly IRoleService _roleService;
        private readonly ILogger<RoleController> _logger;

        public RoleController(IRoleService roleService, ILogger<RoleController> logger)
        {
            _roleService = roleService;
            _logger = logger;
        }

        [HttpGet("All")]
        public async Task<ActionResult<IEnumerable<RoleDTO>>> GetAll()
        {
            try
            {
                var items = await _roleService.FindAll();
                return Ok(items);
            }
            catch (Exception ex)
            {
                var errorMessage = "Error retrieving roles";
                _logger.LogError(ex, errorMessage);
                return StatusCode(500, errorMessage);
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<RoleDTO>> GetById(int id)
        {
            try
            {
                var item = await _roleService.FindById(id);
                return Ok(item);
            }
            catch (Exception ex)
            {
                var errorMessage = $"Error retrieving role with ID: {id}";
                _logger.LogError(ex, errorMessage);
                return StatusCode(500, errorMessage);
            }
        }

        [HttpPost]
        public async Task<ActionResult<RoleDTO>> Create([FromBody] RoleDTO role)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var created = await _roleService.Save(role, null);
                return CreatedAtAction(nameof(GetById), new { created.Id }, created);
            }
            catch (Exception ex)
            {
                var errorMessage = "Error creating role";
                _logger.LogError(ex, errorMessage);
                return StatusCode(500, errorMessage);
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<RoleDTO>> Update(int id, [FromBody] RoleDTO role)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var roleToSend = new RoleDTO(id, role.Name, role.Description);
                var updated = await _roleService.Save(role, id);

                return Ok(updated);
            }
            catch (Exception ex)
            {
                var errorMessage = $"Error updating role with ID: {id}";
                _logger.LogError(ex, errorMessage);
                return StatusCode(500, errorMessage);
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> Delete(int id)
        {
            try
            {
                var deleted = await _roleService.DeleteById(id);
                return Ok(deleted);
            }
            catch (Exception ex)
            {
                var errorMessage = $"Error deleting role with ID: {id}";
                _logger.LogError(ex, errorMessage);
                return StatusCode(500, errorMessage);
            }
        }
    }
}

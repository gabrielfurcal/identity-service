using identity_service.DTOs;
using identity_service.Services.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace identity_service.Controllers
{
    [Authorize(Roles = "ADMIN")]
    [ApiController]
    [Route("api/[controller]")]
    public class PermissionController : ControllerBase
    {
        private readonly IPermissionService _permissionService;
        private readonly ILogger<PermissionController> _logger;

        public PermissionController(IPermissionService permissionService, ILogger<PermissionController> logger)
        {
            _permissionService = permissionService;
            _logger = logger;
        }

        [HttpGet("All")]
        public async Task<ActionResult<IEnumerable<PermissionDTO>>> GetAll()
        {
            try
            {
                var items = await _permissionService.FindAll();
                return Ok(items);
            }
            catch (Exception ex)
            {
                var errorMessage = "Error retrieving permissions";
                _logger.LogError(ex, errorMessage);
                return StatusCode(500, errorMessage);
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PermissionDTO>> GetById(int id)
        {
            try
            {
                var item = await _permissionService.FindById(id);
                return Ok(item);
            }
            catch (Exception ex)
            {
                var errorMessage = $"Error retrieving permission with ID: {id}";
                _logger.LogError(ex, errorMessage);
                return StatusCode(500, errorMessage);
            }
        }

        [HttpPost]
        public async Task<ActionResult<PermissionDTO>> Create([FromBody] PermissionDTO permission)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var created = await _permissionService.Save(permission, null);
                return CreatedAtAction(nameof(GetById), new { created.Id }, created);
            }
            catch (Exception ex)
            {
                var errorMessage = "Error creating permission";
                _logger.LogError(ex, errorMessage);
                return StatusCode(500, errorMessage);
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<PermissionDTO>> Update(int id, [FromBody] PermissionDTO permission)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var permissionToSend = new PermissionDTO(id, permission.Name, permission.Description);
                var updated = await _permissionService.Save(permission, id);

                return Ok(updated);
            }
            catch (Exception ex)
            {
                var errorMessage = $"Error updating permission with ID: {id}";
                _logger.LogError(ex, errorMessage);
                return StatusCode(500, errorMessage);
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> Delete(int id)
        {
            try
            {
                var deleted = await _permissionService.DeleteById(id);
                return Ok(deleted);
            }
            catch (Exception ex)
            {
                var errorMessage = $"Error deleting permission with ID: {id}";
                _logger.LogError(ex, errorMessage);
                return StatusCode(500, errorMessage);
            }
        }
    }
}

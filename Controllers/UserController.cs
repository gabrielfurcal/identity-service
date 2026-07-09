using identity_service.DTOs;
using identity_service.Services.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace identity_service.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly ILogger<UserController> _logger;

        public UserController(IUserService userService, ILogger<UserController> logger)
        {
            _userService = userService;
            _logger = logger;
        }

        [Authorize(Roles = "ADMIN")]
        [HttpGet("All")]
        public async Task<ActionResult<IEnumerable<UserDTO>>> GetAll()
        {
            try
            {
                var items = await _userService.FindAll();
                return Ok(items);
            }
            catch (Exception ex)
            {
                var errorMessage = "Error retrieving users";
                _logger.LogError(ex, errorMessage);
                return StatusCode(500, errorMessage);
            }
        }

        [Authorize(Roles = "ADMIN")]
        [HttpGet("{id}")]
        public async Task<ActionResult<UserDTO>> GetById(Guid id)
        {
            try
            {
                var item = await _userService.FindById(id);
                return Ok(item);
            }
            catch (Exception ex)
            {
                var errorMessage = $"Error retrieving user with ID: {id}";
                _logger.LogError(ex, errorMessage);
                return StatusCode(500, errorMessage);
            }
        }

        [HttpPost]
        public async Task<ActionResult<UserDTO>> Create([FromBody] UserDTO user)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var created = await _userService.Save(user, null);
                return CreatedAtAction(nameof(GetById), new { created.Id }, created);
            }
            catch (Exception ex)
            {
                var errorMessage = "Error creating user";
                _logger.LogError(ex, errorMessage);
                return StatusCode(500, errorMessage);
            }
        }

        [HttpPost("Login")]
        public async Task<ActionResult<UserDTO>> Login([FromBody] UserDTO user)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var created = await _userService.Login(user);
                return Ok(created);
            }
            catch (Exception ex)
            {
                var errorMessage = "Error login user";
                _logger.LogError(ex, errorMessage);
                return StatusCode(500, errorMessage);
            }
        }

        [Authorize(Roles = "ADMIN")]
        [HttpPut("{id}")]
        public async Task<ActionResult<UserDTO>> Update(Guid id, [FromBody] UserDTO user)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var userToSend = new UserDTO(id, user.Email, user.Password, user.IsActive, user.CreatedAt, user.UpdatedAt, 
                    new HashSet<RefreshTokenDTO>(), new HashSet<UserRoleDTO>(), new HashSet<UserGroupDTO>());
                var updated = await _userService.Save(user, id);
                return Ok(updated);
            }
            catch (Exception ex)
            {
                var errorMessage = $"Error updating user with ID: {id}";
                _logger.LogError(ex, errorMessage);
                return StatusCode(500, errorMessage);
            }
        }

        [Authorize(Roles = "ADMIN")]
        [HttpDelete("{Id}")]
        public async Task<ActionResult<bool>> Delete(Guid id)
        {
            try
            {
                var deleted = await _userService.DeleteById(id);
                return Ok(deleted);
            }
            catch (Exception ex)
            {
                var errorMessage = $"Error deleting user with ID: {id}";
                _logger.LogError(ex, errorMessage);
                return StatusCode(500, errorMessage);
            }
        }
    }
}

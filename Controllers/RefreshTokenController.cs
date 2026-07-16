using identity_service.DTOs;
using identity_service.Models;
using identity_service.Services.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace identity_service.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RefreshTokenController : ControllerBase
    {
        private readonly IRefreshTokenService _refreshTokenService;
        private readonly ILogger<RefreshTokenController> _logger;

        public RefreshTokenController(IRefreshTokenService refreshTokenService, ILogger<RefreshTokenController> logger)
        {
            _refreshTokenService = refreshTokenService;
            _logger = logger;
        }

        [Authorize(Roles = "ADMIN")]
        [HttpGet("All")]
        public async Task<ActionResult<IEnumerable<RefreshTokenDTO>>> GetAll()
        {
            try
            {
                var refreshTokens = await _refreshTokenService.FindAll();
                return Ok(refreshTokens);
            }
            catch (Exception ex)
            {
                var errorMessage = "Error retrieving refresh tokens";
                _logger.LogError(ex, errorMessage);
                return StatusCode(500, errorMessage);
            }
        }

        [Authorize(Roles = "ADMIN")]
        [HttpGet("{id}")]
        public async Task<ActionResult<RefreshTokenDTO>> GetById(Guid id)
        {
            try
            {
                var refreshToken = await _refreshTokenService.FindById(id);
                return Ok(refreshToken);
            }
            catch (Exception ex)
            {
                var errorMessage = $"Error retrieving refresh token with ID: {id}";
                _logger.LogError(ex, errorMessage);
                return StatusCode(500, errorMessage);
            }
        }

        [Authorize(Roles = "ADMIN")]
        [HttpPost]
        public async Task<ActionResult<RefreshTokenDTO>> Create([FromBody] RefreshTokenDTO refreshToken)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var created = await _refreshTokenService.Save(refreshToken, null);
                return CreatedAtAction(nameof(GetById), new {  created.Id }, created);
            }
            catch (Exception ex)
            {
                var errorMessage = "Error creating refresh token";
                _logger.LogError(ex, errorMessage);
                return StatusCode(500, errorMessage);
            }
        }

        [HttpPost("Jwt")]
        public async Task<ActionResult<LoginDTO>> Jwt([FromBody] RefreshTokenDTO refreshToken)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var jwtToken = await _refreshTokenService.Validate(refreshToken);
                return Ok(jwtToken);
            }
            catch (Exception ex)
            {
                var errorMessage = "Error refreshing jwt token";
                _logger.LogError(ex, errorMessage);
                return StatusCode(500, errorMessage);
            }
        }

        [Authorize(Roles = "ADMIN")]
        [HttpPut("{id}")]
        public async Task<ActionResult<RefreshTokenDTO>> Update(Guid id, [FromBody] RefreshTokenDTO refreshToken)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var refreshTokenToSend = new RefreshTokenDTO(id, refreshToken.TokenHash, refreshToken.CreatedAt, refreshToken.ExpiresAt, refreshToken.RevokedAt,
                    refreshToken.ReplacedByTokenId, refreshToken.DeviceInfo, refreshToken.IPAddress, refreshToken.User);
                var updated = await _refreshTokenService.Save(refreshToken, id);
                return Ok(updated);
            }
            catch (Exception ex)
            {
                var errorMessage = $"Error updating refresh token with ID: {id}";
                _logger.LogError(ex, errorMessage);
                return StatusCode(500, errorMessage);
            }
        }

        [Authorize(Roles = "ADMIN")]
        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> Delete(Guid id)
        {
            try
            {
                var deleted = await _refreshTokenService.DeleteById(id);
                return Ok(deleted);
            }
            catch (Exception ex)
            {
                var errorMessage = $"Error deleting refresh token with ID: {id}";
                _logger.LogError(ex, errorMessage);
                return StatusCode(500, errorMessage);
            }
        }
    }
}

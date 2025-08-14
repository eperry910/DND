using MongoDB.Driver;
using DND.Models;
using Microsoft.AspNetCore.Http;

namespace DND.Services
{
    public interface IAuthService
    {
        Task<AuthResponse> RegisterAsync(RegisterRequest request);
        Task<AuthResponse> LoginAsync(LoginRequest request);
        Task<AuthResponse> RefreshTokenAsync(string refreshToken);
        Task<bool> ChangePasswordAsync(string userId, ChangePasswordRequest request);
        Task<UserDto> GetUserByIdAsync(string userId);
        Task<UserDto> GetUserByUsernameAsync(string username);
        Task<bool> IsUsernameAvailableAsync(string username);
        Task<bool> IsEmailAvailableAsync(string email);
    }
    
    public class AuthService : IAuthService
    {
        private readonly IMongoCollection<User> _users;
        private readonly IPasswordService _passwordService;
        private readonly IJwtService _jwtService;
        private readonly ILogger<AuthService> _logger;
        
        public AuthService(
            IMongoCollection<User> users,
            IPasswordService passwordService,
            IJwtService jwtService,
            ILogger<AuthService> logger)
        {
            _users = users;
            _passwordService = passwordService;
            _jwtService = jwtService;
            _logger = logger;
        }
        
        public async Task<AuthResponse> RegisterAsync(RegisterRequest request)
        {
            // Check if username is available
            if (!await IsUsernameAvailableAsync(request.Username))
            {
                throw new InvalidOperationException("Username is already taken");
            }
            
            // Check if email is available
            if (!await IsEmailAvailableAsync(request.Email))
            {
                throw new InvalidOperationException("Email is already registered");
            }
            
            // Validate age (must be at least 13 years old)
            var age = DateTime.UtcNow.Year - request.DateOfBirth.Year;
            if (request.DateOfBirth > DateTime.UtcNow.AddYears(-age)) age--;
            if (age < 13)
            {
                throw new InvalidOperationException("User must be at least 13 years old");
            }
            
            var user = new User
            {
                Username = request.Username,
                Email = request.Email,
                FullName = request.FullName,
                DateOfBirth = request.DateOfBirth,
                HashedPassword = _passwordService.HashPassword(request.Password),
                CreatedAt = DateTime.UtcNow,
                LastLoginAt = DateTime.UtcNow,
                IsActive = true,
                CharacterIds = new List<string>()
            };
            
            await _users.InsertOneAsync(user);
            
            var token = _jwtService.GenerateToken(user);
            var refreshToken = _jwtService.GenerateRefreshToken();
            
            return new AuthResponse
            {
                Token = token,
                RefreshToken = refreshToken,
                ExpiresAt = DateTime.UtcNow.AddHours(1),
                User = MapToUserDto(user)
            };
        }
        
        public async Task<AuthResponse> LoginAsync(LoginRequest request)
        {
            var user = await _users.Find(u => u.Username == request.Username).FirstOrDefaultAsync();
            
            if (user == null || !_passwordService.VerifyPassword(request.Password, user.HashedPassword))
            {
                throw new InvalidOperationException("Invalid username or password");
            }
            
            if (!user.IsActive)
            {
                throw new InvalidOperationException("Account is deactivated");
            }
            
            // Update last login
            var update = Builders<User>.Update.Set(u => u.LastLoginAt, DateTime.UtcNow);
            await _users.UpdateOneAsync(u => u.Id == user.Id, update);
            
            var token = _jwtService.GenerateToken(user);
            var refreshToken = _jwtService.GenerateRefreshToken();
            
            return new AuthResponse
            {
                Token = token,
                RefreshToken = refreshToken,
                ExpiresAt = DateTime.UtcNow.AddHours(1),
                User = MapToUserDto(user)
            };
        }
        
        public async Task<AuthResponse> RefreshTokenAsync(string refreshToken)
        {
            // In a real application, you'd store refresh tokens in a separate collection
            // and validate them against the stored tokens. For simplicity, we'll just
            // generate a new token if the refresh token is valid format.
            
            if (string.IsNullOrEmpty(refreshToken))
            {
                throw new InvalidOperationException("Invalid refresh token");
            }
            
            // For now, we'll require the user to provide their username with the refresh token
            // In a real implementation, you'd decode the refresh token to get the user ID
            throw new NotImplementedException("Refresh token implementation requires additional setup");
        }
        
        public async Task<bool> ChangePasswordAsync(string userId, ChangePasswordRequest request)
        {
            var user = await _users.Find(u => u.Id == userId).FirstOrDefaultAsync();
            
            if (user == null)
            {
                return false;
            }
            
            if (!_passwordService.VerifyPassword(request.CurrentPassword, user.HashedPassword))
            {
                return false;
            }
            
            var update = Builders<User>.Update.Set(u => u.HashedPassword, _passwordService.HashPassword(request.NewPassword));
            var result = await _users.UpdateOneAsync(u => u.Id == userId, update);
            
            return result.ModifiedCount > 0;
        }
        
        public async Task<UserDto> GetUserByIdAsync(string userId)
        {
            var user = await _users.Find(u => u.Id == userId).FirstOrDefaultAsync();
            return user != null ? MapToUserDto(user) : null;
        }
        
        public async Task<UserDto> GetUserByUsernameAsync(string username)
        {
            var user = await _users.Find(u => u.Username == username).FirstOrDefaultAsync();
            return user != null ? MapToUserDto(user) : null;
        }
        
        public async Task<bool> IsUsernameAvailableAsync(string username)
        {
            var user = await _users.Find(u => u.Username == username).FirstOrDefaultAsync();
            return user == null;
        }
        
        public async Task<bool> IsEmailAvailableAsync(string email)
        {
            var user = await _users.Find(u => u.Email == email).FirstOrDefaultAsync();
            return user == null;
        }
        
        private static UserDto MapToUserDto(User user)
        {
            return new UserDto
            {
                Id = user.Id,
                Username = user.Username,
                Email = user.Email,
                FullName = user.FullName,
                DateOfBirth = user.DateOfBirth,
                CreatedAt = user.CreatedAt,
                LastLoginAt = user.LastLoginAt,
                CharacterIds = user.CharacterIds
            };
        }
    }
} 
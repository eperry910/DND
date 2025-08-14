# D&D Character Sheet API

A comprehensive ASP.NET Core Web API for managing D&D 5e character sheets with JWT authentication.

## Features

- **JWT Authentication** - Secure user authentication with JWT tokens
- **User Management** - User registration, login, and profile management
- **Character Management** - Full CRUD operations for character sheets
- **Multiclass Support** - Advanced multiclassing with proper spell slot calculations
- **Equipment System** - Flexible equipment and inventory management
- **Spell Management** - Spell preparation and slot tracking
- **MongoDB Integration** - NoSQL database for flexible data storage

## Authentication System

### User Model
```csharp
public class User
{
    public string Id { get; set; }                    // MongoDB ObjectId
    public string Username { get; set; }              // Unique username
    public string Email { get; set; }                 // Unique email
    public string FullName { get; set; }              // User's full name
    public DateTime DateOfBirth { get; set; }         // Date of birth (13+ required)
    public string HashedPassword { get; set; }        // BCrypt hashed password
    public DateTime CreatedAt { get; set; }           // Account creation date
    public DateTime LastLoginAt { get; set; }         // Last login timestamp
    public bool IsActive { get; set; }                // Account status
    public List<string> CharacterIds { get; set; }    // Associated character IDs
}
```

### Authentication Endpoints

#### Register User
```http
POST /api/auth/register
Content-Type: application/json

{
    "username": "dungeonmaster",
    "email": "dm@example.com",
    "fullName": "John Doe",
    "dateOfBirth": "1990-01-01T00:00:00Z",
    "password": "SecurePassword123!",
    "confirmPassword": "SecurePassword123!"
}
```

#### Login
```http
POST /api/auth/login
Content-Type: application/json

{
    "username": "dungeonmaster",
    "password": "SecurePassword123!"
}
```

#### Get Profile
```http
GET /api/auth/profile
Authorization: Bearer {jwt_token}
```

#### Change Password
```http
POST /api/auth/change-password
Authorization: Bearer {jwt_token}
Content-Type: application/json

{
    "currentPassword": "SecurePassword123!",
    "newPassword": "NewSecurePassword456!",
    "confirmNewPassword": "NewSecurePassword456!"
}
```

#### Check Username Availability
```http
GET /api/auth/check-username/{username}
```

#### Check Email Availability
```http
GET /api/auth/check-email/{email}
```

## Configuration

### JWT Settings
Add the following to your `appsettings.json` or `appsettings.Development.json`:

```json
{
  "Jwt": {
    "Secret": "your_super_secret_jwt_key_here_make_it_at_least_32_characters_long",
    "Issuer": "DNDCharacterSheetAPI",
    "Audience": "DNDCharacterSheetUsers"
  },
  "ConnectionString": "your_mongodb_connection_string_here"
}
```

### Security Requirements
- **JWT Secret**: Must be at least 32 characters long
- **Password**: Minimum 8 characters
- **Username**: 3-50 characters
- **Age**: Must be 13 or older
- **Email**: Valid email format

## Authentication Flow

1. **Registration**: User provides credentials → Password is hashed with BCrypt → JWT token generated
2. **Login**: User provides credentials → Password verified → JWT token generated
3. **API Access**: Include JWT token in Authorization header: `Bearer {token}`
4. **Token Expiration**: Tokens expire after 1 hour (configurable)

## Security Features

- **Password Hashing**: BCrypt with salt rounds of 12
- **JWT Tokens**: Signed with HMAC-SHA256
- **Input Validation**: Comprehensive validation on all endpoints
- **Age Verification**: Minimum age requirement enforced
- **Unique Constraints**: Username and email must be unique
- **Account Status**: Support for account deactivation

## Usage Examples

### Using the API with JavaScript/Fetch

```javascript
// Register a new user
const registerResponse = await fetch('/api/auth/register', {
    method: 'POST',
    headers: {
        'Content-Type': 'application/json'
    },
    body: JSON.stringify({
        username: 'dungeonmaster',
        email: 'dm@example.com',
        fullName: 'John Doe',
        dateOfBirth: '1990-01-01T00:00:00Z',
        password: 'SecurePassword123!',
        confirmPassword: 'SecurePassword123!'
    })
});

const authData = await registerResponse.json();
const token = authData.token;

// Use the token for authenticated requests
const profileResponse = await fetch('/api/auth/profile', {
    headers: {
        'Authorization': `Bearer ${token}`
    }
});
```

### Using with Swagger UI

1. Navigate to `/swagger` when the application is running
2. Use the "Authorize" button to enter your JWT token
3. All protected endpoints will now be accessible

## Error Handling

The API returns appropriate HTTP status codes and error messages:

- `400 Bad Request`: Validation errors or business rule violations
- `401 Unauthorized`: Invalid or missing JWT token
- `403 Forbidden`: Insufficient permissions
- `404 Not Found`: Resource not found
- `409 Conflict`: Username/email already exists
- `500 Internal Server Error`: Server-side errors

## Dependencies

- **ASP.NET Core 8.0**
- **MongoDB.Driver** - MongoDB integration
- **Microsoft.AspNetCore.Authentication.JwtBearer** - JWT authentication
- **System.IdentityModel.Tokens.Jwt** - JWT token handling
- **BCrypt.Net-Next** - Password hashing
- **Swashbuckle.AspNetCore** - API documentation

## Getting Started

1. **Clone the repository**
2. **Configure MongoDB connection string** in `appsettings.Development.json`
3. **Set JWT secret** in configuration
4. **Run the application**: `dotnet run`
5. **Access Swagger UI**: Navigate to `https://localhost:5001/swagger`

## Next Steps

- Implement refresh token functionality
- Add role-based authorization
- Add email verification
- Implement password reset functionality
- Add rate limiting
- Add audit logging 
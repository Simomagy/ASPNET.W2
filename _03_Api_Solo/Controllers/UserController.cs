using _01_Auth.Models;
using _03_Api_Solo.Models;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace _03_Api_Solo.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserControllerController : ControllerBase
{
    private readonly DaoUsers _daoUsers = DaoUsers.GetInstance();

    [HttpGet("GetAccounts")]
    [SwaggerOperation(Summary = "Retrieve all users", Description = "Gets a list of all users in the database.")]
    public IActionResult Get()
    {
        return Ok(_daoUsers.GetRecords());
    }

    [HttpGet("{username}-{password}")]
    [SwaggerOperation(Summary = "Retrieve account informations by username",
        Description = "Retrieve account informations by username")]
    public IActionResult Get(string username, string password)
    {
        var userExists = DaoUsers.GetInstance().UserExists(username, password);
        if (!userExists)
            return BadRequest();
        var user = DaoUsers.GetInstance().FindUser(username);
        return Ok(user);

    }

    [HttpPost("NewAccount")]
    [SwaggerOperation(Summary = "Add a new account",
        Description = "Add a new account")]
    public IActionResult Post([FromQuery] UserDto userDto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var user = new User
        {
            Username = userDto.Username,
            Pwd = userDto.Pwd,
            Role = userDto.Role,
            Name = userDto.Name,
            Surname = userDto.Surname,
            Dob = userDto.Dob,
            Propic = userDto.Propic
        };
        if (_daoUsers.CreateRecord(user))
            return Ok();
        return BadRequest();
    }

    [HttpGet("CheckAccount/{username}-{password}")]
    [SwaggerOperation(Summary = "Check whether an account exists or not",
        Description = "Check whether an account exists or not")]
    public IActionResult CheckAccount(string username, string password)
    {
        if (_daoUsers.UserExists(username, password))
            return Ok();
        return NotFound();
    }

    [HttpDelete("DeleteAccount/{username}-{password}")]
    [SwaggerOperation(Summary = "Delete an account",
        Description = "Delete an account")]
    public IActionResult DeleteAccount(string username, string password)
    {
        if (_daoUsers.DeleteRecord(username, password))
            return Ok();
        return NotFound();
    }
}

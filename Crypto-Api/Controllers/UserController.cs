using AutoMapper;
using Cripto_Api.Contract;
using Cripto_Api.Data;
using Cripto_Api.Models.User;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.AspNetCore.Authorization;

namespace Cripto_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {   
        private readonly IMapper _mapper;
        private readonly IUserContract _userContract;
        
        public UserController(IMapper mapper, IUserContract userContract)
        {
            _userContract = userContract;
            _mapper = mapper;
        }

        //Get api/User/{id}
        [HttpGet("{id}")]
        [Authorize(Roles = "user,admin")]
        public async Task<ActionResult<UserDto>> GetUserById(int id)
        {
            var userDetail = await _userContract.GetUserById(id);

            if (userDetail == null)
            {
                return NotFound();
            }
            var mapp = _mapper.Map<UserDto>(userDetail);

            return Ok(mapp);
        }
        
        //Post api/User/Login
        [HttpPost]
        [Route("Login")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult> PostLogin([FromBody]LoginUserDto loginUserDto)
        {
            var user = await _userContract.Login(loginUserDto);

            if (user == null)
            {
                return Unauthorized();
            }

            return Ok(user);
        }

        //Post api/User/Register
        [HttpPost("Register")]
        public async Task<ActionResult<User>> PostUser(CreateUserDto createUserDto)
        {
            var mapp = _mapper.Map<User>(createUserDto);
            var exist = await _userContract.Register(mapp);
;
            if (  exist == null )
            {
                return Ok("already exists");
            }

            return CreatedAtAction("GetUserById", new { id = mapp.Id }, mapp);
        }

        //Update api/User/{id} 
        [HttpPut("{id}")]
        [Authorize(Roles ="user,admin")]
        public async Task<IActionResult> PutUser(int id, UpdateUserDto updateUserDto)
        {
            var user = await _userContract.GetAsync(id);

            if(user == null)
            {
                BadRequest();
            }

            var passUser = await _userContract.UpdatePass(updateUserDto, user);

            if(passUser == null)
            {
                BadRequest();
            }

            _mapper.Map(passUser, user);

            try
            {
                await _userContract.UpdateAsync(passUser);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await UserExist(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        //Delete api/Update/{id}
        [HttpDelete("{id}")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var user = await _userContract.GetAsync(id);
            
            if(user == null)
            {
                return NotFound();
            }

            await _userContract.DeleteAsync(id);

            return NoContent();
        }

        private async Task<bool> UserExist(int id)
        {
            return await _userContract.Exists(id);
        }

    }
}

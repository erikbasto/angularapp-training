using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Api.Data;
using Api.DTOs;
using Api.Entities;
using Api.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : BaseApiController
    {
        private readonly IUserRepository _repository;
        private readonly IMapper _mapper;

        public UsersController(IUserRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<MemberDto>>> GetUsers()
        {
            var users =  await _repository.GetMembersAsync();
            return Ok(users);
        }
        
        /*[HttpGet("{id}")]
        public async Task<ActionResult<MemberDto>> GetUser(int id)
        {
            var user = await _repository.GetUserByIdAsync(id);
            return _mapper.Map<MemberDto>(user);
        }*/


        [HttpGet("{name}")]
        public async Task<ActionResult<MemberDto>> GetUserByName(string name)
        {
            return Ok(await _repository.GetMemberAsync(name));
        }

        [HttpPut]
        public async Task<ActionResult> UpdateUser(MemberUpdateDto memberUpdateDto)
        {
            var username = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var user = await _repository.GetUserByUsernameAsync(username);

            if (user == null) return NotFound();

            _mapper.Map(memberUpdateDto, user);

            if (await _repository.SaveAllAsync()) return NoContent();

            return BadRequest("Failed to update user");
        }
    }
}


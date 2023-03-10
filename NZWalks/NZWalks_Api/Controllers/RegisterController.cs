using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NZWalks_Api.Repositories;

namespace NZWalks_Api.Controllers
{
    [Route("Users")]
    [ApiController]
    public class RegisterController:Controller
    {
        private readonly IUserRepository userRepository;
        private IMapper _mapper;
        public RegisterController(IUserRepository userRepository,IMapper mapper)
        {
            this.userRepository = userRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllUsers()
        {
            /* var regions = new List<Region>()
             {
                 new Region{Id=Guid.NewGuid(),Name="Welligton",Code="WLG",Area=227755,Long=299.88,population=500000},
                 new Region{Id=Guid.NewGuid(),Name="Aukland",Code="AUK",Area=217755,Long=399.88,population=400000}

             };*/
            var users = await userRepository.GetAllUsersAsync();

            var usersDTO = new List<Models.DTO.User>();
            users.ToList().ForEach(user =>
            {
                var userDTO = new Models.DTO.User()
                {
                    Id = user.Id,
                    Username = user.Username,
                    EmailAddress = user.EmailAddress,
                    Password = user.Password,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Roles= user.Roles,
                   
                };

                usersDTO.Add(userDTO);
            });
            /*  var regionsDTO = _mapper.Map<List<Models.Domain.Region>>(regions);*/

            return Ok(usersDTO);
        }

        [HttpGet]
        [Route("{id:guid}")]
        [ActionName("GetAsync")]
        public async Task<IActionResult> GetAsync(Guid id)
        {
            // Validate The Request

            var region = await userRepository.GetUserAsync(id);

            if (region == null)
            {
                return NotFound();
            }

            var regionDTO = _mapper.Map<Models.DTO.User>(region);
            return Ok(regionDTO);
        }
        [HttpPost]

        public async Task<IActionResult> AddAsync(Models.DTO.AddUserRequest addUserRequest)
        {
            //validate request...
            
            //Request mathi je data avaya a aaapne pela domail naakhya..
            var user = new Models.Domain.User()
            {
                Username = addUserRequest.Username,
                FirstName = addUserRequest.FirstName,
                LastName = addUserRequest.LastName,
                Password= addUserRequest.Password,
                EmailAddress = addUserRequest.EmailAddress,
                Roles = addUserRequest.Roles

            };
            user = await userRepository.AddUserAsync(user);

            var userDTO = new Models.DTO.User()
            {
                Id = user.Id,
                Username = user.Username,
                FirstName = user.FirstName,
                LastName =user.LastName,
                Password = user.Password,
                EmailAddress = user.EmailAddress,
                Roles = user.Roles
            };
            return CreatedAtAction(("GetAsync"), new { id = user.Id }, userDTO);
        }
    }
}

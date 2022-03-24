using AutoMapper;
using Dto;
using Dto.ApiRequests;
using Entities;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Repositories;
using Transversals;
using Transversals.Filters;

namespace Services
{
    public class UserServices : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;
        private readonly ITokenClaims _tokenClaims;
        public UserServices(IUserRepository userRepository, IMapper mapper, IConfiguration configuration, ITokenClaims tokenClaims)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _configuration = configuration;
            _tokenClaims = tokenClaims;
        }

        /// <summary>
        /// Gets the JWT for authentiction
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<Response<AuthenticateResponseDto>> Authenticate(AuthenticateRequestDto model)
        {
            var _users = _mapper.Map<IEnumerable<DatumLoginDto>>(await _userRepository.GetAllUsersWithLogin());
            var user = _users.SingleOrDefault(x => x.Username == model.Username && x.Password == model.Password);

            if (user == null) return null;
            var token = await _tokenClaims.GetTokenAsync(user);

            return new Response<AuthenticateResponseDto> { Data = new AuthenticateResponseDto(user, token) };
        }

        /// <summary>
        /// Creates a new User
        /// </summary>
        /// <param name="userData"></param>
        /// <returns></returns>
        public async Task<Response<DatumDto>> CreateUser(DatumDto userData)
        {
            var request = _mapper.Map<Datum>(userData);
            return new Response<DatumDto>() { Data = _mapper.Map<DatumDto>(await _userRepository.CreateUser(request)), Succeeded = true };
        }

        /// <summary>
        /// Generates credential for a given id user
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<Response<DatumLoginDto>> GenerateCredentials(int id)
        {
            var _users = _mapper.Map<IEnumerable<DatumLoginDto>>(await _userRepository.GetAllUsersWithLogin());
            var user = _users.SingleOrDefault(x => x.id == id);
            if (user == null)
                return new Response<DatumLoginDto>() { Succeeded = false, Message = $"User id: {id} doesn't exists" };
            else
                return new Response<DatumLoginDto>() { Succeeded = true, Data = _mapper.Map<DatumLoginDto>(await _userRepository.GenerateCredentials(id)) };
        }

        /// <summary>
        /// Brings an unique User from db
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<Response<DatumDto>> GetUserById(int id)
        {
            return new Response<DatumDto>() { Data = _mapper.Map<DatumDto>(await _userRepository.GetUserById(id)), Succeeded = true };
        }

        /// <summary>
        /// Gets all Users from db
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        public async Task<Response<IEnumerable<DatumDto>>> GetUsers(PaginationFilter filter)
        {
            return new Response<IEnumerable<DatumDto>>() { Data = _mapper.Map<IEnumerable<DatumDto>>(await _userRepository.GetUsers(filter)), Succeeded = true };
        }

        /// <summary>
        /// Gets Users data from external API
        /// </summary>
        /// <returns></returns>
        public async Task<Response<IEnumerable<DatumDto>>> GetUsersFromExternalApi()
        {
            var rootParams = new RootDto();
            var page = 0;
            //Gets params to know wich page request
            try {
                rootParams = _mapper.Map<RootDto>(await _userRepository.GetRootParameters());
            }catch (Exception ex)
            {
                var toLog = ex.ToString();
            }

            //If total pages is reached returns to first page
            if (rootParams.actual_page == rootParams.total_pages)
                rootParams.actual_page = 1;
            else
                page=rootParams.actual_page++;


            HttpClient client = new();

            //gets the base url from settings
            var uri = _configuration.GetSection("ReqresApiRoute").Value + $"{rootParams.actual_page}";
            var externalData = JsonConvert.DeserializeObject<RootDto>(await (await client.GetAsync(uri)).Content.ReadAsStringAsync());
            var usersList = _mapper.Map<IEnumerable<DatumDto>>(await _userRepository.GetAllUsers());
            externalData.actual_page = rootParams.actual_page;

            await _userRepository.CreateUpdateRootParameters(_mapper.Map<Root>(externalData));

            //insert users if doesn't exists in db
            foreach (var user in externalData.data)
            {
                if ((usersList.ToList().Find(x => x.id == user.id) == null))
                {
                    var parsedUser = _mapper.Map<Datum>(user);
                    await _userRepository.CreateUser(parsedUser);
                }
            }
            return new Response<IEnumerable<DatumDto>> { Data = externalData.data };
        }

        /// <summary>
        /// Update an existing register from Db
        /// </summary>
        /// <param name="userData"></param>
        /// <returns></returns>
        public async Task<Response<DatumDto>> UpdateUser(DatumDto userData)
        {
            var request = _mapper.Map<Datum>(userData);
            return new Response<DatumDto>() { Data = _mapper.Map<DatumDto>(await _userRepository.UpdateUser(request)), Succeeded = true };
        }
    }
}
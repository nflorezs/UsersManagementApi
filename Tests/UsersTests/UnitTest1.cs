using AutoMapper;
using Dto;
using Entities;
using Mappers.Users;
using Microsoft.Extensions.Configuration;
using NUnit.Framework;
using Repositories;
using Services;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Telerik.JustMock;
using Telerik.JustMock.Helpers;
using Transversals.Filters;

namespace UsersTests
{
    [TestFixture]
    public class Tests
    {
        private UserService _userService;
        private UserRepository _userRepository;
        private IUserRepository _IuserRepository;
        private PaginationFilter _paginationFilter;
        private IMapper _mapper;
        private IConfiguration _configuration;
        private ITokenClaims _tokenClaims;
        private UsersMapper _usersMapper;
        [SetUp]
        public void Setup()
        {
            _configuration = Mock.Create<IConfiguration>();
            _tokenClaims = Mock.Create<ITokenClaims>();
            _userRepository=new UserRepository(_configuration);
            _IuserRepository=Mock.Create<IUserRepository>(Behavior.Strict);
            _paginationFilter=new PaginationFilter(1,5);
            _mapper = Mock.Create<IMapper>();
            _usersMapper = new UsersMapper();
        }

        [Test]
        public async Task GetUsersTest()
        {
            IEnumerable<Datum> listRepository = new List<Datum>();
            _paginationFilter = new PaginationFilter(Arg.AnyInt, Arg.AnyInt);
            Mock.Arrange(() => _IuserRepository.GetUsers(_paginationFilter)).Returns(Task.FromResult(listRepository));
            _userService = new UserService(_IuserRepository, _mapper, _configuration, _tokenClaims);
            var response = (await _userService.GetUsers(_paginationFilter));

            Assert.IsNotNull(response);
            Assert.IsInstanceOf(typeof(IEnumerable<DatumDto>), response.Data);
        }

        [Test]
        public async Task GetUsersByIdTest()
        {
            Mock.Arrange(() => _IuserRepository.GetUserById(Arg.AnyInt)).Returns(Task.FromResult(new Datum()));
            _userService = new UserService(_IuserRepository, _mapper, _configuration, _tokenClaims);
            var response = (await _userService.GetUserById(Arg.AnyInt));

            Assert.IsNotNull(response);
            Assert.IsInstanceOf(typeof(DatumDto), response.Data);
        }

        [Test]
        public async Task CreateUserTest()
        {
            Mock.Arrange(() => _IuserRepository.CreateUser(Arg.IsAny<Datum>())).Returns(Task.FromResult(new Datum()));
            _userService = new UserService(_IuserRepository, _mapper, _configuration, _tokenClaims);
            var response = (await _userService.CreateUser(Arg.IsAny<DatumDto>()));

            Assert.IsNotNull(response);
            Assert.IsInstanceOf(typeof(DatumDto), response.Data);
        }

        [Test]
        public async Task UpdateUserTest()
        {
            Mock.Arrange(() => _IuserRepository.UpdateUser(Arg.IsAny<Datum>())).Returns(Task.FromResult(new Datum()));
            _userService = new UserService(_IuserRepository, _mapper, _configuration, _tokenClaims);
            var response = (await _userService.UpdateUser(Arg.IsAny<DatumDto>()));

            Assert.IsNotNull(response);
            Assert.IsInstanceOf(typeof(DatumDto), response.Data);
        }
    }
}
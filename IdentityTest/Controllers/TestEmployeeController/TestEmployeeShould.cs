
using AutoFixture.Xunit2;
using FakeItEasy;
using LanTian.Solution.Core.CommonHelper;
using LanTian.Solution.Core.Domain.ICommonService;
using LanTian.Solution.Core.Domain.INpgSqlService;
using LanTian.Solution.Core.Domain.NpgSqlEntities.Identity;
using LanTian.Solution.Core.DTO.Identity;
using LanTian.Solution.Core.PropertyManagementWebAPI.Models;
using LanTian.Solution.Core.Infrastructure.NpgSqlService;
using LanTian.Solution.Core.ParameterModel.ChangeModel.Identity;
using LanTian.Solution.Core.propertyMgtWebAPI;
using LanTian.Solution.Core.propertyMgtWebAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Linq;
using Testing.Shared;
using static LanTian.Solution.Core.EnumAndConstent.Enums.LanTianEnum;

namespace LanTian.WisdomParkTests.Controllers.TestEmployeeController
{
    public class TestEmployeeShould
    {
        private readonly Solution.Core.PropertyManagementWebAPI.Controllers.Identity.EmployeeController _controller;
        private readonly IEmployeeService _fakeEmployeeService;
        private readonly IMemoryCacheHelper _fakeMemoryCacheHelper;
        private readonly IConfiguration _fakeConfig;
        private readonly HttpClient _fakeHttpClient;
        public TestEmployeeShould()
        {
            _fakeEmployeeService = A.Fake<IEmployeeService>();
            _fakeMemoryCacheHelper = A.Fake<IMemoryCacheHelper>();
            _fakeConfig = A.Fake<IConfiguration>();
            _fakeHttpClient = A.Fake<HttpClient>();

            _controller = new Solution.Core.PropertyManagementWebAPI.Controllers.Identity.EmployeeController(
                _fakeEmployeeService,
                _fakeMemoryCacheHelper,
                _fakeConfig,
                _fakeHttpClient
            );
        }
        [Theory]
        [AutoFakeItEasy]
        public async Task AddEmployeeAsync_ReturnsSuccess_WhenValidModel(
                 AddEditEmployeeModel model
            )
        {
            // Arrange
            //model.Cellphone = "123";
            A.CallTo(() => _fakeEmployeeService.AddEmployeeAsync(A<AddEditEmployeeModel>.Ignored, A<CancellationToken>.Ignored))
             .Returns(Task.FromResult(new Tuple<long, string>(2, "success")));

            // Act
            var result = await _controller.AddEmployeeAsync(model);

            // Assert
            //var okResult = Assert.IsType<OkObjectResult>(result);
            //var response = okResult.Value as dynamic;
            var okResult = Assert.IsType<OkObjectResult>(result);
            var response = JObject.FromObject(okResult.Value);// 或定义为具体的类型
    
            Assert.NotNull(response);
            Assert.Equal("success", response["Msg"].ToString());
            Assert.Equal("Ok", response["Status"].ToString());
            Assert.Equal(2, (long)response["Id"]);
           

            // 验证服务层是否被调用
            A.CallTo(() => _fakeEmployeeService.AddEmployeeAsync(model, A<CancellationToken>.Ignored))
                .MustHaveHappenedOnceExactly();
        }

        [Fact]
        public async Task EditEmployeeAsync_ReturnsSuccess_WhenValidModel()
        {
            // Arrange
            var model = new AddEditEmployeeModel
            {
                Id = 1,
                RealName = "Updated Name",
                RoleId = 2
            };

            var existingEmployee = new EmployeeDTO
            {
                Id = 1,
                RoleId = 1
            };

            A.CallTo(() => _fakeEmployeeService.GetEmployeeByIdAsync(A<long>.Ignored, A<CancellationToken>.Ignored))
                .Returns(Task.FromResult(existingEmployee));
            A.CallTo(() => _fakeEmployeeService.EditEmployeeAsync(A<AddEditEmployeeModel>.Ignored, A<CancellationToken>.Ignored))
                .Returns(Task.FromResult(new Tuple<bool, string>(true, "success")));

            // Act
            var result = await _controller.EditEmployeeAsync(model);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var response = JObject.FromObject(okResult.Value);
            Assert.Equal("Ok", response["Status"].ToString());
            Assert.Equal("success", response["Msg"].ToString());
            // 验证服务层是否被调用
            A.CallTo(() => _fakeEmployeeService.EditEmployeeAsync(model, A<CancellationToken>.Ignored))
                .MustHaveHappenedOnceExactly();

        }
        [Fact]
        public async Task GetEmployeeByIdAsync_ReturnsOk_WhenEmployeeExists()
        {
            // Arrange
            var model = new GetByIdModel { Id = 1 };
            var fakeEmployee = new EmployeeDTO
            {
                Id = 1,
                RealName = "Test Employee",
                Cellphone = "12345678901",
                RoleId = 1
            };
            A.CallTo(() => _fakeEmployeeService.GetEmployeeByIdAsync(model.Id, A<CancellationToken>.Ignored))
                .Returns(Task.FromResult(fakeEmployee));

            // Act
            var result = await _controller.GetEmployeeByIdAsync(model);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var response = JObject.FromObject(okResult.Value);
            Assert.Equal("Ok", response["Status"].ToString());
            Assert.NotNull(response["Data"]);
            Assert.Equal(1, (int)response["Data"]["Id"]);
            Assert.Equal("Test Employee", response["Data"]["RealName"]);
        }
        [Fact]
        public async Task GetEmployeeByIdAsync_CallsServiceMethod_Once()
        {
            // Arrange
            var model = new GetByIdModel { Id = 1 };
            var fakeEmployee = new EmployeeDTO { Id = 1, RealName = "Test Employee" };
            A.CallTo(() => _fakeEmployeeService.GetEmployeeByIdAsync(model.Id, A<CancellationToken>.Ignored))
                .Returns(Task.FromResult(fakeEmployee));

            // Act
            await _controller.GetEmployeeByIdAsync(model);

            // Assert
            A.CallTo(() => _fakeEmployeeService.GetEmployeeByIdAsync(model.Id, A<CancellationToken>.Ignored))
                .MustHaveHappenedOnceExactly();
        }
    }
}

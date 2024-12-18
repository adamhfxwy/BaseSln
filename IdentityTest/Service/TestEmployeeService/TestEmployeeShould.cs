using AutoFixture.Xunit2;
using FakeItEasy;
using FluentAssertions;
using LanTian.Solution.Core.Domain.ICommonService;
using LanTian.Solution.Core.Domain.INpgSqlService;
using LanTian.Solution.Core.Domain.NpgSqlEntities.Identity;
using LanTian.Solution.Core.Infrastructure.NpgSqlService;
using LanTian.Solution.Core.ParameterModel.ChangeModel.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Testing.Shared;
using static LanTian.Solution.Core.EnumAndConstent.Enums.LanTianEnum;

namespace LanTian.WisdomParkTests.Service.TestEmployeeService
{
    public class TestEmployeeShould
    {
        [Theory]
        [AutoFakeItEasy]
        public async Task AddEmployeeAsync_ReturnsSuccess_WhenValidModel(
            [Frozen] IRepository<LanTianEmployee> repository,
            EmployeeService sut,string realName,string cellphone, LoginPermissionsEnum loginPermissionsEnum, SexEnum sex,
            CancellationToken cancellationToken
            )
        {
            // Arrange
            var model = new AddEditEmployeeModel
            {
                RealName = realName,
                Cellphone = cellphone,
                LoginPermissions = loginPermissionsEnum,
                Sex = sex
            };
            A.CallTo(() => repository.FindAsync(x => x.Cellphone == cellphone, CancellationToken.None,null))
            .Returns(Task.FromResult<LanTianEmployee?>(null));
            //Act
            var tuple = await sut.AddEmployeeAsync(model, cancellationToken);
            //Assert
            tuple.Item2.Should().BeSameAs("success");
            A.CallTo(() => repository.InsertAsync(A<LanTianEmployee>.That.Matches(u => u.Cellphone == cellphone && u.RealName==realName
            && u.LoginPermissions == loginPermissionsEnum && u.Sex==sex),true, cancellationToken))
                .MustHaveHappenedOnceExactly();

        }
    }
}

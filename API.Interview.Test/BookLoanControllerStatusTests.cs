using System;
using API.Interview.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging.Abstractions;
using API.Interview.Services;

namespace API.Interview.Test;

[TestClass]
public class LoanStatusControllerStatusTests
{
    private static LoanStatusController Create()
    {
        IConfiguration config = new ConfigurationBuilder().Build();
        var loggerFactory = NullLoggerFactory.Instance;
        var repo = new InMemoryInterviewRepository();
        return new LoanStatusController(config, loggerFactory, repo);
    }

    [TestMethod]
    public void GetStatus_Returns_BadRequest_When_Id_Missing()
    {
        var controller = Create();

        var result = controller.GetStatus("");

        result.Result.Should().BeOfType<BadRequestObjectResult>();
    }

    [TestMethod]
    public void GetStatus_Returns_BadRequest_When_Id_Invalid()
    {
        var controller = Create();

        var result = controller.GetStatus("not-a-guid");

        result.Result.Should().BeOfType<BadRequestObjectResult>();
    }

    [TestMethod]
    public void GetStatus_Returns_NotFound_When_Stub_Missing()
    {
        var controller = Create();
        var id = Guid.Parse("00000000-0000-0000-0000-000000000001").ToString();

        var result = controller.GetStatus(id);

        result.Result.Should().BeOfType<NotFoundResult>();
    }

    [TestMethod]
    public void GetStatus_Returns_Ok_When_Stub_Exists()
    {
        var controller = Create();
        var id = Guid.Parse("00000000-0000-0000-0000-000000000002").ToString();

        var actionResult = controller.GetStatus(id);

        var ok = actionResult.Result.Should().BeOfType<OkObjectResult>().Subject;
        var dto = ok.Value.Should().BeOfType<LoanStatusDto>().Subject;
        dto.Id.Should().Be(Guid.Parse(id));
        dto.Status.Should().NotBeNullOrEmpty();
    }
}

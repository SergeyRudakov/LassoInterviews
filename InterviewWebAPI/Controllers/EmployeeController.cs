using InterviewWebAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace InterviewWebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EmployeeController : ControllerBase
    {
       
        private readonly ILogger<EmployeeController> _logger;

        public EmployeeController(ILogger<EmployeeController> logger)
        {
            _logger = logger;
        }

        //Task 1: Add a get endpoint that delivers the list of departments and total employees filtered by State which will be passed as a request parameter
        //Task 2: Add middleware to log execution time of this API

    }
}

using DapperUsingSP.Models;
using DapperUsingSP.Repository.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DapperUsingSP.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeRepository _employeeRepository;
        public EmployeeController(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }
        [HttpGet]
        public async Task<IActionResult> GetEmployees()
        {
            try
            {
                var employee = await _employeeRepository.GetEmployees();
                return Ok(employee);
            }
            catch(Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("id")]
        public async Task<IActionResult> GetEmployeeById(int id)
        {
            try
            {
                var employee= await _employeeRepository.GetEmployeeById(id);
                return Ok(employee);
            }
            catch(Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Insert(Employee employee)
        {
            try
            {
                var result = await _employeeRepository.Insert(employee);
                if (result == 0)
                {
                    return StatusCode(409, "The request could not be processed because of conflict in the request");

                }
                else
                {
                    return StatusCode(200, string.Format("Record Inserted Successfuly with employee Id is {0}", result));

                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
            [HttpPut]
            public async Task<IActionResult> Update(Employee employee)
            {
                try
                {
                    var result = await _employeeRepository.Update(employee);

                    if (result == 0)
                    {
                        return StatusCode(409, "The request could not be processed because of conflict in the request");
                    }
                    else
                    {
                        return StatusCode(200, string.Format("Record Updated Successfuly with order Id {0}", result));
                    }
                }
                catch (Exception ex)
                {
                    return StatusCode(500, ex.Message);
                }
            }
        [HttpDelete("id")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var order = await _employeeRepository.Delete(id);
                return Ok(order);
            }
            catch (Exception ex)
            {
                //log error
                return StatusCode(500, ex.Message);
            }
        }

    }


}


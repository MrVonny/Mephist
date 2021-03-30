using Mephist.Services;
using Mephist.Services.DAL;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mephist.Controllers
{
    [Route("api/university")]
    [ApiController]
    public class UniversityController : ControllerBase
    {
        private readonly UniversityData universityData;
        private readonly IHostEnvironment _environment;
        private readonly UniversityStaticData _universityStaticData;

        public UniversityController(UniversityData universityData, IHostEnvironment environment, UniversityStaticData universityStaticData)
        {
            this.universityData = universityData;
            _environment = environment;
            _universityStaticData = universityStaticData;
        }

        [HttpGet]
        [Route("employees")]
        public async Task<ActionResult> GetEmployees(string subject)
        {
            try
            {
                var employees = await universityData.Employees.GetAsync();
                if (!string.IsNullOrEmpty(subject))
                    employees = employees.Where(e => (e.Subjects ?? new List<string>()).Contains(subject));
                return Ok(employees.Select(e=>e.FullName));
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error retrieving data from the database");
            }

        }

        [HttpGet]
        [Route("subjects")]
        public ActionResult GetSubjects(bool isLaboratory)
        {
            try
            {
                var res = isLaboratory ?
                    _universityStaticData.GetLaboratorySubjects() : _universityStaticData.GetSubjects();
                return Ok(res);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error retrieving data from the database");
            }

        }

        [HttpGet]
        [Route("works")]
        public ActionResult GetSubjects(string subject)
        {
            if (subject is null)
                return BadRequest();
            try
            {
                return Ok(_universityStaticData.GetLaboratoryWorks(subject));
            }
            catch(InvalidCastException)
            {
                return BadRequest("Нет такого предмета");
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error retrieving data from the database");
            }

        }

        
    }
}

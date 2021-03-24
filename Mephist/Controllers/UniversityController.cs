using Mephist.Services;
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
        private readonly IUniversityRepository _repository;
        private readonly IHostEnvironment _environment;
        private readonly UniversityStaticData _universityStaticData;

        public UniversityController(IUniversityRepository repository, IHostEnvironment environment, UniversityStaticData universityStaticData)
        {
            _repository = repository;
            _environment = environment;
            _universityStaticData = universityStaticData;
        }

        [HttpGet]
        [Route("employees")]
        public ActionResult GetEmployees(string subject)
        {
            try
            {
                var employees = _repository.GetEmployees();
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

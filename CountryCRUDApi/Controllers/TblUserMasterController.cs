using CountryCRUDApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CountryCRUDApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class TblUserMasterController : ControllerBase
    {
        private readonly CountryDBContext _context;

        public TblUserMasterController(CountryDBContext context)
        {
            _context = context;
        }

        [HttpGet("GetAllUser")]
        public IActionResult Get()
        {
            try
            {
                var TblUserMasters = _context.TblUserMasters.ToList();
                if (TblUserMasters.Count == 0)
                {
                    return NotFound("No Data Found ");
                }
                return Ok(TblUserMasters);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
                
            }
        }
        [HttpGet("GetById/{id}")]

        public IActionResult Get(int id)
        {
            if(id <= 0)
            {
                return BadRequest("Invalid Request");

            }
            var TblUserMaster = _context.TblUserMasters.Find(id);
            if(TblUserMaster == null)
            {
                return NotFound($"Data Related to this {id} Id is not Found");
            }
            return Ok(TblUserMaster);
        }

        [HttpPost]
        public IActionResult Post([FromBody] List<TblUserMaster> models)
        {
            if (models == null || models.Count == 0)
            {
                return BadRequest("Data Models Not Found ");
            }
            foreach (var model in models)
            {
                _context.Add(model);
            }
            _context.SaveChanges();
            return Ok("Added Successfully");
        }


        [HttpPost("Update")]
        public IActionResult Update([FromBody] List<TblUserMaster> models)
        {
            if (models == null || !models.Any())
            {
                return BadRequest("Data should not be null or empty");
            }

            foreach (var model in models)
            {
                if (model.Id == 0)
                {
                    return BadRequest("There is no Id for one of the objects");
                }

                var TblUserMaster = _context.TblUserMasters.Find(model.Id);
                if (TblUserMaster == null)
                {
                    return NotFound($"The CountryMaster with Id {model.Id} is not found");
                }

                TblUserMaster.Username = model.Username;
                TblUserMaster.Password = model.Password;
                TblUserMaster.RoleId = model.RoleId;
            }

            _context.SaveChanges();
            return Ok("Models updated successfully");
        }

        [HttpDelete("Delete/{id}")]
        public IActionResult Delete([FromBody] List<int> ids)
        {
            if (ids == null)
            {
                return BadRequest("ID list is empty or null");
            }

            foreach (var id in ids)
            {
                var model = _context.TblUserMasters.Find(id);
                if (model == null)
                {
                    return NotFound($"Model with ID {id} not found");
                }
                _context.TblUserMasters.Remove(model);
            }

            _context.SaveChanges();
            return Ok("Models deleted successfully");
        }

    }

}

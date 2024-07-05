using CountryCRUDApi.Common;
using CountryCRUDApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

//namespace CountryCRUDApi.Controllers
//{
//    public class StateController : Controller
//    {
//        private readonly CountryDBContext _context;

//        public StateController(CountryDBContext context)
//        {
//            _context = context;
//        }
//        [HttpPost]
//        public IActionResult Post([FromBody] List<StateRequest> requests)
//        {
//            List<ApiResponse> response = new List<ApiResponse>();
//            if (requests.Count <= 0)
//            {
//                ApiResponse apiResponse = new ApiResponse()
//                {
//                    StatusCode = StatusCodes.Status400BadRequest.ToString(),
//                    StatusMessage = "Enter Valid Records",
//                    Data = null
//                };
//                response.Add(apiResponse);
//            }

//            foreach (var request in requests)
//            {
//                if (request == null || request.StateCode == null || request.StateCode == "" || request.StateName == null || request.StateName == "")
//                {
//                    ApiResponse apiResponse = new ApiResponse()
//                    {
//                        StatusCode = StatusCodes.Status400BadRequest.ToString(),
//                        StatusMessage = "All Fields are Mandatory for Status code : " + request.StateCode + " State Name : " + request.StateName,
//                        Data = null
//                    };
//                    response.Add(apiResponse);
//                    continue;
//                }
//                if (request.CountryId == 0 || request.CountryId == null)
//                {
//                    if (request == null || request.CountryCode == null || request.CountryCode == "" || request.CountryName == null || request.CountryName == "")
//                    {
//                        ApiResponse apiResponse = new ApiResponse()
//                        {
//                            StatusCode = StatusCodes.Status400BadRequest.ToString(),
//                            StatusMessage = "All Fields are Mandatory for Country code : " + request.CountryCode + " Country Name : " + request.CountryName,
//                            Data = null
//                        };
//                        response.Add(apiResponse);
//                        continue;
//                    }
//                    else
//                    {
//                        var CountryData = _context.TheCountryMasters.Where(x => x.CountryCode == request.CountryCode || x.Name == request.CountryName).FirstOrDefault();

//                        if (CountryData != null)
//                        {
//                            TheStateMaster theState = new TheStateMaster()
//                            {
//                                CountryId = CountryData.Id,
//                                StateCode = request.StateCode,
//                                StateName = request.StateName,
//                            };
//                            _context.TheStateMasters.Add(theState);
//                            _context.SaveChanges();
//                        }
//                        else
//                        {
//                            TheCountryMaster theCountry = new TheCountryMaster()
//                            {
//                                Id = request.CountryId,
//                                CountryCode = request.CountryCode,
//                                Name = request.CountryName,
//                            };
//                            _context.TheCountryMasters.Add(theCountry);
//                            _context.SaveChanges();
//                            var insertedId = theCountry.Id;

//                            TheStateMaster theState = new TheStateMaster()
//                            {
//                                CountryId = insertedId,
//                                StateCode = request.StateCode,
//                                StateName = request.StateName,
//                            };
//                            _context.TheStateMasters.Add(theState);
//                            _context.SaveChanges();

//                        }
//                    }
//                }
//                else
//                {
//                    var CountryId = _context.TheCountryMasters.Where(x => x.Id == request.CountryId).FirstOrDefault();
//                    if (CountryId != null)
//                    {
//                        TheStateMaster theState = new TheStateMaster()
//                        {
//                            CountryId = CountryId.Id,
//                            StateCode = request.StateCode,
//                            StateName = request.StateName,
//                        };
//                        _context.TheStateMasters.Add(theState) ;
//                        _context.SaveChanges();
//                    }
//                }
//                //_context.Add(model);
//            }

//            _context.SaveChanges();
//            return Ok("Models added successfully");
//        }
//    }
//}



//using CountryCRUDApi.Common;
//using CountryCRUDApi.Models;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.EntityFrameworkCore;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;

namespace CountryCRUDApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class StateController : ControllerBase
    {
        private readonly CountryDBContext _context;

        public StateController(CountryDBContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] List<StateRequest> requests)
        {
            List<ApiResponse> response = new List<ApiResponse>();
            if (requests.Count <= 0)
            {
                ApiResponse apiResponse = new ApiResponse()
                {
                    StatusCode = StatusCodes.Status400BadRequest.ToString(),
                    StatusMessage = "Enter Valid Records",
                    Data = null
                };
                response.Add(apiResponse);
                return BadRequest(response);
            }

            using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {   
                    foreach (var request in requests)
                    {
                        if (request == null || string.IsNullOrEmpty(request.StateCode) || string.IsNullOrEmpty(request.StateName))
                        {
                            ApiResponse apiResponse = new ApiResponse()
                            {
                                StatusCode = StatusCodes.Status400BadRequest.ToString(),
                                StatusMessage = "All Fields are Mandatory for Status code : " + request.StateCode + " State Name : " + request.StateName,
                                Data = null
                            };
                            response.Add(apiResponse);
                            continue;
                        }

                        if (request.CountryId == 0)
                        {
                            if (string.IsNullOrEmpty(request.CountryCode) || string.IsNullOrEmpty(request.CountryName))
                            {
                                ApiResponse apiResponse = new ApiResponse()
                                {
                                    StatusCode = StatusCodes.Status400BadRequest.ToString(),
                                    StatusMessage = "All Fields are Mandatory for Country code : " + request.CountryCode + " Country Name : " + request.CountryName,
                                    Data = null
                                };
                                response.Add(apiResponse);
                                continue;
                            }
                            else
                            {
                                var CountryData = _context.TheCountryMasters.FirstOrDefault(x => x.CountryCode == request.CountryCode || x.Name == request.CountryName);

                                if (CountryData != null)
                                {
                                    TheStateMaster theState = new TheStateMaster()
                                    {
                                        CountryId = CountryData.Id,
                                        StateCode = request.StateCode,
                                        StateName = request.StateName,
                                    };
                                    _context.TheStateMasters.Add(theState);
                                }
                                else
                                {
                                    Random random = new Random();
                                    int generatedId = random.Next(100, 1000);
                                    TheCountryMaster theCountry = new TheCountryMaster()
                                    {
                                        Id = generatedId,
                                        CountryCode = request.CountryCode,
                                        Name = request.CountryName,
                                    };
                                    _context.TheCountryMasters.Add(theCountry);
                                    await _context.SaveChangesAsync();
                                    var insertedId = theCountry.Id;

                                    TheStateMaster theState = new TheStateMaster()
                                    {
                                        //CountryId = insertedId,
                                        CountryId = Convert.ToInt32("dgt"),
                                        StateCode = request.StateCode,
                                        StateName = request.StateName,
                                    };
                                    _context.TheStateMasters.Add(theState);
                                }
                            }
                        }
                        else
                        {
                            var CountryId = _context.TheCountryMasters.FirstOrDefault(x => x.Id == request.CountryId);
                            if (CountryId != null)
                            {
                                TheStateMaster theState = new TheStateMaster()
                                {
                                    CountryId = CountryId.Id,
                                    StateCode = request.StateCode,
                                    StateName = request.StateName,
                                };
                                _context.TheStateMasters.Add(theState);
                            }
                        }
                    }

                    await _context.SaveChangesAsync();
                    await transaction.CommitAsync();

                    return Ok("Models added successfully");
                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync();
                    ApiResponse apiResponse = new ApiResponse()
                    {
                        StatusCode = StatusCodes.Status500InternalServerError.ToString(),
                        StatusMessage = "An error occurred while processing the request : "+ex.Message.ToString(),
                        Data = ex.Message
                    };
                    response.Add(apiResponse);
                    return StatusCode(StatusCodes.Status500InternalServerError, response);
                }
            }
        }

        [HttpGet("GetAllStateMaster")]
        public async Task<IActionResult> Get()
        {
            using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    var stateMasters = await _context.TheStateMasters.ToListAsync();
                    if (stateMasters.Count == 0)
                    {
                        await transaction.RollbackAsync();
                        return NotFound("No Data Found");
                    }

                    await transaction.CommitAsync();
                    return Ok(stateMasters);
                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync();
                    return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while retrieving data");
                }
            }
        }
        [HttpGet("GetById/{id}")]
        public async Task<IActionResult> Get(int id)
        {
            
            using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    if (id < 0)
                    {
                        await transaction.RollbackAsync(); 
                        return BadRequest($"TheStateMaster Details Not Found for ID: {id}");
                    }

                    
                    var stateMaster = await _context.TheStateMasters.FindAsync(id);

                    
                    if (stateMaster == null)
                    {
                        await transaction.RollbackAsync(); 
                        return NotFound($"TheStateMaster Details Not Found for ID: {id}");
                    }

                    
                    await transaction.CommitAsync();

                    return Ok(stateMaster);
                }
                catch (Exception ex)
                {
                  
                    await transaction.RollbackAsync();
                    return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
                }
            }
        }
        [HttpPost("Update")]
        public async Task<IActionResult> Update([FromBody] List<StateRequest> requests)
        {
            List<ApiResponse> response = new List<ApiResponse>();
            if (requests.Count <= 0)
            {
                ApiResponse apiResponse = new ApiResponse()
                {
                    StatusCode = StatusCodes.Status400BadRequest.ToString(),
                    StatusMessage = "Enter Valid Records",
                    Data = null
                };
                response.Add(apiResponse);
                return BadRequest(response);
            }

            using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    foreach (var request in requests)
                    {
                        if (request == null || string.IsNullOrEmpty(request.StateCode) || string.IsNullOrEmpty(request.StateName))
                        {
                            ApiResponse apiResponse = new ApiResponse()
                            {
                                StatusCode = StatusCodes.Status400BadRequest.ToString(),
                                StatusMessage = "All Fields are Mandatory for Status code : " + request.StateCode + " State Name : " + request.StateName,
                                Data = null
                            };
                            response.Add(apiResponse);
                            continue;
                        }

                        var stateMaster = await _context.TheStateMasters.FindAsync(request.Id);
                        if (stateMaster == null)
                        {
                            ApiResponse apiResponse = new ApiResponse()
                            {
                                StatusCode = StatusCodes.Status404NotFound.ToString(),
                                StatusMessage = "State with ID " + request.Id + " not found",
                                Data = null
                            };
                            response.Add(apiResponse);
                            continue;
                        }

                        stateMaster.StateCode = request.StateCode;
                        stateMaster.StateName = request.StateName;
                        stateMaster.CountryId = request.CountryId;

                        _context.TheStateMasters.Update(stateMaster);
                    }

                    await _context.SaveChangesAsync();
                    await transaction.CommitAsync();

                    return Ok("Models updated successfully");
                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync();
                    ApiResponse apiResponse = new ApiResponse()
                    {
                        StatusCode = StatusCodes.Status500InternalServerError.ToString(),
                        StatusMessage = "An error occurred while processing the request: " + ex.Message,
                        Data = ex.Message
                    };
                    response.Add(apiResponse);
                    return StatusCode(StatusCodes.Status500InternalServerError, response);
                }
            }
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    var stateMaster = await _context.TheStateMasters.FindAsync(id);
                    if (stateMaster == null)
                    {
                        await transaction.RollbackAsync();
                        return NotFound($"TheStateMaster with ID {id} not found");
                    }

                    _context.TheStateMasters.Remove(stateMaster);

                    await _context.SaveChangesAsync();
                    await transaction.CommitAsync();

                    return Ok($"TheStateMaster with ID {id} deleted successfully");
                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync();
                    return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
                }
            }
        }




    }
}

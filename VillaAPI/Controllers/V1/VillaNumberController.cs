using System.Net;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using VillaAPI.Models;
using VillaAPI.Models.DTO;
using VillaAPI.Repository.IRepository;

namespace VillaAPI.Controllers.V1;

[Route("api/v{version:apiVersion}/VillaNumberAPI")]
[ApiController]
[ApiVersion("1.0")]
public class VillaNumberController : ControllerBase
{
    private readonly IVillaNumberRepository _villaNumberDb;
    private readonly IVillaRepository _villaDb;
    private readonly IMapper _mapper;

    public VillaNumberController(
        IVillaNumberRepository villaNumberDb,
        IVillaRepository villaDb,
        IMapper mapper)
    {
        _villaNumberDb = villaNumberDb;
        _villaDb = villaDb;
        _mapper = mapper;
    }
    
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<APIResponse>> GetVillaNumbers()
    {
        try
        {
            IEnumerable<VillaNumber> villaNumbers = await _villaNumberDb.GetAllAsync(includeProperties: "Villa");
            return Ok(new APIResponse()
            {
                StatusCode = HttpStatusCode.OK,
                IsSuccessful = true,
                ErrorMessages = new(),
                Result = _mapper.Map<List<VillaNumberDTO>>(villaNumbers),
            });
        }
        catch (Exception e)
        {
            return new APIResponse()
            {
                StatusCode = HttpStatusCode.InternalServerError,
                IsSuccessful = false,
                ErrorMessages = new() { e.ToString() },
                Result = null,
            };
        }
    }
    
    [HttpGet("{number:int}", Name = "GetVillaNumber")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<APIResponse>> GetVillaNumber(int number)
    {
        try
        {
            if (number == 0)
            {
                return BadRequest(new APIResponse()
                {
                    StatusCode = HttpStatusCode.BadRequest,
                    IsSuccessful = false,
                    ErrorMessages = new(){"Villa number of zero is invalid"},
                    Result = null,
                });
            }

            var villaNumber = await _villaNumberDb.GetAsync(u => u.VillaNo == number, includeProperties: "Villa");

            if (villaNumber is null)
            {
                return NotFound(new APIResponse()
                {
                    StatusCode = HttpStatusCode.NotFound,
                    IsSuccessful = false,
                    ErrorMessages = new(){$"Villa number:{number} not found"},
                    Result = null,
                });
            }
            
            return Ok(new APIResponse()
            {
                StatusCode = HttpStatusCode.OK,
                IsSuccessful = true,
                ErrorMessages = new(),
                Result = _mapper.Map<VillaNumberDTO>(villaNumber),
            });
        }
        catch (Exception e)
        {
            return new APIResponse()
            {
                StatusCode = HttpStatusCode.InternalServerError,
                IsSuccessful = false,
                ErrorMessages = new() { e.ToString() },
                Result = null,
            };
        }
    }

    [HttpPost]
    [Authorize(Roles = "admin")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<APIResponse>> CreateVillaNumber([FromBody]VillaNumberCreateDTO? villaNumberDTO)
    {
        try
        {
            if (villaNumberDTO is null)
            {
                return BadRequest(new APIResponse()
                {
                    StatusCode = HttpStatusCode.BadRequest,
                    IsSuccessful = false,
                    ErrorMessages = new(){"No Villa Number data provided"},
                    Result = null,
                });
            }

            if (await _villaNumberDb.GetAsync(u => u.VillaNo == villaNumberDTO.VillaNo) is not null)
            {
                ModelState.AddModelError("ErrorMessages", "Villa number already exists");
                return BadRequest(new APIResponse()
                {
                    StatusCode = HttpStatusCode.BadRequest,
                    IsSuccessful = false,
                    ErrorMessages = new(){"Villa Number already exists"},
                    Result = ModelState,
                });
            }

            if (await _villaDb.GetAsync(u => u.Id == villaNumberDTO.VillaId) is null)
            {
                ModelState.AddModelError("ErrorMessages", $"Villa:{villaNumberDTO.VillaId} does not exist");
                return BadRequest(new APIResponse()
                {
                    StatusCode = HttpStatusCode.BadRequest,
                    IsSuccessful = false,
                    ErrorMessages = new(){$"Villa:{villaNumberDTO.VillaId} does not exist"},
                    Result = ModelState,
                });
            }

            VillaNumber model = _mapper.Map<VillaNumber>(villaNumberDTO);
            await _villaNumberDb.CreateAsync(model);

            return CreatedAtRoute("GetVillaNumber", new { number = model.VillaNo}, new APIResponse()
            {
                StatusCode = HttpStatusCode.Created,
                IsSuccessful = true,
                ErrorMessages = new(),
                Result = _mapper.Map<VillaNumberDTO>(model),
            });
        }
        catch (Exception e)
        {
            return new APIResponse()
            {
                StatusCode = HttpStatusCode.InternalServerError,
                IsSuccessful = false,
                ErrorMessages = new() { e.ToString() },
                Result = null,
            };
        }
    }

    [HttpDelete("{number:int}", Name = "DeleteVillaNumber")]
    [Authorize(Roles = "admin")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<APIResponse>> DeleteVillaNumber(int number)
    {
        try
        {
            if (number == 0)
            {
                return BadRequest(new APIResponse()
                {
                    StatusCode = HttpStatusCode.BadRequest,
                    IsSuccessful = false,
                    ErrorMessages = new(){"Villa number of zero is invalid"},
                    Result = null,
                });
            }
            
            var villaNumber = await _villaNumberDb.GetAsync(u => u.VillaNo == number);

            if (villaNumber is null)
            {
                return NotFound(new APIResponse()
                {
                    StatusCode = HttpStatusCode.NotFound,
                    IsSuccessful = false,
                    ErrorMessages = new(){$"Villa number:{number} not found"},
                    Result = null,
                });
            }
            
            await _villaNumberDb.RemoveAsync(villaNumber);

            return Ok(new APIResponse()
            {
                StatusCode = HttpStatusCode.NoContent,
                IsSuccessful = true,
                ErrorMessages = new(),
                Result = null,
            });
        }
        catch (Exception e)
        {
            return new APIResponse()
            {
                StatusCode = HttpStatusCode.InternalServerError,
                IsSuccessful = false,
                ErrorMessages = new() { e.ToString() },
                Result = null,
            };
        }
    }
    
    [HttpPut("{number:int}", Name = "UpdateVillaNumber")]
    [Authorize(Roles = "admin")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<APIResponse>> UpdateVillaNumber(int number, [FromBody]VillaNumberUpdateDTO? villaNumberDTO)
    {
        try
        {
            if (villaNumberDTO is null || number != villaNumberDTO.VillaNo)
            {
                return BadRequest(new APIResponse()
                {
                    StatusCode = HttpStatusCode.BadRequest,
                    IsSuccessful = false,
                    ErrorMessages = new(){$"Villa number of {number} is invalid"},
                    Result = null,
                });
            }
            
            if (await _villaNumberDb.GetAsync(u => u.VillaNo == villaNumberDTO.VillaNo) is not null)
            {
                ModelState.AddModelError("CustomError", "Villa number already exists");
                return BadRequest(new APIResponse()
                {
                    StatusCode = HttpStatusCode.BadRequest,
                    IsSuccessful = false,
                    ErrorMessages = new(){"Villa Number already exists"},
                    Result = ModelState,
                });
            }

            VillaNumber model = _mapper.Map<VillaNumber>(villaNumberDTO);
        
            await _villaNumberDb.UpdateAsync(model);

            return Ok(new APIResponse()
            {
                StatusCode = HttpStatusCode.NoContent,
                IsSuccessful = true,
                ErrorMessages = new(),
                Result = _mapper.Map<VillaNumberDTO>(model),
            });
        }
        catch (Exception e)
        {
            return new APIResponse()
            {
                StatusCode = HttpStatusCode.InternalServerError,
                IsSuccessful = false,
                ErrorMessages = new() { e.ToString() },
                Result = null,
            };
        }
    }

    [HttpPatch("{number:int}", Name = "UpdatePartialVillaNumber")]
    [Authorize(Roles = "admin")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<APIResponse>> UpdatePartialVilla(int number, JsonPatchDocument<VillaNumberUpdateDTO>? patchNumberDTO)
    {
        try
        {
            if (patchNumberDTO is null || number == 0)
            {
                return BadRequest(new APIResponse()
                {
                    StatusCode = HttpStatusCode.BadRequest,
                    IsSuccessful = false,
                    ErrorMessages = new(){$"Villa number of {number} is invalid"},
                    Result = null,
                });
            }

            var villaNumber = await _villaNumberDb.GetAsync(u => u.VillaNo == number, false);

            if (villaNumber is null)
            {
                return NotFound(new APIResponse()
                {
                    StatusCode = HttpStatusCode.NotFound,
                    IsSuccessful = false,
                    ErrorMessages = new(){$"Villa number:{number} not found"},
                    Result = null,
                });
            }

            VillaNumberUpdateDTO villaNumberDTO = _mapper.Map<VillaNumberUpdateDTO>(villaNumber);
        
            patchNumberDTO.ApplyTo(villaNumberDTO, ModelState);

            if (!ModelState.IsValid)
            {
                return BadRequest(new APIResponse()
                {
                    StatusCode = HttpStatusCode.BadRequest,
                    IsSuccessful = false,
                    ErrorMessages = new(){$"Unable to patch Villa Number"},
                    Result = ModelState,
                });
            }

            VillaNumber model = _mapper.Map<VillaNumber>(villaNumberDTO);

            await _villaNumberDb.UpdateAsync(model);

            return Ok(new APIResponse()
            {
                StatusCode = HttpStatusCode.NoContent,
                IsSuccessful = true,
                ErrorMessages = new(),
                Result = _mapper.Map<VillaNumberDTO>(model),
            });
        }
        catch (Exception e)
        {
            return new APIResponse()
            {
                StatusCode = HttpStatusCode.InternalServerError,
                IsSuccessful = false,
                ErrorMessages = new() { e.ToString() },
                Result = null,
            };
        }
    }
}
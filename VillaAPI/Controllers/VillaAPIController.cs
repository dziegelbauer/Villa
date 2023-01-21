using System.Net;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using VillaAPI.Models;
using VillaAPI.Models.DTO;
using VillaAPI.Repository.IRepository;

namespace VillaAPI.Controllers;

[Route("api/VillaAPI")]
[ApiController]
public class VillaApiController : ControllerBase
{
    private readonly IVillaRepository _villaDb;
    private readonly IMapper _mapper;

    public VillaApiController(
        IVillaRepository villaDb,
        IMapper mapper)
    {
        _villaDb = villaDb;
        _mapper = mapper;
    }
    
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<ActionResult<APIResponse>> GetVillas()
    {
        try
        {
            IEnumerable<Villa> villas = await _villaDb.GetAllAsync();
            return Ok(new APIResponse()
            {
                StatusCode = HttpStatusCode.OK,
                IsSuccessful = true,
                ErrorMessages = new(),
                Result = _mapper.Map<List<VillaDTO>>(villas),
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
    
    [HttpGet("{id:int}", Name = "GetVilla")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<APIResponse>> GetVilla(int id)
    {
        try
        {
            if (id == 0)
            {
                return BadRequest(new APIResponse()
                {
                    StatusCode = HttpStatusCode.BadRequest,
                    IsSuccessful = false,
                    ErrorMessages = new(){"Villa id of zero is invalid"},
                    Result = null,
                });
            }

            var villa = await _villaDb.GetAsync(u => u.Id == id);

            if (villa is null)
            {
                return NotFound(new APIResponse()
                {
                    StatusCode = HttpStatusCode.NotFound,
                    IsSuccessful = false,
                    ErrorMessages = new(){$"Villa id:{id} not found"},
                    Result = null,
                });
            }
            
            return Ok(new APIResponse()
            {
                StatusCode = HttpStatusCode.OK,
                IsSuccessful = true,
                ErrorMessages = new(),
                Result = _mapper.Map<VillaDTO>(villa),
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
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<APIResponse>> CreateVilla([FromBody]VillaCreateDTO? villaDTO)
    {
        try
        {
            if (villaDTO is null)
            {
                return BadRequest(new APIResponse()
                {
                    StatusCode = HttpStatusCode.BadRequest,
                    IsSuccessful = false,
                    ErrorMessages = new(){"No Villa data provided"},
                    Result = null,
                });
            }

            if (await _villaDb.GetAsync(u => String.Equals(u.Name, villaDTO.Name)) is not null)
            {
                ModelState.AddModelError("CustomError", "Villa already exists");
                return BadRequest(new APIResponse()
                {
                    StatusCode = HttpStatusCode.BadRequest,
                    IsSuccessful = false,
                    ErrorMessages = new(){"Villa already exists"},
                    Result = ModelState,
                });
            }

            Villa model = _mapper.Map<Villa>(villaDTO);
            await _villaDb.CreateAsync(model);

            return CreatedAtRoute("GetVilla", new { id = model.Id}, new APIResponse()
            {
                StatusCode = HttpStatusCode.Created,
                IsSuccessful = true,
                ErrorMessages = new(),
                Result = _mapper.Map<VillaDTO>(model),
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

    [HttpDelete("{id:int}", Name = "DeleteVilla")]
    [Authorize(Roles = "admin")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<APIResponse>> DeleteVilla(int id)
    {
        try
        {
            if (id == 0)
            {
                return BadRequest(new APIResponse()
                {
                    StatusCode = HttpStatusCode.BadRequest,
                    IsSuccessful = false,
                    ErrorMessages = new(){"Villa id of zero is invalid"},
                    Result = null,
                });
            }
            
            var villa = await _villaDb.GetAsync(u => u.Id == id);

            if (villa is null)
            {
                return NotFound(new APIResponse()
                {
                    StatusCode = HttpStatusCode.NotFound,
                    IsSuccessful = false,
                    ErrorMessages = new(){$"Villa id:{id} not found"},
                    Result = null,
                });
            }
            
            await _villaDb.RemoveAsync(villa);

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
    
    [HttpPut("{id:int}", Name = "UpdateVilla")]
    [Authorize(Roles = "admin")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<APIResponse>> UpdateVilla(int id, [FromBody]VillaUpdateDTO? villaDTO)
    {
        try
        {
            if (villaDTO is null || id != villaDTO.Id)
            {
                return BadRequest(new APIResponse()
                {
                    StatusCode = HttpStatusCode.BadRequest,
                    IsSuccessful = false,
                    ErrorMessages = new(){$"Villa id of {id} is invalid"},
                    Result = null,
                });
            }

            Villa model = _mapper.Map<Villa>(villaDTO);
        
            await _villaDb.UpdateAsync(model);

            return Ok(new APIResponse()
            {
                StatusCode = HttpStatusCode.NoContent,
                IsSuccessful = true,
                ErrorMessages = new(),
                Result = _mapper.Map<VillaDTO>(model),
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

    [HttpPatch("{id:int}", Name = "UpdatePartialVilla")]
    [Authorize(Roles = "admin")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<APIResponse>> UpdatePartialVilla(int id, JsonPatchDocument<VillaUpdateDTO>? patchDTO)
    {
        try
        {
            if (patchDTO is null || id == 0)
            {
                return BadRequest(new APIResponse()
                {
                    StatusCode = HttpStatusCode.BadRequest,
                    IsSuccessful = false,
                    ErrorMessages = new(){$"Villa id of {id} is invalid"},
                    Result = null,
                });
            }

            var villa = await _villaDb.GetAsync(u => u.Id == id, false);

            if (villa is null)
            {
                return NotFound(new APIResponse()
                {
                    StatusCode = HttpStatusCode.NotFound,
                    IsSuccessful = false,
                    ErrorMessages = new(){$"Villa id:{id} not found"},
                    Result = null,
                });
            }

            VillaUpdateDTO villaDTO = _mapper.Map<VillaUpdateDTO>(villa);
        
            patchDTO.ApplyTo(villaDTO, ModelState);

            if (!ModelState.IsValid)
            {
                return BadRequest(new APIResponse()
                {
                    StatusCode = HttpStatusCode.BadRequest,
                    IsSuccessful = false,
                    ErrorMessages = new(){$"Unable to patch Villa"},
                    Result = ModelState,
                });
            }

            Villa model = _mapper.Map<Villa>(villaDTO);

            await _villaDb.UpdateAsync(model);

            return Ok(new APIResponse()
            {
                StatusCode = HttpStatusCode.NoContent,
                IsSuccessful = true,
                ErrorMessages = new(),
                Result = _mapper.Map<VillaDTO>(model),
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
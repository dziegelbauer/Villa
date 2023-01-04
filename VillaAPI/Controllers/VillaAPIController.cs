using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using VillaAPI.Data;
using VillaAPI.Logging;
using VillaAPI.Models.DTO;

namespace VillaAPI.Controllers;

[Route("api/VillaAPI")]
[ApiController]
public class VillaApiController : ControllerBase
{
    private readonly ILogging _logger;

    public VillaApiController(ILogging logger)
    {
        _logger = logger;
    }
    
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public ActionResult<IEnumerable<VillaDTO>> GetVillas()
    {
        _logger.Log("Getting all villas", String.Empty);
        return Ok(VillaStore.VillaList);
    }
    
    [HttpGet("{id:int}", Name = "GetVilla")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult<VillaDTO> GetVilla(int id)
    {
        if (id == 0)
        {
            _logger.Log($"GetVilla error: id = {id}", "error");
            return BadRequest();
        }

        var villa = VillaStore.VillaList.FirstOrDefault(u => u.Id == id);

        if (villa is null)
        {
            return NotFound();
        }
        
        return Ok(villa);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public ActionResult<VillaDTO> CreateVilla([FromBody]VillaDTO? villaDTO)
    {
        if (villaDTO is null)
        {
            return BadRequest(villaDTO);
        }
        
        if (VillaStore.VillaList.FirstOrDefault(u => String.Equals(u.Name, villaDTO.Name, StringComparison.CurrentCultureIgnoreCase)) is not null)
        {
            ModelState.AddModelError("CustomError", "Villa already exists");
            return BadRequest(ModelState);
        }
        
        if (villaDTO.Id != 0)
        {
            return StatusCode(StatusCodes.Status500InternalServerError);
        }

        villaDTO.Id = VillaStore.VillaList.MaxBy(u => u.Id)!.Id + 1;
        
        VillaStore.VillaList.Add(villaDTO);

        return CreatedAtRoute("GetVilla", new { id = villaDTO.Id}, villaDTO);
    }

    [HttpDelete("{id:int}", Name = "DeleteVilla")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult DeleteVilla(int id)
    {
        if (id == 0)
        {
            return BadRequest();
        }
        
        var villa = VillaStore.VillaList.FirstOrDefault(u => u.Id == id);

        if (villa is null)
        {
            return NotFound();
        }

        VillaStore.VillaList.Remove(villa);

        return NoContent();
    }
    
    [HttpPut("{id:int}", Name = "UpdateVilla")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult UpdateVilla(int id, [FromBody]VillaDTO? villaDTO)
    {
        if (villaDTO is null || id != villaDTO.Id)
        {
            return BadRequest();
        }

        var villa = VillaStore.VillaList.FirstOrDefault(u => u.Id == id);

        if (villa is null)
        {
            return NotFound();
        }

        villa.Name = villaDTO.Name;
        villa.Sqft = villaDTO.Sqft;
        villa.Occupancy = villaDTO.Occupancy;

        return NoContent();
    }

    [HttpPatch("{id:int}", Name = "UpdatePartialVilla")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult UpdatePartialVilla(int id, JsonPatchDocument<VillaDTO>? patchDTO)
    {
        if (patchDTO is null || id == 0)
        {
            return BadRequest();
        }

        var villa = VillaStore.VillaList.FirstOrDefault(u => u.Id == id);

        if (villa is null)
        {
            return NotFound();
        }
        
        patchDTO.ApplyTo(villa, ModelState);

        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        return NoContent();
    }
}
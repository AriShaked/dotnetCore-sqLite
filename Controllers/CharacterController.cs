
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using dotnet_rpg.Dtos.Character;
using dotnet_rpg.models;
using dotnet_rpg.services.CharacterService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace dotnet_rpg.Controllers
{
    [Authorize(Roles="Player , Admin")]
    [ApiController]
    [Route("[Controller]")]
    public class CharacterController: ControllerBase
    {
        private readonly ICharacterService _characterService;
        public CharacterController(ICharacterService characterService)
        {
            _characterService = characterService;
        }

        // [AllowAnonymous] // make it public without Auth
        [HttpGet("GetAll")]
        public async Task<IActionResult> Get() 
        {
            return Ok(await _characterService.GetAllCharacters());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetSingle(int id) 
        {
            return Ok(await _characterService.GetCharacterById(id));
        }

        [HttpPost]
        public async Task<IActionResult> AddCharacter(AddCharacterDto newCharacter) {
            return Ok(await _characterService.AddCharacter(newCharacter));
        }
        
        [HttpPut]
        public async Task<IActionResult> UpdateCharacter(UpdateCharacterDto updatedCharacter) 
        {

            ServiceResponse<GetCharacterDto> response = await _characterService.UpdateCharacter(updatedCharacter);
            if (response.Data == null) {
                return NotFound(response);
            }
            return Ok(response);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteCharacterById(int id) 
        {
            ServiceResponse<List<GetCharacterDto>> response = await _characterService.DeleteCharacterById(id);
            if (response.Data == null) {
                return NotFound(response);
            }
            return Ok(response);

        }
    }
}
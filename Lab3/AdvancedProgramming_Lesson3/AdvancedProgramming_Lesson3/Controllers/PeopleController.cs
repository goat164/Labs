using AdvancedProgramming_Lesson3.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdvancedProgramming_Lesson3.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PeopleController : ControllerBase
    {
        private readonly PeopleContext _context;

        public PeopleController(PeopleContext context)
        {
            _context = context;
        }

        // GET: api/People
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PeopleDto>>> GetPersons()
        {
            return await _context.Peoples
                .Select(x => PersonToDTO(x))
                .ToListAsync();
        }

        // GET: api/People/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PeopleDto>> GetPerson(long id)
        {
            var person = await _context.Peoples.FindAsync(id);
            if (person == null)
            {
                return NotFound();
            }

            return PersonToDTO(person);
        }

        [HttpPost]
        [Route("UpdatePerson")]
        public async Task<ActionResult<PeopleDto>> UpdatePerson(PeopleDto personDto)
        {
            var person = await _context.Peoples.FindAsync(personDto.Id);
            if (person == null)
            {
                return NotFound();
            }

            person.Name = personDto.Name;
            person.Surname = personDto.Surname;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException) when (!PersonExists(personDto.Id))
            {
                return NotFound();
            }

            return CreatedAtAction(
                nameof(UpdatePerson),
                new { id = person.Id },
                PersonToDTO(person));
        }

        [HttpPost]
        [Route("CreatePerson")]
        public async Task<ActionResult<People>> CreatePerson(PeopleDto personDto)
        {
            var person = new People
            {
                Name = personDto.Name,
                Surname = personDto.Surname
            };

            _context.Peoples.Add(person);
            await _context.SaveChangesAsync();

            return CreatedAtAction(
                nameof(GetPerson),
                new { id = person.Id },
                PersonToDTO(person));
        }

        // DELETE: api/People/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<People>> DeletePerson(long id)
        {
            var person = await _context.Peoples.FindAsync(id);
            if (person == null)
            {
                return NotFound();
            }
            _context.Peoples.Remove(person);
            await _context.SaveChangesAsync();
            return NoContent();
        }


        private bool PersonExists(long id) =>
            _context.Peoples.Any(e => e.Id == id);

        private static PeopleDto PersonToDTO(People person) =>
            new PeopleDto()
            {
                Id = person.Id,
                Name = person.Name,
                Surname = person.Surname
            };
    }
}

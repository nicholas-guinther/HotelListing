using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using HotelListing.API.Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HotelListing.API.Data;
using HotelListing.API.Models.Country;

namespace HotelListing.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CountriesController : ControllerBase
    {
        private readonly ICountriesRepository _repository;
        private readonly IMapper _mapper;

        public CountriesController(ICountriesRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        // GET: api/Countries
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GetCountryDto>>> GetCountries()
        { 
            var countries = await _repository.GetAllAsync();
            var records = _mapper.Map<List<GetCountryDto>>(countries);

            return Ok(records);
        }

        // GET: api/Country/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CountryDto>> GetCountry(int id)
        {
            var country = await _repository.GetDetails(id);

            if (country == null)
            {
                return NotFound();
            }

            var record = _mapper.Map<CountryDto>(country);

            return record;
        }

        // PUT: api/Country/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCountry(int id, UpdateCountryDto updateCountryDto)
        {
            if (id != updateCountryDto.Id)
            {
                return BadRequest();
            }

            // _context.Entry(country).State = EntityState.Modified;

            var country = await _repository.GetAsync(id);
            
            if (country == null)
            {
                return NotFound();
            }

            _mapper.Map(updateCountryDto, country);

            try
            {
                await _repository.UpdateAsync(country);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await CountryExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Country
        [HttpPost]
        public async Task<ActionResult<Country>> PostCountry(CreateCountryDto createCountryDto)
        {

            var countryOld = new Country
          {
              Name = createCountryDto.Name,
              ShortName = createCountryDto.ShortName
          };

          var country = _mapper.Map<Country>(createCountryDto);

          await _repository.AddAsync(country);

            return CreatedAtAction("GetCountry", new { id = country.Id }, country);
        }

        // DELETE: api/Country/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCountry(int id)
        {

            var country = await _repository.GetAsync(id);
            if (country == null)
            {
                return NotFound();
            }
            
            await _repository.DeleteAsync(id);

            return NoContent();
        }

        private async Task<bool> CountryExists(int id)
        {
            return await _repository.Exists(id);
        }
    }
}

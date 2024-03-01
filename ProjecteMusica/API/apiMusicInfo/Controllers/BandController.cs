using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using apiMusicInfo.Models;
using apiMusicInfo.Services;

namespace apiMusicInfo.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BandsController : ControllerBase
    {
        private readonly BandService _bandService;

        public BandsController(BandService bandService)
        {
            _bandService = bandService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Band>> GetBands()
        {
            var bands = _bandService.GetAllBands();
            return Ok(bands);
        }

        [HttpGet("{name}/{foundationDate}")]
        public ActionResult<Band> GetBand(string name, DateTime foundationDate)
        {
            var band = _bandService.GetBand(name, foundationDate);
            if (band == null)
                return NotFound();
            return Ok(band);
        }

        [HttpPost]
        public ActionResult<Band> CreateBand(Band band)
        {
            var createdBand = _bandService.CreateBand(band);
            return CreatedAtAction(nameof(GetBand), new { name = createdBand.Name, foundationDate = createdBand.FoundationDate }, createdBand);
        }

        [HttpPut("{name}/{foundationDate}")]
        public IActionResult UpdateBand(string name, DateTime foundationDate, Band band)
        {
            if (name != band.Name || foundationDate != band.FoundationDate)
                return BadRequest();

            var updatedBand = _bandService.UpdateBand(band);
            if (updatedBand == null)
                return NotFound();

            return NoContent();
        }

        [HttpDelete("{name}/{foundationDate}")]
        public IActionResult DeleteBand(string name, DateTime foundationDate)
        {
            var result = _bandService.DeleteBand(name, foundationDate);
            if (!result)
                return NotFound();
            return NoContent();
        }
    }
}

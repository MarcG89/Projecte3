﻿using ApiMusica.Classes.Model;
using ApiMusica.Controllers.v1.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using mla.ApiMusica.Services;
using System;
using System.Threading.Tasks;

namespace ApiMusica.Controllers.v1
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class AlbumController : ControllerBase
    {
        private readonly AlbumService _albumService;
        private readonly ILogger<AlbumController> _logger;

        public AlbumController(AlbumService albumService, ILogger<AlbumController> logger)
        {
            _albumService = albumService ?? throw new ArgumentNullException(nameof(albumService));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }
        [HttpPost]
        public async Task<ActionResult<Album>> CreateAlbumWithImages([FromForm] AlbumUploadModel albumModel)
        {
            try
            {
                // Subir las imágenes a GridFS y obtener los ObjectIds
                var frontCoverId = await _albumService.SubirImagenAsync(albumModel.FrontCover, $"{albumModel.Titol}_FrontCover", "image/jpeg");
                var backCoverId = await _albumService.SubirImagenAsync(albumModel.BackCover, $"{albumModel.Titol}_BackCover", "image/jpeg");

                // Crear un nuevo objeto Album y establecer los ObjectID de las imágenes
                var album = new Album
                {
                    Titol = albumModel.Titol,
                    Year = albumModel.Year,
                    Gender = albumModel.Gender,
                    FrontCover = frontCoverId,
                    BackCover = backCoverId
                };

                // Crear el álbum en la base de datos
                await _albumService.CreateAlbum(album);

                return Ok(album);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al crear el álbum con imágenes.");
                return StatusCode(500, "Error interno del servidor.");
            }
        }

        [HttpGet("{titol}")]
        public async Task<ActionResult<Album>> GetByTitol(string titol)
        {
            try
            {
                var album = await _albumService.GetByTitol(titol);
                if (album == null)
                {
                    return NotFound();
                }
                return Ok(album);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener el álbum por título.");
                return StatusCode(500, "Error interno del servidor.");
            }
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Album>>> GetAll()
        {
            try
            {
                var albums = await _albumService.GetAll();
                return Ok(albums);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener todos los álbumes.");
                return StatusCode(500, "Error interno del servidor.");
            }
        }
    }
}
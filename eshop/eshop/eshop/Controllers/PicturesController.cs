using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eshop.Logger;
using eshop.Models;
using eshop.Repositories.Core;
using eshop.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace eshop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class PicturesController : ControllerBase
    {
        private IImageService ImageService;
        private IRepositoryWrapper RepoW;
        private ILoggerManager Logger;

        public PicturesController(IImageService imageService, IRepositoryWrapper repoW, ILoggerManager logger)
        {
            ImageService = imageService;
            RepoW = repoW;
            Logger = logger;
        }

        [HttpPost]
        public IActionResult SavePictures(ICollection<IFormFile> pictures)
        {
            if (pictures.Count > 0)
            { 
                try
                {
                    ImageService.CreateRangePictures(pictures);
                    RepoW.Save();

                    Logger.LogInfo($@"Création d'images avec succès - FCT : SavePictures");
                    return Ok();
                }
                catch (Exception e)
                {
                    Logger.LogError($@"Une erreur est survenue lors de la création d'images - FCT : SavePictures - DETAIL : {e.Message}");
                    return StatusCode(500, $@"Un problème interne est survenu");
                }
            }
            return NoContent();
        }
    }
}
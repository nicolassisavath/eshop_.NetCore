using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eshop.Logger;
using eshop.Models;
using eshop.Repositories.Core;
using eshop.Services.Core;
using eshop.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace eshop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ArticlesController : ControllerBase
    {
        private IRepositoryWrapper RepoW;
        private IServicesWrapper SvcW;
        private ILoggerManager Logger;

        public ArticlesController(IServicesWrapper svcW, IRepositoryWrapper repoW, ILoggerManager logger)
        {
            RepoW = repoW;
            SvcW = svcW;
            Logger = logger;
        }

        [HttpPost]
        public IActionResult CreateArticle([FromForm] Articles newArticle, ICollection<IFormFile> pictures)
        {
            if (!ModelState.IsValid)
                return BadRequest($@"Le modele n'est pas valide");

            if (newArticle.ATitle == null || newArticle.ADescription == null || newArticle.APriceunit == 0)
                return BadRequest($@"Des données sont manquantes");

            try
            {
                Articles CreatedArticle = SvcW.ArticleSvc.CreateArticle(newArticle); // Create the Article
                if (pictures.Count > 0)
                {
                    IList<Pictures> createdPictures = SvcW.ImageSvc.CreateRangePictures(pictures); // Create the pictures
                    SvcW.ImageSvc.JoinPicturesToArticle(CreatedArticle, createdPictures); // Create Link the pictures to the article
                }
                RepoW.Save();

                Logger.LogInfo($@"Un article a été crée id : {newArticle.AId}");
                return Ok(CreatedArticle);
            }
            catch (Exception e)
            {
                Logger.LogError($@"La création de l'article a échoué - FCT : CreateArticle - DETAIL : {e.Message}");
                return StatusCode(500, $@"Un problème interne est survenu");
            }
        }

        [HttpGet]
        public IActionResult GetArticlesList()
        {
            try
            {
                IEnumerable<Articles> articles = RepoW.ArtRepo.GetAll();
                return Ok(articles);
            }
            catch(Exception e)
            {
                Logger.LogError($@"Une erreur est survenue - FCT : GetArticlesList - DETAIL : {e.Message}");
                return StatusCode(500, $@"Un problème interne est survenu");
            }
        }

        [HttpGet("{id}")]
        public IActionResult GetArticleById(string id)
        {
            try
            {
                var article = RepoW.Repo.Articles
                    .Include(a => a.ArticlesPictures)
                        .ThenInclude(a => a.AcPic)
                    .SingleOrDefault(a => a.AId.Equals(id));

                if (article != null)
                    return Ok(article);
                else
                {
                    Logger.LogDebug($@"The article was not found {id} - FCT : GetArticleById");
                    return BadRequest($@"The article with id : {id} was not found.");
                }
            }
            catch(Exception e)
            {
                Logger.LogError($@"Une erreur est survenue avec l'article : {id} - FCT : GetArticleById - DETAIL : {e.Message}");

                return StatusCode(500, $@"Un problème interne est survenu");
            }
        }
    }
}
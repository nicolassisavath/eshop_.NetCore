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
using Microsoft.Extensions.Logging;

namespace eshop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UsersController : ControllerBase
    {
        private IRepositoryWrapper RepoW;
        private IServicesWrapper SvcW;
        private ILoggerManager Logger;

        public UsersController( IRepositoryWrapper repo, IServicesWrapper svcW, ILoggerManager logger)
        {
            RepoW = repo;
            SvcW = svcW;
            Logger = logger;
        }

        [HttpPost("create")]
        [AllowAnonymous]
        public IActionResult CreateAccount([FromForm] Users newUser, ICollection<IFormFile> avatar)
        {
            if (!ModelState.IsValid)
                return BadRequest($@"Le modèle n'est pas valide");
            

            if (newUser.ULogin == null || newUser.UPassword == null)
                return BadRequest($@"Toutes les valeurs ne sont pas renseignées");

            // Check the unicity of the login
            if ( RepoW.UserRep.ExistUserWithLogin(newUser.ULogin) )
                return StatusCode(400, $@"Un utilisateur avec le login { newUser.ULogin } existe déjà");

            try
            {
                // Initialize and save the user
                newUser = SvcW.AuthentSvc.InitializeSaltAndPasswordUser(newUser);
                newUser = SvcW.UserSvc.InitNewUser(newUser);

                if (avatar.Count > 0)
                    newUser.UAvatar = SvcW.ImageSvc.ConvertFileToByteArrayImage(avatar.ElementAt(0));

                RepoW.UserRep.Create(newUser);
                RepoW.Save();

                newUser.UPassword = null;
                newUser.USalt = null;

                Logger.LogInfo($@"L'utilisateur {newUser.ULogin} a été crée avec succès");

                return Ok(newUser);
            }
            catch (Exception e)
            {
                Logger.LogError($@"La création de l'utilisateur a échoué {newUser.ULogin} - FCT : CreateAccount - DETAIL : {e.Message}");

                return StatusCode(500, $@"Une erreur est survenue");
            }
        }

        [HttpPost("login")]
        [AllowAnonymous]
        public IActionResult Login([FromBody] Users userCredentials)
        {
            if (userCredentials.ULogin == null || userCredentials.UPassword == null)
                return BadRequest("Des credentials sont manquants");

            try
            {
                Users authentifiedUser = SvcW.AuthentSvc.AuthentUser(userCredentials);

                if (authentifiedUser == null)
                {
                    Logger.LogWarn($@"Tentative de connection de {userCredentials.ULogin} a échoué");

                    return BadRequest("Le login ou mot de passe est erroné");
                }
                else
                {
                    var token = SvcW.TokenSvc.ProvideJWT(authentifiedUser);

                    Logger.LogInfo($@"L'utilisateur {authentifiedUser.ULogin} vient de se connecter");

                    return Ok(new { user = authentifiedUser, token = token });
                }
            }
            catch (Exception e)
            {
                Logger.LogError($@"La connection de l'utilisateur a échoué {userCredentials.ULogin} - FCT : Login - DETAIL : {e.Message}");

                return StatusCode(500, $@"Une erreur est survenue");
            }
        }

        [HttpGet]
        public IActionResult GetUsersList()
        {
            try
            {
                var users = RepoW.UserRep.GetAll()
                    .Select( u => new Users {
                        ULogin = u.ULogin ,
                        UAvatar = u.UAvatar,
                        UDatecreation = u.UDatecreation
                    });
                return Ok(users);
            }
            catch(Exception e)
            {
                Logger.LogError($@"La récupération des utilisateurs a échoué - FCT : GetUsersList - DETAIL : {e.Message}");

                return StatusCode(500, $@"Une erreur est survenue");
            }
        }

        [HttpGet("{id}")]
        public IActionResult GetUserByGuid(string id)
        {
            try
            {
                Users user = RepoW.UserRep.FindOneByCondition(u => u.UId == id);
                if (user == null)
                    return BadRequest($@"L'utilisateur n'a pas été trouvé");
                else
                { 
                    user.USalt = null;
                    user.UPassword = null;
                    return Ok(user);
                }
            }
            catch(Exception e)
            {
                Logger.LogError($@"La récupération de l'utilisateur {id} a échoué - FCT : GetUserByGuid - DETAIL : {e.Message}");

                return StatusCode(500, $@"Une erreur est survenue");
            }
        }

        [HttpDelete]
        public IActionResult DeleteUser(string id)
        {
            try
            {
                Users user = RepoW.UserRep.FindOneByCondition(u => u.UId.Equals(id));
                if (user == null)
                    return BadRequest($@"L'utilisateur n'a pas été trouvé");
                else
                {
                    Logger.LogInfo($@"L'utilisateur {user.UId} - {user.ULogin} a été supprimé");
                    RepoW.UserRep.Delete(user);
                    RepoW.Save();
                    return Ok($@"L'utilisateur a bien été supprimé");
                }
            }
            catch(Exception e)
            {
                Logger.LogError($@"La suppression de l'utilisateur {id} a échoué - FCT : DeleteUser - DETAIL : {e.Message}");

                return StatusCode(500, $@"Une erreur est survenue");
            }
        }

        [HttpPut]
        public IActionResult UpdateUser([FromBody] Users user, IFormFile avatar)
        {
            Users DbUser = RepoW.UserRep.GetUserBylogin(user.UId);
            if (DbUser == null)
                return NotFound($@"The user was not found");

            try
            {
                if (avatar != null)
                    user.UAvatar = SvcW.ImageSvc.ConvertFileToByteArrayImage(avatar);

                if (Users.Map(DbUser, user))
                    RepoW.UserRep.Update(user);
                RepoW.Save();
                return Ok(user);
            }
            catch(Exception e)
            {
                Logger.LogError($@"La modification de l'utilisateur {user.UId} a échoué - FCT : UpdateUser - DETAIL : {e.Message}");

                return StatusCode(500, $@"Une erreur est survenue");
            }
        }
    }
}
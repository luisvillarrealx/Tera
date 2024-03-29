﻿using Microsoft.AspNetCore.Mvc;
using System.Linq.Expressions;
using Tera_API.Entities;
using Tera_API.Models;
using static Tera_API.Models.Logs;

namespace Tera_API.Controllers
{
    /// <summary>
    /// Controlador para la gestión de usuarios.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : Controller
    {
        private readonly IConfiguration _configuration;
        private UserModel userModel = new UserModel();

        //lo usamos para el combobox
        private RoleModel roleModel = new RoleModel();
        private SiteModel siteModel = new SiteModel();


        /// <summary>
        /// Crea una nueva instancia del controlador UserController.
        /// </summary>
        /// <param name="configuration">La configuración de la aplicación.</param>
        public UserController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        /// <summary>
        /// Obtiene la lista de usuarios.
        /// </summary>
        /// <returns>Una lista de objetos UserObj que representan a los usuarios.</returns>
        [HttpGet]
        [Route("GetList")]
        public List<UserObj> List()
        {
            try
            {
                return userModel.ListUser(_configuration);
            }
            catch (Exception ex)
            {
                string methodName = nameof(List);
                string logFilePath = @"C:\Users\JUANK'S-PC\Desktop\Logs";
                string additionalParams = null;

                WriteLog.Log(methodName, ex.InnerException?.Message ?? ex.Message, logFilePath, additionalParams);
                // Puedes agregar aquí código adicional para manejar la excepción, si es necesario.
                // Por ejemplo, puedes lanzar una nueva excepción personalizada o devolver una lista vacía.
                throw; // Opcionalmente, puedes lanzar la excepción original.
            }
        }



        /// <summary>
        /// Obtiene un usuario por su ID.
        /// </summary>
        /// <param name="id">El ID del usuario.</param>
        /// <returns>Un objeto UserObj que representa al usuario encontrado.</returns>
        [HttpGet]
        [Route("GetUser/{id}")]
        public UserObj Get(int id)
        {
            return userModel.GetUser(_configuration, id);
        }

        /// <summary>
        /// Registra un nuevo usuario.
        /// </summary>
        /// <param name="userObj">El objeto UserObj que contiene los datos del usuario a registrar.</param>
        /// <returns>Un IActionResult que indica el resultado de la operación.</returns>
        [HttpPost]
        [Route("Register")]
        public IActionResult Register(UserObj userObj)
        {
            if (userModel.Register(userObj, _configuration) > 0)
            {
                return Ok();
            }
            return BadRequest();
        }

        [HttpGet]
        [Route("EmailExists")]
        public bool EmailExists(string userEmail)
        {
            UserModel userModel = new UserModel();
            return userModel.EmailExists(userEmail, _configuration);
        }

        /// <summary>
        /// Actualiza los datos de un usuario existente.
        /// </summary>
        /// <param name="userObj">El objeto UserObj que contiene los datos actualizados del usuario.</param>
        /// <returns>Un ActionResult que contiene el objeto UserObj actualizado o un error si la operación falla.</returns>
        [HttpPut]
        [Route("EditUser")]
        public ActionResult EditUser(UserObj userObj)
        {
            if (userModel.EditUser(userObj, _configuration) > 0)
            {
                return Ok(userObj);
            }
            return BadRequest();
        }

        [HttpPut]
        [Route("ResetPassword")]
        public ActionResult ResetPassword(UserObj userObj)
        {
            if (userModel.ResetPassword(userObj, _configuration) > 0)
            {
                return Ok(userObj);
            }
            return BadRequest();
        }

        [HttpPost]
        [Route("ResetPasswordSmtp")]
        public void ResetPasswordSmtp(UserObj userObj)
        {
            userModel.ResetPasswordSmtp(userObj, _configuration);
        }

        /// <summary>
        /// Elimina un usuario por su ID.
        /// </summary>
        /// <param name="userId">El ID del usuario a eliminar.</param>
        /// <returns>Un ActionResult que indica el resultado de la operación.</returns>
        //[HttpDelete]
        //[Route("DeleteUser")]
        //public ActionResult DeleteUser(int userId)
        //{
        //    try
        //    {
        //        if (userId == null)
        //        {
        //            NotFound();
        //        }
        //        else
        //        {
        //            var persona = userModel.DeleteUser(userId, _configuration);
        //        }
        //        return Ok();
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(500, ex.Message);
        //    }
        //}

        [HttpGet]
        [Route("ComboBoxConsultRoles")]
        public ActionResult<List<RoleObj>> ComboBoxConsultRoles()
        {
            return Ok(roleModel.ComboBoxRoles(_configuration));
        }

        [HttpGet]
        [Route("ComboBoxConsultSites")]
        public ActionResult<List<SiteObj>> ComboBoxConsultSites()
        {
            return Ok(siteModel.ComboBoxSites(_configuration));
        }

    }
}
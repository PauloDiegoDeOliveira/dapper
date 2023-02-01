﻿using Microsoft.AspNetCore.Mvc;

namespace Empresa.Dapper.API.V1.Controllers
{
    [ApiVersion("1.0")]
    [Route("/v{version:apiVersion}/versao")]
    [ApiController]
    public class VersaoController : ControllerBase
    {
        /// <summary>
        /// Informa a versão da API.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public string Valor()
        {
            return "Esta é a versão V1.";
        }
    }
}
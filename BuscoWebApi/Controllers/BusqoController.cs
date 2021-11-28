using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using Busqo.Base;
using CotizacionService.Models;
using Microsoft.AspNetCore.Mvc;

namespace BuscoWebApi.Controllers
{
   /// <summary>
   /// RecaudosController
   /// </summary>
   [Route("api/[controller]")]
   [ApiController]
   public class BusqoController : ControllerBase
   {
      private readonly ServiceBase crearCotizacion;
      private readonly ServiceBase consutarCotizaciones;

      /// <summary>
      /// Constructor
      /// </summary>
      public BusqoController(IEnumerable<ServiceBase> services)
      {
         this.crearCotizacion = services.FirstOrDefault(x => x.ServiceName() == "CrearCotizacion");
         this.consutarCotizaciones = services.FirstOrDefault(x => x.ServiceName() == "ConsultarCotizaciones");
      }

      [HttpGet]
      [Route("ConsultarCotizaciones")]
      [Consumes(MediaTypeNames.Application.Json)]
      [Produces(MediaTypeNames.Application.Json)]
      // [ProducesResponseType(typeof(ConteoVehiculosResponse), StatusCodes.Status200OK)]
      public IActionResult ConsultarCotizaciones()
      {
         var response = this.consutarCotizaciones.Execute();
         return Ok(response);
      }

      [HttpPost]
      [Route("CrearCotizacion")]
      [Consumes(MediaTypeNames.Application.Json)]
      [Produces(MediaTypeNames.Application.Json)]
      public IActionResult CrearCotizacion([FromBody] CrearCotizacionRequest request)
      {
         var response = this.crearCotizacion.Execute(request);
         return Ok(response);
      }
   }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using Busqo.Base;
using CotizacionService.Models;
using Microsoft.AspNetCore.Http;
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
      private readonly ServiceBase consultarProductos;
      private readonly ServiceBase consultarProveedores;


      /// <summary>
      /// Constructor
      /// </summary>
      public BusqoController(IEnumerable<ServiceBase> services)
      {
         this.crearCotizacion = services.FirstOrDefault(x => x.ServiceName() == "CrearCotizacion");
         this.consutarCotizaciones = services.FirstOrDefault(x => x.ServiceName() == "ConsultarCotizaciones");
         this.consultarProductos = services.FirstOrDefault(x => x.ServiceName() == "ConsultarProductos");
         this.consultarProveedores = services.FirstOrDefault(x => x.ServiceName() == "ConsultarProveedores");
      }

      [HttpGet]
      [Route("ConsultarCotizaciones")]
      [Consumes(MediaTypeNames.Application.Json)]
      [Produces(MediaTypeNames.Application.Json)]
      [ProducesResponseType(typeof(ResponseBase), StatusCodes.Status200OK)]
      // [ProducesResponseType(typeof(ConteoVehiculosResponse), StatusCodes.Status200OK)]
      public IActionResult ConsultarCotizaciones()
      {
         try
         {
            ResponseBase response = this.consutarCotizaciones.Execute();
            if (response.ReturnCode == ReturnCodeList.SUCCESS)
            {
               return Ok(response);
            }
            else
            {
               return BadRequest(response);
            }
         }
         catch (System.Exception ex)
         {
            Console.WriteLine(ex.Message);
            return BadRequest(new ResponseBase
            {
               ReturnCode = ReturnCodeList.INTERNAL_ERROR,
               Message = ex.Message
            });
         }
      }

      [HttpGet]
      [Route("ConsultarProductos")]
      [Consumes(MediaTypeNames.Application.Json)]
      [Produces(MediaTypeNames.Application.Json)]
      [ProducesResponseType(typeof(ResponseBase), StatusCodes.Status200OK)]
      public IActionResult ConsultarProductos()
      {
         try
         {
            ResponseBase response = this.consultarProductos.Execute();
            if (response.ReturnCode == ReturnCodeList.SUCCESS)
            {
               return Ok(response);
            }
            else
            {
               return BadRequest(response);
            }
         }
         catch (System.Exception ex)
         {
            Console.WriteLine(ex.Message);
            return BadRequest(new ResponseBase
            {
               ReturnCode = ReturnCodeList.INTERNAL_ERROR,
               Message = ex.Message
            });
         }
      }

      [HttpGet]
      [Route("ConsultarProveedores")]
      [Consumes(MediaTypeNames.Application.Json)]
      [Produces(MediaTypeNames.Application.Json)]
      [ProducesResponseType(typeof(ResponseBase), StatusCodes.Status200OK)]
      public IActionResult ConsultarProveedores()
      {
         try
         {
            ResponseBase response = this.consultarProveedores.Execute();
            if (response.ReturnCode == ReturnCodeList.SUCCESS)
            {
               return Ok(response);
            }
            else
            {
               return BadRequest(response);
            }
         }
         catch (System.Exception ex)
         {
            Console.WriteLine(ex.Message);
            return BadRequest(new ResponseBase
            {
               ReturnCode = ReturnCodeList.INTERNAL_ERROR,
               Message = ex.Message
            });
         }
      }

      [HttpPost]
      [Route("CrearCotizacion")]
      [Consumes(MediaTypeNames.Application.Json)]
      [Produces(MediaTypeNames.Application.Json)]
      [ProducesResponseType(typeof(ResponseBase), StatusCodes.Status200OK)]
      public IActionResult CrearCotizacion([FromBody] CrearCotizacionRequest request)
      {
         try
         {
            if (request != null)
            {
               ResponseBase response = this.crearCotizacion.Execute(request);
               if (response.ReturnCode == ReturnCodeList.SUCCESS)
               {
                  return Ok(response);
               }
               else
               {
                  return BadRequest(response);
               }

            }
            else
            {
               throw new Exception("No se recibieron datos");
            }
         }
         catch (System.Exception ex)
         {
            Console.WriteLine(ex.Message);
            return BadRequest(new ResponseBase
            {
               ReturnCode = ReturnCodeList.INTERNAL_ERROR,
               Message = ex.Message
            });
         }
      }
   }
}
using System.Net;
using Busqo.Base;
using CotizacionService.Commands;
using CotizacionService.Models;

namespace CotizacionService.Services
{
   public class CrearCotizacion : ServiceBase
   {
      public override ResponseBase Execute()
      {
         return null;
      }

      public override ResponseBase Execute(RequestBase request)
      {
         CrearCotizacionRequest crearCotizacionRequest = request as CrearCotizacionRequest;

         try
         {
            //Verificacion si el cliente ya se encuentra registrado en la bd
            //Si no se encuentra registrado, se registra
            using (ClienteGetByCedulaCommand command = new ClienteGetByCedulaCommand(crearCotizacionRequest.Cliente.Cedula))
            {
               HttpStatusCode httpStatusCode = command.Execute();
               if (httpStatusCode == HttpStatusCode.OK)
               {
                  crearCotizacionRequest.Cliente = command.Cliente;
               }
               else if (httpStatusCode == HttpStatusCode.NotFound)
               {
                  using (ClientesAddCommand commandCliente = new ClientesAddCommand(crearCotizacionRequest.Cliente))
                  {
                     HttpStatusCode httpStatusCodeCliente = commandCliente.Execute();
                     if (httpStatusCodeCliente == HttpStatusCode.Created)
                     {
                        crearCotizacionRequest.Cliente.Id = commandCliente.IdCliente;
                     }
                     else
                     {
                        return commandCliente.Response;
                     }
                  }
               }
               else
               {
                  return command.ResponseBase;
               }
            }

            //Se crea la cotizacion
            using (CotizacionAddCommand commandCotizacion = new CotizacionAddCommand(crearCotizacionRequest))
            {
               HttpStatusCode httpStatusCodeCotizacion = commandCotizacion.Execute();
               if (httpStatusCodeCotizacion == HttpStatusCode.Created)
               {
                  return new ResponseBase
                  {
                     ReturnCode = ReturnCodeList.SUCCESS,
                     Message = "Cotizacion creada con exito"
                  };
               }
               else
               {
                  return commandCotizacion.Response;
               }
            }
         }
         catch (System.Exception ex)
         {
            return new ResponseBase
            {
               ReturnCode = ReturnCodeList.INTERNAL_ERROR,
               Message = ex.Message
            };
         }
      }
   }
}
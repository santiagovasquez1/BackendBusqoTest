using System;
using System.Net;
using Busqo.Base;
using CotizacionService.Commands;
using CotizacionService.Models;

namespace CotizacionService.Services
{
   public class ConsultarCotizaciones : ServiceBase
   {
      public override ResponseBase Execute()
      {
         try
         {
            using (CotizacionesGetCommand command = new CotizacionesGetCommand())
            {
               HttpStatusCode statusCode = command.Execute();
               if (statusCode == HttpStatusCode.OK)
               {
                  return new CotizacionesResponse
                  {
                     ReturnCode = ReturnCodeList.SUCCESS,
                     Message = "Cotizaciones obtenidas con exito",
                     Cotizaciones = command.Cotizaciones
                  };
               }
               else
               {
                  throw new Exception("No se encontraron cotizaciones");
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

      public override ResponseBase Execute(RequestBase request)
      {
         return null;
      }
   }
}
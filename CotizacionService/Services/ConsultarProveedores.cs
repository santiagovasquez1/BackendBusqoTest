using System;
using System.Net;
using Busqo.Base;
using CotizacionService.Commands;
using CotizacionService.Models;

namespace CotizacionService.Services
{
   public class ConsultarProveedores : ServiceBase
   {
      public override ResponseBase Execute()
      {
         try
         {
            using (ProvedoresGetCommand command = new ProvedoresGetCommand())
            {
               HttpStatusCode statusCode = command.Execute();
               if (statusCode == HttpStatusCode.OK)
               {
                  return new ProvedorResponse
                  {
                     ReturnCode = ReturnCodeList.SUCCESS,
                     Message = "Consulta de productos exitosa",
                     Proveedores = command.Provedores
                  };
               }
               else
               {
                  throw new Exception("No se encontraron productos");
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
         return this.Execute();
      }
   }
}
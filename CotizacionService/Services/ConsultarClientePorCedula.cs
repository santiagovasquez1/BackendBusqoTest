using System;
using System.Net;
using Busqo.Base;
using CotizacionService.Commands;
using CotizacionService.Models;

namespace CotizacionService.Services
{
   public class ConsultarClientePorCedula : ServiceBase
   {
      public override ResponseBase Execute()
      {
         throw new System.NotImplementedException();
      }

      public override ResponseBase Execute(RequestBase request)
      {
         try
         {
            using (ClienteGetByCedulaCommand command = new ClienteGetByCedulaCommand(request.Id))
            {
               HttpStatusCode httpStatusCode = command.Execute();
               if (httpStatusCode == HttpStatusCode.OK)
               {
                  return new ClienteResponse
                  {
                     Cliente = command.Cliente,
                     Message = "Cliente encontrado",
                     ReturnCode = ReturnCodeList.SUCCESS
                  };
               }
               else
               {
                  throw new Exception("No se encontraron clientes");
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
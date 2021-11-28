using System;
using System.Net;
using Busqo.Base;
using CotizacionService.Commands;
using CotizacionService.Models;

namespace CotizacionService.Services
{
   public class ConsultarProductos : ServiceBase
   {
      public override ResponseBase Execute()
      {
         try
         {
            using (ProductosGetCommand command = new ProductosGetCommand())
            {
               HttpStatusCode statusCode = command.Execute();
               if (statusCode == HttpStatusCode.OK)
               {
                  return new ProductoResponse
                  {
                     ReturnCode = ReturnCodeList.SUCCESS,
                     Message = "Consulta de productos exitosa",
                     Productos = command.Productos
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
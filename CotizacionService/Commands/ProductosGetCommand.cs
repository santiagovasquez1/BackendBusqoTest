using System.Collections.Generic;
using System;
using System.Net;
using Busqo.Base;
using CotizacionService.Models;
using System.Data.Common;
using Newtonsoft.Json;

namespace CotizacionService.Commands
{
   public class ProductosGetCommand : BusqoCommandBase
   {
      private static String fGetProductos = "SELECT * FROM BUSCOTEST.PRODUCTOS";
      public ResponseBase Response { get; set; }
      public List<ProductoInfo> Productos { get; set; }
      public override HttpStatusCode Execute()
      {
         try
         {
            using (DbConnection connection = this.RDSProvider.CreateConnection())
            {
               connection.ConnectionString = this.ConnectionString;
               connection.Open();
               using (DbCommand command = connection.CreateCommand())
               {
                  command.CommandText = fGetProductos;
                  using (DbDataReader reader = command.ExecuteReader())
                  {
                     if (reader.HasRows)
                     {
                        this.Productos = JsonConvert.DeserializeObject<List<ProductoInfo>>(this.ToJson(reader));
                        return HttpStatusCode.OK;
                     }
                     else
                     {
                        this.Response = new ResponseBase
                        {
                           ReturnCode = ReturnCodeList.NOT_FOUND,
                           Message = "No se encontraron productos"
                        };
                        return HttpStatusCode.NotFound;
                     }
                  }
               }
            }
         }
         catch (System.Exception ex)
         {
            Console.WriteLine("Excepción no controlada al consultar las transacciones x " + ex);
            this.Response = new ResponseBase
            {
               Message = "Excepción no controlada al consultar las transacciones x " + ex,
               ReturnCode = ReturnCodeList.INTERNAL_ERROR
            };
            return HttpStatusCode.InternalServerError;
         }
      }
   }
}
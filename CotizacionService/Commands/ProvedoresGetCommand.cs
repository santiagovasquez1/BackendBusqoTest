using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Net;
using Busqo.Base;
using CotizacionService.Models;
using Newtonsoft.Json;

namespace CotizacionService.Commands
{
   public class ProvedoresGetCommand : BusqoCommandBase
   {
      private static String fGetProveedores = "SELECT * FROM BUSCOTEST.PROVEEDORES";
      public ResponseBase Response { get; set; }
      public List<ProvedorInfo> Provedores { get; set; }

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
                  command.CommandText = fGetProveedores;
                  using (DbDataReader reader = command.ExecuteReader())
                  {
                     if (reader.HasRows)
                     {
                        this.Provedores = JsonConvert.DeserializeObject<List<ProvedorInfo>>(this.ToJson(reader));
                        return HttpStatusCode.OK;
                     }
                     else
                     {
                        this.Response = new ResponseBase
                        {
                           ReturnCode = ReturnCodeList.NOT_FOUND,
                           Message = "No se encontraron provedores"
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
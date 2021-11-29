using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Net;
using Busqo.Base;
using CotizacionService.Models;
using Newtonsoft.Json;

namespace CotizacionService.Commands
{
   public class CotizacionesGetCommand : BusqoCommandBase
   {
      private static String fGetCotizaciones = "SELECT "
      + "C.ID id, "
      + "CL.NOMBRE nombre, "
      + "CL.APELLIDO apellido, "
      + "CL.CEDULA cedula, "
      + "CL.EMAIL EMAIL, "
      + "CL.CELULAR celular, "
      + "CL.PLACA placa, "
      + "P.NOMBRE producto, "
      + "PR.NOMBRE proveedor, "
      + "C.VALOR_COTIZACION valorCotizacion "
      + "FROM BUSCOTEST.COTIZACIONES AS C "
      + "INNER JOIN BUSCOTEST.CLIENTES AS CL ON C.CLIENTE_ID = CL.ID "
      + "INNER JOIN BUSCOTEST.PRODUCTOS AS P ON C.PRODUCTO_ID = P.ID "
      + "INNER JOIN BUSCOTEST.PROVEEDORES AS PR ON C.PROVEEDOR_ID = PR.ID "
      + "ORDER BY C.ID;";

      public CotizacionesGetCommand()
      {

      }
      public ResponseBase Response { get; set; }
      public List<CotizacionInfo> Cotizaciones { get; set; }

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
                  command.CommandText = fGetCotizaciones;
                  using (DbDataReader reader = command.ExecuteReader())
                  {
                     if (reader.HasRows)
                     {
                        var response = this.ToJson(reader);
                        this.Cotizaciones = JsonConvert.DeserializeObject<List<CotizacionInfo>>(response);
                        return HttpStatusCode.OK;
                     }
                     else
                     {
                        this.Response = new ResponseBase
                        {
                           ReturnCode = ReturnCodeList.NOT_FOUND,
                           Message = "No se encontraron registros"
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
               ReturnCode = ReturnCodeList.INTERNAL_ERROR,
               Message = "Excepción no controlada al consultar las transacciones x " + ex
            };
            return HttpStatusCode.InternalServerError;
         }
      }
   }

}
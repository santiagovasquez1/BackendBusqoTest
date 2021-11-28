using System.Data.Common;
using System;
using System.Net;
using Busqo.Base;
using CotizacionService.Models;

namespace CotizacionService.Commands
{
   public class CotizacionAddCommand : BusqoCommandBase
   {
      public static String ParClienteId = "ClienteId";
      public static String ParProductoId = "ProductoId";
      public static String ParProveedorId = "ProveedorId";
      public static String ParValorCotizacion = "ValorCotizacion";
      public static String fAddCotizacion = "INSERT INTO BUSCOTEST.COTIZACIONES"
      + " (CLIENTE_ID, PRODUCTO_ID, PROVEEDOR_ID, VALOR_COTIZACION)"
      + " VALUES ("
      + " @" + ParClienteId + ", "
      + " @" + ParProductoId + ", "
      + " @" + ParProveedorId + ", "
      + " @" + ParValorCotizacion + ")";

      public CrearCotizacionRequest Request { get; set; }
      public ResponseBase Response { get; set; }
      public CotizacionAddCommand(CrearCotizacionRequest request)
      {
         this.Request = request;
      }

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
                  command.CommandText = fAddCotizacion;
                  this.AddParameterWithValue(command, ParClienteId, this.Request.Cliente.Id);
                  this.AddParameterWithValue(command, ParProductoId, this.Request.ProductoId);
                  this.AddParameterWithValue(command, ParProveedorId, this.Request.ProveedorId);
                  this.AddParameterWithValue(command, ParValorCotizacion, this.Request.valorCotizacion);
                  command.Prepare();
                  command.ExecuteNonQuery();
                  return HttpStatusCode.Created;
               }
            }
         }
         catch (System.Exception ex)
         {
            Console.WriteLine(ex.Message);
            this.Response = new ResponseBase
            {
               ReturnCode = ReturnCodeList.INTERNAL_ERROR,
               Message = ex.Message
            };
            return HttpStatusCode.InternalServerError;
         }
      }
   }
}
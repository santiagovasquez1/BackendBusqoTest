using System.Data.Common;
using System;
using System.Net;
using Busqo.Base;
using CotizacionService.Models;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;

namespace CotizacionService.Commands
{
   public class ClienteGetByCedulaCommand : BusqoCommandBase
   {
      public ClienteGetByCedulaCommand(Int64 cedula, Cliente cliente, ResponseBase responseBase)
      {
         this.Cedula = cedula;
         this.Cliente = cliente;
         this.ResponseBase = responseBase;

      }
      public Int64 Cedula { get; set; }
      public Cliente Cliente { get; set; }
      public ResponseBase ResponseBase { get; set; }
      public static String ParCedula = "cedula";

      public static String fGetCliente = "SELECT ID, NOMBRE, APELLIDO, CEDULA, EMAIL, CELULAR, PLACA"
      + " FROM BUSCOTEST.CLIENTES"
      + " WHERE CEDULA = @" + ParCedula;

      public ClienteGetByCedulaCommand(Int64 cedula)
      {
         this.Cedula = cedula;
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
                  command.CommandText = fGetCliente;
                  this.AddParameterWithValue(command, ParCedula, this.Cedula);

                  using (DbDataReader reader = command.ExecuteReader())
                  {
                     if (reader.HasRows)
                     {
                        this.Cliente = JsonConvert.DeserializeObject<List<Cliente>>(this.ToJson(reader)).FirstOrDefault();
                        return HttpStatusCode.OK;
                     }
                     else
                     {
                        this.ResponseBase = new ResponseBase
                        {
                           ReturnCode = ReturnCodeList.NOT_FOUND,
                           Message = "No se encontró el cliente"
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
            this.ResponseBase = new ResponseBase
            {
               Message = "Excepción no controlada al consultar las transacciones x " + ex,
               ReturnCode = ReturnCodeList.INTERNAL_ERROR
            };
            return HttpStatusCode.InternalServerError;
         }
      }
   }
}
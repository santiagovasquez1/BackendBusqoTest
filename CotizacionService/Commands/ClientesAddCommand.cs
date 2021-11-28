using System.Linq;
using System.Data.Common;
using System;
using System.Net;
using Busqo.Base;
using CotizacionService.Models;
using System.Text;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace CotizacionService.Commands
{
   public class ClientesAddCommand : BusqoCommandBase
   {
      private static String ParNombre = "Nombre";
      private static String ParApellido = "Apellido";
      private static String ParCedula = "Cedula";
      private static String ParEmail = "Email";
      private static String ParCelular = "Celular";
      private static String ParPlaca = "Placa";

      private static String fAddCliente = "INSERT INTO BUSCOTEST.CLIENTES"
      + "(NOMBRE, APELLIDO, CEDULA, EMAIL, CELULAR, PLACA)"
      + "VALUES( "
      + "@" + ParNombre + ", "
      + "@" + ParApellido + ", "
      + "@" + ParCedula + ", "
      + "@" + ParEmail + ", "
      + "@" + ParCelular + ", "
      + "@" + ParPlaca + "); Select LAST_INSERT_ID();";

      private String fGetLastId = "SELECT ID FROM BUCOTEST.CLIENTES"
      + "WHERE"
      + " WHERE CEDULA = @" + ParCedula;

      private Cliente Cliente { get; set; }
      public ResponseBase Response { get; set; }
      public Int64 IdCliente { get; set; }
      public ClientesAddCommand(Cliente cliente)
      {
         this.Cliente = cliente;
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
                  command.CommandText = new StringBuilder(fAddCliente).ToString();

                  this.AddParameterWithValue(command, ParNombre, Cliente.Nombre);
                  this.AddParameterWithValue(command, ParApellido, Cliente.Apellido);
                  this.AddParameterWithValue(command, ParCedula, Cliente.Cedula);
                  this.AddParameterWithValue(command, ParEmail, Cliente.Email);
                  this.AddParameterWithValue(command, ParCelular, Cliente.Celular);
                  this.AddParameterWithValue(command, ParPlaca, Cliente.Placa);

                  this.IdCliente = Convert.ToInt64(command.ExecuteScalar());
                  return HttpStatusCode.Created;
               }
            }
         }
         catch (System.Exception ex)
         {
            Console.WriteLine("Excepción no controlada al consultar las transacciones x " + ex);
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
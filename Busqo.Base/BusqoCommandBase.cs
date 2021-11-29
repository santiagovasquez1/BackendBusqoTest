using System;
using System.Data.Common;
using System.IO;
using System.Net;
using System.Text;
using Newtonsoft.Json;

namespace Busqo.Base
{
   public abstract class BusqoCommandBase : IDisposable
   {
      private DbProviderFactory fRDSProvider;
      /// <summary>
      /// Proveedor de base de datos para instanciar las conexiones y los comandos a RDS
      /// </summary>
      protected DbProviderFactory RDSProvider
      {
         get { return this.fRDSProvider; }
         set { this.fRDSProvider = value; }
      }
      private DbConnection fRDSConnection;
      public DbConnection RDSConnection
      {
         get { return fRDSConnection; }
         set { fRDSConnection = value; }
      }

      private String fConnectionString;
      /// <summary>
      /// Cadena de conexion a BD
      /// </summary>
      protected String ConnectionString
      {
         get
         {
            return this.fConnectionString;
         }
         set
         {
            this.fConnectionString = value;
         }
      }

      public BusqoCommandBase()
      {
         DbProviderFactory providerFactory = DbProviderFactories.GetFactory("MySql.Data.MySqlClient");
         if (providerFactory != null)
         {
            this.RDSProvider = providerFactory;
         }
         this.fConnectionString = DatabaseOptions.ConnectionString;
      }

      /// <summary>
      /// Libera los recursos utilizados por el objeto
      /// </summary>
      public void Dispose()
      {
         this.Dispose(true);
      }

      /// <summary>
      /// Se encarga de hacer el dispose de la conexión de instancia RDSConnection y
      /// dejarla en null si disposing está en true.
      /// </summary>
      /// <param name="disposing"></param>
      protected void Dispose(Boolean disposing)
      {
         if (disposing && this.RDSConnection != null)
         {
            this.RDSConnection.Dispose();
            this.RDSConnection = null;
         }
      }

      /// <summary>
      /// Premite agregar un parametro con su nomnbre y valor
      ///<param name="command"></param>
      ///<param name="parameterName"></param>
      ///<param name="value"></param>
      /// </summary>
      public DbParameter AddParameterWithValue(DbCommand command, String parameterName, Object value)
      {
         DbParameter parameter = command.CreateParameter();
         parameter.ParameterName = parameterName;
         parameter.Value = value;
         command.Parameters.Add(parameter);
         return parameter;
      }

      /// <summary>
      /// Permite convertir el DbDataReader en JSON
      /// </summary>
      public String ToJson(DbDataReader reader)
      {
         StringBuilder stringBuilder = new StringBuilder();
         StringWriter stringWriter = new StringWriter(stringBuilder);

         using (JsonWriter jsonWriter = new JsonTextWriter(stringWriter))
         {
            jsonWriter.WriteStartArray();

            while (reader.Read())
            {
               jsonWriter.WriteStartObject();

               for (int i = 0; i < reader.FieldCount; i++)
               {
                  jsonWriter.WritePropertyName(reader.GetName(i));
                  jsonWriter.WriteValue(reader.GetValue(i));
               }
               jsonWriter.WriteEndObject();
            }

            jsonWriter.WriteEndArray();
            return stringWriter.ToString();
         }
      }

      /// <summary>
      /// Metodo encargado de ejecutar un comando en la base de datos
      /// </summary>
      public abstract HttpStatusCode Execute();

   }
}
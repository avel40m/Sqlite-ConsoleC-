using System;
using Microsoft.Data.Sqlite;

namespace APP_Sqlite
{
    class Program
    {
        static void Main(string[] args)
        {
            //Crear una conexion con sqlite que cree un archivo inter .db
            var ConnectionStringBuilder = new SqliteConnectionStringBuilder();
            ConnectionStringBuilder.DataSource = "./Empleados.db";
            
            using(var connection = new SqliteConnection(ConnectionStringBuilder.ConnectionString))
            {
                connection.Open();
                //Creo la tabla
                var TablaCmd = connection.CreateCommand();
                TablaCmd.CommandText = "CREATE TABLE Empleado(Nombre VARCHAR(50),Apellido VARCHAR(50), Edad INT,Puesto VARCHAR(50));";
                TablaCmd.ExecuteNonQuery();

                //INSERTAR DATOS EN LA TABLA
                using(var transaction = connection.BeginTransaction())
                {
                    var InsertarCmd = connection.CreateCommand();
                    InsertarCmd.CommandText = "INSERT INTO Empleado VALUES('Avelino','Mendez',28,'Desarrollador Web')";
                    InsertarCmd.ExecuteNonQuery();

                    InsertarCmd.CommandText = "INSERT INTO Empleado VALUES('Geronimo','Mendez',28,'Desarrollador Grafico')";
                    InsertarCmd.ExecuteNonQuery();

                    InsertarCmd.CommandText = "INSERT INTO Empleado VALUES('Graciela','Fernandez',28,'Jefa de Produccion')";
                    InsertarCmd.ExecuteNonQuery();

                    transaction.Commit();
                }
                //LEER DATOS

                var seleccionarCmd = connection.CreateCommand();
                seleccionarCmd.CommandText = "SELECT * FROM Empleado where Nombre = 'Avelino'";
                using(var reader = seleccionarCmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var Resultado = reader.GetString(3);
                        Console.WriteLine(Resultado);
                    }
                }

            }

        }
    }
}

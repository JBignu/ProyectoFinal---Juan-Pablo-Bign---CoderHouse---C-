using System.Data.SqlClient;
using System.Data; 

namespace ProyectoFinal
{

    public static class ProductoHandler 
    {

        static string connectionString = "Server=localhost;Database=SistemaGestion;Trusted_Connection=True";

        private static Producto IniciarProductosBaseDatos(SqlDataReader reader)
        {
            Producto NuevopProducto = new Producto();
            NuevopProducto.id = Convert.ToInt32(reader["id"]);
            NuevopProducto.descripcion = (string)reader["descripciones"];
            NuevopProducto.costo = Convert.ToInt32(reader["costo"]);
            NuevopProducto.precioVenta = Convert.ToInt32(reader["precioVenta"]);
            NuevopProducto.stock = Convert.ToInt32(reader["stock"]);
            NuevopProducto.idUsuario = Convert.ToInt32(reader["idUsuario"]);

            return NuevopProducto;

        }

        public static List<Producto> ObtenerProductos()
        {
            List<Producto> productosBaseDatos = new List<Producto>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = " SELECT * FROM [SistemaGestion].[dbo].[Producto] ";

                SqlCommand command = new SqlCommand(query, connection);

                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Producto producto = IniciarProductosBaseDatos(reader);

                    productosBaseDatos.Add(producto);
                }

                reader.Close();

            }
            return productosBaseDatos;
        }

        public static Producto ConsultarStock(Producto producto)
        {
            
            if (producto.id <= 0)
            {
                return producto;
            }

            using (SqlConnection sqlConnection = new SqlConnection(connectionString)) 
            {
                const string querySelect = "SELECT * FROM [SistemaGestion].[dbo].[Producto] WHERE Id = @id"; 

                using (SqlCommand sqlCommand = new SqlCommand(querySelect, sqlConnection)) 
                {
                    var sqlParameter = new SqlParameter();      
                    sqlParameter.ParameterName = "id";         
                    sqlParameter.SqlDbType = SqlDbType.BigInt;  
                    sqlParameter.Value = producto.id;            
                    sqlCommand.Parameters.Add(sqlParameter);    

                    sqlConnection.Open(); 

                    using (SqlDataReader sqlDataReader = sqlCommand.ExecuteReader()) 
                    {
                        if (sqlDataReader.HasRows & sqlDataReader.Read()) 
                        {
                            producto.stock = Convert.ToInt32(sqlDataReader["Stock"]); 
                        }
                    }
                    sqlConnection.Close(); 
                }
            }
            return producto; 
        }

        public static bool ModificarStock(Producto producto)
        {
            if (producto.id <= 0)
            {
                return false;
            }

            bool resultado = false; 
            int rowsAffected = 0;  

            using (SqlConnection sqlConnection = new SqlConnection(connectionString)) 
            {
                string queryUpdate = "UPDATE [SistemaGestion].[dbo].[Producto] " + 
                                        "SET Stock = @stock " +
                                        "WHERE Id = @id";

                var parameterId = new SqlParameter("id", SqlDbType.BigInt); 
                parameterId.Value = producto.id;                            

                var parameterStock = new SqlParameter("stock", SqlDbType.Int);
                parameterStock.Value = producto.stock;

                sqlConnection.Open(); 

                using (SqlCommand sqlCommand = new SqlCommand(queryUpdate, sqlConnection)) 
                {
                    sqlCommand.Parameters.Add(parameterId); 
                    sqlCommand.Parameters.Add(parameterStock);
                    rowsAffected = sqlCommand.ExecuteNonQuery(); 
                }
                sqlConnection.Close(); 
            }
            if (rowsAffected == 1) 
            {
                resultado = true; 
            }
            return resultado;
        }

        public static bool ModificarProducto(Producto producto)
        {
            bool resultado = false;
            int rowsAffected = 0;

            if (producto.id <= 0)
            {
                return false;
            }

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "UPDATE [SistemaGestion].[dbo].[Producto] " + "SET Descripciones = @descripciones, " +
                               "Costo = @costo, " +
                               "PrecioVenta = @precioVenta, " +
                               "stock = @Stock, " +
                               "IdUsuario = @idUsuario " +
                               "WHERE Id = @id";

                var parameterDescripciones = new SqlParameter();
                parameterDescripciones.ParameterName = "descripciones";
                parameterDescripciones.SqlDbType = SqlDbType.VarChar;
                parameterDescripciones.Value = producto.descripcion;

                var parameterCosto = new SqlParameter();
                parameterCosto.ParameterName = "costo";
                parameterCosto.SqlDbType = SqlDbType.Money;
                parameterCosto.Value = producto.costo;

                var parameterPrecioVenta = new SqlParameter();
                parameterPrecioVenta.ParameterName = "PrecioVenta";
                parameterPrecioVenta.SqlDbType = SqlDbType.Money;
                parameterPrecioVenta.Value = producto.precioVenta;

                var parameterStock = new SqlParameter();
                parameterStock.ParameterName = "stock";
                parameterStock.SqlDbType = SqlDbType.Int;
                parameterStock.Value = producto.stock;

                var parameterIdUsuario = new SqlParameter();
                parameterIdUsuario.ParameterName = "idUsuario";
                parameterIdUsuario.SqlDbType = SqlDbType.BigInt;
                parameterIdUsuario.Value = producto.idUsuario;

                connection.Open();

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.Add(parameterDescripciones);
                    command.Parameters.Add(parameterCosto);
                    command.Parameters.Add(parameterPrecioVenta);
                    command.Parameters.Add(parameterStock);
                    command.Parameters.Add(parameterIdUsuario);
                    rowsAffected = command.ExecuteNonQuery();
                }

                connection.Close();
            }
            if (rowsAffected == 0)
            {
                resultado = true;
            }
            return resultado;
        }

        public static bool CrearProducto(Producto producto)
        {
            bool resultado = false;
            long idProducto = 0;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "INSERT INTO [SistemaGestion].[dbo].[Producto] (Descripciones, Costo, PrecioVenta, Stock, IdUsuario)" +
                                "VALUES (@descripciones, @costo, @precioVenta, @stock, @idUsuario) " +
                                "SELECT @@IDENTITY";

                var parameterDescripciones = new SqlParameter();
                parameterDescripciones.ParameterName = "descipciones";
                parameterDescripciones.SqlDbType = SqlDbType.VarChar;
                parameterDescripciones.Value = producto.descripcion;

                var parameterCosto = new SqlParameter();
                parameterCosto.ParameterName = "costo";
                parameterCosto.SqlDbType = SqlDbType.Money;
                parameterCosto.Value = producto.costo;

                var parameterPrecioVenta = new SqlParameter();
                parameterPrecioVenta.ParameterName = "PrecioVenta";
                parameterPrecioVenta.SqlDbType = SqlDbType.Money;
                parameterPrecioVenta.Value = producto.precioVenta;

                var parameterStock = new SqlParameter();
                parameterStock.ParameterName = "stock";
                parameterStock.SqlDbType = SqlDbType.Int;
                parameterStock.Value = producto.stock;

                var parameterIdUsuario = new SqlParameter();
                parameterIdUsuario.ParameterName = "idUsuario";
                parameterIdUsuario.SqlDbType = SqlDbType.BigInt;
                parameterIdUsuario.Value = producto.idUsuario;

                connection.Open();

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.Add(parameterDescripciones);
                    command.Parameters.Add(parameterCosto);
                    command.Parameters.Add(parameterPrecioVenta);
                    command.Parameters.Add(parameterStock);
                    command.Parameters.Add(parameterIdUsuario);
                    idProducto = Convert.ToInt64(command.ExecuteScalar());
                }

                connection.Close();
            }
            if (idProducto != 0)
            {
                resultado = true;
            }
            return resultado;
        }

        public static bool EliminarProducto(long id)
        {
            bool resultado = false;
            int rowsAffected = 0;

            if (id <= 0)
            {
                return false;
            }

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "DELETE FROM [SistemaGestion].[dbo].[Producto] " +
                                "WHERE Id = @id";

                var parameterId = new SqlParameter();
                parameterId.ParameterName = "id";
                parameterId.SqlDbType = SqlDbType.BigInt;
                parameterId.Value = id;

                connection.Open();

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.Add(parameterId);
                    rowsAffected = command.ExecuteNonQuery();
                }

                connection.Close();
            }
            if (rowsAffected == 1)
            {
                resultado = true;
            }
            return resultado;
        }

    }
}

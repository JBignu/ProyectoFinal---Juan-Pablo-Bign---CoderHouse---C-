using System.Data.SqlClient; 
using System.Data;

namespace ProyectoFinal
{
    
    public static class ProductoVendidoHandler
    {

        static string connectionString = "Server=localhost;Database=SistemaGestion;Trusted_Connection=True";

        public static List<ProductoVendido> ObtenerProductosVendidos()
        {
            List<ProductoVendido> productosVendidos = new List<ProductoVendido>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT id, idProducto, stock, idVenta FROM ProductoVendido";

                SqlCommand command = new SqlCommand(query, connection);

                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    ProductoVendido productoVendido = new ProductoVendido();
                    productoVendido.Id = Convert.ToInt32(reader["id"]);
                    productoVendido.IdProducto = Convert.ToInt32(reader["idProducto"]);
                    productoVendido.Stock = Convert.ToInt32(reader["stock"]);
                    productoVendido.IdVenta = Convert.ToInt32(reader["idVenta"]);

                    productosVendidos.Add(productoVendido);
                }

                reader.Close();
            }

            return productosVendidos;
        }

        public static bool CargarProductoVendido(List<ProductoVendido> productoVendidos)
        {
            bool resultado = false;
            long idProductoVendido = 0;
            int ElementosEnLaLista = 0;
            int idValidoEncontrado = 0;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "INSERT INTO [SistemaGestion].[dbo].[ProductoVenido] (idProducto, Stock, idVenta)" +
                                "VALUES (@idProducto , @Stock , @idVenta) " +
                                "SELECT @@IDENTITY";

                var parameterIdProducto = new SqlParameter();
                parameterIdProducto.ParameterName = "idProducto";
                parameterIdProducto.SqlDbType = SqlDbType.BigInt;
                parameterIdProducto.Value = 0;

                var parameterStock = new SqlParameter();
                parameterStock.ParameterName = "stock";
                parameterStock.SqlDbType = SqlDbType.Int;
                parameterStock.Value = 0;

                var parameterIdVenta = new SqlParameter();
                parameterIdVenta.ParameterName = "idVenta";
                parameterIdVenta.SqlDbType = SqlDbType.BigInt;
                parameterIdVenta.Value = 0;

                connection.Open();

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.Add(parameterIdProducto);
                    command.Parameters.Add(parameterStock);
                    command.Parameters.Add(parameterIdVenta);
                    command.Parameters.Add(parameterStock);

                    foreach (ProductoVendido item in productoVendidos)
                    {
                        parameterIdProducto.Value = item.IdProducto;
                        parameterStock.Value = item.Stock;
                        parameterIdVenta.Value = item.IdVenta;
                        ElementosEnLaLista++;
                        idProductoVendido = Convert.ToInt64(command.ExecuteScalar());
                        if (idProductoVendido > 0)
                        {
                            idValidoEncontrado++;
                        }
                    }
                }
                connection.Close();
            }
            if (idValidoEncontrado == ElementosEnLaLista)
            {
                resultado = true;
            }
            return resultado;
        }

        public static bool EliminarProductoVendido(long idProducto)
        {
            bool resultado = false;
            int rowsAffected = 0;

            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                string queryUpdate = "DELETE FROM [SistemaGestion].[dbo].[ProductoVendido] " +
                                        "WHERE IdProducto = @idProducto";

                var parameterIdProducto = new SqlParameter("idProducto", SqlDbType.BigInt);
                parameterIdProducto.Value = idProducto;

                sqlConnection.Open();

                using (SqlCommand sqlCommand = new SqlCommand(queryUpdate, sqlConnection))
                {
                    sqlCommand.Parameters.Add(parameterIdProducto);
                    rowsAffected = sqlCommand.ExecuteNonQuery();
                }
                sqlConnection.Close();
            }
            if (rowsAffected >= 1)
            {
                resultado = true;
            }
            return resultado;
        }


    }
}
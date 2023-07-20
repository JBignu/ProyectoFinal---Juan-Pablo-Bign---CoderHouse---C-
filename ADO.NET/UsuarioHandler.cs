
using System.Data.SqlClient; 
using System.Data;
using System;

namespace ProyectoFinal
{
    
    public static class UsuarioHandler
    {
        static string connectionString = "Server=localhost;Database=SistemaGestion;Trusted_Connection=True";

        public static List<Usuario> ObtenerUsuarios()
        {
            List<Usuario> usuarios = new List<Usuario>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT id, nombre, apellido, nombreUsuario, contrasena, mail FROM Usuario";

                SqlCommand command = new SqlCommand(query, connection);

                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Usuario usuario = new Usuario();
                    usuario.id = Convert.ToInt32(reader["id"]);
                    usuario.nombre = (string)reader["nombre"];
                    usuario.apellido = (string)reader["apellido"];
                    usuario.nombreUsuario = (string)reader["nombreUsuario"];
                    usuario.contrasena = (string)reader["contrasena"];
                    usuario.mail = (string)reader["mail"];

                    usuarios.Add(usuario);
                }

                reader.Close();
            }

            return usuarios;
        }

        public static bool ModificarUsuario(Usuario usuario)
        {
            bool resultado = false;
            int rowsAffected = 0;


            if (usuario.id <= 0)
            {
                return false;
            }

            if (String.IsNullOrEmpty(usuario.nombre) ||
                String.IsNullOrEmpty(usuario.apellido) ||
                String.IsNullOrEmpty(usuario.nombreUsuario) ||
                String.IsNullOrEmpty(usuario.contrasena) ||
                String.IsNullOrEmpty(usuario.mail))
            {
                return false;
            }

            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                string query = "UPDATE [SistemaGestion].[dbo].[Usuario] " +
                                        "SET Nombre = @nombre, " +
                                            "Apellido = @apellido, " +
                                            "NombreUsuario = @nombreUsuario, " +
                                            "Contrasena = @contrasena, " +
                                            "Mail = @mail " +
                                            "WHERE Id = @id";

                var parameterNombre = new SqlParameter("nombre", SqlDbType.VarChar);
                parameterNombre.Value = usuario.nombre;

                var parameterApellido = new SqlParameter("apellido", SqlDbType.VarChar);
                parameterApellido.Value = usuario.apellido;

                var parameterNombreUsuario = new SqlParameter("nombreUsuario", SqlDbType.VarChar);
                parameterNombreUsuario.Value = usuario.nombreUsuario;

                var parameterContraseña = new SqlParameter("contrasena", SqlDbType.VarChar);
                parameterContraseña.Value = usuario.contrasena;

                var parameterMail = new SqlParameter("mail", SqlDbType.VarChar);
                parameterMail.Value = usuario.mail;

                var parameterId = new SqlParameter("id", SqlDbType.BigInt);
                parameterId.Value = usuario.id;

                sqlConnection.Open();

                using (SqlCommand sqlCommand = new SqlCommand(query, sqlConnection))
                {
                    sqlCommand.Parameters.Add(parameterNombre);
                    sqlCommand.Parameters.Add(parameterApellido);
                    sqlCommand.Parameters.Add(parameterNombreUsuario);
                    sqlCommand.Parameters.Add(parameterContraseña);
                    sqlCommand.Parameters.Add(parameterMail);
                    sqlCommand.Parameters.Add(parameterId);
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

        public static Usuario InicioDeSesion(string nombreUsuario, string contraseña)
        {
            Usuario usuario = new Usuario();

            
            if (String.IsNullOrEmpty(nombreUsuario) || String.IsNullOrEmpty(contraseña)) 
            {
                return usuario;
            }

            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                using (SqlCommand sqlCommand = new SqlCommand("SELECT * FROM [SistemaGestion].[dbo].[Usuario] WHERE (NombreUsuario = @nombreUsuario AND Contrasena = @contrasena)", sqlConnection))
                {
                    var sqlParameter1 = new SqlParameter();
                    sqlParameter1.ParameterName = "nombreUsuario";
                    sqlParameter1.SqlDbType = SqlDbType.VarChar;
                    sqlParameter1.Value = nombreUsuario;
                    sqlCommand.Parameters.Add(sqlParameter1);

                    var sqlParameter2 = new SqlParameter();
                    sqlParameter2.ParameterName = "contrasena";
                    sqlParameter2.SqlDbType = SqlDbType.VarChar;
                    sqlParameter2.Value = contraseña;
                    sqlCommand.Parameters.Add(sqlParameter2);

                    sqlConnection.Open();

                    using (SqlDataReader dataReader = sqlCommand.ExecuteReader())
                    {
                        if (dataReader.HasRows & dataReader.Read())
                        {
                            usuario.id = Convert.ToInt32(dataReader["Id"]);
                            usuario.nombre = dataReader["Nombre"].ToString();
                            usuario.apellido = dataReader["Apellido"].ToString();
                            usuario.nombreUsuario = dataReader["NombreUsuario"].ToString();
                            usuario.contrasena = dataReader["Contrasena"].ToString();
                            usuario.mail = dataReader["Mail"].ToString();
                        }
                    }
                    sqlConnection.Close();
                }
            }
            return usuario;
        }

        public static bool EliminarUsuario(long id)
        {
            bool resultado = false;
            int rowsAffected = 0;

            
            if (id <= 0) 
            {
                return false;
            }

            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                string queryDelete = "DELETE FROM [SistemaGestion].[dbo].[Usuario] " + 
                                        "WHERE Id = @id";

                var parameterId = new SqlParameter("id", SqlDbType.BigInt);
                parameterId.Value = id;

                sqlConnection.Open();

                using (SqlCommand sqlCommand = new SqlCommand(queryDelete, sqlConnection))
                {
                    sqlCommand.Parameters.Add(parameterId);
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


    }
}

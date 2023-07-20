namespace ProyectoFinal
{
    public class Venta
    {
        public int id { get; set; }
        public string Comentarios { get; set; }
        public int idUsuario { get; set; }

        public Venta()
        {
            id = 0;
            Comentarios = string.Empty;
            idUsuario = 0;
        }
    }
}


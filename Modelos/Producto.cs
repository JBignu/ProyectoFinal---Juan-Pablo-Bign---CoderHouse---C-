namespace ProyectoFinal
{
 
    public class Producto
    {
        public int id { get; set; }
        public string descripcion { get; set; }
        public double costo { get; set; }
        public double precioVenta { get; set; }
        public int stock { get; set; }
        public int idUsuario { get; set; }

        public Producto()
        {
            id = 0;
            descripcion = string.Empty;
            costo = 0;
            precioVenta = 0;
            stock = 0;
            idUsuario = 0;
        }
    }
}


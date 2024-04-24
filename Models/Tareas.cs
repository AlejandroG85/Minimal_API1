namespace Minimal_API.Models
{
    public class Tareas
    {
        public int Id { get; set; }
        public required string Nombre { get; set; }
        //public required string Descripcion { get; set; }
        public bool Finalizada { get; set; }
        public int Orden { get; set; }
    }
}

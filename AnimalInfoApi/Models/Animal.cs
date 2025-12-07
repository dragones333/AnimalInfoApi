namespace AnimalInfoApi.Models
{
    public class Animal
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = "";
        public string Especie { get; set; } = "";
        public string Habitat { get; set; } = "";
        public string Descripcion { get; set; } = "";
        public string ImagenUrl { get; set; } = "";
    }
}
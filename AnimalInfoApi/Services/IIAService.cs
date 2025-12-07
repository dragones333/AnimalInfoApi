namespace AnimalInfoApi.Services
{
    public interface IIAService
    {
        Task<string> GenerarDescripcion(string animal);
    }
}
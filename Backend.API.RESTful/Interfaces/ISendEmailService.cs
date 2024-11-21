namespace Backend.API.RESTful.Interfaces
{
    public interface ISendEmailService
    {
        Task EnviarEmailCliente(string emailcliente, string nroCupon);
    }
}

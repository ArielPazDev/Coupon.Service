namespace Backend.API.RESTful.Interfaces
{
    public interface IMailerService
    {
        Task SendEmail(string emailcliente, string nroCupon);
    }
}

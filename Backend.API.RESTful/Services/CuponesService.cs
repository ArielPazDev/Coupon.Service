using Backend.API.RESTful.Interfaces;
using System.Text;

namespace Backend.API.RESTful.Services
{
    public class CuponesService : ICuponesService
    {
        private Random random;
        private const string characteres = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";

        public async Task<string> GenerarNroCupon()
        {
            random = new Random();
            StringBuilder code = new StringBuilder();

            code.Append(blockRandom());
            code.Append("-");
            code.Append(blockRandom());
            code.Append("-");
            code.Append(blockRandom());

            return code.ToString();
        }

        private string blockRandom()
        {
            string block = "";
            int indice;

            for (int i = 0; i < 3; i++)
            {
                indice = random.Next(characteres.Length);

                block += characteres[indice];
            }

            return block;
        }
    }
}

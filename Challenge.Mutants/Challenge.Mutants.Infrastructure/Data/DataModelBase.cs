using Newtonsoft.Json;
using System.Net.Http;
using System.Text;

namespace Challenge.Mutants.Infrastructure.Data
{
    public class DataModelBase
    {
        public DataModelBase() { }

        public StringContent Serialize()
        {
            var jsonPeticion = JsonConvert.SerializeObject(this);
            return new StringContent(jsonPeticion, Encoding.UTF8, "application/json");
        }
    }
}

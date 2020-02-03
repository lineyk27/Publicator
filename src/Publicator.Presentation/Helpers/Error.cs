using Newtonsoft.Json;

namespace Publicator.Presentation.Helpers
{
    public class Error
    {
        public string Message { get; set; }
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}

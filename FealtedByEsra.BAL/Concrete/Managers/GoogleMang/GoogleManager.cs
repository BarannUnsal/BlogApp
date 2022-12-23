using FealtedByEsra.BAL.Abstract.Services.GoogleService;
using FealtedByEsra.BAL.Helpers.GoogleHelpers;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace FealtedByEsra.BAL.Concrete.Managers.GoogleMang
{
    public class GoogleManager : IGoogleService
    {
		private readonly IOptionsMonitor<GoogleHelper> _config;
		public GoogleManager(IOptionsMonitor<GoogleHelper> config)
		{
			_config = config;
		}

		public async Task<bool> VertifyToken(string token)
        {
			try
			{
                HttpClient client = new HttpClient();
                HttpResponseMessage response = await client.PutAsync(string.Format($"https://www.google.com/recaptcha/api/siteverify?secret={_config.CurrentValue.SecretKey}&response={token}"), null);

                if (response.IsSuccessStatusCode)
                {
                    var resultString = await response.Content.ReadAsStringAsync();
                    var resp = JsonConvert.DeserializeObject<GoogleHelperResponse>(resultString);
                    if (resp!.success)
                    {
                        return true;
                    }
                }
                return false;
			}
			catch (Exception)
			{
				return false;
			}
        }
    }
}

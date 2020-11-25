namespace BloodDonationApp.Web.Helpers
{
    using Microsoft.AspNetCore.Http;
    using Newtonsoft.Json;

    public static class SessionHelper
    {
        public static void SetObjectAsJson(this ISession session, string key, string value)
        {
            session.SetString(key, value);
        }

        public static T GetObjectFromJson<T>(this ISession session, string key)
        {
            var value = session.GetString(key);
            return value == null ? default(T) : JsonConvert.DeserializeObject<T>(value);
        }
    }
}

using System;

namespace SopVault.Helpers
{
    public class AppConstants
    {
        public static string ConnectionString => GetValue<string>("connectionstring", true);

        private static T GetValue<T>(string key, bool addDefault = false)
        {

            if (string.IsNullOrWhiteSpace(Environment.GetEnvironmentVariable(key) ?? "") && !addDefault)
            {
                VariableNotFound(key);
            }

            try
            {
                return (T)Convert.ChangeType(Environment.GetEnvironmentVariable(key), typeof(T));
            }
            catch
            {
                return default(T);
            }
        }

        private static void VariableNotFound(string variableName)
        {
            throw new NullReferenceException($"The environment variable, {variableName}, cannot be found.");
        }
    }
}

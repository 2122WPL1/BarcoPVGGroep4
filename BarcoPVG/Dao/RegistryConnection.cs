using Microsoft.Win32;

namespace BarcoPVG.Dao
{
    public static class RegistryConnection
    {
        /// <summary>
        /// Gets all values from a subkey and stores them in an object
        /// </summary>
        /// <typeparam name="T">Type that can store the values in a subkey</typeparam>
        /// <param name="subkeyPath">Relative path to the subkey, starting at the Current User key</param>
        /// <returns></returns>
        public static T GetValueObject<T>(string subkeyPath) where T : new()
        {
            var storage = new T();

            using (RegistryKey valueKey = Registry.CurrentUser.OpenSubKey(subkeyPath))
            {
                foreach (var property in typeof(T).GetProperties())
                {
                    property.SetValue(storage, valueKey.GetValue(property.Name));
                }
            }
            return storage;
        }
    }
}
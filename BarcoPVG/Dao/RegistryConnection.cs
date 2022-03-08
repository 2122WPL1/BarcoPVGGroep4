using Microsoft.Win32;

namespace BarcoPVG.Dao
{
    /// <summary>
    /// Gets all values from a registry key and stores them in an object
    /// Kaat
    /// </summary>
    public static class RegistryConnection
    {
        /// <summary>
        /// Gets all values from a subkey and stores them in an object
        /// </summary>
        /// <typeparam name="T">Type that can store the values in a subkey</typeparam>
        /// <param name="subkeyPath">Relative path to the subkey, starting at the Current User key</param>
        /// <returns></returns>


        //changing the login when opening the application that the username and password is asked instead of the registry key.
        //Jarne
        [AssemblyInitialize]
        public static void AssemblyInitialize(TestContext testContext)
        {
            //long setPrompt = -1;
            //string regEditKey = @"SOFTWARE\Microsoft\Windows\CurrentVersion\InternetSettings\Zones\1";

            //using (RegistryKey regKeys =
            //RegistryKey.OpenBaseKey(RegistryHive.CurrentUser,
            //RegistryView.Registry64).OpenSubKey(regEditKey, true))
            //{
            //    if (regKeys != null)
            //    {
            //        setPrompt = Convert.ToInt64(regKeys.GetValue("1A00"));
            //        if (setPrompt != 65536)
            //        {
            //            regKeys.SetValue("1A00", (object)65536,
            //            RegistryValueKind.DWord);
            //            regKeys.Close();
            //            regKeys.Flush();
            //        }
            //    }
            //}
        }
    }
}

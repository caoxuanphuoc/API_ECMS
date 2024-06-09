using ECMS.Debugging;

namespace ECMS
{
    public class ECMSConsts
    {
        public const string LocalizationSourceName = "ECMS";

        public const string ConnectionStringName = "Default";

        public const bool MultiTenancyEnabled = false;


        /// <summary>
        /// Default pass phrase for SimpleStringCipher decrypt/encrypt operations
        /// </summary>
        public static readonly string DefaultPassPhrase =
            DebugHelper.IsDebug ? "gsKxGZ012HLL3MI5" : "fc2aaa871e8b43a18b87143b5094888f";
    }
}

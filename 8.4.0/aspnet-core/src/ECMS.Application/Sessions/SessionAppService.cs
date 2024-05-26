using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Abp.Auditing;
using ECMS.Sessions.Dto;
using System.Security.Cryptography;
using ECMS.Common.SystemConst;
using System.IO;
using System;

namespace ECMS.Sessions
{
    public class SessionAppService : ECMSAppServiceBase, ISessionAppService
    {
        [DisableAuditing]
        public async Task<GetCurrentLoginInformationsOutput> GetCurrentLoginInformations()
        {
            var output = new GetCurrentLoginInformationsOutput
            {
                Application = new ApplicationInfoDto
                {
                    Version = AppVersionHelper.Version,
                    ReleaseDate = AppVersionHelper.ReleaseDate,
                    Features = new Dictionary<string, bool>()
                }
            };

            if (AbpSession.TenantId.HasValue)
            {
                output.Tenant = ObjectMapper.Map<TenantLoginInfoDto>(await GetCurrentTenantAsync());
            }

            if (AbpSession.UserId.HasValue)
            {
                output.User = ObjectMapper.Map<UserLoginInfoDto>(await GetCurrentUserAsync());
                string plainText = output.User.UserName + "-" + output.User.Id + "-" + output.User.EmailAddress;
                string key = KeyHash.KeyHashUser;
                using (Aes aesAlg = Aes.Create())
                {
                    aesAlg.Key = Encoding.UTF8.GetBytes(key);
                    aesAlg.IV = new byte[16]; // Khởi tạo IV bằng mảng byte có giá trị 0

                    ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

                    using (MemoryStream msEncrypt = new MemoryStream())
                    {
                        using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                        {
                            using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                            {
                                swEncrypt.Write(plainText);
                            }
                            byte[] encrypted = msEncrypt.ToArray();
                            output.User.KeyHash = Convert.ToBase64String(encrypted);
                        }
                    }
                }
            }

            return output;
        }
    }
}

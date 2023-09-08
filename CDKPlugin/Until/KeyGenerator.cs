using CDKPlugin.Infrastructure.Enum;
using CDKPlugin.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Localization;
using OpenMod.Core.Ioc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CDKPlugin.Until
{
    [DontAutoRegister]
    internal static class KeyGenerator
    {
        private static ICDKPluginRepository? m_repository;
        private static IConfiguration? m_configuration;
        private static IStringLocalizer? m_localization;

        internal static void Initialize(IServiceProvider serviceProvider)
        {
            m_repository = serviceProvider.GetService<ICDKPluginRepository>();
            m_configuration = serviceProvider.GetService<IConfiguration>();
            m_localization = serviceProvider.GetService<IStringLocalizer>();

            if (m_repository == null || m_configuration == null || m_localization == null)
            {
                throw new ArgumentException(nameof(serviceProvider), "The service container cannot get the service");
            }
        }
        internal static string GenerateKey()
        {
            var randomType = m_configuration?.GetSection("random:type")
                .Get<ERandomType>();
            Random random = new Random();
            switch (randomType)
            {
                case ERandomType.Default:
                    random = new Random();
                    break;
                case ERandomType.HashCode:
                    random = new Random(Guid.NewGuid().GetHashCode());
                    break;
                default:
                    throw new ArgumentNullException(nameof(randomType), m_localization["error:invaild_random_type"]);
            }
           var charset = m_configuration.GetValue<string>("random:CharSet").Split(',');
           var MaxLength = m_configuration.GetValue<int>("random:Maxlength");
           var characterSpacing = m_configuration.GetValue<int>("random:characterSpacing");

            if (charset.Length == 0) throw new ArgumentNullException(nameof(charset),m_localization?["error:invaild_charset"]);

            StringBuilder randomString = new StringBuilder();

            for (int i = 0; i < MaxLength; i++)
            {
                // 生成一个随机数
                int charsetIndex = random.Next(charset.Length);
                randomString.Append(charset[charsetIndex]);
            }


            

            // 使用for循环在每指定个数字符后面插入"-"
            for (int i = characterSpacing; i < randomString.Length; i += characterSpacing+1)
            {
                randomString = randomString.Insert(i, "-");
            }

            // 使用ToUpper()方法将字符串转换成大写
            string cdKey = randomString.ToString().ToUpper();

            //检查Key是否已存在，避免随机碰撞

            bool exist = m_repository?.KeyExist(cdKey) ?? false;

            if (exist)
            {
                return GenerateKey();
            }
            else
            {
                return cdKey;
            }
            
        }
    }
}

using GDC_API.Data;
using Microsoft.EntityFrameworkCore;
using System.Data.Common;

namespace GDC_API.Extensions
{
    // Classe estática (não precisa ser instanciada) do qual será uma extensão da classe de serviços para incluirmos métodos adicionais
    public static class ServiceExtensions
    {
        // É um mecanismo que dá direitos para o usuário acessar os recursos do servidor em um domínio diferente
        // https://developer.mozilla.org/pt-BR/docs/Web/HTTP/CORS
        public static void ConfigureCors(this IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy", builder => builder.AllowAnyOrigin() // Permite qualquer domínio acessar
                                                                  .AllowAnyMethod() // Permite qualquer método HTTP (GET, POST, etc)
                                                                  .AllowAnyHeader()); // Permite qualquer cabeçalho ("accept", "content-type", etc)
            });
        }

        // Auxilia na integração com o IIS
        public static void ConfigureIISIntegration(this IServiceCollection services)
        {
            services.Configure<IISOptions>(options =>
            {
                options.AutomaticAuthentication = true;
                options.AuthenticationDisplayName = null;
                options.ForwardClientCertificate = true;
            });
        }

        // Configura o contexto do servidor em MySQL para acessarmos os dados
        // IConfiguration permite a leitura dos dados salvos no User Secrets
        public static void ConfigureMySqlContext(this IServiceCollection services, IConfiguration config) 
        {
            // Documentação para DbConnectionStringBuilder() -> https://docs.microsoft.com/en-us/dotnet/api/system.data.common.dbconnectionstringbuilder?view=net-6.0
            var connectionStrBuilder = new DbConnectionStringBuilder
            {
                // MySQL database no servidor de desenvolvimento
                { "server", config["DATABASE_DEV:HOST"] },
                { "userid", config["DATABASE_DEV:USER"] },
                { "password", config["DATABASE_DEV:PASSWORD"] },
                { "database", config["DATABASE_DEV:DB_NAME"] }
            };

            services.AddDbContext<MySQLDatabaseContext>(options => options.UseMySql(connectionStrBuilder.ConnectionString, ServerVersion.AutoDetect(connectionStrBuilder.ConnectionString)));
        }
    }
}

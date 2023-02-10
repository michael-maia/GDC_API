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
    }
}

using Microsoft.Extensions.DependencyInjection;
using TopModel.Generator.Core;

using static TopModel.Utils.ModelUtils;

namespace TopModel.Generator.Prisma;

public class GeneratorRegistration : IGeneratorRegistration<PrismaConfig>
{
    /// <inheritdoc cref="IGeneratorRegistration{T}.Register" />
    public void Register(IServiceCollection services, PrismaConfig config, int number)
    {
        TrimSlashes(config, c => c.EntityFilePath);

        config.Language ??= "prisma";
        services.AddGenerator<PrismaModelGenerator, PrismaConfig>(config, number);

    }
}

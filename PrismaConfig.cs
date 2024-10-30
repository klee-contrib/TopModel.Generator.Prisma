using TopModel.Core;
using TopModel.Generator.Core;
using TopModel.Utils;

namespace TopModel.Generator.Prisma;

/// <summary>
/// Paramètres pour la génération du Prisma.
/// </summary>
public class PrismaConfig : GeneratorConfigBase
{
    /// <summary>
    /// Chemin vers lequel sont créés les fichiers d'entités persistées créés, relatif au répertoire de génération.
    /// </summary>
    public string? EntityFilePath { get; set; }

    public override string[] PropertiesWithTagVariableSupport => new[]
    {
        nameof(EntityFilePath)
    };

    protected override bool UseNamedEnums => false;

    protected override string NullValue => "undefined";

    protected override string GetEnumType(string className, string propName, bool isPrimaryKeyDef = false)
    {
        return $"{className.ToPascalCase()}{propName.ToPascalCase()}";
    }

    protected override bool IsEnumNameValid(string name)
    {
        return true;
    }

    protected override string ResolveTagVariables(string value, string tag)
    {
        return base.ResolveTagVariables(value, tag).Trim('/');
    }
}

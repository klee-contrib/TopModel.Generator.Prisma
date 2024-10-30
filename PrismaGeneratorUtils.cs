namespace TopModel.Generator.Prisma;

using TopModel.Core;

public static class PrismaGeneratorUtils
{
    public static IList<IProperty> GetProperties(this Class classe, IEnumerable<Class> availableClasses)
    {
        return classe.Properties.Concat(classe.GetReverseProperties(availableClasses).Select(p => new ReverseAssociationProperty()
        {
            Association = p.Class,
            Type = p.Type == AssociationType.OneToMany ? AssociationType.ManyToOne
                : p.Type == AssociationType.ManyToOne ? AssociationType.OneToMany
                : p.Type == AssociationType.OneToOne ? AssociationType.OneToOne
                : AssociationType.ManyToMany,
            Comment = $"Association réciproque de {p.Class.NamePascal}.{p.Name}",
            Class = classe,
            ReverseProperty = p,
            Role = p.Role,
        })).ToList();
    }

    public static List<AssociationProperty> GetReverseProperties(this Class classe, IEnumerable<Class> availableClasses)
    {
        return availableClasses
            .SelectMany(c => c.Properties)
            .OfType<AssociationProperty>()
            .Where(p => p is not ReverseAssociationProperty)
            .Where(p => p.Type != AssociationType.OneToOne)
            .Where(p => p.Class.IsPersistent)
            .Where(p => p.Association.PrimaryKey.Count() == 1 || p.Type == AssociationType.ManyToOne)
            .Where(p => p.Association == classe)
            .ToList();
    }
}
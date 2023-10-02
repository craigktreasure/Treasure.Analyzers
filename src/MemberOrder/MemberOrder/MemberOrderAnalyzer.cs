namespace Treasure.Analyzers.MemberOrder;

using System.Collections.Immutable;
using System.Linq;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Diagnostics;

/// <summary>
/// Represents an analyzer that ensure the order of members.
/// Implements the <see cref="DiagnosticAnalyzer" />
/// </summary>
/// <seealso cref="DiagnosticAnalyzer" />
[DiagnosticAnalyzer(LanguageNames.CSharp)]
public class MemberOrderAnalyzer : DiagnosticAnalyzer
{
    /// <summary>
    /// The diagnostic identifier.
    /// </summary>
    public const string DiagnosticId = "Treasure0001";

    private const string Category = "Ordering";

    private static readonly LocalizableString Description = new LocalizableResourceString(nameof(Resources.Treasure0001AnalyzerDescription), Resources.ResourceManager, typeof(Resources));

    private static readonly LocalizableString MessageFormat = new LocalizableResourceString(nameof(Resources.Treasure0001AnalyzerMessageFormat), Resources.ResourceManager, typeof(Resources));

    // You can change these strings in the Resources.resx file. If you do not want your analyzer to be localize-able, you can use regular strings for Title and MessageFormat.
    // See https://github.com/dotnet/roslyn/blob/main/docs/analyzers/Localizing%20Analyzers.md for more on localization
    private static readonly LocalizableString Title = new LocalizableResourceString(nameof(Resources.Treasure0001AnalyzerTitle), Resources.ResourceManager, typeof(Resources));

    private static readonly DiagnosticDescriptor Rule = new(DiagnosticId, Title, MessageFormat, Category, DiagnosticSeverity.Warning, isEnabledByDefault: true, description: Description);

    /// <summary>
    /// Gets the supported diagnostics.
    /// </summary>
    public override ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics => ImmutableArray.Create(Rule);

    /// <summary>
    /// Gets the accessibility modifier.
    /// </summary>
    /// <param name="member">The member.</param>
    /// <returns><see cref="int"/>.</returns>
    public static int GetAccessibilityModifierOrder(MemberDeclarationSyntax member)
    {
        if (member is null)
        {
            throw new ArgumentNullException(nameof(member));
        }

        if (member is ConstructorDeclarationSyntax && member.Modifiers.Any(SyntaxKind.StaticKeyword))
        {
            // Special case static constructors to come first.
            return -1;
        }

        // public
        if (member.Modifiers.Any(SyntaxKind.PublicKeyword))
        {
            return 0;
        }

        // internal
        else if (member.Modifiers.Any(SyntaxKind.InternalKeyword) && !member.Modifiers.Any(SyntaxKind.ProtectedKeyword))
        {
            return 1;
        }

        // protected internal
        else if (member.Modifiers.Any(SyntaxKind.ProtectedKeyword) && member.Modifiers.Any(SyntaxKind.InternalKeyword))
        {
            return 2;
        }

        // private protected
        else if (member.Modifiers.Any(SyntaxKind.PrivateKeyword) && member.Modifiers.Any(SyntaxKind.ProtectedKeyword))
        {
            return 3;
        }

        // protected
        else if (member.Modifiers.Any(SyntaxKind.ProtectedKeyword) && !member.Modifiers.Any(SyntaxKind.PrivateKeyword))
        {
            return 4;
        }

        // private
        else if (member.Modifiers.Any(SyntaxKind.PrivateKeyword))
        {
            return 5;
        }
        else
        {
            return 99;
        }
    }

    /// <summary>
    /// Gets the member category.
    /// </summary>
    /// <param name="member">The member.</param>
    /// <returns><see cref="int"/>.</returns>
    public static int GetMemberCategoryOrder(MemberDeclarationSyntax member)
    {
        if (member is null)
        {
            throw new ArgumentNullException(nameof(member));
        }

        return member switch
        {
            FieldDeclarationSyntax => 0,
            PropertyDeclarationSyntax => 1,
            DelegateDeclarationSyntax => 2,
            EventFieldDeclarationSyntax => 3,
            EventDeclarationSyntax => 3,
            IndexerDeclarationSyntax => 4,
            ConstructorDeclarationSyntax => 5,
            DestructorDeclarationSyntax => 6,
            MethodDeclarationSyntax => 7,
            _ => 99,
        };
    }

    /// <summary>
    /// Gets the name of the member.
    /// </summary>
    /// <param name="member">The member.</param>
    /// <returns><see cref="string"/>.</returns>
    public static string GetMemberName(MemberDeclarationSyntax member)
    {
        if (member is null)
        {
            throw new ArgumentNullException(nameof(member));
        }

        return member switch
        {
            FieldDeclarationSyntax field => field.Declaration.Variables.First().Identifier.Text,
            PropertyDeclarationSyntax property => property.Identifier.Text,
            DelegateDeclarationSyntax @delegate => @delegate.Identifier.Text,
            EventDeclarationSyntax @event => @event.Identifier.Text,
            EventFieldDeclarationSyntax eventField => eventField.Declaration.Variables.First().Identifier.Text,
            IndexerDeclarationSyntax => string.Empty,
            ConstructorDeclarationSyntax constructor => constructor.Identifier.Text,
            DestructorDeclarationSyntax destructor => destructor.Identifier.Text,
            MethodDeclarationSyntax method => method.Identifier.Text,
            _ => throw new InvalidOperationException($"Unable to get member name: '{member}'"),
        };
    }

    /// <summary>
    /// Gets the special keyword order.
    /// </summary>
    /// <param name="member">The member.</param>
    /// <returns><see cref="int"/>.</returns>
    public static int GetSpecialKeywordOrder(MemberDeclarationSyntax member)
    {
        if (member is null)
        {
            throw new ArgumentNullException(nameof(member));
        }

        if (member.Modifiers.Any(SyntaxKind.ConstKeyword))
        {
            return 0;
        }

        if (member.Modifiers.Any(SyntaxKind.StaticKeyword))
        {
            return 1;
        }

        return 99;
    }

    /// <summary>
    /// Initializes the specified context.
    /// </summary>
    /// <param name="context">The context.</param>
    public override void Initialize(AnalysisContext context)
    {
        if (context is null)
        {
            throw new ArgumentNullException(nameof(context));
        }

        context.ConfigureGeneratedCodeAnalysis(GeneratedCodeAnalysisFlags.None);
        context.EnableConcurrentExecution();

        context.RegisterSyntaxNodeAction(AnalyzeNode, SyntaxKind.ClassDeclaration);
    }

    private static void AnalyzeNode(SyntaxNodeAnalysisContext context)
    {
        ClassDeclarationSyntax classDeclaration = (ClassDeclarationSyntax)context.Node;
        Location classLocation = classDeclaration.GetLocation();
        SyntaxList<MemberDeclarationSyntax> members = classDeclaration.Members;
        List<MemberDeclarationSyntax> sortedMembers = members
            .OrderBy(GetMemberCategoryOrder)
            .ThenBy(GetAccessibilityModifierOrder)
            .ThenBy(GetSpecialKeywordOrder)
            .ThenBy(GetMemberName)
            .ToList();

        for (int i = 0; i < members.Count; i++)
        {
            MemberDeclarationSyntax member = members[i];
            MemberDeclarationSyntax sortedMember = sortedMembers[i];
            if (sortedMember != member)
            {
                Diagnostic diagnostic = Diagnostic.Create(Rule, classLocation, classDeclaration.Identifier.Text);
                context.ReportDiagnostic(diagnostic);
                break;
            }
        }
    }
}

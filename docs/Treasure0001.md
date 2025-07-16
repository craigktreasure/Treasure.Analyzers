# Treasure0001 ![Static Badge](https://img.shields.io/badge/Warning-yellow?style=for-the-badge)

Members should be ordered by type, keyword, accessibility level, and name

## Cause

Members contained in an object are not organized by type, keyword, accessibility level, and name.

## How to fix violations

To fix a violation of this rule, order the members by type, keyword, accessibility level, and name.

**Enhanced member-level diagnostics**: Starting with version 0.4.0, the analyzer now reports diagnostics on individual out-of-order members instead of the entire type. This provides more precise guidance on which specific members need to be repositioned.

You can use the code fixers to resolve violations:

- **"Reposition member 'memberName'"**: Moves just the specific member to its correct position
- **"Reorder members"**: Reorders all members in the type (preserves existing workflow)

![Treasure0001 Code Fixer](assets/Treasure0001Demo.gif)

## Examples

### Violates

```csharp
public class MyClass
{
    private int field = 0;

    private static readonly int staticField = 0;

    private const int constantField = 0;

    ~MyClass() { }

    public MyClass() { }

    static MyClass() { }

    private int PrivateMethod() => 1;

    public int PublicMethod() => 1;

    public static int StaticMethod() => 1;
}
```

### Does not violate

```csharp
public class MyClass
{
    // Constant fields
    private const int constantField = 0;

    // Static fields
    private static readonly int staticField = 0;

    // Fields
    private int field = 0;

    // Static constructor
    static MyClass() { }

    // Constructors
    public MyClass() { }

    // Finalizer
    ~MyClass() { }

    // Static methods
    public static int StaticMethod() => 1;

    // Public methods
    public int PublicMethod() => 1;

    // Private methods
    private int PrivateMethod() => 1;
}
```

# Treasure0001 ![Static Badge](https://img.shields.io/badge/Warning-yellow?style=for-the-badge)

Members should be ordered by type, keyword, accessibility level, and name

## Cause

Members contained in an object are not organized by type, keyword, accessibility level, and name.

## How to fix violations

To fix a violation of this rule, order the members by type, keyword, accessibility level, and name.

You can also use the "Reorder members" code fixer:

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

# Calculator WPF

Integer calculator with WPF interface. Supports basic math operations and batch file processing.

## Features

- Basic operations: +, -, *, /
- Operator precedence (multiply/divide before add/subtract)
- Negative numbers
- Large number support
- Batch file processing
- Async file operations

## Requirements

- .NET 8 SDK
- Windows (WPF requires Windows)

## Building

```bash
dotnet build
```

## Running

```bash
dotnet run --project CalculatorWPF/CalculatorWPF.csproj
```

Or open in Visual Studio and press F5.

## Testing

```bash
dotnet test
```

## Usage

### Calculator Tab

Enter an expression and click Calculate or press Enter.

Examples:
- `2 + 3 * 2` = 8
- `10 - 5` = 5
- `-3 * 4` = -12

### Batch Processing Tab

1. Select input file (one expression per line)
2. Choose output file path
3. Click Process File

Input file example:
```
2 + 3 * 2
10 - 5
-3 * 4
```

Output file:
```
8
5
-12
```

## Error Messages

- Division by zero: `Error - Division by zero`
- Invalid characters: `Error - Invalid character: 'x'`
- Decimals not supported: `Error - Decimal numbers are not supported`
- Empty expression: `Error - Expression cannot be empty`

## Project Structure

```
CalculatorWPF/
├── CalculatorWPF/
│   ├── Models/
│   │   └── Token.cs
│   ├── Services/
│   │   ├── Tokenizer.cs
│   │   ├── ExpressionEvaluator.cs
│   │   ├── CalculatorEngine.cs
│   │   └── FileProcessor.cs
│   └── MainWindow.xaml
└── CalculatorWPF.Tests/
    └── Services/
        └── (test files)
```

using System.Numerics;
using CalculatorWPF.Models;

namespace CalculatorWPF.Services
{
    // Evaluates expressions with proper operator precedence
    public class ExpressionEvaluator
    {
        private List<Token> _tokens;
        private int _currentIndex;

        public ExpressionEvaluator()
        {
            _tokens = new List<Token>();
            _currentIndex = 0;
        }

        // Evaluates expression and returns result
        public BigInteger Evaluate(string expression)
        {
            if (string.IsNullOrWhiteSpace(expression))
            {
                throw new InvalidOperationException("Expression cannot be empty");
            }

            var tokenizer = new Tokenizer(expression);
            _tokens = tokenizer.Tokenize();
            _currentIndex = 0;

            BigInteger result = ParseExpression();

            // Make sure we parsed everything
            if (_currentIndex < _tokens.Count - 1)
            {
                throw new InvalidOperationException($"Unexpected token at position {_tokens[_currentIndex].Position}");
            }

            return result;
        }

        // Handles + and - (lowest precedence)
        private BigInteger ParseExpression()
        {
            BigInteger left = ParseTerm();

            while (_currentIndex < _tokens.Count)
            {
                Token current = _tokens[_currentIndex];
                
                if (current.Type == TokenType.Operator && (current.Operator == "+" || current.Operator == "-"))
                {
                    _currentIndex++;
                    BigInteger right = ParseTerm();

                    if (current.Operator == "+")
                    {
                        left = BigInteger.Add(left, right);
                    }
                    else
                    {
                        left = BigInteger.Subtract(left, right);
                    }
                }
                else
                {
                    break;
                }
            }

            return left;
        }

        // Handles * and / (higher precedence)
        private BigInteger ParseTerm()
        {
            BigInteger left = ParseFactor();

            while (_currentIndex < _tokens.Count)
            {
                Token current = _tokens[_currentIndex];
                
                if (current.Type == TokenType.Operator && (current.Operator == "*" || current.Operator == "/"))
                {
                    string op = current.Operator;
                    _currentIndex++;
                    BigInteger right = ParseFactor();

                    if (op == "*")
                    {
                        left = BigInteger.Multiply(left, right);
                    }
                    else
                    {
                        // Check for division by zero
                        if (right == 0)
                        {
                            throw new DivideByZeroException("Division by zero");
                        }
                        left = BigInteger.Divide(left, right);
                    }
                }
                else
                {
                    break;
                }
            }

            return left;
        }

        // Reads a number
        private BigInteger ParseFactor()
        {
            if (_currentIndex >= _tokens.Count)
            {
                throw new InvalidOperationException("Unexpected end of expression");
            }

            Token current = _tokens[_currentIndex];

            if (current.Type == TokenType.Number)
            {
                _currentIndex++;
                return current.Value!.Value;
            }

            throw new InvalidOperationException($"Expected number at position {current.Position}");
        }
    }
}

using System;
using System.IO;
using System.Collections;
using System.Security.Cryptography.X509Certificates;

namespace Modulo_adicional_2
{
    public class Program
    {
        public static void Main()
        {
            int numero;
            char operando;
            char caracter;
            int prioridad1 = 0;
            int prioridad2 = 0;
            bool p1 = false;
            bool p2 = false;
            bool imp = true;
            int Z = 90;
            char[] operacion;
            Queue Cola = new Queue();
            Stack Pila = new Stack();

            Console.Write("Introduzca una operación: ");
            operacion = Console.ReadLine().ToCharArray();

            Pila.Clear();
            Cola.Clear();
            for (int i = 0; i < operacion.Length; i++)
            {
                char c = operacion[i];

                if (c == '+' || c == '-' || c == '*' || c == '/' || c == '^' || c == '(' || c == ')')
                {
                    operando = c;

                    if (Pila.Count != 0)
                    {
                        switch (operando)
                        {
                            case '+':
                                prioridad1 = 1;
                                break;
                            case '-':
                                prioridad1 = 1;
                                break;
                            case '*':
                                prioridad1 = 2;
                                break;
                            case '/':
                                prioridad1 = 2;
                                break;
                            case '^':
                                prioridad1 = 3;
                                break;
                            case '(':
                                p1 = true;
                                break;
                            case ')':
                                p2 = true;
                                break;
                        }

                        switch (Pila.Peek())
                        {
                            case '+':
                                prioridad2 = 1;
                                break;
                            case '-':
                                prioridad2 = 1;
                                break;
                            case '*':
                                prioridad2 = 2;
                                break;
                            case '/':
                                prioridad2 = 2;
                                break;
                            case '^':
                                prioridad2 = 3;
                                break;
                        }

                        if (p1 && p2)
                        {
                            while (Pila.Count != 0)
                            {
                                Cola.Enqueue(Pila.Pop());
                            }

                            p1 = p2 = false;
                        }
                        else // if (prioridad1 >= prioridad2)
                        {
                            if (operando != '(' && operando != ')')
                            {
                                Pila.Push(operando);
                            }
                        }
                    }
                    else if (Pila.Count == 0)
                    {
                        if (operando != '(' && operando != ')')
                        {
                            Pila.Push(operando);
                        }
                        else if (operando == '(')
                        {
                            p1 = true;
                        }
                        else if (operando == ')')
                        {
                            p2 = true;
                        }
                    }
                }
                else if (char.IsLetter(c))
                {
                    caracter = c;
                    Cola.Enqueue(caracter);
                }
                else if (char.IsDigit(c))
                {
                    numero = c - '0'; //Restamos el valor ASCII de lo que haya en c y el valor ASCII de 0. El resultado nos dará el número contenido en c en entero

                    if (i + 1 < operacion.Length && char.IsDigit(operacion[i + 1]))
                    {
                        i++;
                        numero = numero * 10 + (operacion[i] - '0');
                    }

                    if (numero >= 0 && numero < 10)
                    {
                        Cola.Enqueue(numero);
                    }
                    else
                    {
                        Console.WriteLine($"El número {numero} es mayor a 9");
                        imp = false;
                    }
                }
                else
                {
                    Console.WriteLine($"El valor '{c}' no es válido para realizar la operación");
                    imp = false;
                }
            }

            if (imp)
            {
                while (Pila.Count != 0)
                {
                    Cola.Enqueue(Pila.Pop());
                }

                Console.Write("SUFIJO: ");
                while (Cola.Count != 0)
                {
                    Console.Write($"{Cola.Dequeue()} ");
                }
                Console.WriteLine("\n");
            }

        }
    }
}
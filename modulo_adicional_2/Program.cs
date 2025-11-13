using System;
using System.IO;
using System.Collections;
using System.Text;
using System.Reflection.PortableExecutable;

namespace Modulo_adicional_2
{
    public class Program
    {
        public static void Main()
        {
            int numero;
            char operando;
            char caracter;
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
                    Cola.Enqueue(operando);
                }
                else if (char.IsLetter(c))
                {
                    caracter = c;
                    Pila.Push(caracter);
                }
                else if (char.IsDigit(c))
                {
                    numero = c - '0'; //Restamos el valor ASCII de lo que haya en c y el valor ASCII de 0. El resultado nos dará el número contenido en c en entero

                    if (i + 1 < operacion.Length && char.IsDigit(operacion[i + 1]))
                    {
                        i++;
                        numero = numero * 10 + (operacion[i] - '0');
                    }
                    
                    if(numero >= 0 && numero < 10)
                    {
                        Pila.Push(numero);
                    }else
                    {
                        Console.WriteLine($"El número {numero} es mayor a 9");
                        break;
                    }
                }
                else
                {
                    Console.WriteLine($"El valor '{c}' no es válido para realizar la operación");
                    break;
                }
            }
        }
    }
}
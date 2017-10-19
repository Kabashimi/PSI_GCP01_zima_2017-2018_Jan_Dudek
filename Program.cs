using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Perceptron
{
    class Program
    {


        class Input
        {
            public double x0;
            public double x1;
            public double y;
            public Input(double x0, double x1, double y)
            {
                this.x0 = x0;
                this.x1 = x1;
                this.y = y;
            }
        }

        


        class Perceptron
        {                          
            public List<double> W;
            public double Bias;
            public List<Input> data;
            public double N;
            int licznikEpok;
            string[] output;
            int accuratness;


            public void Run()
            {
                List<Input> inputLearn = new List<Input>();     //Ustawia dane uczące
                inputLearn.Add(new Input(0, 0, 0));
                inputLearn.Add(new Input(1, 0, 0));
                inputLearn.Add(new Input(0, 1, 0));
                inputLearn.Add(new Input(1, 1, 1));
                List<Input> inputCheck = new List<Input>();     //Ustawia dane walidujące
                inputCheck.Add(new Input(0, 0, 0));
                inputCheck.Add(new Input(1, 0, 0));
                inputCheck.Add(new Input(0, 1, 0));
                inputCheck.Add(new Input(1, 1, 1));

                for (int i = 0; i < 100; i++)                  //Każdy obieg pętli to kolejna epoka
                {
                    licznikEpok++;
                    LoadInput(inputLearn);
                    Learn();
                    LoadInput(inputCheck);
                    Evaluate();
                    if (licznikEpok % 1000 == 0)
                        Console.WriteLine(licznikEpok);
                }
                System.IO.File.WriteAllLines(@"OUTPUT.txt", output);
                Console.WriteLine("Waga 0: " + W[0]);
                Console.WriteLine("Waga 1: " + W[1]);
            }

            private int activateFunction(double x)
            {
                if (x < 1)
                    return 0;
                return 1;
            }

            public void LoadInput(List<Input> input)
            {
                data = input;
            }

            

            public void Learn()
            {
                double sum;
                foreach (Input element in data)
                {
                    sum = 0;
                    //sum = element.x0 * W[0] + element.x1 * W[1] + Bias;
                    sum = element.x0 * W[0] + element.x1 * W[1];
                    int result = activateFunction(sum);
                    if (element.y == result)
                    {
                        //neuron działa poprawnie
                    }
                    else
                    {
                        //trzeba poprawić wagi
                        W[0] += N * (element.y - result) * element.x0;
                        W[1] += N * (element.y - result) * element.x1;
                        Bias += N * (1 - result) * 1;
                    }

                }
            }


            public void Evaluate()
            {
                accuratness = 0;
                double sum;
                foreach (Input element in data)
                {
                    sum = 0;
                    //sum = element.x0 * W[0] + element.x1 * W[1] + Bias;
                    sum = element.x0 * W[0] + element.x1 * W[1] ;
                    double result = activateFunction(sum);
                    if (element.y == result)
                    {
                        //neuron działa poprawnie
                        accuratness++;
                    }
                    else
                    {
                        //błąd
                    }

                }
                output[licznikEpok - 1] = accuratness.ToString();
            }

            public Perceptron()
            {
                W = new List<double>();     //Lista wag
                
                Random random = new Random();
                W.Add(random.NextDouble());
                W.Add(random.NextDouble());
                Bias = random.NextDouble();
                N = 0.01;                   //Współczynnik uczenia
                licznikEpok = 0;
                output = new string[100];  //rozmiar tabicy na dane wyjściowe
            }

        }



        static void Main(string[] args)
        {            
            Perceptron p1 = new Perceptron();
            p1.Run();
            
            Console.WriteLine("Press any key");
            Console.ReadLine();
        }
    }
}

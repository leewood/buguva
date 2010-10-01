using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace Lygiagretus01
{
    /**
     * 
     * @author nku
     * 
     * Programa imituoja branduolinio reaktoriaus darbą.
     * Branduolinės reakcijos reaktoriuje valdomas ileidžiant arba ištraukiant
     * specialius kontrolinius anglies strypus. Strypų iškėlimas valdomas specialių
     * pultų. Egzistruoja pagrindinis ir atsarginis pultas. Reaktorius automatika 
     * pultų komandas gali priimti lygiagrečiai. Strypų negalima iškelti daugiau
     * nei leidžiamas limitas. Automatima turėtų prieš atlikdama operaciją pasitikrinti
     * ar neviršijamas limitas, tačiau vykdant operacijas lygiagrečiai tikrinant sąlygą
     * gali įvykti nepageidaujamas situacijos. Tai ir bandoma simuliuoti šioje
     * programoje. Pagrindiniai reikalavimai strypų būsenos pakeitimo operacijai:
     * 1. Būsenos prieš ir po operaciją turi būti suderinamos, t.y. jei stypas įleidžiamas giliau,
     *    tai po įleidimo jo būsena turi būti didesnė už pradinę, jei iškeliamas - atvirkščiai.
     * 2. Būsena negali tapti mažesnė už leistiną ribą (ji nustatyta 10)
     *
     * Kad būtų paprasčiau nustatyti ar reaktoriaus būsenos korektiškai keičiamos,
     * (t.y. mažinant būseną 100 vienetu gaunama būsena 99), skaičiuojamas visų įgytų būsenų vidurkis
     * Programos vykdymo pabaigoje pateikiamas planuojamas vidurkis ir toks, kokį gavo programa,
     * bei planuojama galutinė reaktoriaus būsena ir gautoji.
     */
    class Program
    {
        static void Main(string[] args)
        {
            int maxCount = 1000000;
            int controllerSteps = 50000;
            NuclearReactor r = new NuclearReactor(maxCount);
            var c1 = new Thread(() => Run(r, controllerSteps));
            var c2 = new Thread(() => Run(r, controllerSteps));
            c1.Start();
            c2.Start();
            //Sulaukiam kol gijos baigs darbą
            c1.Join();
            c2.Join();
            //Išvedame rezultatus
            System.Console.WriteLine("Current reactor status: " + r.controlRodsPosition);
            int expectedReactorStatus = ((controllerSteps * 2 > maxCount - 10) ? 10 : (maxCount - controllerSteps * 2));
            System.Console.WriteLine("Expected reactor status: " + expectedReactorStatus);
            /* Kadangi kiekvienas valdiklis tik mažina būseną ir visada tik vienetu, tai vidutinė būsena turėtų būti lygi
               aritmetinės progresijos narių nuo pradinės iki galutinės būsenos sumai padalintai iš narių skaičiaus.
            */
            System.Console.WriteLine("Result average state: " + (double)r.sum / (double)r.count);
            // Apskaičiuojam planuojamą vidurkį:
            double expectedSum = ((double)((maxCount - expectedReactorStatus) * (maxCount - expectedReactorStatus - 1)) / 2 + (expectedReactorStatus + 1) * (maxCount - expectedReactorStatus));
            double expectedSteps = (controllerSteps * 2 > maxCount - 10) ? maxCount - 10 : controllerSteps * 2;
            System.Console.WriteLine("Expected average: " + (expectedSum / expectedSteps));
            System.Console.ReadLine();
        }

        public static void Run(NuclearReactor r, int stepsCount)
        {
            for (int i = 0; i < stepsCount; i++)
            {
                r.ChangeControlRodStatus(-1);
            }

        }
    }
}

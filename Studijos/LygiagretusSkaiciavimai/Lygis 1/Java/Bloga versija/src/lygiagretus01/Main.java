package lygiagretus01;

import java.util.logging.Level;
import java.util.logging.Logger;

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
public class Main {
    public static void main(String[] args) {
        int maxCount = 1000000;
        int controllerSteps = 50000;
        NuclearReactor r = new NuclearReactor(maxCount);        
        NuclearReactorController c1 = new NuclearReactorController(r, controllerSteps);
        NuclearReactorController c2 = new NuclearReactorController(r, controllerSteps);
        c1.start();
        c2.start();

        // Sulaukiam kol gijos baigs darbą
        try {
            c1.join();
        } catch (InterruptedException ex) {            
        }
        try {
            c2.join();
        } catch (InterruptedException ex) {            
        }

        //Išvedame rezultatus
        System.out.println("Current reactor status: " + r.controlRodsPosition);
        int expectedReactorStatus = ((controllerSteps * 2 > maxCount - 10)? 10: (maxCount - controllerSteps * 2));
        System.out.println("Expected reactor status: " + expectedReactorStatus);
        /* Kadangi kiekvienas valdiklis tik mažina būseną ir visada tik vienetu, tai vidutinė būsena turėtų būti lygi
           aritmetinės progresijos narių nuo pradinės iki galutinės būsenos sumai padalintai iš narių skaičiaus.
        */
        System.out.println("Result average state: " + (double)r.sum / (double)r.count);
        // Apskaičiuojam planuojamą vidurkį:
        double expectedSum = ((double)((maxCount - expectedReactorStatus) * (maxCount - expectedReactorStatus - 1))/2 + (expectedReactorStatus + 1) * (maxCount - expectedReactorStatus));
        double expectedSteps = (controllerSteps * 2 > maxCount - 10)? maxCount - 10: controllerSteps * 2;
        System.out.println("Expected average: " +  (expectedSum / expectedSteps ));
    }
}

package lygiagretus01;
public class NuclearReactorController extends Thread {
    public NuclearReactor reactor;    
    public int stepsCount = 100;
    
    public NuclearReactorController(NuclearReactor r, int stepsCount) {
        reactor = r;
        this.stepsCount = stepsCount;
    }
    @Override
    public void run() {
        for (int i = 0; i < stepsCount; i++) {
            reactor.ChangeControlRodStatus(-1);
        }
    }
}
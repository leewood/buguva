/*
 * ChaosMain.java
 */

package Chaos1;

import org.jdesktop.application.Application;
import org.jdesktop.application.SingleFrameApplication;
import java.applet.Applet;
import java.awt.*;

import jv.object.*;
import jv.project.PvDisplayIf;
import jv.viewer.PvViewer;
/**
 * The main class of the application.
 */



public class ChaosMain extends SingleFrameApplication {

    @Override protected void startup() {
        show(new ChaosMainView(this));
    }

    @Override protected void configureWindow(java.awt.Window root) {
    }

    public static ChaosMain getApplication() {
        return Application.getInstance(ChaosMain.class);
    }

    public static void main(String[] args) {
        launch(ChaosMain.class, args);
    }
}

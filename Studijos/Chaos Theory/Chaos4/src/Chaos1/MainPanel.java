/*
 * To change this template, choose Tools | Templates
 * and open the template in the editor.
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
 *
 * @author kuosis
 */
public class MainPanel extends java.awt.Panel {
	public		Frame				m_frame			= null;
	/** 3D-viewer window for graphics output and which is embedded into the applet */
	protected	PvViewer			m_viewer;

	/** Interface of applet to inform about author, version, and copyright */
	public String getAppletInfo() {
		return "Maldebrotas";
	}

	/**
	 * Configure and initialize the viewer, load system and user projects.
	 * One of the user projects must be selected here.
	 */
	public void init() {
		//m_viewer = new PvViewer(this, m_frame);
        m_viewer = new PvViewer();
		// Create and load a project
		PjFractalImage pjFractal = new PjFractalImage();
		m_viewer.addProject(pjFractal);
        try
        {

		m_viewer.selectProject(pjFractal);
        } catch (Exception e)
        {
            
        }
		// Get 3d display from viewer and add it to applet
		setLayout(new BorderLayout());
		PsPanel pDisplay = new PsPanel();
		pDisplay.setPreferredSize(640, 512);
		pDisplay.setLayout(new GridLayout(2, 2));
        pDisplay.add((Component)pjFractal.getDispNiuton());
		pDisplay.add((Component)pjFractal.getDispJulia());
		pjFractal.addDisplay(pjFractal.getDispMandelbrot());
		
        
        pDisplay.add((Component)pjFractal.getDispMandelbrot());
		add(pDisplay, BorderLayout.NORTH);
		add(pjFractal.getInfoPanel(), BorderLayout.CENTER);

		validate();
	}
	/**
	 * Standalone application support. The main() method acts as the applet's
	 * entry point when it is run as a standalone application. It is ignored
	 * if the applet is run from within an HTML page.
	 */

	/** Print info while initializing applet and viewer. */
	public void paint(Graphics g) {
		g.setColor(Color.yellow);
		g.drawString(PsConfig.getProgram()+" v."+PsConfig.getVersion(), 20, 40);
		g.drawString("Loading Project .....", 20, 60);
	}

	/**
	 * Does clean-up when applet is destroyed by the browser.
	 * Here we just close and dispose all our control windows.
	 */
	public void destroy()	{ m_viewer.destroy(); }

	/** Start viewer, e.g. start animation if requested */
	public void start()		{ m_viewer.start(); }

	/** Stop viewer, e.g. stop animation if requested */
	public void stop()		{ m_viewer.stop(); }


}

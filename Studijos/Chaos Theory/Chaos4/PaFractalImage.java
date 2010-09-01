package vgp.tutor.fractal;

import java.applet.Applet;
import java.awt.*;

import jv.object.*;
import jv.project.PvDisplayIf;
import jv.viewer.PvViewer;

/**
 * Demo applet for working with pixel images. Applet shows a Julia set
 * and a Mandelbrot set where the Julia set is determined by picking a complex parameter c
 * which is picked in the Mandelbrot image.
 * <p>
 * Picking in the Julia display will show several iterations of the picked point.
 * 
 * @see			jv.viewer.PvViewer
 * @author		Konrad Polthier
 * @version		01.10.05, 2.10 revised (kp) User interface improved.<br>
 *					10.08.05, 2.00 revised (kp) Functional extensions and efficiency optimization.<br>
 *					26.09.99, 1.00 created (kp) 
 */
public class PaFractalImage extends Applet {
	/** frame if run standalone, null if run as applet */
	public		Frame				m_frame			= null;
	/** 3D-viewer window for graphics output and which is embedded into the applet */
	protected	PvViewer			m_viewer;

	/** Interface of applet to inform about author, version, and copyright */
	public String getAppletInfo() {
		return "Name: "		+ this.getClass().getName()+ "\r\n" +
				 "Author: "		+ "Konrad Polthier" + "\r\n" +
				 "Version: "	+ "2.00" + "\r\n" +
				 "Applet shows usage of images in display" + "\r\n";
	}

	/**
	 * Configure and initialize the viewer, load system and user projects.
	 * One of the user projects must be selected here.
	 */
	public void init() {
		m_viewer = new PvViewer(this, m_frame);

		// Create and load a project
		PjFractalImage pjFractal = new PjFractalImage();
		m_viewer.addProject(pjFractal);
		m_viewer.selectProject(pjFractal);

		// Get 3d display from viewer and add it to applet
		setLayout(new BorderLayout());
		PsPanel pDisplay = new PsPanel();
		pDisplay.setPreferredSize(640, 256);
		pDisplay.setLayout(new GridLayout(1, 2));
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
	public static void main(String args[]) {
		PaFractalImage va	= new PaFractalImage();
		// Create toplevel window of application containing the applet
		Frame frame	= new jv.object.PsMainFrame(va, args);
		va.m_frame = frame;
		va.init();

		// frame.setMenuBar(va.m_viewer.newMenuBar(frame));
		frame.pack();
		va.start();
		frame.setBounds(new Rectangle(420, 5, 640, 470));
		frame.setVisible(true);
	}

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
